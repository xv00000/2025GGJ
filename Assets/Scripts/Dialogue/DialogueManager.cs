using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public TextMeshProUGUI score;
    public GameObject endpanel;
    bool start = false;
    bool end1 = false;
    bool end2 = false;
    public GameObject diagpanel;
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
    private void Awake()
    {
         Instance = this;
    }
    private void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("DiagStart"+Data.stage);//这里不要加文件扩展名
        string text = "";
        if (textAsset != null)
        {
            text = textAsset.text;
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
            startlines = System.Text.Encoding.UTF8.GetString(bytes).Split('\n');
        }
        else
        {
            Debug.LogError("Text file not found in Resources!");
        }
        
        TextAsset textAsset1 = Resources.Load<TextAsset>("DiagEnd1" + Data.stage);//这里不要加文件扩展名
        string text1 = "";
        if (textAsset1 != null)
        {
            text1 = textAsset1.text;
            byte[] bytes1 = System.Text.Encoding.UTF8.GetBytes(text1);
            endlines = System.Text.Encoding.UTF8.GetString(bytes1).Split('\n');
        }
        else
        {
            Debug.LogError("Text file not found in Resources!");
        }
        
        TextAsset textAsset2 = Resources.Load<TextAsset>("DiagEnd2" + Data.stage);//这里不要加文件扩展名
        string text2 = "";
        if (textAsset2 != null)
        {
            text2 = textAsset2.text;
            byte[] bytes2 = System.Text.Encoding.UTF8.GetBytes(text2);
            endlines1 = System.Text.Encoding.UTF8.GetString(bytes2).Split('\n');
        }
        else
        {
            Debug.LogError("Text file not found in Resources!");
        }
        
    }
    
    public void BeginStartDialogue() {
        diagpanel.SetActive(true);
        start = true;
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
        else { start = false; diagpanel.SetActive(false); ProcessManager.instance.Initialize(); Time.timeScale = 1; }
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
            else { start = false; diagpanel.SetActive(false); ProcessManager.instance.Initialize();Time.timeScale = 1; }
        }
        if (end1 && Input.GetMouseButtonDown(0))
        {
            if (endlines[endcnt][0] != 'f')
            {
                int id = endlines[endcnt][0] - '0';
                string content = "";
                for (int j = 2; j < endlines[endcnt].Length; j++)
                {
                    if (endlines[endcnt][j] != '\n') content += endlines[endcnt][j];
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
            else
            {
                if (Data.stage != 8)
                {
                    end1 = false; diagpanel.SetActive(false); score.text = "绩效：" + Data.score.ToString(); endpanel.SetActive(true);
                }
                else {
                    if ((float)Data.dream / (Data.dream + Data.normal) >= 0.3) Data.ending = 1;
                    else Data.ending = 2;
                    SceneManager.LoadScene(2);


                }
            }
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
            else {
                if (Data.stage != 8)
                {
                    end2 = false; diagpanel.SetActive(false); score.text = "绩效：" + Data.score.ToString(); endpanel.SetActive(true);
                }
                else
                {
                    if ((float)Data.dream / (Data.dream + Data.normal) >= 0.3) Data.ending = 1;
                    else Data.ending = 2;
                    SceneManager.LoadScene(2);


                }
            }
    }
    }
    
    public void BeginEnd1Dialogue() { 
        diagpanel.SetActive(true);
        end1 = true;
        if (endlines[endcnt][0] != 'f')
        {
            int id = endlines[endcnt][0] - '0';
            string content = "";
            for (int j = 2; j < endlines[endcnt].Length; j++)
            {
                if (endlines[endcnt][j] != '\n') content += endlines[endcnt][j];
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
        else
        {
            end1 = false; diagpanel.SetActive(false); score.text = "绩效：" + Data.score.ToString(); endpanel.SetActive(true);
        }


    }
    public void BeginEnd2Dialogue() {
        diagpanel.SetActive(true);
        end2 = true;
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
        else
        {
            end2 = false; diagpanel.SetActive(false); score.text = "绩效：" + Data.score.ToString(); endpanel.SetActive(true);
        }

    }

}
