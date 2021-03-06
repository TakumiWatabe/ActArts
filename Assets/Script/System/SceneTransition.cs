﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
//using UnityEditor;
public class SceneTransition : MonoBehaviour {
    SceneManagement scene;
    FadeScript fade;
    CancelScript decision;
    //CharacterSelect select;
    //CharacterSelect select2;
    DataRetention datare;
    CancelScript enter;
    PlayMenuSystem sys;
    MenuEvent menu;
    bool p1 = true;
    bool p2 = true;
    bool sceneFlagMenu = false;
    bool fadeFlag = true;
    bool sceneFlag = true;
    int state = 0;
    AudioSource audio;

    [SerializeField]
    AudioClip dicideSE;
    [SerializeField]
    AudioClip cancelSE;

    // Use this for initialization
    void Start () {
        if(this.GetComponent<AudioSource>() != null) audio = this.GetComponent<AudioSource>();
        scene = this.GetComponent<SceneManagement>();
        fade = GameObject.Find("FadePanel").GetComponent<FadeScript>();
        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            menu = GameObject.Find("SelectSceneObj").GetComponent<MenuEvent>();
            enter = GameObject.Find("SelectSceneObj").GetComponent<CancelScript>();
            //select = GameObject.Find("P1Image").GetComponent<CharacterSelect>();
            //select2 = GameObject.Find("P2Image").GetComponent<CharacterSelect>();
        }
        if (SceneManager.GetActiveScene().name == "PlayMenuScene")
            datare = GameObject.Find("GameSystem").GetComponent<DataRetention>();
        if (SceneManager.GetActiveScene().name == "PlayScene" || SceneManager.GetActiveScene().name == "PlayMenuScene")
        {
            sys = GameObject.Find("GameSystem").GetComponent<PlayMenuSystem>();
        }
        if (SceneManager.GetActiveScene().name == "PlayMenuScene")
        {
            sys = GameObject.Find("PlayMenuSystemObj").GetComponent<PlayMenuSystem>();
        }
        //Debug.Log("sys:" + sys);
        //Debug.Log("datare:" + datare);
        sceneFlagMenu = false;
    }

    // Update is called once per frame
    void Update () {
        // Escapeキーでゲームを終了する
        if (Input.GetKey(KeyCode.Escape))
        {
//#if UNITY_EDITOR
//            EditorApplication.isPlaying = false;
//#elif UNITY_STANDALONE
//                Application.Quit();
//#endif
            //AIの初期化
        }
        //====================================================================================================
        // タイトルシーンでの処理
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            if (sceneFlag)
            {
                if (Input.anyKeyDown)
                {

                    sceneFlag = false;
                    audio.Stop();
                    audio.PlayOneShot(dicideSE, 1.0f);
                    fade.FadeOutFlag();
                }
            }
            if (fade.GetAlpha() >= 1.0f)
            {
                scene.SceneChange("menu");
            }
        }
        //====================================================================================================
        // キャラクターセレクトシーンでの処理
        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            if(enter.GetEnterFlag()==false)
            {
                fade.FadeOutFlag();
                if (fade.GetAlpha() >= 1.0f)
                {
                    scene.SceneChange("play");
                }
            }
            //if (select.GetP1Frag() == false && select2.GetP2Frag() == false)
            //{
            //    fade.FadeOutFlag();
            //    if (fade.GetAlpha() >= 1.0f)
            //    {
            //        scene.SceneChange("play");
            //    }
            //}
            if (menu.GetSceneFlag())
            {
                if (!fadeFlag)
                {
                    fade.FadeOutFlag();
                }
                if (fade.GetAlpha() >= 1.0f)
                {
                    scene.SceneChange("menu");
                }
            }
        }
        //====================================================================================================
        //プレイメニューシーンでの処理
        if (SceneManager.GetActiveScene().name == "PlayMenuScene")
        {
            if (Input.GetButtonDown("AButton") && sceneFlagMenu == false)
            {
                fade.FadeOutFlag();
                audio.PlayOneShot(dicideSE, 1.0f);
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
        //====================================================================================================
        // タイトルシーン以外での処理
        if (SceneManager.GetActiveScene().name != "TitleScene")
        {
            if (fade.GetAlpha() >= 1.0f && fadeFlag == true)
            {
                fade.FadeInFlag();
                fadeFlag = false;
            }
            if(fadeFlag)
            {
                if (fade.GetAlpha() >= 1.0f)
                {
                    fade.FadeInFlag();
                    fadeFlag = false;
                }
            }
        }
    }
    public void FadeEvent(string name)
    {
        if (!fadeFlag)
        {
            fade.FadeOutFlag();
        }
        if (fade.GetAlpha() >= 1.0f)
        {
            scene.SceneChange(name);

        }
    }
}
