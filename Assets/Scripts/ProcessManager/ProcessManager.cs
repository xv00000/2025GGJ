using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Students { 
    //public static List<>


}
public static class Data {
    public static int score;


}
public class ProcessManager : MonoBehaviour
{
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
    public void GenerateBubble(int id, int score,Sprite sprite) {
        GameObject gameObject = Instantiate(BubblePrefab);
        
        

    }
    public void AddScore(int score) { 
        Data.score += score;
    }
}
