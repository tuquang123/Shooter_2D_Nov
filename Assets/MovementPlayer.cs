using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.position = new Vector3(UltimateJoystick.GetHorizontalAxis("Mov"),
            UltimateJoystick.GetVerticalAxis("Mov"), 500f * Time.deltaTime);
    }
}
