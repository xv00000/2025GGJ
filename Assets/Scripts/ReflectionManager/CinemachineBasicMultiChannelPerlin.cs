using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSetup : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        if (virtualCamera != null)
        {
            // ����Ƿ��Ѿ����� CinemachineBasicMultiChannelPerlin
            var noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (noise == null)
            {
                // ��̬��� CinemachineBasicMultiChannelPerlin ���
                noise = virtualCamera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }

            // ���ó�ʼֵ
            noise.m_AmplitudeGain = 0f;
            noise.m_FrequencyGain = 0f;
        }
    }
}
