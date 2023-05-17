using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    public int id;
    public int price;
    public GameObject boughtItem;
    public GameObject equipItem;
    public GameObject removeItem;
    public Text textPrice;

    private void Awake()
    {
        textPrice.text = price.ToString();
    }

    public void Purchase()
    {
        if (GameManager.Instance.gold < price) return;
        GameManager.Instance.gold -= price;
        GameManager.Instance.goldText.text = GameManager.Instance.gold.ToString();
        boughtItem.SetActive(false);
        equipItem.SetActive(true);
    }
    List<T> RemoveDuplicates<T>(List<T> list)
    {
        List<T> result = new List<T>();

        foreach (T item in list)
        {
            if (!result.Contains(item))
            {
                result.Add(item);
            }
        }

        return result;
    }
    public void Equipment()
    {
        if (GameManager.Instance.itemID.Count == 2)
        {
            if (GameManager.Instance.itemID[0] == GameManager.Instance.itemID[1])
            {
                GameManager.Instance.itemID.RemoveAt(1);
                //GameManager.Instance.itemID.RemoveAt(0);
            }
        }
        if (GameManager.Instance.countEquipGun < 2)
        {
            GameManager.Instance.countEquipGun++;
            if (GameManager.Instance.itemID != null) GameManager.Instance.itemID.Add(id);
            equipItem.SetActive(false);
            removeItem.SetActive(true);
        }
    }

    public void Remove()
    {
        GameManager.Instance.countEquipGun--;
        GameManager.Instance.itemID.Remove(id);
        removeItem.SetActive(false);
        equipItem.SetActive(true);
    }
}
