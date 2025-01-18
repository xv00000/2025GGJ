using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // 震动参数
    public float shakeDuration = 0.5f; // 震动持续时间
    public float shakeMagnitude = 0.1f; // 震动强度
    public float dampingSpeed = 1.0f; // 震动衰减速度

    private Vector3 initialPosition; // 相机初始位置
    private float currentShakeDuration = 0f; // 当前震动剩余时间

    void Awake()
    {
        // 记录相机的初始位置
        if (Camera.main != null)
        {
            initialPosition = Camera.main.transform.localPosition;
        }
    }

    void Update()
    {
        // 如果震动时间未结束，则继续震动
        if (currentShakeDuration > 0)
        {
            // 随机生成震动偏移量
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;

            // 更新相机位置
            Camera.main.transform.localPosition = initialPosition + shakeOffset;

            // 减少震动时间
            currentShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            // 震动结束，恢复相机位置
            currentShakeDuration = 0f;
            Camera.main.transform.localPosition = initialPosition;
        }
    }



    void Start()
    {
        TriggerShake();
    }

    // 触发相机震动
    public void TriggerShake()
    {
        currentShakeDuration = shakeDuration;
    }

    // 设置震动参数
    public void SetShakeParameters(float duration, float magnitude, float damping)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        dampingSpeed = damping;
    }
}