using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;

public class SceneMane : MonoBehaviour
{
    //シーンに遷移する変数を設定するスクリプト

    private Dictionary<string, string> sceneName = new Dictionary<string, string>()
    {
        {"Title","TitleScene" },
        {"Menu","PlayMenuScene" },
        {"Select","SelectScene" },
        {"Play","PlayScene" },
        {"Result","ResultScene" },
    };

    void Update()
    {
        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    //エディターとアプリケーションを判別して動作する
        //    //※プラットフォーム依存コンパイル
        //    #if UNITY_EDITOR
        //        EditorApplication.isPlaying = false;
        //    #elif UNITY_STANDALONE
        //        Application.Quit();
        //    #endif
        //}
    }

    //シーンの名前を返す
    public string Scenes(string nextScene)
    {
        return sceneName[nextScene];
    }
}
