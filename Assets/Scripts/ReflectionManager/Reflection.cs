using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Unity.Mathematics;

public class Reflection : MonoBehaviour
{
    float time;
    private Vector3 randomDirection; // 随机方向
    // Start is called before the first frame update
    void Start()
    {
        randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0.5f, 1f), 0).normalized;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0, (1 - time) * 2 * Time.deltaTime, 0);
        // 向上移动物体，带有随机 X 方向偏移，速度逐渐减慢
        //transform.Translate(new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), 1, 0) * (1 - time) * 2 * Time.deltaTime);
        transform.Translate(randomDirection * (1 - time) * 2 * Time.deltaTime);
        time = time + Time.deltaTime;
        if (time > 1) { Destroy(gameObject); }
    }
}