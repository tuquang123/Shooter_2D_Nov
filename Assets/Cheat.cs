using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public void AddGold()
    {
        GameManager.Instance.gold += 10000;
    }

    public void AddDame()
    {
        GameManager.Instance.dame += 10;
    }
}
