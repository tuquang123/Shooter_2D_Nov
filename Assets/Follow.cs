using System;
using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Character character;
    public float speed = 2;
    public Transform player;
    public float minimumDis = 2;

    private void Awake()
    {
        player = FindObjectOfType<Move>().gameObject.transform;
    }

    private void LateUpdate()
    {
        if (Vector2.Distance(transform.position, player.position) > minimumDis)
        {
            character.SetState(CharacterState.Run);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            character.SetState(CharacterState.Idle);
        }
    }
}
