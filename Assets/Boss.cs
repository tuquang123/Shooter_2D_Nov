using Assets.FantasyMonsters.Scripts;
using Assets.FantasyMonsters.Scripts.Tweens;

using DamageNumbersPro;
using Minimalist.Bar.Quantity;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour, GameManager.IDamageableEnemy
{
    public GameObject hpBar;
    
    public QuantityBhv quantityBhv;
    
    public DamageNumber numberPrefab;
    
    public DamageNumber numberPrefabGold;
    
    public Transform rectParent;
    
    public Transform target;
    
    public int dame;

    public float speed;
    
    public float hp = 1000;
    
    private void Start()
    {
        hpBar.SetActive(true);
        quantityBhv.MaximumAmount = hp;
        quantityBhv.Amount = hp;
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }
    
    private void FixedUpdate()
    {
        if (target == null) return;
        //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.position += Vector3.left * speed * Time.deltaTime;
        GetComponent<Monster>().SetState(MonsterState.Run);
    }
    
    public void TakeDame(float damage)
    {
        hpBar.SetActive(true);
        quantityBhv.Amount -= damage;
        //Spawn new popup with a random number between 0 and 100.
        DamageNumber damageNumber = numberPrefab.Spawn(Vector3.zero, - damage);

        //Set the rect parent and anchored position.
        damageNumber.SetAnchoredPosition(rectParent,rectParent.position );
        damageNumber.transform.parent = null;
        
        ScaleSpring.Begin(this, 1f, 1.1f, 50, 6);
        hp -= damage;
        Die();
    }
    public void Die()
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            hpBar.SetActive(false);
            
            var randomGold = Random.Range(SpawnerEnemy.Instance.lv,40);
            DamageNumber damageNumberGold = numberPrefabGold.Spawn(Vector3.zero, randomGold);
            
            damageNumberGold.SetAnchoredPosition(rectParent,rectParent.position );
            damageNumberGold.transform.parent = null;

            GameManager.Instance.gold += randomGold;
            MyPooler.ObjectPooler.Instance.GetFromPool("F", transform.position, Quaternion.identity);

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
