using Assets.FantasyMonsters.Scripts;
using Assets.FantasyMonsters.Scripts.Tweens;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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

        //rb.AddForce(transform.forward * 2);
        //rb.velocity = new Vector3(-speed, 0f, 0f);

        //if (Vector2.Distance(transform.position, target.position) > target.position.x)
        else
        {
            if (this.target == null) return;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            GetComponent<Monster>().SetState(MonsterState.Run);
            Die();
        }
    }
    public void TakeDame(int dame)
    {
        ScaleSpring.Begin(this, 1f, 1.1f, 40, 3);

        hp -= dame;
        
    }
    public void Die()
    {
        if (hp <= 0)
        {
            GetComponent<Monster>().Die();
            speed = 0;
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
