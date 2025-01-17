using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using Unity.VisualScripting;

public class Tool
{
    public static Tool instance = new Tool();
    public async void DelayTime(Action action, float seconds)
    {
        await Task.Delay((int)(seconds * 1000 / (Time.timeScale != 0 ? Time.timeScale : 1)));
        action?.Invoke();
    }


}