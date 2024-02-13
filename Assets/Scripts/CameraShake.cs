using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera CinemachineVirtualCamera;
    public float ShakeIntensity = 0.7f;
    public float ShakeTime = 0.1f;

    private float Timer;
    private CinemachineBasicMultiChannelPerlin _cbmcp;

    void Awake() 
    {
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = ShakeIntensity;
        Timer = ShakeTime;
    }

    void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;
        Timer = 0f;
    }


    void Start()
    {
        StopShake();
    }
    void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0f)
            {
                StopShake();
            }
        }
    }
}
