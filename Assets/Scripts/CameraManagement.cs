using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManagement : MonoBehaviour
{
    public static CameraManagement Instance { get; private set; }
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera= GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        Instance = this;
        cinemachineVirtualCamera= GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity,float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain= intensity;
        shakeTimer= time;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer < 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
