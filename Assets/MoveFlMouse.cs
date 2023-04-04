using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlMouse : MonoBehaviour
{
    private Vector3 pos;
    public float speed = 1f;
    void Update()
    {
        /*Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition;*/
        pos = Input.mousePosition;
        pos.z = speed;
        transform.position = Camera.main.ScreenToViewportPoint(pos);
    }
}
