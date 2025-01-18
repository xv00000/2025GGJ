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
            // 检查是否已经存在 CinemachineBasicMultiChannelPerlin
            var noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (noise == null)
            {
                // 动态添加 CinemachineBasicMultiChannelPerlin 组件
                noise = virtualCamera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }

            // 设置初始值
            noise.m_AmplitudeGain = 0f;
            noise.m_FrequencyGain = 0f;
        }
    }
}
