using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxPooling : MonoBehaviour
{
    public string poolTag;
    
    public void DiscardToPool()
    {
        MyPooler.ObjectPooler.Instance.ReturnToPool(poolTag, this.gameObject);
    }
}
