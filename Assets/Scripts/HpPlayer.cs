using System;
using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using Minimalist.Bar.Quantity;
using UnityEngine;
using UnityEngine.UI;

public class HpPlayer : MonoBehaviour
{
    public QuantityBhv qt;
    public GameObject panelLoss;
    public float hp = 100;

    public Text textHp;

    private void Start()
    {
        qt.Amount = hp;
    }

    public void TakeDame(int dame)
    {
        qt.Amount -= dame;
        textHp.text = hp.ToString();
        hp -= dame;
        textHp.text = hp.ToString();
        hp = qt.FillAmount;
        Die();
    }
    public void Die()
    {
        if (hp <= 0)
        {
            panelLoss.SetActive(true);
        }
    }

}
