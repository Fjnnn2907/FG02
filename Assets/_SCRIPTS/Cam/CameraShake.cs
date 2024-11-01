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
        instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        // Lấy Perlin noise component để điều khiển độ rung
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
        // Đặt biên độ và thời gian cho camera rung
        shakeCam = intensity;
        shakeTime = timer;
        this.timer = shakeTime;

        // Bắt đầu rung
        cam.m_AmplitudeGain = shakeCam;
    }

    public void StopCamera()
    {
        // Ngưng rung bằng cách đặt biên độ về 0
        cam.m_AmplitudeGain = 0f;
        timer = 0;
    }
}
