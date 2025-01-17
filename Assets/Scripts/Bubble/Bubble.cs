using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Bubble : MonoBehaviour
{
    [SerializeField]GameObject effect;
    GameObject self;
    public int id;        // ���ݵı��
    public Sprite sprite; // ���ݵ�ͼ��
    public int score;     // ���ݵķ���
    private bool isMaxSize = false;
    int studentId;
    [SerializeField]SpriteRenderer spriteRenderer;// �Ƿ�ﵽ���ߴ�
    [SerializeField]BubbleScript script;

    public void Init(BubbleScript bubbleScript, Vector2 offset,int studentId)
    {
        this.studentId = studentId;
        self = gameObject;
        script = bubbleScript;
        // ����id��Sprite��Score
        this.id = bubbleScript.id;
        this.sprite = bubbleScript.sprite;
        this.score = bubbleScript.score;
        Debug.Log(Data.students[studentId].name);
        // ��������λ�ã�����ƫ�ƣ�
        transform.position = (Vector2)Data.students[studentId].transform.position + offset;
        spriteRenderer.sprite = sprite;
        Data.students[studentId].GetComponent<Student>().ChangeState(StudentState.Distract);
        
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
        isMaxSize = true; // ��������Ѵﵽ���
        yield return new WaitForSeconds(0.5f);
        Tool.instance.DelayTime(() => { if (self) { Destroy(self); Data.students[studentId].GetComponent<Student>().ChangeState(StudentState.Idle); } },3);
        isMaxSize = false;
    }
    private void OnMouseDown()
    {
        if (isMaxSize)
        {
            // ���ųɹ���Ч
            AudioManager.instance.PlayEffect("Sounds/Effects/��������");
            // ���÷�������
            ProcessManager.instance.AddScore(score);
            Debug.Log(score);
        }
        else
        {
            // ����ʧ����Ч
            AudioManager.instance.PlayEffect("Sounds/Effects/ʧ�ܵ���������");

            // ���÷�������
            ProcessManager.instance.AddScore(score / 10);
            Debug.Log(score/10);
        }
        ReflectionManager.Instance.HitEffect(transform.position);
        Destroy(gameObject);
        Data.students[studentId].GetComponent<Student>().ChangeState(StudentState.Amaze);
        //ProcessManager.instance.students[script.studentId].GetComponent<Student>().ChangeState(StudentState.Amaze);
        Tool.instance.DelayTime(() => { Data.students[studentId].GetComponent<Student>().ChangeState(StudentState.Idle); },2);
        // ��������
        
    }
}

/*
 using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int Id;        // ���ݵı��
    public Sprite Sprite; // ���ݵ�ͼ��
    public int Score;     // ���ݵķ���
    private bool isMaxSize = false; // �Ƿ�ﵽ���ߴ�

    public void Init(string assetPath, Vector2 offset)
    {
        // ��ָ��·�����ؽű�������
        BubbleScript bubbleScript = Resources.Load<BubbleScript>(assetPath);
        if (bubbleScript == null)
        {
            Debug.LogError($"δ�ҵ�·��Ϊ {assetPath} ��BubbleScript��");
            return;
        }

        // ����Id��Sprite��Score
        this.Id = bubbleScript.Id;
        this.Sprite = bubbleScript.Sprite;
        this.Score = bubbleScript.Score;

        // ����Ŀ��λ�õ�GameObject
        GameObject targetGameObject = GameObject.Find(bubbleScript.Id.ToString());
        if (targetGameObject == null)
        {
            Debug.LogError($"�Ҳ������Ϊ {bubbleScript.Id} ��GameObject��");
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
        innerRenderer.sprite = this.Sprite;

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
        isMaxSize = true; // ��������Ѵﵽ���
    }

    private void OnMouseDown()
    {
        if (isMaxSize)
        {
            // ���ųɹ���Ч
            AudioManager.instance.PlayEffect("Sounds/Effects/��������");

            // ���÷�������
            FeedbackManager.AddScore(Score);

            // ��������
            Destroy(gameObject);
        }
        else
        {
            // ����ʧ����Ч
            AudioManager.instance.PlayEffect("Sounds/Effects/ʧ�ܵ���������");
        }
    }
}

 
 */