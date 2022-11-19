using Assets.FantasyMonsters.Scripts;
using Assets.FantasyMonsters.Scripts.Tweens;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    Transform target;

    public GameObject fx;

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
    
}
