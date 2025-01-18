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



    // ����Ч������
    public float breathSpeed = 1.0f; // �����ٶ�
    public float breathRange = 0.2f; // ���ű仯��Χ
    private Vector3 initialScale; // ��ʼ����

    // ���캯��
    public void Init(StudentScript studentScript)
    {
        _currentState = StudentState.Idle;
        this.studentScript  = studentScript;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = studentScript.IdleSprite;
        /*_currentState = StudentState.Amaze; // ��ʼ״̬����ΪIdle
        Debug.Log(_currentState + "test");
        // ��¼��ʼ����
        initialScale = transform.localScale;*/
    }

    void Start()
    {
        initialScale = transform.localScale;
        _currentState = StudentState.Idle;
    }

    void Update()
    {
        // �����ǰ״̬��Amaze����ִ�к���Ч��
        if (_currentState == StudentState.Idle || _currentState == StudentState.Distract)
        {
            ApplyBreathEffect();
        }
    }

    // Ӧ�ú���Ч��
    private void ApplyBreathEffect()
    {
        // �������Ч������
        float breathFactor = Mathf.Sin(Time.time * breathSpeed) * breathRange;

        // �����µ����ţ�ȷ������ֵ����
        Vector3 newScale = initialScale * (1 + breathFactor);

        // ���¶�������
        transform.localScale = newScale;
    }

    public void ChangeState(StudentState newState)
    {
        _currentState = newState;
        switch (newState)
        {
            case StudentState.Idle:
                spriteRenderer.sprite = studentScript.IdleSprite;
                ResetScale(); // ��������
                break;
            case StudentState.Distract:
                spriteRenderer.sprite = studentScript.DistractSprite;
                ResetScale(); // ��������
                break;
            case StudentState.Amaze:
                spriteRenderer.sprite = studentScript.AmazeSprite;
                ResetScale(); // �л���Amaze״̬ʱҲ��������
                break;
        }
    }

    // ��������
    private void ResetScale()
    {
        transform.localScale = initialScale;
    }
}