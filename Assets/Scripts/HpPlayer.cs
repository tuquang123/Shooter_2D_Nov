using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPlayer : MonoBehaviour
{
    public Character character;
    public int hp = 1000;

    private void Update()
    {
        Die();
    }
    public void TakeDame(int dame)
    {
        character.Spring();
        hp -= dame;
    }
    public void Die()
    {
        if (hp <= 0)
        {
            character.SetState(CharacterState.DeathB);
        }
    }
}
