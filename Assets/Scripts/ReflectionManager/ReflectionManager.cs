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

        // 预加载图片资源
        hitSprite = Resources.Load<Sprite>("Images/教材打击");
        if (hitSprite == null)
        {
            Debug.LogError("HitEffect: 图片 'Images/教材打击' 未找到！");
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
        Text textComponent = instance.GetComponentInChildren<Text>();

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
        Text textComponent = instance.GetComponentInChildren<Text>();

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
    public void HitEffect(Vector3 position)
    {
        // 加载图片资源
        if (hitSprite == null)
        {
            Debug.LogError("HitEffect: 图片 'Images/教材打击' 未找到！");
            return;
        }

        // 创建新的GameObject用于显示图片
        GameObject hitEffectObject = new GameObject("HitEffect");
        hitEffectObject.transform.position = position;

        // 添加SpriteRenderer组件
        SpriteRenderer spriteRenderer = hitEffectObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = hitSprite;

        // 设置为0.5秒后销毁
        Destroy(hitEffectObject, 0.5f);
    }
}