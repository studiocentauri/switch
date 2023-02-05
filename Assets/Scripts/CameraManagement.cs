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

    IEnumerator ShakeCameraCoroutine(float intensity, float startTime) {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        for(float time = startTime; time >= 0; time -= Time.deltaTime) {
            float t = time/startTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.SmoothStep(0, intensity, t);
            yield return null;
        }

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }    
    public void ShakeCamera(float intensity, float time)
    {
        StartCoroutine(ShakeCameraCoroutine(intensity, time));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
