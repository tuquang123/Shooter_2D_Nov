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
    public Character character;
    public float hp = 100;

    public Text textHp;

    private void Start()
    {
        qt.Amount = hp;
    }

    /*private void Update()
    {
        textHp.text = hp.ToString();
        Die();
    }*/
    public void TakeDame(int dame)
    {
        qt.Amount -= dame;
        textHp.text = hp.ToString();
        character.Hit();
        character.Spring();
        hp -= dame;
        textHp.text = hp.ToString();
        hp = qt.FillAmount;
        Die();
    }
    public void Die()
    {
        if (hp <= 0)
        {
            character.SetState(CharacterState.DeathB);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.68f);
        Time.timeScale = 0;
        panelLoss.SetActive(true);
    }
}
