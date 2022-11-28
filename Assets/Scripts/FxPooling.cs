using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxPooling : MonoBehaviour
{
    public string poolTag;
    public bool isActive = false;

    public void OnRequestedFromPool()
    {
        isActive = true;
        //timeToDestroy = defTimerToDestroy;
    }

    public void DiscardToPool()
    {
        MyPooler.ObjectPooler.Instance.ReturnToPool(poolTag, this.gameObject);
        isActive = false;
    }
}
