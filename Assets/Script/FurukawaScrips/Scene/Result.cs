using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour {

    //リザルトシーンうぃ行う処理のスクリプト
    //シーンマネージャー
    [SerializeField]
    private GameObject manager;

    private SceneMane SMana;
    private SceneFade SFade;

    private bool fade = false;

	// Use this for initialization
	void Start ()
    {
        SMana = manager.GetComponent<SceneMane>();
        SFade = manager.GetComponent<SceneFade>();

        SFade.ImageAlpha = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //フェードアウト
        SFade.FadeOut();

        //シーン遷移
        changeScene();
	}

    //決定ボタンを押したらシーン遷移する
    private void changeScene()
    {
        if(Input.GetButtonDown("AButton"))
        {
            fade = true;
            SFade.FFlag = true;
        }

        if(fade)
        {
            //フェードイン
            SFade.FadeIn();
            if(SFade.ImageAlpha==1)
            {
                SceneManager.LoadScene(SMana.Scenes("Title"));
            }
        }
    }
}
