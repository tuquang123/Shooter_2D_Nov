using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : Singleton<SpawnerEnemy>
{
    public GameObject bossObj;
    public float timeNextSpawn = 0.02f;
    public int lv;
    float randy;
    
    Vector2 whereToSpawn;
    
    public int[] randomPos = {-9, -11 , -13};

    [Range(0.5f,3f)]
    public const float minRate = 0.7f;
    
    public float spawnRate;
    
    float nextSpawn = 0.0f;
    
    
    public List<Transform> objects = new List<Transform>();

    bool max = true;
   
    private bool checkBoss;

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

    private void FixedUpdate()
    {
        if (Time.time > nextSpawn)
        {
            lv++;
            nextSpawn = Time.time + spawnRate;

            int index = Random.Range(0, 3);
            int randomNumber = randomPos[index];
            whereToSpawn = new Vector2(40, randomNumber);
            
            int randomIndex = Random.Range(1,6);
            switch (randomIndex)
            {
                case 1:
                    //SpawnEnemy("E1");
                    var enemyPrefab = MyPooler.ObjectPooler.Instance.GetFromPool("E", whereToSpawn, Quaternion.identity);
                   
                    MyPooler.ObjectPooler.Instance.GetFromPool("F2", whereToSpawn, Quaternion.identity);
                    objects.Add(enemyPrefab.transform);
                    break;
                case 2:
                    var enemyPrefab2 = MyPooler.ObjectPooler.Instance.GetFromPool("E2", whereToSpawn, Quaternion.identity);
                    
                    MyPooler.ObjectPooler.Instance.GetFromPool("F2", whereToSpawn, Quaternion.identity);
                    objects.Add(enemyPrefab2.transform);
                    break;
                case 3:
                    var enemyPrefab3 = MyPooler.ObjectPooler.Instance.GetFromPool("E3", whereToSpawn, Quaternion.identity);
                    
                    MyPooler.ObjectPooler.Instance.GetFromPool("F2", whereToSpawn, Quaternion.identity);
                    objects.Add(enemyPrefab3.transform);
                    break;
                case 4:
                    var enemyPrefab4 = MyPooler.ObjectPooler.Instance.GetFromPool("E4", whereToSpawn, Quaternion.identity);
                   
                    MyPooler.ObjectPooler.Instance.GetFromPool("F2", whereToSpawn, Quaternion.identity);
                    objects.Add(enemyPrefab4.transform);
                    break;
                case 5:
                    var enemyPrefab5 = MyPooler.ObjectPooler.Instance.GetFromPool("E5", whereToSpawn, Quaternion.identity);
                    
                    MyPooler.ObjectPooler.Instance.GetFromPool("F2", whereToSpawn, Quaternion.identity);
                    objects.Add(enemyPrefab5.transform);
                    break;
            }
            if (max)
            {
                spawnRate -= timeNextSpawn;
            }
            if(spawnRate <= minRate)
            {
                spawnRate = minRate;
                max = false;
            }
            if (spawnRate <= minRate && !checkBoss)
            {
                bossObj.SetActive(true);
                objects.Add(bossObj.transform);
                checkBoss = true;
            }
        }
    }
}
