using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public GameObject enemy;
    float randy;
    Vector2 whereToSpawn;

    public float spawnRate;
    float nextSpawn = 0.0f;

    public int miny, maxY;

    private void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randy = Random.Range(miny, maxY);
            whereToSpawn = new Vector2(transform.position.x, randy);
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
        }
    }
}
