using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.U2D;

public enum StudentState
{
    Idle,
    Distract,
    Amaze
}

public class Student : MonoBehaviour
{
    public int _student_id;
    public SpriteRenderer spriteRenderer;
    public StudentState _currentState;
    public StudentScript studentScript;



    // 呼吸效果参数
    public float breathSpeed = 1.0f; // 呼吸速度
    public float breathRange = 0.2f; // 缩放变化范围
    private Vector3 initialScale; // 初始缩放

    // 构造函数
    public void Init(StudentScript studentScript)
    {
        _currentState = StudentState.Idle;
        this.studentScript  = studentScript;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = studentScript.IdleSprite;
        /*_currentState = StudentState.Amaze; // 初始状态设置为Idle
        Debug.Log(_currentState + "test");
        // 记录初始缩放
        initialScale = transform.localScale;*/
    }

    void Start()
    {
        initialScale = transform.localScale;
        _currentState = StudentState.Idle;
    }

    void Update()
    {
        // 如果当前状态是Amaze，则执行呼吸效果
        if (_currentState == StudentState.Idle || _currentState == StudentState.Distract)
        {
            ApplyBreathEffect();
        }
    }

    // 应用呼吸效果
    private void ApplyBreathEffect()
    {
        // 计算呼吸效果因子
        float breathFactor = Mathf.Sin(Time.time * breathSpeed) * breathRange;

        // 计算新的缩放，确保缩放值合理
        Vector3 newScale = initialScale * (1 + breathFactor);

        // 更新对象缩放
        transform.localScale = newScale;
    }

    public void ChangeState(StudentState newState)
    {
        _currentState = newState;
        switch (newState)
        {
            case StudentState.Idle:
                spriteRenderer.sprite = studentScript.IdleSprite;
                ResetScale(); // 重置缩放
                break;
            case StudentState.Distract:
                spriteRenderer.sprite = studentScript.DistractSprite;
                ResetScale(); // 重置缩放
                break;
            case StudentState.Amaze:
                spriteRenderer.sprite = studentScript.AmazeSprite;
                ResetScale(); // 切换到Amaze状态时也重置缩放
                break;
        }
    }

    // 重置缩放
    private void ResetScale()
    {
        transform.localScale = initialScale;
    }
}