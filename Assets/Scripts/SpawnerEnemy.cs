using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public static int lv;
    public GameObject enemy;
    float randy;
    Vector2 whereToSpawn;

    [Range(0.5f,3f)]
    public const float minRate = 1f;
    public float spawnRate;
    float nextSpawn = 0.0f;

    public int miny, maxY;

    public static SpawnerEnemy instance;
    public List<Transform> objects = new List<Transform>();

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void RemoveList()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            Transform obj = this.objects[i];
            if (obj.gameObject.activeSelf) continue;
            objects.Remove(obj);
        }
    }
    private void LateUpdate()
    {
        RemoveList();
    }
    bool max = true;
    private void FixedUpdate()
    {
        if (Time.time > nextSpawn)
        {
            lv++;
            nextSpawn = Time.time + spawnRate;
            randy = Random.Range(miny, maxY);
            whereToSpawn = new Vector2(transform.position.x, randy);
            //var enemyPrefab = Instantiate(enemy, whereToSpawn, Quaternion.identity);
            var enemyPrefab = MyPooler.ObjectPooler.Instance.GetFromPool("E", whereToSpawn, Quaternion.identity);
            var enemyPrefab2 = MyPooler.ObjectPooler.Instance.GetFromPool("E2", whereToSpawn, Quaternion.identity);
            objects.Add(enemyPrefab.transform);
            objects.Add(enemyPrefab2.transform);
            if (max)
            {
                spawnRate -= 0.1f;
            }
            if(spawnRate <= minRate)
            {
                spawnRate = minRate;
                max = false;
            }
        }
    }
}
