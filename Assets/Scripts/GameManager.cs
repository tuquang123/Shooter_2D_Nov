using System;
using System.Collections.Generic;
using Assets.HeroEditor.Common.CharacterScripts.Firearms;
using Assets.HeroEditor.Common.CharacterScripts.Firearms.Enums;
using Assets.HeroEditor.Common.Data;
using Assets.HeroEditor.Common.EditorScripts;
using Assets.HeroEditor.FantasyInventory.Scripts.Interface.Elements;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    //public Firearm firearm;
    public int dame = 10;
    public int gold = 1000;

    //public Text goldShow;
    
    public Text daysText;
    
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
    
    public GameObject paneShop;

    //public RectTransform open;

    public List<GameObject> player;
    
    public List<int> itemID;
    
    public List<Transform> slotItem;
    
    public int countEquipGun = 0;
    
    public GameObject days;

    public int daysLive = 1;

    //public CharacterEditor CharacterEditor;
    public GridLayoutGroup GridLayoutGroup;

    private void Start()
    {
        goldText.text = gold.ToString();
        //Open();
        Invoke("DayFist",3);

        gold = PlayerPrefs.GetInt("gold", 0);

        openShopGun.onClick.AddListener(Open);

        closeGun.onClick.AddListener(Close);

        restart.onClick.AddListener(Restart);
    }

    public void DayFist()
    {
        days.SetActive(false);
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.Save();
    }

    private void FixedUpdate()
    {
        //goldText.text = gold.ToString();
        //goldShow.text = gold.ToString();
    }

    public void Open()
    {
        //open.anchoredPosition = new Vector2(1675, 213);
        //panelGun.SetActive(true);
        paneShop.SetActive(true);
        Time.timeScale = 0;
    }

    public void Close()
    {
        Time.timeScale = 1;
        panelGun.SetActive(false);
        scrollInventory.OnReset();
        SpawnerEnemy.Instance.countMaxSpawn *= 2;
        SpawnerEnemy.Instance.countSpawn = 0;
        SpawnerEnemy.Instance.isSpawn = true;
        SpawnerEnemy.Instance.isOpen = false;
        SpawnerEnemy.Instance.countWaveEnemy = 0;
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        //gold -= 1000;
        paneLoss.SetActive(false);
    }

    public void PlayerActive(int i)
    {
        //for (int j = 0; j < player.Count; j++)
        player[i].SetActive(true);
        player[0].SetActive(false);
        characterEditor.UpdatePlayer();
        player[i].transform.position = player[0].transform.position;
    }

    public void ShopAcitveFalse()
    {
        paneShop.SetActive(false);
        characterEditor.OnSelectTab(true);
        Time.timeScale = 1;
        days.SetActive(true);
        Invoke("Day",3);
    }
   
    public void Day()
    {
        daysText.text = daysLive.ToString("Day " + daysLive);
        days.SetActive(false);
       SpawnerEnemy.Instance.countMaxSpawn *= 2;
       SpawnerEnemy.Instance.countSpawn = 0;
       SpawnerEnemy.Instance.isSpawn = true;
       SpawnerEnemy.Instance.isOpen = false;
       SpawnerEnemy.Instance.countWaveEnemy = 0;
       daysLive++;
       SpawnerEnemy.Instance.goldReward = 0;
    }

    public interface IDamageableEnemy
    {
        void TakeDame(float damage);
    }
}