using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.U2D;
public static class Data {
    public static int stage = 0;
    public static int score = 0;
    public static List<GameObject> students = new List<GameObject>();
    public static List<StudentScript> studentScripts = new List<StudentScript>();
    public static List<BubbleScript> bubbleScripts = new List<BubbleScript>();
}
public class ProcessManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer back;
    [SerializeField] List<Sprite> backGrounds;
    [SerializeField] TextMeshProUGUI scoreText;
    //[SerializeField] List<Sprite> BubbleSprites = new List<Sprite>();
    [SerializeField] GameObject BubblePrefab;
    public static ProcessManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void Initialize() { 
        int count = 0;
        Data.score = 0;
        back.sprite = backGrounds[Data.stage];
        TextAsset textAsset = Resources.Load<TextAsset>("bubbleGenerateTxt");//这里不要加文件扩展名
        if (textAsset != null)
        {
            string text = textAsset.text;
        }
        else
        {
            Debug.LogError("Text file not found in Resources!");
        }
        string[] lines = textAsset.text.Split('\n');
        StartCoroutine(Generatebu(count, lines));
    }
    public void StartGame()
    {
        for (int i = 0; i < Data.students.Count; i++) { }
    }
    public void EndGame()
    {
        Data.stage++;
    }
    public void GenerateBubble(BubbleScript bubbleScript) {
        Bubble bubble = Instantiate(BubblePrefab).GetComponent<Bubble>();
        bubble.Init(bubbleScript,new Vector2(0,0));
        //Data.students[bubbleScript.id];
    }
    public BubbleScript FindBubble(int id) {
        for (int i = 0; i < Data.bubbleScripts.Count; i++) {
            if (id == Data.bubbleScripts[i].id) { 
                return Data.bubbleScripts[i];
            
            }
        
        }
        return null;
    
    }
    public void AddScore(int score) { 
        Data.score += score;
    }
    private IEnumerator Generatebu(int count, string[] lines)
    {
        //if (count == 1) { AudioManager.instance.PlayEffect(""); }   
        string line = lines[count];
        count++;
        if (line[0] == 'f') { EndGame(); }
        else
        {
            int id = (line[0] - 48) * 10 + line[1] - 48;
            int studentId = (line[3] - 48) * 10 + line[4] - 48;
            string type = line[6].ToString() + line[7].ToString();
            float nextTime = (line[9] - 48) * 10 + line[10] - 48 + (line[11] - 48) * 0.1f;
            switch (type) {
                case "01": GenerateBubble(FindBubble(id));break;       
            }
            
            yield return new WaitForSeconds(nextTime);
        }
        if (count < lines.Length) StartCoroutine(Generatebu(count, lines));
    }
}
