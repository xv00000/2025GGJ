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
    public int id;        // 气泡的编号
    public Sprite sprite; // 气泡的图像
    public Sprite bubbleSprite;// 气泡的底图
    public int score;     // 气泡的分数
    private bool isMaxSize = false;
    [SerializeField]SpriteRenderer spriteRenderer;// 是否达到最大尺寸
    [SerializeField]BubbleScript script;

    public void Init(BubbleScript bubbleScript, Vector2 offset)
    {
        self = gameObject;
        script = bubbleScript;
        // 设置id、Sprite和Score
        this.id = bubbleScript.id;
        this.sprite = bubbleScript.sprite;
        this.score = bubbleScript.score;

        // 根据分数修改气泡底图
        SpriteRenderer bubbleSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        switch (score)
        {
            case 100:
                bubbleSpriteRenderer.sprite = Resources.Load<Sprite>("Assets/Resources/Images/普通气泡.png");
                break;
            case 200:
                bubbleSpriteRenderer.sprite = Resources.Load<Sprite>("Assets/Resources/Images/高级气泡.png");
                break;
            default:
                break;

        }

        // 设置气泡位置（允许偏移）
        transform.position = (Vector2)Data.students[bubbleScript.studentId].transform.position + offset;
        spriteRenderer.sprite = sprite;
        Data.students[bubbleScript.studentId].GetComponent<Student>().ChangeState(StudentState.Distract);
        
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
        isMaxSize = true; // 标记气泡已达到最大
        yield return new WaitForSeconds(0.5f);
        Tool.instance.DelayTime(() => { if (self) { Destroy(self); } },3);
        isMaxSize = false;
    }
    private void OnMouseDown()
    {
        if (isMaxSize)
        {
            // 播放成功音效
            AudioManager.instance.PlayEffect("Sounds/Effects/气泡破裂");
            // 调用分数管理
            ProcessManager.instance.AddScore(score);
        }
        else
        {
            // 播放失败音效
            AudioManager.instance.PlayEffect("Sounds/Effects/失败的气泡破裂");

            // 调用分数管理
            ProcessManager.instance.AddScore(score / 10);
        }
        ReflectionManager.Instance.HitEffect(new Vector3(0, 0, 0));
        Data.studentScripts[script.studentId].GetComponent<Student>().ChangeState(StudentState.Amaze);
        Tool.instance.DelayTime(() => { Data.studentScripts[script.studentId].GetComponent<Student>().ChangeState(StudentState.Amaze);Destroy(gameObject); },2);
        // 销毁气泡
        
    }
}