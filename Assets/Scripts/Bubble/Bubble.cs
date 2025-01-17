using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int id;        // 气泡的编号
    public Sprite sprite; // 气泡的图像
    public int score;     // 气泡的分数

    public void Init(string assetPath, Vector2 offset)
    {
        BubbleScript bubbleScript = Resources.Load<BubbleScript>(assetPath);
        if (bubbleScript == null)
        {
            Debug.LogError($"未找到路径为 {assetPath} 的BubbleScript！");
            return;
        }
        
        // 设置id、Sprite和Score
        this.id = bubbleScript.id;
        this.sprite = bubbleScript.sprite;
        this.score = bubbleScript.score;

        // 查找目标位置的GameObject
        GameObject targetGameObject = GameObject.Find(bubbleScript.id.ToString());
        if (targetGameObject == null)
        {
            Debug.LogError($"找不到编号为 {bubbleScript.id} 的GameObject！");
            return;
        }

        // 设置气泡位置（允许偏移）
        transform.position = (Vector2)targetGameObject.transform.position + offset;

        // 加载气泡图片
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Images/临时气泡");
        if (spriteRenderer.sprite == null)
        {
            Debug.LogError("未找到指定的气泡图片！");
        }

        // 设置子物体显示传入的Sprite
        GameObject innerBubble = new GameObject("InnerBubble");
        innerBubble.transform.SetParent(transform);
        innerBubble.transform.localPosition = Vector2.zero;

        SpriteRenderer innerRenderer = innerBubble.AddComponent<SpriteRenderer>();
        innerRenderer.sprite = this.sprite;

        // 开始从小变大的动画
        StartCoroutine(PlayBubbleAnimation());
    }

    private IEnumerator PlayBubbleAnimation()
    {
        float duration = 0.5f; // 动画持续时间
        float elapsedTime = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(startScale, endScale, progress);
            yield return null;
        }

        transform.localScale = endScale;
    }
}

