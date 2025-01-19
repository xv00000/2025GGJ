using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("动画控制")]
    public Animator animator; // Animator组件
    public string animationName = "Idle"; // 动画的名称（需在Animator中配置）

    void Start()
    {
        if (animator == null)
        {
            // 自动尝试获取Animator组件
            animator = GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("没有找到Animator组件，请手动设置！");
            return;
        }

        PlayAnimation();
    }

    void PlayAnimation()
    {
        // 检查动画状态是否存在
        if (!animator.HasState(0, Animator.StringToHash(animationName)))
        {
            Debug.LogError($"动画 '{animationName}' 不存在于当前Animator Controller中！");
            return;
        }

        // 播放动画
        animator.Play(animationName, 0, 0);
    }
}
