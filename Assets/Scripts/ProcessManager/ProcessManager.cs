using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public static class Data {
    public static int score;
    public static List<GameObject> students = new List<GameObject>();
    public static List<StudentScript> studentScripts = new List<StudentScript>();
}
public class ProcessManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] List<Sprite> BubbleSprites = new List<Sprite>();
    [SerializeField] GameObject BubblePrefab;
    public static ProcessManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void Initialize() { 
        Data.score = 0;
    }
    public void StartGame()
    {
        for (int i = 0; i < Data.students.Count; i++) { }
    }
    public void EndGame()
    {
        
    }
    public void GenerateBubble(BubbleScript bubbleScript) {
        Bubble bubble = Instantiate(BubblePrefab).GetComponent<Bubble>();
        bubble.Init(bubbleScript,new Vector2(0,0));
        //Data.students[bubbleScript.id];
        
    }
    public void AddScore(int score) { 
        Data.score += score;
    }
}
