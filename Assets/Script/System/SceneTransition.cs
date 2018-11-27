using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour {
    SceneManagement scene;
    FadeScript fade;
    CharacterSelect select;
    CharacterSelect select2;
    DataRetention datare;

    PlayMenuSystem sys;

    bool p1 = true;
    bool p2 = true;
    bool sceneFlagMenu = false;

    int state = 0;

	// Use this for initialization
	void Start () {
        scene = this.GetComponent<SceneManagement>();
        fade = GameObject.Find("FadePanel").GetComponent<FadeScript>();
        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            select = GameObject.Find("P1Image").GetComponent<CharacterSelect>();
            select2 = GameObject.Find("P2Image").GetComponent<CharacterSelect>();
        }
        datare = GameObject.Find("GameSystem").GetComponent<DataRetention>();
        sys = GameObject.Find("PlayMenuSystemObj").GetComponent<PlayMenuSystem>();

        sceneFlagMenu = false;
    }

    // Update is called once per frame
    void Update () {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            if(Input.anyKeyDown)
            {
                fade.FadeOutFlag();
            }
            if (fade.GetAlpha() >= 1.0f)
            {
                scene.SceneChange("menu");
            }
        }
        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            if (select.GetP1Frag() == false && select2.GetP2Frag() == false)
            {
                fade.FadeOutFlag();
                if (fade.GetAlpha() >= 1.0f)
                {
                    scene.SceneChange("play");
                }

            }
        }

        if (SceneManager.GetActiveScene().name == "PlayMenuScene")
        {
            if (Input.GetButtonDown("AButton"))
            {
                fade.FadeOutFlag();
                sceneFlagMenu = true;
                //float a = fade.GetAlpha();
            }
            if (fade.GetAlpha() >= 1.0f && sceneFlagMenu)
            {
                datare.Mode = sys.menuType;
                sceneFlagMenu = false;
                scene.SceneChange("select");
            }
        }

        if (SceneManager.GetActiveScene().name != "TitleScene")
        {
            if (fade.GetAlpha() >= 1.0f)
            {
                fade.FadeInFlag();
            }
        }
    }
}
