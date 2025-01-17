using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReflectionManager : MonoBehaviour
{
    public GameObject hitEffectObject;
    public GameObject reflectTextPrefab;
    public static ReflectionManager Instance;
    public float hitEffectTime = 0.5f;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // ȷ��������Ψһ��
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// �����λ����ʾ�����ı�
    /// </summary>
    public void Reflect(string reflectText)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // ȷ�� Z ������ȷ

        GameObject instance = Instantiate(reflectTextPrefab, mousePosition, Quaternion.identity);
        Text textComponent = instance.GetComponentInChildren<Text>();

        if (textComponent != null)
        {
            textComponent.text = reflectText;
        }
    }
    /// <summary>
    /// ��ָ��λ����ʾ�����ı�����ָ����ɫ
    /// </summary>
    public void Reflect(string reflectText, Vector3 position, Color color)
    {
        GameObject instance = Instantiate(reflectTextPrefab, position, Quaternion.identity);
        Text textComponent = instance.GetComponentInChildren<Text>();

        if (textComponent != null)
        {
            textComponent.text = reflectText;
            textComponent.color = color;
        }
    }

    /// <summary>
    /// ��ָ��λ�����ɴ����Ч
    /// </summary>
    /// <param name="position">������Ч��λ��</param>
    public void HitEffect(Vector3 position)
    {

        // �����µ�GameObject������ʾͼƬ
        GameObject temp = Instantiate(hitEffectObject, position, Quaternion.identity);

        // ����Ϊ0.5�������
        Destroy(temp, hitEffectTime);
    }
}