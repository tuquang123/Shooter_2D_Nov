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
        //public List<Renderer> Renderers;
        public GameObject Trail;
        public GameObject Impact;
	    public Rigidbody Rigidbody;
        
	    public void FixedUpdate()
	    {
		    if (Rigidbody != null && Rigidbody.useGravity)
		    {
			    transform.right = Rigidbody.velocity.normalized;
                dame = GameManager.Instance.dame;
		    }
	    }

        public void OnTriggerEnter(Collider other)
        {
            Bang(other.gameObject);
            GameManager.IDamageableEnemy iDameDamageableEnemy = other.gameObject.GetComponent<GameManager.IDamageableEnemy>();
            iDameDamageableEnemy.TakeDame(dame);
            DiscardToPool();

        }
        private void Bang(GameObject other)
        {
            
            ReplaceImpactSound(other);
            Impact.SetActive(true);
            foreach (var ps in Trail.GetComponentsInChildren<ParticleSystem>())
            {
                ps.Stop();
            }

	        foreach (var tr in Trail.GetComponentsInChildren<TrailRenderer>())
	        {
		        tr.enabled = false;
			}
            MyPooler.ObjectPooler.Instance.GetFromPool("F2", transform.position, Quaternion.identity);
        }
        public void DiscardToPool()
        {
            Impact.SetActive(false);
            
            dame = GameManager.Instance.dame;
            foreach (var ps in Trail.GetComponentsInChildren<ParticleSystem>())
            {
                ps.Play();
            }

            foreach (var tr in Trail.GetComponentsInChildren<TrailRenderer>())
            {
                tr.enabled = true;
            }
            MyPooler.ObjectPooler.Instance.ReturnToPool("B", gameObject);
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