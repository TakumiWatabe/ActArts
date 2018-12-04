using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;

public class MenuEvent : MonoBehaviour {
    [SerializeField]
    GameObject returnMenu;
    [SerializeField]
    Image CursorIcon;
    [SerializeField]
    private int choiceMenu = 0;
    private SceneTransition sm;
    private string controllerName;
    private float oldStick = 0.0f;
    private float oldCuosorButton = 0.0f;
    private bool menuFlag;
    private bool sceneFlag;
    // Use this for initialization
    void Start () {
        controllerName = Input.GetJoystickNames()[0];
        if (controllerName != "Arcade Stick (MadCatz FightStick Neo)")
            controllerName = "";
        menuFlag = false;
        sceneFlag = false;
        sm = GameObject.Find("SceneManager").GetComponent<SceneTransition>();

    }

    // Update is called once per frame
    void Update () {
        if (returnMenu.activeSelf)
        {
            //戻るメニューを消す
            if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.One))
            {
                returnMenu.SetActive(false);
                menuFlag = false;
            }
        }
        else
        {
            //戻るメニューを開く
            if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.One))
            {
                returnMenu.SetActive(true);
                menuFlag = true;
            }
        }
        //メニューを開いていたら操作する
        if(menuFlag)
        {
            //XBOXコントローラーの時
            if (controllerName == "")
            {
                //上
                if ((GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y >= 1.0f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y != oldStick) ||
                    (GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y >= 1.0f && GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y != oldCuosorButton))
                {
                    choiceMenu--;
                    if (choiceMenu < 0)
                    {
                        choiceMenu = 1;
                        MoveImage(choiceMenu);
                    }
                }

                //下
                if ((GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y <= -1.0f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y != oldStick) ||
                    (GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y <= -1.0f && GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y != oldCuosorButton))
                {
                    choiceMenu++;
                    if (choiceMenu > 1)
                    {
                        choiceMenu = 0;
                        MoveImage(choiceMenu);
                    }
                }
                // 選んだメニューに応じたアクション
                if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One))
                {
                    if (choiceMenu == 0)
                    {
                        returnMenu.SetActive(false);
                        menuFlag = false;
                    }
                    else if (choiceMenu == 1)
                    {
                        sceneFlag = true;
                    }
                }
                
                //トリガー処理
                oldStick = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y;
                oldCuosorButton = GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y;
            }
            //アケコンの時
            if (controllerName == "Arcade Stick (MadCatz FightStick Neo)")
            {
                //上
                if ((GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y <= -1.0f &&
                    GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y != oldStick))
                {
                    choiceMenu--;
                    if (choiceMenu < 0)
                    {
                        choiceMenu = 1;
                        MoveImage(choiceMenu);
                    }
                }

                //下
                if ((GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y >= 1.0f &&
                    GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y != oldStick))
                {
                    choiceMenu++;
                    if (choiceMenu > 1)
                    {
                        choiceMenu = 0;
                        MoveImage(choiceMenu);
                    }
                }

                ////ボタンを押すとシーンチェンジ
                //if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.Two))
                //{
                //    dr.Mode = choiceMenu;
                //    sm.SceneChange("select");
                //}

                //トリガー処理
                oldStick = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y;
                oldCuosorButton = GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y;
            }
        }

    }
    void MoveImage(int num)
    {
        switch(num)
        {
            // 下
            case 0:
                CursorIcon.transform.localPosition = new Vector3(0, -55, 0);
                break;
            // 上
            case 1:
                CursorIcon.transform.localPosition = new Vector3(0, -5, 0);
                break;
        }
    }
    public bool GetMenuFlag()
    {
        return menuFlag;
    }
    public bool GetSceneFlag()
    {
        return sceneFlag;
    }
}
