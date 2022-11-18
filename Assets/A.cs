using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }
}