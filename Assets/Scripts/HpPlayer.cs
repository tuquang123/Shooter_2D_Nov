using System.Globalization;
using Minimalist.Bar.Quantity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HpPlayer : MonoBehaviour
{
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
