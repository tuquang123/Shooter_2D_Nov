using Assets.HeroEditor.Common.EditorScripts;
using Assets.HeroEditor.FantasyInventory.Scripts.Interface.Elements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public ScrollInventory scrollInventory;
    public Button restart;
    public Text goldText;
    public Button closeGun;
    public Button closeHat;
    public Button closeCoat;
    public Button openShopGun;
    public Button openShopHat;
    public Button openShopCoat;

    public GameObject panelGun;
    public GameObject panelHat;
    public GameObject panelCoat;
    public CharacterEditor characterEditor;
    public CharacterEditor characterEditor2;
    public CharacterEditor characterEditor3;
    public int dame = 10;
    public int gold = 1000;
    private bool i;
    private bool i2;
    private bool i3;
    
    public GameObject paneLoss;
    private void Start()
    {
        gold = PlayerPrefs.GetInt("gold", 0);
        openShopGun.onClick.AddListener(Open);
        openShopHat.onClick.AddListener(Open2);
        openShopCoat.onClick.AddListener(Open3);
        closeGun.onClick.AddListener(Close);
        closeHat.onClick.AddListener(Close2);
        closeCoat.onClick.AddListener(Close3);
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
        panelCoat.SetActive(false);
        panelHat.SetActive(false);
    }
    public void Open2()
    {
        if (!i2) 
        {
            characterEditor2.OnSelectTab(true);
            i2=true;
        }
        panelHat.SetActive(true);
        Time.timeScale = 0;
        panelCoat.SetActive(false);
        panelGun.SetActive(false);
    }
    public void Open3()
    {
        if (!i3) 
        {
            characterEditor3.OnSelectTab(true);
            i3=true;
        }
        panelCoat.SetActive(true);
        Time.timeScale = 0;
        panelGun.SetActive(false);
        panelHat.SetActive(false);
    }
    public void Close()
    {
        Time.timeScale = 1;
        panelGun.SetActive(false);
        scrollInventory.OnReset();
        
    }
    public void Close2()
    {
        Time.timeScale = 1;
        panelHat.SetActive(false);
        scrollInventory.OnReset();
    }
    public void Close3()
    {
        Time.timeScale = 1;
        panelCoat.SetActive(false);
        scrollInventory.OnReset();
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        gold -= 1000;
        paneLoss.SetActive(false);
    }
}
