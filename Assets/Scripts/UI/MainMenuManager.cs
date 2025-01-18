using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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

    public void StageSelect(int stage_id)
        {
        Data.stage = stage_id;
        LoadScene(1);//����Ĭ�Ϲؿ�Ϊscene1
        }

    }
