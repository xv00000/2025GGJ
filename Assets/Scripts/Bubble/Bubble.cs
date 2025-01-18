using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
public static class Texture
    {
    public static Texture2D normalCursor; // 常态鼠标样式
    public static Texture2D clickCursor;

    }
public class Bubble : MonoBehaviour
    {
    [SerializeField] GameObject effect;
    GameObject self;
    public int id;        // ���ݵı��
    public Sprite sprite; // ���ݵ�ͼ��
    public Sprite bubbleSprite;// ���ݵĵ�ͼ
    public int score;     // ���ݵķ���
    private bool isMaxSize = false;
    int studentId;
    [SerializeField] SpriteRenderer spriteRenderer;// �Ƿ�ﵽ���ߴ�
    [SerializeField] BubbleScript script;

    public void Init(BubbleScript bubbleScript, Vector2 offset, int studentId)
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
        isMaxSize = true;
        while (elapsedTime < duration)
            {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(startScale, endScale, progress);
            yield return null;
            }

        transform.localScale = endScale;

        yield return new WaitForSeconds(1f);
        Tool.instance.DelayTime(() => { if (self) { Destroy(self); Data.students[studentId].GetComponent<Student>().ChangeState(StudentState.Idle); } }, 3);
        isMaxSize = false;
        }
    private void OnMouseDown()
        {
        if (isMaxSize)
            {
            Data.combo += 1;
            // ���ųɹ���Ч
            AudioManager.instance.PlayEffect("气泡破裂");
            ReflectionManager.Instance.Reflect((score).ToString(), transform.position, Color.green);
            // ���÷�������
            ProcessManager.instance.AddScore(score);
            //Debug.Log(score);
            }
        else
            {
            Data.combo = 0;
            // ����ʧ����Ч
            AudioManager.instance.PlayEffect("气泡破裂");
            ReflectionManager.Instance.Reflect((score / 2).ToString(), transform.position, Color.green);
            // ���÷�������
            ProcessManager.instance.AddScore(score / 2);
            //Debug.Log(score/10);
            }
        Debug.Log("woc1");
        ReflectionManager.Instance.HitEffect(transform.position);//后面的浮点数是震动强度
        Destroy(gameObject);
        Data.students[studentId].GetComponent<Student>().ChangeState(StudentState.Amaze);
        //ProcessManager.instance.students[script.studentId].GetComponent<Student>().ChangeState(StudentState.Amaze);
        Tool.instance.DelayTime(() => { Data.students[studentId].GetComponent<Student>().ChangeState(StudentState.Idle); }, 2);
        // ��������

        }
    private void OnMouseOver()
        {
        if (Skill._1)
            {
            if (isMaxSize)
                {
                Data.combo += 1;
                // ���ųɹ���Ч
                AudioManager.instance.PlayEffect("气泡破裂");
                ReflectionManager.Instance.Reflect((score).ToString(), transform.position, Color.green);
                // ���÷�������
                ProcessManager.instance.AddScore(score);
                Debug.Log(score);
                }
            else
                {
                Data.combo = 0;
                // ����ʧ����Ч
                AudioManager.instance.PlayEffect("气泡破裂");
                ReflectionManager.Instance.Reflect((score / 2).ToString(), transform.position, Color.green);
                // ���÷�������
                ProcessManager.instance.AddScore(score / 2);
                Debug.Log(score / 10);
                }
            Cursor.SetCursor(Texture.clickCursor, new Vector2(-0.35f, 0.25f), CursorMode.Auto);
            Debug.Log("woc2");
            ReflectionManager.Instance.HitEffect(transform.position);//后面的浮点数是震动强度
            Destroy(gameObject);
            Data.students[studentId].GetComponent<Student>().ChangeState(StudentState.Amaze);
            //ProcessManager.instance.students[script.studentId].GetComponent<Student>().ChangeState(StudentState.Amaze);
            Tool.instance.DelayTime(() => { Data.students[studentId].GetComponent<Student>().ChangeState(StudentState.Idle); }, 2);
            Tool.instance.DelayTime(() => { Cursor.SetCursor(Texture.normalCursor, new Vector2(-0.35f, 0.25f), CursorMode.Auto); }, 0.2f);

            }
        }
    }