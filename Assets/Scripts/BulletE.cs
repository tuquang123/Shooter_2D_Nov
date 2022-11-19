using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletE : MonoBehaviour
{
    public GameObject fx;
    public int dame = 10;
    Vector3 target;
    public float speed = 4f;
    //public Rigidbody rb;

    private void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        target = FindObjectOfType<Move>().transform.position;
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        //rb.AddForce(transform.forward * speed);

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);

        //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (transform.position == target)
        {
            Die();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HpPlayer enemy = other.GetComponent<HpPlayer>();
            enemy.TakeDame(dame);
            Die();
        }

    }
    public void Die()
    {
        Destroy(gameObject);
        Instantiate(fx, transform.position, Quaternion.identity);
        
    }

}
