using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    bool start = false;
    bool end1 = false;
    bool end2 = false;
    public string[] startlines;
    int startcnt = 0, endcnt = 0,endcnt1=0;
    public string[] endlines;
    public string[] endlines1;
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
        string text = "";
        if (textAsset != null)
        {
            text = textAsset.text;
        }
        else
        {
            Debug.LogError("Text file not found in Resources!");
        }
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
        startlines = System.Text.Encoding.UTF8.GetString(bytes).Split('\n');
        TextAsset textAsset1 = Resources.Load<TextAsset>("DiagStart");//这里不要加文件扩展名
        string text1 = "";
        if (textAsset1 != null)
        {
            text1 = textAsset1.text;
        }
        else
        {
            Debug.LogError("Text file not found in Resources!");
        }
        byte[] bytes1 = System.Text.Encoding.UTF8.GetBytes(text1);
        endlines = System.Text.Encoding.UTF8.GetString(bytes1).Split('\n');
        TextAsset textAsset2 = Resources.Load<TextAsset>("DiagStart");//这里不要加文件扩展名
        string text2 = "";
        if (textAsset2 != null)
        {
            text2 = textAsset2.text;
        }
        else
        {
            Debug.LogError("Text file not found in Resources!");
        }
        byte[] bytes2 = System.Text.Encoding.UTF8.GetBytes(text2);
        endlines1 = System.Text.Encoding.UTF8.GetString(bytes2).Split('\n');
    }
    
    public void BeginStartDialogue() {
        start = true;
        //while (startlines[startcnt][0]!='f') {
        //    int id = startlines[startcnt][0] - '0';
        //    string content ="";
        //    for (int j = 2; j < startlines[startcnt].Length; j++)
        //    {
        //        if (startlines[startcnt][j] != '\n') content += startlines[startcnt][j];
        //    }
        //    string name = names[id];
        //    if (id == 1)
        //    {
        //        name1.text = name;
        //        spriteRenderer1.gameObject.SetActive(true);
        //        spriteRenderer2.gameObject.SetActive(false);
        //        name2.text = "";
        //        this.content.text = content;
        //    }
        //    else if (id == 2) { 
        //        name1.text = "";
        //        spriteRenderer2.gameObject.SetActive(true);
        //        spriteRenderer1.gameObject.SetActive(false);
        //        name2.text = name;
        //        this.content.text = content;
        //    }
        //}
        //start = false;
    }
    private void Update()
    {
        //Debug.Log(start+" "+startcnt);
        if (start && Input.GetMouseButtonDown(0))
        {
            if (startlines[startcnt][0] != 'f')
            {
                int id = startlines[startcnt][0] - '0';
                string content = "";
                for (int j = 2; j < startlines[startcnt].Length; j++)
                {
                    if (startlines[startcnt][j] != '\n') content += startlines[startcnt][j];
                }
                string name = names[id];
                Debug.Log(name + " " + content);
                if (id == 1)
                {
                    name1.text = name;
                    spriteRenderer1.gameObject.SetActive(true);
                    spriteRenderer2.gameObject.SetActive(false);
                    name2.text = "";
                    this.content.text = content;
                }
                else if (id == 0)
                {
                    name1.text = "";
                    spriteRenderer2.gameObject.SetActive(true);
                    spriteRenderer1.gameObject.SetActive(false);
                    name2.text = name;
                    this.content.text = content;
                }
                startcnt++;
            }
            else start = false;
        }
        if (end1 && Input.GetMouseButtonDown(0))
        {
            if (endlines[endcnt][0] != 'f')
            {
                int id = endlines[endcnt][0] - '0';
                string content = "";
                for (int j = 2; j < endlines[endcnt].Length; j++)
                {
                    if (endlines[startcnt][j] != '\n') content += endlines[endcnt][j];
                }
                string name = names[id];
                Debug.Log(name + " " + content);
                if (id == 1)
                {
                    name1.text = name;
                    spriteRenderer1.gameObject.SetActive(true);
                    spriteRenderer2.gameObject.SetActive(false);
                    name2.text = "";
                    this.content.text = content;
                }
                else if (id == 0)
                {
                    name1.text = "";
                    spriteRenderer2.gameObject.SetActive(true);
                    spriteRenderer1.gameObject.SetActive(false);
                    name2.text = name;
                    this.content.text = content;
                }
                endcnt++;
            }
            else end1 = false;
        }
        if (end2 && Input.GetMouseButtonDown(0))
        {
            if (endlines1[endcnt1][0] != 'f')
            {
                int id = endlines1[endcnt1][0] - '0';
                string content = "";
                for (int j = 2; j < endlines1[endcnt1].Length; j++)
                {
                    if (endlines1[endcnt1][j] != '\n') content += endlines1[endcnt1][j];
                }
                string name = names[id];
                Debug.Log(name + " " + content);
                if (id == 1)
                {
                    name1.text = name;
                    spriteRenderer1.gameObject.SetActive(true);
                    spriteRenderer2.gameObject.SetActive(false);
                    name2.text = "";
                    this.content.text = content;
                }
                else if (id == 0)
                {
                    name1.text = "";
                    spriteRenderer2.gameObject.SetActive(true);
                    spriteRenderer1.gameObject.SetActive(false);
                    name2.text = name;
                    this.content.text = content;
                }
                endcnt1++;
            }
            else end2 = false;
        }
    }
    public void BeginEnd1Dialogue() { 
        end1 = true;
    
    
    }public void BeginEnd2Dialogue() { 
        end2 = true;
    
    
    }

}
