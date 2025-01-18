using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(
    menuName = "Bubble/BubbleScript",
    fileName = "BubbleScript",
    order = 0)]
public class BubbleScript : ScriptableObject
    {
    public int id;
    public int studentId;
    public int score;
    /// <summary>
    /// ����
    /// </summary>
    public Sprite sprite;
    /// <summary>
    /// ���ݿ�
    /// </summary>
    public Sprite sprite_bubble;

    public Vector2 offect;

    }
