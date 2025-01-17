using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDriver : MonoBehaviour
{
    //public Bubble Bubble;
    //public BubbleScript BubbleScript;
    void Start()
    {
        //ReflectionManager.Instance.HitEffect(new Vector3(0, 0, 0));
        ProcessManager.instance.Initialize();

        
        //Bubble.Init(BubbleScript,new Vector2(0,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
