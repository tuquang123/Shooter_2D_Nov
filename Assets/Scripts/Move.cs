using System;
using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //public Joystick joystick;
    public Character Character;

    public float speed = 3f;

    public Rigidbody rb;

    Vector3 move;

    private void Reset()
    {
        Character = FindObjectOfType<Character>();
        speed = 2f;
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX |
                                RigidbodyConstraints.FreezeRotationY |
                                RigidbodyConstraints.FreezeRotationZ |
                                RigidbodyConstraints.FreezePositionZ;
        rigidbody.useGravity = false;
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = new Vector3(0f, 1.2f, 0f);
        boxCollider.size = new Vector3(1.3f, 2.7f, 1f);
        rb = FindObjectOfType<Rigidbody>();
    }

    private void Update()
    {
        move.x = UltimateJoystick.GetHorizontalAxisRaw( "Mov" );
        move.y = UltimateJoystick.GetVerticalAxisRaw( "Mov" );
        /*if (joystick.Horizontal >= .2f)
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
        }*/
        //move.x = joystick.Horizontal;
        //move.y = joystick.Vertical;
    }
    private void FixedUpdate()
    {
        if (move == Vector3.zero ) 
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
