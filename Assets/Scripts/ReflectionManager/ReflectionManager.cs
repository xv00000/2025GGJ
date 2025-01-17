using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReflectionManager : MonoBehaviour
{
    public GameObject reflectTextPrefab;
    public static ReflectionManager Instance;
    private Sprite hitSprite;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Ԥ����ͼƬ��Դ
        hitSprite = Resources.Load<Sprite>("Images/�̲Ĵ��");
        if (hitSprite == null)
        {
            Debug.LogError("HitEffect: ͼƬ 'Images/�̲Ĵ��' δ�ҵ���");
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
        // ����ͼƬ��Դ
        if (hitSprite == null)
        {
            Debug.LogError("HitEffect: ͼƬ 'Images/�̲Ĵ��' δ�ҵ���");
            return;
        }

        // �����µ�GameObject������ʾͼƬ
        GameObject hitEffectObject = new GameObject("HitEffect");
        hitEffectObject.transform.position = position;

        // ���SpriteRenderer���
        SpriteRenderer spriteRenderer = hitEffectObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = hitSprite;

        // ����Ϊ0.5�������
        Destroy(hitEffectObject, 0.5f);
    }
}