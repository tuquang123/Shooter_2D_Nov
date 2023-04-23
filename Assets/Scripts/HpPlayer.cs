using System.Globalization;
using Minimalist.Bar.Quantity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Cinemachine;

public class HpPlayer : MonoBehaviour
{
    //public CinemachineImpulseSource camShake;
    public QuantityBhv hpBar;
    public GameObject panelLoss;
    public float hp = 100;

    private void Start()
    {
        hpBar.Amount = hp;
    }
    public void TakeDame(int dame)
    {
        hpBar.Amount -= dame;
        hp -= dame;
        hp = hpBar.FillAmount;
        Die();
        //camShake.GenerateImpulse(10f);
    }

    private void Die()
    {
        if (hp <= 0)
        {
            panelLoss.SetActive(true);
            Time.timeScale = 0;
        }
    }

}
