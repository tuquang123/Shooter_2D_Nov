using System;
using Cinemachine;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera instance { get; private set; }
    public float shakeTime;
    public CinemachineVirtualCamera camera;

    private void Awake()
    {
        instance = this;
    }

    public void Shake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cam = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cam.m_AmplitudeGain = intensity;
        shakeTime = time;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime <= 0f)
            {
                //timeover
                CinemachineBasicMultiChannelPerlin cam =
                    camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cam.m_AmplitudeGain = 0f;
            }
        }
    }
}