using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("��������")]
    public Animator animator; // Animator���
    public string animationName = "Idle"; // ���������ƣ�����Animator�����ã�

    void Start()
    {
        if (animator == null)
        {
            // �Զ����Ի�ȡAnimator���
            animator = GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("û���ҵ�Animator��������ֶ����ã�");
            return;
        }

        PlayAnimation();
    }

    void PlayAnimation()
    {
        // ��鶯��״̬�Ƿ����
        if (!animator.HasState(0, Animator.StringToHash(animationName)))
        {
            Debug.LogError($"���� '{animationName}' �������ڵ�ǰAnimator Controller�У�");
            return;
        }

        // ���Ŷ���
        animator.Play(animationName, 0, 0);
    }
}
