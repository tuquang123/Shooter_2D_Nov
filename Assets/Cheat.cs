using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityAsyncAwaitUtil
{
public class Cheat : MonoBehaviour
{
    public void AddGold()
    {
        GameManager.Instance.gold += 10000;
    }

    public void AddDame()
    {
        GameManager.Instance.dame += 20;
    }

    public void AddAttackSpeed()
    {
        GameManager.Instance.attackSpeed -= .5f;
    }
    public void SpawnRate()
    {
        SpawnerEnemy.Instance.spawnRate -= .5f;
    }

    public void SpawnLevel()
    {
        SpawnerEnemy.Instance.lv += 10;
    }
}
    
}
