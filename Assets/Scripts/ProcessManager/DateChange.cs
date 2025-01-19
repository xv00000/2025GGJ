using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DateChange : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    private int[] date = { 99, 82, 64, 42, 31, 17, 7 };
    public void ChangeDate(int stage)
    {
        int dateValue = date[stage];

        // �޸�TextMeshPro������ı�����
        if (dateValue > 9)
        {
            textMeshPro.text = dateValue.ToString();
        }
        else
        {
            textMeshPro.text = "0"+dateValue.ToString();
        }
    }
}
