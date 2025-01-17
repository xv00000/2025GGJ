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
        
    }
    public void EndGame()
    {
        
    }
    public void GenerateBubble(BubbleScript bubbleScript) {
        GameObject gameObject = Instantiate(BubblePrefab);
        
        
    }
    public void AddScore(int score) { 
        Data.score += score;
    }
}
