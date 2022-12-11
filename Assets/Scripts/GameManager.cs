using System;
using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.EditorScripts;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Button close;
    public Button open;

    public GameObject panel;
    public CharacterEditor characterEditor;
    private void Start()
    {
        open.onClick.AddListener(Open);
        close.onClick.AddListener(Close);
    }
    //public GameObject panel;
    public int dame = 10;
    public int gold = 1000;
    public bool i = false;
    public void Open()
    {
        
        if (!i) 
        {
            characterEditor.OnSelectTab(true);
            i=true;
        }
        panel.SetActive(true);
        //characterEditor.OnSelectTab(false);
    }
    public void Close()
    {
       panel.SetActive(false);
    }
}
