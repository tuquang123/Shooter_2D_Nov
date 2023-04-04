using System;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.CharacterScripts.Firearms;
using Assets.HeroEditor.Common.CharacterScripts.Firearms.Enums;
using HeroEditor.Common.Enums;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.HeroEditor.Common.ExampleScripts
{
    /// <summary>
    /// Rotates arms and passes input events to child components like FirearmFire and BowExample.
    /// </summary>
    public class AttackingExample : MonoBehaviour
    {
        //public Character character;
        public Transform target;
        public float attackRage = 20f;
        [HideInInspector] public float attackTimer;
        [HideInInspector] public float targetDis = Mathf.Infinity;
        [HideInInspector] public Vector3 targetDir;

        public Character Character;
        public BowExample BowExample;
        public Firearm Firearm;
        public FirearmFire fire;
        public Transform ArmL;
        public Transform ArmR;
        public KeyCode FireButton;
        public KeyCode ReloadButton;
        [Header("Check to disable arm auto rotation.")]
	    public bool FixedArm;

        public bool Auto;

        public Vector3 mouseToChar = Vector3.zero;
        
        private void Turning()
        {
            /*if (target == null) return;
            //if (turnOff)
            {
                targetDir = target.position - Character.transform.position;
                Transform charTrans = Character.transform;
                charTrans.transform.localScale = new Vector3(Mathf.Sign(targetDir.x), 1, 1);
            }*/
            Vector3 mouse = Input.mousePosition;
            Vector3 vec4 = new Vector3(mouse.x, mouse.y, Character.transform.position.y);
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(vec4);
            
            mouseToChar = mouseWorld - Character.transform.position;
            //Character.transform.localScale = new Vector3(Mathf.Sign(mouseToChar.x), 1, 1);
        }
       
        public virtual void FindEnemy()
        {
            //if (this.target== null) return;

            if (this.target) return;
            float dis;
            foreach (Transform obj in SpawnerEnemy.Instance.objects)
            {
                dis = Vector3.Distance(transform.position, obj.position);
                if (dis <= attackRage)
                {
                    SetTaget(obj);
                    return;
                }

            }
        }
        public void SetTaget(Transform target)
        {
            this.target = target;
            return;
        }

        public void IsTargetTooFar()
        {
            if (target == null) return;
            if (!target.gameObject.activeSelf)
            {
                target = null;
                return;
            }
            targetDis = Vector3.Distance(transform.position, this.target.position);
            if (targetDis > attackRage) target = null;
        }
        public void Start()
        {
            Character.Animator.SetBool("Ready", true);
            if ((Character.WeaponType == WeaponType.Firearms1H || Character.WeaponType == WeaponType.Firearms2H) && Firearm.Params.Type == FirearmType.Unknown)
            {
                throw new Exception("Firearm params not set.");
            }
        }
        void AutoFire()
        {
            if (target == null) return;
            if (target != null)
            {
                fire.StartCoroutine(fire.Fire());
            }
        }
        
        public void Update()
        {
            if(Auto)AutoFire();
            
            Turning();
            FindEnemy();
            IsTargetTooFar();
            
            if (Character.Animator.GetInteger("State") >= (int) CharacterState.DeathB) return;

            switch (Character.WeaponType)
            {
                case WeaponType.Melee1H:
                case WeaponType.Melee2H:
                case WeaponType.MeleePaired:
                    if (Input.GetKeyDown(FireButton))
                    {
                        Character.Slash();
                    }
                    break;
                case WeaponType.Bow:
                    BowExample.ChargeButtonDown = Input.GetKeyDown(FireButton);
                    BowExample.ChargeButtonUp = Input.GetKeyUp(FireButton);
                    break;
                case WeaponType.Firearms1H:
                case WeaponType.Firearms2H:
                    /*Firearm.Fire.FireButtonDown = Input.GetKeyDown(FireButton);
                    Firearm.Fire.FireButtonPressed = Input.GetKey(FireButton);
                    Firearm.Fire.FireButtonUp = Input.GetKeyUp(FireButton);
                    Firearm.Reload.ReloadButtonDown = Input.GetKeyDown(ReloadButton);*/
                    Firearm.Fire.FireButtonDown = CrossPlatformInputManager.GetButtonDown("Shoot");
                    Firearm.Fire.FireButtonPressed = CrossPlatformInputManager.GetButton("Shoot");
                    Firearm.Fire.FireButtonUp = CrossPlatformInputManager.GetButtonUp("Shoot");
                    Firearm.Reload.ReloadButtonDown = CrossPlatformInputManager.GetButtonDown("Reload");
                    break;
	            case WeaponType.Supplies:
		            if (Input.GetKeyDown(FireButton))
		            {
			            Character.Animator.Play(Time.frameCount % 2 == 0 ? "UseSupply" : "ThrowSupply", 0); // Play animation randomly.
		            }
		            break;
			}

            if (Input.GetKeyDown(FireButton))
            {
                Character.GetReady();
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere( Character.transform.position, attackRage);
        }

        /// <summary>
        /// Called each frame update, weapon to mouse rotation example.
        /// </summary>
        public void LateUpdate()
        {
            switch (Character.GetState())
            {
                case CharacterState.DeathB:
                case CharacterState.DeathF:
                    return;
            }

            Transform arm;
            Transform weapon;

            switch (Character.WeaponType)
            {
                case WeaponType.Bow:
                    arm = ArmL;
                    weapon = Character.BowRenderers[3].transform;
                    break;
                case WeaponType.Firearms1H:
                case WeaponType.Firearms2H:
                    arm = ArmR;
                    weapon = Firearm.FireTransform;
                    break;
                default:
                    return;
            }

            if (Character.IsReady())
            {
                if ( target== null) return;
                Vector3 enemyS = target.position;
                //RotateArm(arm, weapon, FixedArm ? arm.position + 1000 * Vector3.right : enemyS, -90, 90);
                RotateArm(arm, weapon, FixedArm ? arm.position + 1000 * Vector3.right : Camera.main.ScreenToWorldPoint(Input.mousePosition), -40, 40);
            }
        }

        /// <summary>
        /// Selected arm to position (world space) rotation, with limits.
        /// </summary>
        public void RotateArm(Transform arm, Transform weapon, Vector2 target, float angleMin, float angleMax) // TODO: Very hard to understand logic.
        {
            target = arm.transform.InverseTransformPoint(target);
            
            var angleToTarget = Vector2.SignedAngle(Vector2.right, target);
            var angleToFirearm = Vector2.SignedAngle(weapon.right, arm.transform.right) * Math.Sign(weapon.lossyScale.x);
            var fix = weapon.InverseTransformPoint(arm.transform.position).y / target.magnitude;

            if (fix < -1) fix = -1;
            if (fix > 1) fix = 1;

            var angleFix = Mathf.Asin(fix) * Mathf.Rad2Deg;
            var angle = angleToTarget + angleToFirearm + angleFix;

            angleMin += angleToFirearm;
            angleMax += angleToFirearm;

            var z = arm.transform.localEulerAngles.z;

            if (z > 180) z -= 360;

            if (z + angle > angleMax)
            {
                angle = angleMax;
            }
            else if (z + angle < angleMin)
            {
                angle = angleMin;
            }
            else
            {
                angle += z;
            }

            if (float.IsNaN(angle))
            {
                Debug.LogWarning(angle);
            }

            arm.transform.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}