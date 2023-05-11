using System;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.CharacterScripts.Firearms;
using Assets.HeroEditor.Common.CharacterScripts.Firearms.Enums;
using HeroEditor.Common.Enums;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace HeroEditor.Common.ExampleScripts
{
    /// <summary>
    /// Rotates arms and passes input events to child components like FirearmFire and BowExample.
    /// </summary>
    public sealed class AttackingExample : MonoBehaviour
    {
        Transform _target;
        Vector3 _mouseToChar = Vector3.zero;

        [Header("Public variable")] 
        public float attackRage = 20f;
        public float timeBetweenShots;
        float nextShotTime;
        [HideInInspector] public float targetDis = Mathf.Infinity;
        [HideInInspector] public Vector3 targetDir;
        
        public Character character;
        public Firearm firearm;
        public FirearmFire fire;
        public Transform armL;
        public Transform armR;
        public KeyCode fireButton;

        [Header("Disable arm auto rotation , auto shoot")]
        public bool fixedArm;

        public bool auto;

        private static readonly int Ready = Animator.StringToHash("Ready");
        private static readonly int State = Animator.StringToHash("State");

        #region Turn , Find , Set , Remove : Target

        /// <summary>
        /// Turning Player
        /// </summary>
        private void Turning()
        {
            // Rotation Fl enemy
            if (_target == null) return;
            //if (turnOff)
            {
                targetDir = _target.position - character.transform.position;
                Transform charTrans = character.transform;
                charTrans.transform.localScale = new Vector3(Mathf.Sign(targetDir.x), 1, 1);
            }

            // Rotation Fl mouse
            /*Vector3 mouse = Input.mousePosition;
            var position = character.transform.position;
            Vector3 vec4 = new Vector3(mouse.x, mouse.y, position.y);
            if (Camera.main != null)
            {
                Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(vec4);

                _mouseToChar = mouseWorld - position;
            }
            character.transform.localScale = new Vector3(Mathf.Sign(_mouseToChar.x), 1, 1);*/
        }

        /// <summary>
        /// Find Enemy in List SpawnerEnemy
        /// </summary>
        private void FindEnemy()
        {
            if (_target) return;
            foreach (Transform obj in SpawnerEnemy.Instance.objects)
            {
                var dis = Vector3.Distance(transform.position, obj.position);
                if (dis <= attackRage)
                {
                    SetTarget(obj);
                    return;
                }
            }
        }

        /// <summary>
        /// Set enemy for player
        /// </summary>
        /// <param name="targetEnemy"></param>
        private void SetTarget(Transform targetEnemy)
        {
            _target = targetEnemy;
        }

        /// <summary>
        /// if target to Far with Player
        /// </summary>
        private void IsTargetTooFar()
        {
            if (_target == null) return;
            if (!_target.gameObject.activeSelf)
            {
                _target = null;
                return;
            }

            targetDis = Vector3.Distance(transform.position, this._target.position);
            if (targetDis > attackRage) _target = null;
        }

        /// <summary>
        /// Auto Fire Bullet no click
        /// </summary>
        void AutoFire()
        {
            if (_target == null) return;
            if (_target != null)
            {
                fire.StartCoroutine(fire.Fire());
            }
        }

        #endregion
        public void Start()
        {
            timeBetweenShots = GameManager.Instance.attackSpeed;
            character.Animator.SetBool(Ready, true);
        }
        
        public void Update()
        {
            if (auto)
            {
                if (Time.time > nextShotTime)
                {
                    AutoFire();
                    timeBetweenShots = GameManager.Instance.attackSpeed;
                    nextShotTime = Time.time + timeBetweenShots;
                }
            }
            Turning();
            FindEnemy();
            IsTargetTooFar();

            if (character.Animator.GetInteger(State) >= (int)CharacterState.DeathB) return;

            switch (character.WeaponType)
            {
                case WeaponType.Melee1H:
                case WeaponType.Melee2H:
                case WeaponType.MeleePaired:
                    if (CrossPlatformInputManager.GetButtonDown("Shoot"))
                    {
                        character.Slash();
                    }
                    break;
                case WeaponType.Bow:
                    //BowExample.ChargeButtonDown = Input.GetKeyDown(fireButton);
                    //BowExample.ChargeButtonUp = Input.GetKeyUp(fireButton);
                    break;
                case WeaponType.Firearms1H:
                case WeaponType.Firearms2H:
                    /*Firearm.Fire.FireButtonDown = Input.GetKeyDown(FireButton);
                    Firearm.Fire.FireButtonPressed = Input.GetKey(FireButton);
                    Firearm.Fire.FireButtonUp = Input.GetKeyUp(FireButton);
                    Firearm.Reload.ReloadButtonDown = Input.GetKeyDown(ReloadButton);*/
                    firearm.Fire.FireButtonDown = CrossPlatformInputManager.GetButtonDown("Shoot");
                    firearm.Fire.FireButtonPressed = CrossPlatformInputManager.GetButton("Shoot");
                    firearm.Fire.FireButtonUp = CrossPlatformInputManager.GetButtonUp("Shoot");
                    firearm.Reload.ReloadButtonDown = CrossPlatformInputManager.GetButtonDown("Reload");
                    break;
               
            }
        }

        /// <summary>
        /// Called each frame update, weapon to mouse rotation example.
        /// </summary>
        public void LateUpdate()
        {
            #region Sealed Fuction
            switch (character.GetState())
            {
                case CharacterState.DeathB:
                case CharacterState.DeathF:
                    return;
            }

            Transform arm;
            Transform weapon;

            switch (character.WeaponType)
            {
                case WeaponType.Bow:
                    arm = armL;
                    weapon = character.BowRenderers[3].transform;
                    break;
                case WeaponType.Firearms1H:
                case WeaponType.Firearms2H:
                    arm = armR;
                    weapon = firearm.FireTransform;
                    break;
                default:
                    return;
            }
            #endregion
            if (character.IsReady())
            {
                if (_target == null) return;
                Vector3 enemyS = _target.position;
                //Rotate target is Enemy 
                RotateArm(arm, weapon, fixedArm ? arm.position + 1000 * Vector3.right : enemyS, -90, 90);

                //Rotate target input mouse
                /*if (Camera.main != null)
                    RotateArm(arm, weapon,
                        fixedArm
                            ? arm.position + 1000 * Vector3.right
                            : Camera.main.ScreenToWorldPoint(Input.mousePosition), -40, 40);*/
            }
        }

        /// <summary>
        /// Draw Attack Rage
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(character.transform.position, attackRage);
        }

        /// <summary>
        /// Selected arm to position (world space) rotation, with limits.
        /// </summary>
        private void RotateArm(Transform arm, Transform weapon, Vector2 target, float angleMin, float angleMax) // TODO: Very hard to understand logic.
        {
            target = arm.transform.InverseTransformPoint(target);

            var angleToTarget = Vector2.SignedAngle(Vector2.right, target);
            var angleToFirearm =
                Vector2.SignedAngle(weapon.right, arm.transform.right) * Math.Sign(weapon.lossyScale.x);
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