using System;
using Assets.HeroEditor.Common.CharacterScripts.Firearms;
using Assets.HeroEditor.Common.CharacterScripts.Firearms.Enums;
using Assets.HeroEditor.Common.Data;
using Assets.HeroEditor.Common.EditorScripts;
using Assets.HeroEditor.FantasyInventory.Scripts.Interface.Elements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Firearm firearm;
    public int dame = 10;
    public int gold = 1000;
    public float attackSpeed = 1;
    public ScrollInventory scrollInventory;
    public Button restart;
    public Text goldText;
    public Button closeGun;
  
    public Button openShopGun;
    
    public GameObject panelGun;
    
    public CharacterEditor characterEditor;
    
    private bool i;
   
    public GameObject paneLoss;
   
    private void Start()
    {
        //Open();
        
        gold = PlayerPrefs.GetInt("gold", 0);
        
        openShopGun.onClick.AddListener(Open);
        
        closeGun.onClick.AddListener(Close);
       
        restart.onClick.AddListener(Restart);
    }
    
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.Save();
    }

    private void FixedUpdate()
    {
        goldText.text = gold.ToString();
    }
    public void Open()
    {
        if (!i) 
        {
            characterEditor.OnSelectTab(true);
            i=true;
        }
        panelGun.SetActive(true);
        Time.timeScale = 0; 
        //SpawnerEnemy.Instance.isSpawn = true;
    }
    public void Close()
    {
        Time.timeScale = 1;
        panelGun.SetActive(false);
        scrollInventory.OnReset();
        
    }
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        //gold -= 1000;
        paneLoss.SetActive(false);
    }
    public interface IDamageableEnemy
    {
        void TakeDame (float damage);
    }
}
