using System.Collections.Generic;
using UnityEngine;

namespace Assets.HeroEditor.Common.ExampleScripts
{
    /// <summary>
    /// General behaviour for projectiles: bullets, rockets and other.
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        public int dame = 10;
        public List<Renderer> Renderers;
        public GameObject Trail;
        public GameObject Impact;
	    public Rigidbody Rigidbody;
        
		public void Start()
        {
            dame = GameManager.Instance.dame;
            //Destroy(gameObject, 5);
            //Invoke("DiscardToPool",3);
        }

	    public void FixedUpdate()
	    {
		    if (Rigidbody != null && Rigidbody.useGravity)
		    {
			    transform.right = Rigidbody.velocity.normalized;
		    }
	    }

        public void OnTriggerEnter(Collider other)
        {
            Bang(other.gameObject);
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.TakeDame(dame);
            }
            
        }
        /*public void OnCollisionEnter(Collision other)
        {
            Bang(other.gameObject);
            if (other.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.TakeDame(dame);
            }
        }*/

        private void Bang(GameObject other)
        {
            ReplaceImpactSound(other);
            Impact.SetActive(true);
            
            //Destroy(GetComponent<SpriteRenderer>());
            //Destroy(GetComponent<Rigidbody>());
            //Destroy(GetComponent<Collider>());
            //Destroy(gameObject, 1);
            //Invoke("DiscardToPool",0.1f);
            //DiscardToPool();
            //ReSetBullet();

            foreach (var ps in Trail.GetComponentsInChildren<ParticleSystem>())
            {
                //ps.Stop();
            }

	        foreach (var tr in Trail.GetComponentsInChildren<TrailRenderer>())
	        {
		        //tr.enabled = false;
			}
		}

        public void ReSetBullet()
        {
            Invoke("DiscardToPool",0.05f);
        }
        public void DiscardToPool()
        {
            MyPooler.ObjectPooler.Instance.ReturnToPool("B", this.gameObject);
            Impact.SetActive(false);
            dame = GameManager.Instance.dame;
        }

        private void ReplaceImpactSound(GameObject other)
        {
            var sound = other.GetComponent<AudioSource>();

            if (sound != null && sound.clip != null)
            {
                Impact.GetComponent<AudioSource>().clip = sound.clip;
            }
        }
    }
}