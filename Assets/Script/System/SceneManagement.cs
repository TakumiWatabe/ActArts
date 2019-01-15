using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

    // タイトルシーン
    string titleScene = "TitleScene";
    // プレイシーン
    string playScene = "PlayScene";
    // キャラクターセレクトシーン
    string characterSelectScene = "SelectScene";
    // プレイメニューシーン
    string playMenuScene = "PlayMenuScene";
    // リザルトシーン
    string resultScene = "ResultScene";

    ////----------------------------------------------------------------------
    ////! @brief シーン遷移
    ////!
    ////! @param[in] シーン名
    ////!
    ////! @return なし
    ////----------------------------------------------------------------------
    public void SceneChange(string name)
    {
        string sceneName = "";
        switch (name)
        {
            case "title":
                sceneName = titleScene;
                break;
            case "menu":
                sceneName = playMenuScene;
                break;
            case "select":
                sceneName = characterSelectScene;
                break;
            case "play":
                sceneName = playScene;
                break;
            case "result":
                sceneName = resultScene;
                break;
        }

        SceneManager.LoadScene(sceneName);
    }
}



