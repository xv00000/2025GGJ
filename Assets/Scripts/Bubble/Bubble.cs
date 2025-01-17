using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int id;        // ���ݵı��
    public Sprite sprite; // ���ݵ�ͼ��
    public int score;     // ���ݵķ���

    public void Init(string assetPath, Vector2 offset)
    {
        BubbleScript bubbleScript = Resources.Load<BubbleScript>(assetPath);
        if (bubbleScript == null)
        {
            Debug.LogError($"δ�ҵ�·��Ϊ {assetPath} ��BubbleScript��");
            return;
        }
        
        // ����id��Sprite��Score
        this.id = bubbleScript.id;
        this.sprite = bubbleScript.sprite;
        this.score = bubbleScript.score;

        // ����Ŀ��λ�õ�GameObject
        GameObject targetGameObject = GameObject.Find(bubbleScript.id.ToString());
        if (targetGameObject == null)
        {
            Debug.LogError($"�Ҳ������Ϊ {bubbleScript.id} ��GameObject��");
            return;
        }

        // ��������λ�ã�����ƫ�ƣ�
        transform.position = (Vector2)targetGameObject.transform.position + offset;

        // ��������ͼƬ
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Images/��ʱ����");
        if (spriteRenderer.sprite == null)
        {
            Debug.LogError("δ�ҵ�ָ��������ͼƬ��");
        }

        // ������������ʾ�����Sprite
        GameObject innerBubble = new GameObject("InnerBubble");
        innerBubble.transform.SetParent(transform);
        innerBubble.transform.localPosition = Vector2.zero;

        SpriteRenderer innerRenderer = innerBubble.AddComponent<SpriteRenderer>();
        innerRenderer.sprite = this.sprite;

        // ��ʼ��С���Ķ���
        StartCoroutine(PlayBubbleAnimation());
    }

    private IEnumerator PlayBubbleAnimation()
    {
        float duration = 0.5f; // ��������ʱ��
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

