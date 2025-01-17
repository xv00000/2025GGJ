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
    public int score;
    public Sprite sprite;

}
