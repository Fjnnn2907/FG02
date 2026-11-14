using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeCam = 1f;
    private float shakeTime = 0.2f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin cam;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (cinemachineVirtualCamera != null)
            cam = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        StopCamera();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StopCamera();
            }
        }
    }

    public void ShakeCamera(float intensity, float timer)
    {
        shakeCam = intensity;
        shakeTime = timer;
        this.timer = shakeTime;

        if (cam != null)
            cam.m_AmplitudeGain = shakeCam;
    }

    public void StopCamera()
    {
        if (cam != null)
            cam.m_AmplitudeGain = 0f;
        timer = 0;
    }
}
