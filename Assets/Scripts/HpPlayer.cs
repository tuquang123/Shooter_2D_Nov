using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpPlayer : MonoBehaviour
{
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
            Time.timeScale = 0;
        }
    }
}
