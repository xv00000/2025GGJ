using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using Unity.Mathematics;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;
public static class Data {
    public static int stage = 1;
    public static int score = 0;
    public static int normal = 0;
    public static int dream = 0;
    
    public static List<GameObject> students = new List<GameObject>();
    public static List<StudentScript> studentScripts = new List<StudentScript>();
    public static List<BubbleScript> bubbleScripts = new List<BubbleScript>();
}
public class ProcessManager : MonoBehaviour
{
    bool a = false;
    float aimFill;
    float length = 20;
    int count;
    public List<GameObject> students = new List<GameObject>();
    public List<StudentScript> studentScripts = new List<StudentScript>();
    public List<BubbleScript> bubbleScripts = new List<BubbleScript>();
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image processBar;
    [SerializeField] GameObject processBarGam;
    [SerializeField] SpriteRenderer back;
    [SerializeField] List<Sprite> backGrounds;
    [SerializeField] GameObject PausePanel;
    //[SerializeField] List<Sprite> BubbleSprites = new List<Sprite>();
    [SerializeField] GameObject BubblePrefab;
    public static ProcessManager instance;
    public List<GameObject> hand = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        Data.students = students;
        Data.studentScripts = studentScripts;
        Data.bubbleScripts = bubbleScripts;
        for (int i = 0; i < students.Count; i++) {
            students[i].GetComponent<Student>().Init(studentScripts[UnityEngine.Random.Range(0,studentScripts.Count)]);
            
            
            if (UnityEngine.Random.value < 0.5f) // 50%的概率
            {
                GameObject handInstance = Instantiate(hand[i]);
                handInstance.transform.position = transform.position + new Vector3( students[i].transform.position.x+4.145f-4.01f, students[i].transform.position.y + 2.958f -3f, 0) ; // 设置hand的位置
                handInstance.transform.parent = transform;
            }
            

        }
    }
    public void Initialize() { 
        count = 0;
        Data.score = 0;
        back.sprite = backGrounds[0];
        StartGame();
        TextAsset textAsset = Resources.Load<TextAsset>("bubbleGenerate"+Data.stage.ToString());//这里不要加文件扩展名
        if (textAsset != null)
        {
            string text = textAsset.text;
        }
        else
        {
            Debug.LogError("Text file not found in Resources!");
        }
        string[] lines = textAsset.text.Split('\n');
        length = lines.Length;
        StartCoroutine(Generatebu(lines));
    }
    public void StartGame()
    {
        processBarGam.SetActive(true);
        
    }
    public void EndGame()
    {
        Data.stage++;
    }
    public void GenerateBubble(BubbleScript bubbleScript,int studentId) {
        Bubble bubble = Instantiate(BubblePrefab).GetComponent<Bubble>();
        bubble.Init(bubbleScript,new Vector2(0.5f,0.5f),studentId);
        //Data.students[bubbleScript.id];
    }
    public float GetProcess() {
        if (count == length) { Tool.instance.DelayTime(() =>
        {
            if (!a)
            {
                a = true;
                if (Data.stage != 7)
                {
                    if (Data.score >= 200) { DialogueManager.Instance.BeginEnd1Dialogue(); Time.timeScale = 0; }
                    else DialogueManager.Instance.BeginEnd2Dialogue(); Time.timeScale = 0;
                }
                else
                {
                    SceneManager.LoadScene(2);
                }
            }

        }, 3); }
        //Debug.Log(count+" "+length);
        return count/length;
    }
    public BubbleScript FindBubble(int id) {
        for (int i = 0; i < Data.bubbleScripts.Count; i++) {
            if (id == Data.bubbleScripts[i].id) { 
                return Data.bubbleScripts[i];
            }
        
        }
        return null;
    
    }
    private void Update()
    {
        processBar.fillAmount = math.lerp(processBar.fillAmount,GetProcess(),Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0f;

        }
    }
    public void AddScore(int score) { 
        Data.score += score;
        scoreText.text = Data.stage+ "绩效："+Data.score;

    }
    private IEnumerator Generatebu(string[] lines)
    {
        //if (count == 1) { AudioManager.instance.PlayEffect(""); }   
        string line = lines[count];
        //Debug.Log(line);
        count++;
        if (line[0] == 'f') { EndGame(); }
        else
        {
            int id = (line[0] - 48) * 10 + line[1] - 48;
            int studentId = (line[3] - 48) * 10 + line[4] - 48;
            string type = line[6].ToString() + line[7].ToString();
            float nextTime = (line[9] - 48) * 10 + line[10] - 48 + (line[11] - 48) * 0.1f;//(line[9] - 48) * 100 + (line[10] - 48)*10 + (line[11] - 48) * 1f+ (line[12] - 48) * 0.1f+ (line[13] - 48) * 0.01f+ (line[14] - 48) * 0.0011f;
            Debug.Log(id + " "+studentId+" "+type +" "+nextTime);
            switch (type) {
                case "01": GenerateBubble(FindBubble(id),studentId);break;       
            }
            
            yield return new WaitForSeconds(nextTime);
        }
        if (count < lines.Length) StartCoroutine(Generatebu(lines));
    }
    public void TimeNormal() { 
        Time.timeScale = 1.0f;
    
    }
    public void nextStage() {
        SceneManager.LoadScene(1);
    
    }
}