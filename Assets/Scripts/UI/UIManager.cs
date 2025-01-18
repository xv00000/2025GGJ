using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
    {
    public bool scoreTextIsActive;
    public bool menuIsActive;

    public TextMeshProUGUI ScoreText;
    public ProcessManager pm;

    void Update()
        {
        if (pm != null) ScoreText.text = Data.score.ToString();//分数文本替换
        else ScoreText.text = "Process对象没有获取到, 但是TMP获取到了";

        //if (scoreTextIsActive == true) { ScoreText = }


        }
    }
