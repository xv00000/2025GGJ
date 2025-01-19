using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReflectionManager : MonoBehaviour
    {
    public GameObject reflectTextPrefab;
    public GameObject effectPrefab;
    public static ReflectionManager Instance;
    public CinemachineVirtualCamera virtualCamera; // 虚拟摄像机引用
    private CinemachineBasicMultiChannelPerlin noise; // 控制噪声的组件

    private void Awake()
        {
        if (Instance != null && Instance != this)
            {
            Destroy(gameObject);
            return;
            }
        Instance = this;

        if (virtualCamera != null)
            {
            // 获取 Perlin Noise 组件
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
        else
            {
            Debug.LogWarning("Virtual Camera is not assigned in the inspector!");
            }
        }

    /// <summary>
    /// 在鼠标位置显示反射文本
    /// </summary>
    public void Reflect(string reflectText)
        {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 确保 Z 坐标正确

        GameObject instance = Instantiate(reflectTextPrefab, mousePosition, Quaternion.identity);
        TextMeshPro textComponent = instance.GetComponentInChildren<TextMeshPro>();

        if (textComponent != null)
            {
            textComponent.text = "+" + reflectText;
            }

        Destroy(instance, 2.0f); // 2秒后销毁

        }
    /// <summary>
    /// 在指定位置显示反射文本，并指定颜色
    /// </summary>
    public void Reflect(string reflectText, Vector3 position, Color color)
        {
        GameObject instance = Instantiate(reflectTextPrefab, position, Quaternion.identity);
        TextMeshPro textComponent = instance.GetComponentInChildren<TextMeshPro>();

        if (textComponent != null)
            {
            textComponent.text = reflectText;
            textComponent.color = color;
            }

        Destroy(instance, 2.0f); // 2秒后销毁

        }
    public void Reflect(string reflectText, Vector3 position, Color color,int size)
        {
        GameObject instance = Instantiate(reflectTextPrefab, position, Quaternion.identity);
        TextMeshPro textComponent = instance.GetComponentInChildren<TextMeshPro>();

        if (textComponent != null)
            {
            textComponent.text = reflectText;
            textComponent.color = color;
            textComponent.fontSize = size;
            }

        Destroy(instance, 2.0f); // 2秒后销毁

        }

    /// <summary>
    /// 在指定位置生成打击特效并触发屏幕抖动
    /// </summary>
    /// <param name="position">生成特效的位置</param>
    /// <param name="shakeIntensity">抖动强度</param>
    /// <param name="shakeDuration">抖动持续时间</param>
    public void HitEffect(Vector3 position, float shakeIntensity = 0.66f, float shakeDuration = 0.1f)
        {
        // 生成特效
        GameObject temp = Instantiate(effectPrefab, position, Quaternion.identity);
        Destroy(temp, 0.5f);

        // 触发屏幕抖动
        if (noise != null)
            {
            StartCoroutine(ApplyShake(shakeIntensity, shakeDuration));
            }
        else
            {
            Debug.LogWarning("CinemachineBasicMultiChannelPerlin component is not assigned!");
            }
        }

    /// <summary>
    /// 应用摄像机震动
    /// </summary>
    /// <param name="intensity">震动强度</param>
    /// <param name="duration">震动持续时间</param>
    private IEnumerator ApplyShake(float intensity, float duration)
        {
        noise.m_AmplitudeGain = intensity; // 设置震动强度
        noise.m_FrequencyGain = intensity; // 设置震动频率
        yield return new WaitForSeconds(duration); // 持续时间
        noise.m_AmplitudeGain = 0f; // 震动结束时重置
        noise.m_FrequencyGain = 0f; // 重置频率
        }

    }