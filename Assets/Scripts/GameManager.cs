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

    public Text goldShow;
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

    public RectTransform open;

    public List<GameObject> player;
    
    public List<int> itemID;
    
    public int countEquipGun = 0;

    //public CharacterEditor CharacterEditor;

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
        goldShow.text = gold.ToString();
    }

    public void Open()
    {
        if (!i)
        {
            characterEditor.OnSelectTab(true);
            i = true;
        }

        //open.anchoredPosition = new Vector2(1675, 213);
        panelGun.SetActive(true);
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

    public void ShopAcitve(bool stage)
    {
        paneShop.SetActive(stage);
    }

    public interface IDamageableEnemy
    {
        void TakeDame(float damage);
    }
}