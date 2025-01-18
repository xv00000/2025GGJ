using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // �𶯲���
    public float shakeDuration = 0.5f; // �𶯳���ʱ��
    public float shakeMagnitude = 0.1f; // ��ǿ��
    public float dampingSpeed = 1.0f; // ��˥���ٶ�

    private Vector3 initialPosition; // �����ʼλ��
    private float currentShakeDuration = 0f; // ��ǰ��ʣ��ʱ��

    void Awake()
    {
        // ��¼����ĳ�ʼλ��
        if (Camera.main != null)
        {
            initialPosition = Camera.main.transform.localPosition;
        }
    }

    void Update()
    {
        // �����ʱ��δ�������������
        if (currentShakeDuration > 0)
        {
            // ���������ƫ����
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;

            // �������λ��
            Camera.main.transform.localPosition = initialPosition + shakeOffset;

            // ������ʱ��
            currentShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            // �𶯽������ָ����λ��
            currentShakeDuration = 0f;
            Camera.main.transform.localPosition = initialPosition;
        }
    }



    void Start()
    {
        TriggerShake();
    }

    // ���������
    public void TriggerShake()
    {
        currentShakeDuration = shakeDuration;
    }

    // �����𶯲���
    public void SetShakeParameters(float duration, float magnitude, float damping)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        dampingSpeed = damping;
    }
}