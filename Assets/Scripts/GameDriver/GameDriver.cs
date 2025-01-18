using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Skill {
    public static bool _1 = false;


}
public class GameDriver : MonoBehaviour
{
    //public Bubble Bubble;
    //public BubbleScript BubbleScript;
    void Start()
    {
        DialogueManager.Instance.BeginStartDialogue();
        //ReflectionManager.Instance.HitEffect(new Vector3(0, 0, 0));

        
        //Bubble.Init(BubbleScript,new Vector2(0,0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Skill._1 = !Skill._1;
        
        }
        Debug.Log((float)Data.dream / (Data.dream + Data.normal));
    }
}
