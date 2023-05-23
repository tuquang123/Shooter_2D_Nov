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
    
    private void Start()
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
            //if(GameManager.Instance.countEquipGun == 0 )
            {
                transform.position = GameManager.Instance.slotItem.position;
                transform.SetParent(GameManager.Instance.slotItem);
            }
            GameManager.Instance.countEquipGun++;
            //GameManager.Instance.countEquipGun++;
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
        
        //RectTransform rectTransform = GetComponent<RectTransform>();
        //rectTransform.anchoredPosition = originalPosition;
        transform.SetParent(GameManager.Instance.GridLayoutGroup.transform);
        GameManager.Instance.GridLayoutGroup.SetLayoutHorizontal();
        GameManager.Instance.GridLayoutGroup.SetLayoutVertical();
    }
}
