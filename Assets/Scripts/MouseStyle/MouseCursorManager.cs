using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorManager : MonoBehaviour
{
    public Texture2D normalCursor; // ��̬�����ʽ
    public Texture2D clickCursor;  // ���ʱ�����ʽ
    public Vector2 hotspot = Vector2.zero; // ����ȵ�λ�ã�Ĭ��ΪͼƬ���Ͻ�

    void Awake()
    {
        //Texture.normalCursor = normalCursor;
        Texture.clickCursor = clickCursor;  
        // ���ó�ʼ�����ʽ
        //Cursor.SetCursor(normalCursor, hotspot, CursorMode.Auto);
    }

    //void Update()
    //{
    //    // �������Ƿ���
    //    if (Input.GetMouseButtonDown(0)) // ����������
    //    {
    //        Cursor.SetCursor(clickCursor, hotspot, CursorMode.Auto);
    //    }
    //    else if (Input.GetMouseButtonUp(0)) // ������̧��
    //    {
    //        Cursor.SetCursor(normalCursor, hotspot, CursorMode.Auto);
    //    }
    //}
}

