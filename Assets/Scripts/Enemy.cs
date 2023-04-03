using Assets.FantasyMonsters.Scripts;
using Assets.FantasyMonsters.Scripts.Tweens;
using DamageNumbersPro;
using Minimalist.Bar.Quantity;
using Minimalist.Bar.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject hpBar;
    public QuantityBhv qt;
    //Assign prefab in inspector.
    public DamageNumber numberPrefab;
    public Transform rectParent;
    
    public int dame;

    public float speed;

    Transform target;

    public float hp = 100;

    public GameObject projecttile;

    public float timeBetweenShots;
    float nextShotTime;

    public float minimumDistance;

    public bool enemyShoter;

    //public Rigidbody rb;

    private void Start()
    {
        hpBar.SetActive(false);
        qt.MaximumAmount = hp;
        qt.Amount = hp;
        //rectParent = transform;
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
            }
        }
        else
        {
            if (this.target == null) return;
            //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.position += Vector3.left * speed * Time.deltaTime;
            GetComponent<Monster>().SetState(MonsterState.Run);
        }
    }
    public string poolTag;
    
    public void DiscardToPool()
    {
        var a = 0;
        MyPooler.ObjectPooler.Instance.ReturnToPool(poolTag, this.gameObject);
        //isActive = false;
        a = 100;
        a += SpawnerEnemy.Instance.lv;

        hp = a;

        qt.MaximumAmount = a;
        qt.Amount += a;
        qt.FillAmount = 1;
        speed += 0.1f;
    }

    public void TakeDame(int dame)
    {
        hpBar.SetActive(true);
        qt.Amount -= dame;
        //Spawn new popup with a random number between 0 and 100.
        DamageNumber damageNumber = numberPrefab.Spawn(Vector3.zero, - dame);

      
        //Set the rect parent and anchored position.
        //var pos = transform.position;
        damageNumber.SetAnchoredPosition(rectParent,rectParent.position );
        
        ScaleSpring.Begin(this, 1f, 1.1f, 50, 5);
        hp -= dame;
        //hp = qt.FillAmount;
        Invoke(nameof(OffHpBar),.5f);
    }

    public void OffHpBar()
    {
        hpBar.SetActive(false);
    }
    public void Die()
    {
        if (hp <= 0)
        {
            GameManager.Instance.gold+= Random.Range(10,40);
            MyPooler.ObjectPooler.Instance.GetFromPool("F", transform.position, Quaternion.identity);
            DiscardToPool();
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
