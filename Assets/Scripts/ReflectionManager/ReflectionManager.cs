using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReflectionManager : MonoBehaviour
{
    public GameObject reflectTextPrefab;
    public GameObject effectPrefab;
    public static ReflectionManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
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
        Text textComponent = instance.GetComponentInChildren<Text>();

        if (textComponent != null)
        {
            textComponent.text = reflectText;
            textComponent.color = color;
        }

        Destroy(instance, 2.0f); // 2�������

    }

    /// <summary>
    /// ��ָ��λ�����ɴ����Ч
    /// </summary>
    /// <param name="position">������Ч��λ��</param>
    public void HitEffect(Vector3 position)
    {
        GameObject temp = Instantiate(gameObject,position,Quaternion.identity);

       

        // ����Ϊ0.5�������
        Destroy(temp, 0.5f);
    }
}