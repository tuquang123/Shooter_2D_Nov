using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject fx;
    public int hp = 100;
    public void TakeDame(int dame)
    {
        hp -= dame;
        if (hp <= 0)
        {
            Destroy(gameObject);
            Instantiate(fx, transform.position, quaternion.identity);
        }
    }
    
}
