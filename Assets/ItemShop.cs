using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    public TypeITem typeITem;
    public enum TypeITem
    {
        Gun,
        Suit,
        Partner,
        Bullet
        
    }
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
        if (typeITem == TypeITem.Bullet)
        {
            GameManager.Instance.numberBullet ++;
            equipItem.SetActive(false);

        }
        if (GameManager.Instance.itemID.Count == 2)
        {
            if (GameManager.Instance.itemID[0] == GameManager.Instance.itemID[1])
            {
                GameManager.Instance.itemID.RemoveAt(1);
            }
        }
        //gun
        if (typeITem == TypeITem.Gun && GameManager.Instance.countEquipGun < 2 )
        {
            GameManager.Instance.countEquipGun++;
            
            if (GameManager.Instance.itemID != null) GameManager.Instance.itemID.Add(id);
            transform.position = GameManager.Instance.slotItem.position;
            transform.SetParent(GameManager.Instance.slotItem);
       
            equipItem.SetActive(false);
            removeItem.SetActive(true);
            
        }
        //suit
        if (typeITem == TypeITem.Suit && GameManager.Instance.countSuit < 1)
        {
            GameManager.Instance.countSuit++;
            transform.position = GameManager.Instance.slotItem.position;
            transform.SetParent(GameManager.Instance.slotItem);
       
            equipItem.SetActive(false);
            removeItem.SetActive(true);
        }
        //partner
        if (typeITem == TypeITem.Partner && GameManager.Instance.countPartNer < 1)
        {
            GameManager.Instance.countPartNer++;
            transform.position = GameManager.Instance.slotItem.position;
            transform.SetParent(GameManager.Instance.slotItem);
       
            equipItem.SetActive(false);
            removeItem.SetActive(true);
            
        }
    }

    public void Remove()
    {
        if (typeITem == TypeITem.Gun)
        {
            GameManager.Instance.countEquipGun--;
            GameManager.Instance.itemID.Remove(id);
            removeItem.SetActive(false);
            equipItem.SetActive(true);
        
            transform.SetParent(GameManager.Instance.GridLayoutGroup.transform);
            GameManager.Instance.GridLayoutGroup.SetLayoutHorizontal();
            GameManager.Instance.GridLayoutGroup.SetLayoutVertical();
        }
        if (typeITem == TypeITem.Suit)
        {
            GameManager.Instance.countSuit--;
            removeItem.SetActive(false);
            equipItem.SetActive(true);
        
            transform.SetParent(GameManager.Instance.GridLayoutGroup.transform);
            GameManager.Instance.GridLayoutGroup.SetLayoutHorizontal();
            GameManager.Instance.GridLayoutGroup.SetLayoutVertical();
        }
        if (typeITem == TypeITem.Partner)
        {
            GameManager.Instance.countPartNer--;
            removeItem.SetActive(false);
            equipItem.SetActive(true);
        
            transform.SetParent(GameManager.Instance.GridLayoutGroup.transform);
            GameManager.Instance.GridLayoutGroup.SetLayoutHorizontal();
            GameManager.Instance.GridLayoutGroup.SetLayoutVertical();
        }
    }
}
