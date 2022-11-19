using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFx : MonoBehaviour
{
    public bool enemyFx;
    public GameObject fx;
   public void Destroy()
   {
        Destroy(gameObject);
        if (enemyFx)
        {
            Instantiate(fx, transform.position, Quaternion.identity);
        }
        
   }
}
