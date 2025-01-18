using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Combo : MonoBehaviour
{
    TextMeshPro combo;
    private void Start()
    {
        combo = GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        if (Data.combo >= 1) {
            //combo.text = "Á¬»÷X"+Data.combo;
            ReflectionManager.Instance.Reflect("Á¬»÷X" + Data.combo,new Vector3(-7,3,0),Color.red);
        }
    }
}
