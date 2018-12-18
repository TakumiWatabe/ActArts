using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;
using System;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    public int controller = 0;

    static int pvcController = 0;


    static int pvcCount = 0;
    [SerializeField]
    int pvcTime = 50;

    //　アイコンが1秒間に何ピクセル移動するか
    [SerializeField]
    private float iconSpeed = Screen.width;
    //コントローラーの名前
    [SerializeField]
    public string controllerName = "";
    [SerializeField]
    GameObject aoiModel;
    [SerializeField]
    GameObject hikariModel;
    [SerializeField]
    GameObject aoiModel2;
    [SerializeField]
    GameObject hikariModel2;
    float modelPosX = 7.5f;
    float modelPosY = -3.5f;
    //　アイコンのサイズ取得で使用
    private RectTransform rect;
    DataRetention gameData;
    //　アイコンが画面内に収まる為のオフセット値
    private Vector2 offset;
    private GamepadState state;
    private GamepadState state1;
    private GamepadState state2;
    private Vector2 pos;
    private string charName = "";
    private bool controlFlag1P;
    private bool controlFlag2P;
    private SceneManagement scene;
    private bool sceneFlag1 = true;
    private bool sceneFlag2 = true;
    Vector2[] framePos = new Vector2[7]
    {
        new Vector2( -70.0f, 140.0f),
        new Vector2(-140.0f,   0.0f),
        new Vector2( -70.0f,-140.0f),
        new Vector2(  70.0f, 140.0f),
        new Vector2( 140.0f,   0.0f),
        new Vector2(  70.0f,-140.0f),
        new Vector2(   0.0f,   0.0f)
    };

    private string selectName = null;
    private GameObject player_One;
    private GameObject player_Two;
    private MenuEvent menuFlag;
    void Start()
    {
        gameData = GameObject.Find("GameSystem").GetComponent<DataRetention>();
        sceneFlag1 = true;
        sceneFlag2 = true;
        controlFlag1P = true;
        controlFlag2P = true;
        rect = GetComponent<RectTransform>();
        menuFlag = GameObject.Find("SelectSceneObj").GetComponent<MenuEvent>();
        player_One = GameObject.FindGameObjectWithTag("icon1");
        player_Two = GameObject.FindGameObjectWithTag("icon2");
        //　オフセット値をアイコンのサイズの半分で設定
        offset = new Vector2(rect.sizeDelta.x / 2f, rect.sizeDelta.y / 2f);

        if (controller > 0) controllerName = Input.GetJoystickNames()[controller - 1];

        pvcController = 0;
    }

    void Update()
    {
        // PvPモードの時
        if (gameData.Mode == 0)
        {
            if (controllerName == "Arcade Stick (MadCatz FightStick Neo)")
            {
                if (controller == 1)
                {
                    if (controlFlag1P)
                    {
                        // "B"ボタンを押したとき
                        if (Input.GetButtonDown("AButton"))
                        {
                            controlFlag1P = false;
                            CreateModel("player1");
                            sceneFlag1 = false;
                        }
                        if(menuFlag.GetMenuFlag()==false)
                        {
                            // アイコンの移動処理
                            IconMove(controllerName, GamePad.Index.One, player_One);
                        }
                    }
                }
                else
                {
                    if (controlFlag2P)
                    {
                        // "B"ボタンを押したとき
                        if (Input.GetButtonDown("AButton2"))
                        {
                            controlFlag1P = false;
                            CreateModel("player2");
                            sceneFlag2 = false;
                        }
                        if (menuFlag.GetMenuFlag() == false)
                        {
                            // アイコンの移動処理
                            IconMove(controllerName, GamePad.Index.Two, player_Two);
                        }
                    }
                }
            }
            else
            {
                if (controller == 1)
                {
                    if (controlFlag1P)
                    {
                        // "B"ボタンを押したとき
                        if (Input.GetButtonDown("AButton"))
                        {
                            controlFlag1P = false;
                            CreateModel("player1");
                            sceneFlag1 = false;
                        }
                        if (menuFlag.GetMenuFlag() == false)
                        {
                            // アイコンの移動処理
                            IconMove(controllerName, GamePad.Index.One, player_One);
                        }
                    }
                }
                else if (controller == 2)
                {

                    if (controlFlag2P)
                    {
                        // "B"ボタンを押したとき
                        if (Input.GetButtonDown("AButton2"))
                        {

                            controlFlag2P = false;
                            CreateModel("player2");
                            sceneFlag2 = false;
                        }
                        if (menuFlag.GetMenuFlag() == false)
                        {
                            // アイコンの移動処理
                            IconMove(controllerName, GamePad.Index.Two, player_Two);
                        }

                    }
                }
            }

        }
        // PvCモードの時
        else if (gameData.Mode == 1)
        {
            if (controllerName == "Arcade Stick (MadCatz FightStick Neo)")
            {
                if (pvcController == 0)
                {
                    if (controlFlag1P)
                    {
                        if (Input.GetButtonDown("AButton"))
                        {
                            controlFlag1P = false;
                            CreateModel("player1");
                            pvcCount = 0;
                        }

                        // アイコンの移動処理
                        IconMove(controllerName, GamePad.Index.One, player_One);
                    }
                }
                else if (pvcController == 1)
                {
                    pvcCount++;
                    if (controlFlag2P && pvcTime < pvcCount)
                    {

                        if (Input.GetButtonDown("AButton"))
                        {

                            controlFlag2P = false;
                            CreateModel("player2");

                        }
                        // アイコンの移動処理
                        IconMove(controllerName, GamePad.Index.One, player_Two);
                    }
                }
            }
            else
            {
                if (pvcController == 0 && controller == 1)
                {
                    if (controlFlag1P)
                    {
                        if (Input.GetButtonDown("AButton"))
                        {
                            controlFlag1P = false;
                            CreateModel("player1");
                            pvcController = 1;
                        }

                        // アイコンの移動処理
                        IconMove(controllerName, GamePad.Index.One, player_One);
                    }
                    pvcCount = 0;

                }
                else if (pvcController == 1)
                {
                    if(sceneFlag1)
                    {
                        pos = new Vector2(0f, 0f);
                        sceneFlag1 = false;
                    }
                    if (controlFlag2P)
                    {
                        if (Input.GetButtonDown("AButton") && controller == -1)
                        {

                            controlFlag2P = false;
                            CreateModel("player2");

                        }

                        // アイコンの移動処理
                        IconMove(controllerName, GamePad.Index.One, player_Two);
                    }
                    else
                    {
                        sceneFlag1 = false;
                        sceneFlag2 = false;
                    }
                }
                if (controller == 2 && pvcController == 1)
                {
                    controller = -1;
                }
            }

        }

        if (Input.GetButtonDown("AButton"))
        {
            if (charName != "None" && selectName == null)
            {
                selectName = charName;
            }
        }
    }
    public Vector2 GetFramePos(string name)
    {
        Vector2 pos = new Vector2(0, 0);
        switch (name)
        {
            case "char1":
                pos = framePos[0];
                charName = "Aoi";
                break;
            case "char2":
                pos = framePos[1];
                charName = "None";
                break;
            case "char3":
                pos = framePos[2];
                charName = "None";
                break;
            case "char4":
                pos = framePos[3];
                charName = "Hikari";
                break;
            case "char5":
                pos = framePos[4];
                charName = "None";
                break;
            case "char6":
                pos = framePos[5];
                charName = "None";
                break;
            case "random":
                pos = framePos[6];
                charName = "None";
                break;
        }
        return pos;
    }
    public string GetCharName()
    {
        return charName;
    }
    public bool GetP1Frag()
    {
        return sceneFlag1;
    }
    public bool GetP2Frag()
    {
        return sceneFlag2;
    }
    public string GetSelectName { get { return selectName; } }
    /// <summary>
    /// 選んだキャラクターのモデルを生成
    /// </summary>
    /// <param name="player"></param>
    public void CreateModel(string player)
    {
        // プレイヤー1が選んだキャラを生成
        if (player == "player1")
        {
            if (GetCharName() == "Aoi")
            {
                aoiModel.transform.position = new Vector3(-modelPosX, modelPosY, 0);
                Instantiate(aoiModel);
                pvcController = 1;
            }
            else if (GetCharName() == "Hikari")
            {
                Instantiate(hikariModel);
                hikariModel2.transform.position = new Vector3(-modelPosX, modelPosY, 0);
                pvcController = 1;
            }
            else
            {
                pvcController = 0;
                controlFlag1P = true;
            }
        }
        // プレイヤー2が選んだキャラを生成
        else if (player == "player2")
        {
            if (GetCharName() == "Aoi")
            {
                aoiModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                Instantiate(aoiModel2);
            }
            else if (GetCharName() == "Hikari")
            {
                hikariModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                Instantiate(hikariModel2);
            }
            else
            {
                controlFlag2P = true;
            }
        }
    }
    public void IconMove(string conName,GamePad.Index num,GameObject player)
    {
        if(conName== "Arcade Stick (MadCatz FightStick Neo)")
        {
            //　移動キーを押していなければ何もしない
            if (GamePad.GetAxis(GamePad.Axis.LeftStick, num).x == 0.0f && GamePad.GetAxis(GamePad.Axis.LeftStick, num).y == 0.0f)
            {
                return;
            }
            //　移動先を計算
            pos += new Vector2(GamePad.GetAxis(GamePad.Axis.LeftStick, num).x * iconSpeed, GamePad.GetAxis(GamePad.Axis.LeftStick, num).y * iconSpeed * -1) * Time.deltaTime;
            //　アイコン位置を設定
            player.transform.localPosition = pos;
        }
        else
        {
            //　移動キーを押していなければ何もしない
            if (GamePad.GetAxis(GamePad.Axis.LeftStick, num).x == 0.0f && GamePad.GetAxis(GamePad.Axis.LeftStick, num).y == 0.0f)
            {
                return;
            }
            //　移動先を計算
            pos += new Vector2(GamePad.GetAxis(GamePad.Axis.LeftStick, num).x * iconSpeed, GamePad.GetAxis(GamePad.Axis.LeftStick, num).y * iconSpeed) * Time.deltaTime;
            //　アイコン位置を設定
            player.transform.localPosition = pos;
        }
    }
}