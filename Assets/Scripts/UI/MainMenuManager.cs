using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
    {
    /// <summary>
    /// ��ȡ������
    /// </summary>
    /// <param name="sceneID"></param>
    public void LoadScene(int sceneID)
        {
        SceneManager.LoadScene(sceneID);
        Debug.Log("�����л��ɹ�");
        }
    /// <summary>
    /// �˳���Ϸ��
    /// </summary>
    public void ExitGame()
        {
        Application.Quit();
        }

    }
