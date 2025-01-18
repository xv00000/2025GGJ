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
    public Sprite bubbleSprite;// ���ݵĵ�ͼ
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

        // ���ݷ����޸����ݵ�ͼ
        SpriteRenderer bubbleSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //switch (score)
        //{
        //    case 100:
        //        bubbleSpriteRenderer.sprite = Resources.Load<Sprite>("Assets/Resources/Images/��ͨ����.png");
        //        break;
        //    case 200:
        //        bubbleSpriteRenderer.sprite = Resources.Load<Sprite>("Assets/Resources/Images/�߼�����.png");
        //        break;
        //    default:
        //        break;

        //}
        bubbleSpriteRenderer.sprite = bubbleScript.sprite_bubble;
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
            AudioManager.instance.PlayEffect("气泡破裂");
            ReflectionManager.Instance.Reflect((score).ToString(), transform.position, Color.green);
            // ���÷�������
            ProcessManager.instance.AddScore(score);
            //Debug.Log(score);
        }
        else
        {
            // ����ʧ����Ч
            AudioManager.instance.PlayEffect("气泡破裂");
            ReflectionManager.Instance.Reflect((score / 10).ToString(), transform.position, Color.green);
            // ���÷�������
            ProcessManager.instance.AddScore(score / 10);
            //Debug.Log(score/10);
        }
        Debug.Log("woc1");
        ReflectionManager.Instance.HitEffect(transform.position);
        Destroy(gameObject);
        Data.students[studentId].GetComponent<Student>().ChangeState(StudentState.Amaze);
        //ProcessManager.instance.students[script.studentId].GetComponent<Student>().ChangeState(StudentState.Amaze);
        Tool.instance.DelayTime(() => { Data.students[studentId].GetComponent<Student>().ChangeState(StudentState.Idle); },2);
        // ��������
        
    }
    private void OnMouseOver()
    {
        if (Skill._1) {
            if (isMaxSize)
            {
                // ���ųɹ���Ч
                AudioManager.instance.PlayEffect("气泡破裂");
                ReflectionManager.Instance.Reflect("+"+(score).ToString(), transform.position, Color.green);
                // ���÷�������
                ProcessManager.instance.AddScore(score);
                Debug.Log(score);
            }
            else
            {
                // ����ʧ����Ч
                AudioManager.instance.PlayEffect("气泡破裂");
                ReflectionManager.Instance.Reflect("+"+(score/10).ToString(),transform.position,Color.green);
                // ���÷�������
                ProcessManager.instance.AddScore(score / 10);
                Debug.Log(score / 10);
            }
            Debug.Log("woc2");
            ReflectionManager.Instance.HitEffect(transform.position, 5.0f);
            Destroy(gameObject);
            Data.students[studentId].GetComponent<Student>().ChangeState(StudentState.Amaze);
            //ProcessManager.instance.students[script.studentId].GetComponent<Student>().ChangeState(StudentState.Amaze);
            Tool.instance.DelayTime(() => { Data.students[studentId].GetComponent<Student>().ChangeState(StudentState.Idle); }, 2);


        }
    }
}