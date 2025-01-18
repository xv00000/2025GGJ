using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorManager : MonoBehaviour
{
    public Texture2D normalCursor; // 常态鼠标样式
    public Texture2D clickCursor;  // 点击时鼠标样式
    public Vector2 hotspot = Vector2.zero; // 鼠标热点位置，默认为图片左上角

    void Awake()
    {
        // 设置初始鼠标样式
        if (normalCursor != null)
        {
            Cursor.SetCursor(normalCursor, hotspot, CursorMode.Auto);
        }
        else
        {
            Debug.LogWarning("Normal cursor texture is not assigned.");
        }
    }

    void Update()
    {
        // 检测鼠标是否按下
        if (Input.GetMouseButtonDown(0)) // 鼠标左键按下
        {
            if (clickCursor != null)
            {
                Cursor.SetCursor(clickCursor, hotspot, CursorMode.Auto);
            }
        }
        else if (Input.GetMouseButtonUp(0)) // 鼠标左键抬起
        {
            if (normalCursor != null)
            {
                Cursor.SetCursor(normalCursor, hotspot, CursorMode.Auto);
            }
        }
    }
}
