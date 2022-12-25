using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpPlayer : MonoBehaviour
{
    public GameObject panelLoss;
    public Character character;
    public int hp = 100;

    public Text textHp;
    private void Update()
    {
        textHp.text = hp.ToString();
        Die();
    }
    public void TakeDame(int dame)
    {
        character.Hit();
        character.Spring();
        hp -= dame;
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
