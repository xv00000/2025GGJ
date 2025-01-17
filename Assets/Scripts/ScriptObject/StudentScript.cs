using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(
    menuName = "Student/StudentScript",
    fileName = "StudentScript",
    order = 0)]
public class StudentScript : ScriptableObject
{
    public int id;
    public Sprite IdleSprite;
    public Sprite AmazeSprite;
    public Sprite DistractSprite;
}
