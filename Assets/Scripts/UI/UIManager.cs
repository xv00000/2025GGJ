using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
    {
    public bool scoreTextIsActive;
    public bool menuIsActive;

    public TextMeshPro ScoreText;
    public ProcessManager pm;

    void Update()
        {
        if (pm != null) ScoreText.text = Data.score.ToString();//�����ı��滻
        else ScoreText.text = "Process����û�л�ȡ��, ����TMP��ȡ����";

        //if (scoreTextIsActive == true) { ScoreText = }


        }
    }
