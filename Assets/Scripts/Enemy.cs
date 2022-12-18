using Assets.FantasyMonsters.Scripts;
using Assets.FantasyMonsters.Scripts.Tweens;
using System.Collections;
using System.Collections.Generic;
using DamageNumbersPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    //Assign prefab in inspector.
    public DamageNumber numberPrefab;
    public Transform rectParent;
    
    public int dame;

    public float speed;

    Transform target;

    public int hp = 100;

    public GameObject projecttile;

    public float timeBetweenShots;
    float nextShotTime;

    public float minimumDistance;

    public bool enemyShoter;

    //public Rigidbody rb;

    private void Start()
    {
        rectParent = transform;
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }
    private void LateUpdate()
    {
        if (transform.position.x < target.transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void FixedUpdate()
    {

        //transform.position += transform.forwar * speed * Time.deltaTime;
        Die();
        if (enemyShoter)
        {
            if (Time.time > nextShotTime)
            {
                GetComponent<Monster>().Attack();
                Instantiate(projecttile, transform.position, Quaternion.identity);
                nextShotTime = Time.time + timeBetweenShots;
            }
            if (Vector2.Distance(transform.position, target.position) > minimumDistance)
            {
                if (this.target == null) return;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                GetComponent<Monster>().SetState(MonsterState.Run);
                Die();
            }
        }
        else
        {
            if (this.target == null) return;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            GetComponent<Monster>().SetState(MonsterState.Run);
            Die();
        }
    }
    public string poolTag;
    
    public void DiscardToPool()
    {
        MyPooler.ObjectPooler.Instance.ReturnToPool(poolTag, this.gameObject);
        //isActive = false;
        hp = 100;
        hp += SpawnerEnemy.lv;
    }

    public void TakeDame(int dame)
    {
        //Spawn new popup with a random number between 0 and 100.
        DamageNumber damageNumber = numberPrefab.Spawn(Vector3.zero, -dame);

      
        //Set the rect parent and anchored position.
        var pos = transform.position;
        damageNumber.SetAnchoredPosition(rectParent,pos );
        
        ScaleSpring.Begin(this, 1f, 1.1f, 40, 3);
        hp -= dame;
    }
    public void Die()
    {
        if (hp <= 0)
        {
            DiscardToPool();
            MyPooler.ObjectPooler.Instance.GetFromPool("F", transform.position, Quaternion.identity);
            //GetComponent<Monster>().Die();
            //speed = 0;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HpPlayer enemy = other.GetComponent<HpPlayer>();
            enemy.TakeDame(dame);
            hp = 0;
            Die();
        }

    }

}
