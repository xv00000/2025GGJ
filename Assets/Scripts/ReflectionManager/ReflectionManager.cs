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
    public CinemachineImpulseSource impulseSource; // 添加一个 CinemachineImpulseSource 的引用

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (impulseSource == null)
        {
            Debug.LogWarning("CinemachineImpulseSource is not assigned in the inspector!");
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

    /// <summary>
    /// 在指定位置生成打击特效
    /// </summary>
    /// <param name="position">生成特效的位置</param>
    public void HitEffect(Vector3 position, float shakeIntensity)
    {
        GameObject temp = Instantiate(effectPrefab,position,Quaternion.identity);

        // 触发 Cinemachine 屏幕抖动
        if (impulseSource != null)
        {
            // 根据输入的强度设置 Impulse 的振幅增益
            impulseSource.m_ImpulseDefinition.m_AmplitudeGain = shakeIntensity;

            impulseSource.GenerateImpulse();
        }

        // 设置为0.5秒后销毁
        Destroy(temp, 0.5f);
    }
    public void HitEffect(Vector3 position)
    {
        HitEffect(position, 2.0f); // 默认抖动强度
    }

}