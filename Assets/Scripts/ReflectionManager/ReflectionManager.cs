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
    public CinemachineVirtualCamera virtualCamera; // �������������
    private CinemachineBasicMultiChannelPerlin noise; // �������������

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
            // ��ȡ Perlin Noise ���
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
        else
            {
            Debug.LogWarning("Virtual Camera is not assigned in the inspector!");
            }
        }

    /// <summary>
    /// �����λ����ʾ�����ı�
    /// </summary>
    public void Reflect(string reflectText)
        {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // ȷ�� Z ������ȷ

        GameObject instance = Instantiate(reflectTextPrefab, mousePosition, Quaternion.identity);
        TextMeshPro textComponent = instance.GetComponentInChildren<TextMeshPro>();

        if (textComponent != null)
            {
            textComponent.text = "+" + reflectText;
            }

        Destroy(instance, 2.0f); // 2�������

        }
    /// <summary>
    /// ��ָ��λ����ʾ�����ı�����ָ����ɫ
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

        Destroy(instance, 2.0f); // 2�������

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

        Destroy(instance, 2.0f); // 2�������

        }

    /// <summary>
    /// ��ָ��λ�����ɴ����Ч��������Ļ����
    /// </summary>
    /// <param name="position">������Ч��λ��</param>
    /// <param name="shakeIntensity">����ǿ��</param>
    /// <param name="shakeDuration">��������ʱ��</param>
    public void HitEffect(Vector3 position, float shakeIntensity = 0.66f, float shakeDuration = 0.1f)
        {
        // ������Ч
        GameObject temp = Instantiate(effectPrefab, position, Quaternion.identity);
        Destroy(temp, 0.5f);

        // ������Ļ����
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
    /// Ӧ���������
    /// </summary>
    /// <param name="intensity">��ǿ��</param>
    /// <param name="duration">�𶯳���ʱ��</param>
    private IEnumerator ApplyShake(float intensity, float duration)
        {
        noise.m_AmplitudeGain = intensity; // ������ǿ��
        noise.m_FrequencyGain = intensity; // ������Ƶ��
        yield return new WaitForSeconds(duration); // ����ʱ��
        noise.m_AmplitudeGain = 0f; // �𶯽���ʱ����
        noise.m_FrequencyGain = 0f; // ����Ƶ��
        }

    }