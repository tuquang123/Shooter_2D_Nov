using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class SpawnerEnemy : Singleton<SpawnerEnemy>
{
    public GameObject bossObj;
    public GameObject goldRewardObj;
    
    public float timeNextSpawn = 0.02f;
    public int lv;
    
    public int PosX;
    
    float randy;

    Vector2 whereToSpawn;

    public int[] randomPos = { -9, -11, -13 };

    [Range(0.5f, 3f)] public const float minRate = 0.7f;

    public float spawnRate;

    float nextSpawn = 0.0f;

    public List<Transform> objects = new List<Transform>();

    bool max = true;

    private bool checkBoss;

    public bool isSpawn = true;

    public bool isOpen;

    public bool isEndWave ;

    public int countSpawn;

    public int countMaxSpawn;

    public int countWaveEnemy;

    public DOTweenAnimation textGold;
    
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

    public void IsSpawnEnemy()
    {
        if (countWaveEnemy < 3 && isEndWave == false)
        {
            countWaveEnemy++; 
            isSpawn = true; 
            isEndWave = true;
            countSpawn = 0;
        }
    }

    public void ActiveGold()
    {
        goldRewardObj.SetActive(true);
        textGold.DOPlay();
        textGold.DORestart();
    }
    public void IsActiveFalse()
    {
        goldRewardObj.SetActive(false);
        GameManager.Instance.Open();
    }

    private void FixedUpdate()
    {
        if (countSpawn == countMaxSpawn)
        {
            isSpawn = false;
            if (isSpawn == false && objects.Count == 0) 
            {
                Invoke("IsSpawnEnemy", 4f);
                if (countWaveEnemy >= 1)
                {
                    isEndWave = false;
                }
            }
            if (objects.Count == 0 && isOpen == false && countWaveEnemy == 3)
            {
                Invoke("IsActiveFalse", 3.2f);
                //GameManager.Instance.Open();
                //goldRewardObj.SetActive(true);
                ActiveGold();
                isOpen = true;
                //countSpawn++;
            }
        }

        if (Time.time > nextSpawn)
        {
            lv++;
            nextSpawn = Time.time + spawnRate;

            int index = Random.Range(0, 3);
            int randomNumber = randomPos[index];
            whereToSpawn = new Vector2(PosX, randomNumber);
            //int randomIndex = 5;
            int randomIndex = Random.Range(1, 6);
            if (isSpawn)
            {
                countSpawn++;
                switch (randomIndex)
                {
                    case 1:
                        //SpawnEnemy("E1");
                        var enemyPrefab =
                            MyPooler.ObjectPooler.Instance.GetFromPool("E", whereToSpawn, Quaternion.identity);

                        MyPooler.ObjectPooler.Instance.GetFromPool("F2", whereToSpawn, Quaternion.identity);
                        objects.Add(enemyPrefab.transform);
                        break;
                    case 2:
                        var enemyPrefab2 =
                            MyPooler.ObjectPooler.Instance.GetFromPool("E2", whereToSpawn, Quaternion.identity);

                        MyPooler.ObjectPooler.Instance.GetFromPool("F2", whereToSpawn, Quaternion.identity);
                        objects.Add(enemyPrefab2.transform);
                        break;
                    case 3:
                        var enemyPrefab3 =
                            MyPooler.ObjectPooler.Instance.GetFromPool("E3", whereToSpawn, Quaternion.identity);

                        MyPooler.ObjectPooler.Instance.GetFromPool("F2", whereToSpawn, Quaternion.identity);
                        objects.Add(enemyPrefab3.transform);
                        break;
                    case 4:
                        var enemyPrefab4 =
                            MyPooler.ObjectPooler.Instance.GetFromPool("E4", whereToSpawn, Quaternion.identity);

                        MyPooler.ObjectPooler.Instance.GetFromPool("F2", whereToSpawn, Quaternion.identity);
                        objects.Add(enemyPrefab4.transform);
                        break;
                    case 5:
                        var enemyPrefab5 =
                            MyPooler.ObjectPooler.Instance.GetFromPool("E5", whereToSpawn, Quaternion.identity);

                        MyPooler.ObjectPooler.Instance.GetFromPool("F2", whereToSpawn, Quaternion.identity);
                        objects.Add(enemyPrefab5.transform);
                        break;
                }
            }

            if (max)
            {
                spawnRate -= timeNextSpawn;
            }

            if (spawnRate <= minRate)
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