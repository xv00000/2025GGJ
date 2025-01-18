using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI name1;
    public TextMeshProUGUI name2;
    public TextMeshProUGUI content;
    public SpriteRenderer spriteRenderer1;
    public SpriteRenderer spriteRenderer2;
    public Sprite Teacher;
    public Sprite Head;
    public List<string> names = new List<string>();
    private void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("DiagStart");//这里不要加文件扩展名
        if (textAsset != null)
        {
            string text = textAsset.text;
        }
        else
        {
            Debug.LogError("Text file not found in Resources!");
        }
        string[] lines = textAsset.text.Split('\n');
    }

}
