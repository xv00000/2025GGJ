using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
    {
    /// <summary>
    /// 读取场景呗
    /// </summary>
    /// <param name="sceneID"></param>
    public void LoadScene(int sceneID)
        {
        SceneManager.LoadScene(sceneID);
        Debug.Log("场景切换成功");
        }
    /// <summary>
    /// 退出游戏呗
    /// </summary>
    public void ExitGame()
        {
        Application.Quit();
        }

    public void StageSelect(int stage_id)
        {
        Data.stage = stage_id;
        LoadScene(1);//这里默认关卡为scene1
        }

    }
