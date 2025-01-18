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
        // ���ó�ʼ�����ʽ
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
        // �������Ƿ���
        if (Input.GetMouseButtonDown(0)) // ����������
        {
            if (clickCursor != null)
            {
                Cursor.SetCursor(clickCursor, hotspot, CursorMode.Auto);
            }
        }
        else if (Input.GetMouseButtonUp(0)) // ������̧��
        {
            if (normalCursor != null)
            {
                Cursor.SetCursor(normalCursor, hotspot, CursorMode.Auto);
            }
        }
    }
}
