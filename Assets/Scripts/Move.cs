using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Joystick joystick;
    public Character Character;

    public float speed = 3f;

    public Rigidbody2D rb;

    Vector2 move;

    private void Update()
    {
        //move.x = Input.GetAxisRaw("Horizontal");
        if (joystick.Horizontal >= .2f)
        {
            move.x = speed;
        }
        else if(joystick.Horizontal <= -.2f)
        {
            move.x = -speed;
        }
        else
        {
            move.x = 0f;
        }
        if (joystick.Vertical >= .2f)
        {
            move.y = speed;
        }
        else if (joystick.Vertical <= -.2f)
        {
            move.y = -speed;
        }
        else
        {
            move.y = 0f;
        }
        //move.x = joystick.Horizontal;
        //move.y = joystick.Vertical;
        //move.y = Input.GetAxisRaw("Vertical"); 
    }
    private void FixedUpdate()
    {
        if (move == Vector2.zero) 
        {
            Character.SetState(CharacterState.Idle);
        }
        else 
        {
            rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);

            Character.SetState(CharacterState.Run);
        }
  
    }
   
}
