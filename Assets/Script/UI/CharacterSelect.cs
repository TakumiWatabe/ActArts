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
    //選択時に表示するモデル
    private GameObject aoiModel;
    private GameObject hikariModel;
    private GameObject xionModel;
    private GameObject mariModel;
    private GameObject chloeModel;
    private GameObject shiroganeModel;

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
    private GameObject player1;
    private GameObject player2;
    private GameObject frame1;
    private GameObject frame2;
    private int rnd;
    private Vector2 iconPos1;
    private Vector2 iconPos2;
    private bool stayFlag;
    private CancelScript enter; 
    void Start()
    {
        //Resourcesからモデルを拝借
        aoiModel = (GameObject)Resources.Load("Model/Aoi");
        hikariModel = (GameObject)Resources.Load("Model/Hikari");
        xionModel = (GameObject)Resources.Load("Model/Xion");
        chloeModel = (GameObject)Resources.Load("Model/Chloe");
        shiroganeModel = (GameObject)Resources.Load("Model/Shirogane");
        mariModel = (GameObject)Resources.Load("Model/Mari");

        frame1 = GameObject.FindGameObjectWithTag("frame1");
        frame2 = GameObject.FindGameObjectWithTag("frame2");
        gameData = GameObject.Find("GameSystem").GetComponent<DataRetention>();
        sceneFlag1 = true;
        sceneFlag2 = true;
        controlFlag1P = true;
        controlFlag2P = true;
        stayFlag = true;
        rect = GetComponent<RectTransform>();
        menuFlag = GameObject.Find("SelectSceneObj").GetComponent<MenuEvent>();
        enter = GameObject.Find("SelectSceneObj").GetComponent<CancelScript>();
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
            // アケコン
            if (controllerName == "Arcade Stick (MadCatz FightStick Neo)")
            {
                PVPcontrol("Arcade");
            }
            // XBOXコントローラ
            else
            {
                PVPcontrol("Xbox");
            }
        }
//=====================================================================================================================
        // PvCモードの時
        else if (gameData.Mode == 1)
        {
            if (controllerName == "Arcade Stick (MadCatz FightStick Neo)")
            {
                PVCcontrol("Arcade");
            }
            else
            {
                PVCcontrol("Xbox");
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
    /// <summary>
    /// フレームの座標を取得
    /// </summary>
    /// <param name="name">Characterの名前</param>
    /// <returns></returns>
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
                charName = "Hikari";
                break;
            case "char3":
                pos = framePos[2];
                charName = "Shirogane";
                break;
            case "char4":
                pos = framePos[3];
                charName = "Xion";
                break;
            case "char5":
                pos = framePos[4];
                charName = "Chloe";
                break;
            case "char6":
                pos = framePos[5];
                charName = "Mari";
                break;
            case "random":
                pos = framePos[6];
                charName = "Random";
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
            if (GetCharName() == "Random")
            {
                rnd = UnityEngine.Random.Range(0, 6);
                player_One.transform.localPosition = frame1.transform.localPosition = RandomChar(rnd);
                ChangeName(rnd);
            }
            //else
            //{
            //    pvcController = 0;
            //    controlFlag1P = true;
            //}
            player1 = GenerateChar(player1);
            player1.transform.localPosition = new Vector3(-1.5f, 0.3f, -8f);
            player1.transform.localScale = new Vector3(1f, 1f, 1f);
            player1.name = "player1";
            gameData.fighterName[0] = GetCharName();
        }
        // プレイヤー2が選んだキャラを生成
        else if (player == "player2")
        {
            if (GetCharName() == "Random")
            {
                rnd = UnityEngine.Random.Range(0, 6);
                player_Two.transform.localPosition = frame2.transform.localPosition = RandomChar(rnd);
                ChangeName(rnd);
            }
            player2 = GenerateChar(player2);
            player2.transform.localPosition = new Vector3(1.5f, 0.3f, -8f);
            player2.transform.localScale = new Vector3(1f, 1f, -1f);
            player2.name = "player2";
            gameData.fighterName[1] = GetCharName();
        }
    }
    public void IconMove(string conName,GamePad.Index num,GameObject player,string name)
    {
        if (name == "Arcade")
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
            Debug.Log("Arcade");
        }
        else if (name == "Xbox")
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
            Debug.Log("Xbox");

        }
    }
    public GameObject GenerateChar(GameObject obj)
    {


        if (GetCharName() == "Aoi")
        {
            obj = Instantiate(aoiModel);
        }
        else if (GetCharName() == "Hikari")
        {
            obj = Instantiate(hikariModel);
        }
        else if (GetCharName() == "Xion")
        {
            obj = Instantiate(xionModel);
        }
        else if (GetCharName() == "Mari")
        {
            obj = Instantiate(mariModel);
        }
        else if (GetCharName() == "Chloe")
        {
            obj = Instantiate(chloeModel);
        }
        else if (GetCharName() == "Shirogane")
        {
            obj = Instantiate(shiroganeModel);
        }
        return obj;
    }
    public Vector2 RandomChar(int rnd)
    {
        Vector2 pos = framePos[rnd];
        return pos;
    }
    public void ChangeName(int num)
    {
        switch(num)
        {
            case 0:
                charName = "Aoi";
                break;
            case 1:
                charName = "Hikari";
                break;
            case 2:
                charName = "Shirogane";
                break;
            case 3:
                charName = "Xion";
                break;
            case 4:
                charName = "Chloe";
                break;
            case 5:
                charName = "Mari";
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// PVCのコントローラーの操作
    /// </summary>
    /// <param name="name"></param>
    public void PVCcontrol(string name)
    {
        if (pvcController == 0 && controller == 1)
        {
            if (controlFlag1P)
            {
                // アイコンの移動処理
                if (menuFlag.GetMenuFlag() == false)
                {
                    if (Input.GetButtonDown("AButton"))
                    {
                        controlFlag1P = false;
                        CreateModel("player1");
                        pvcController = 1;
                        // 現在の座標を保存
                        iconPos1 = player_One.transform.localPosition;
                        iconPos2 = player_Two.transform.localPosition;
                    }
                    IconMove(controllerName, GamePad.Index.One, player_One, name);
                }
            }
            pvcCount = 0;

        }
        else if (pvcController == 1)
        {
            if (sceneFlag1)
            {
                pos = iconPos2;
                sceneFlag1 = false;
            }
            if (controlFlag2P)
            {
                if (menuFlag.GetMenuFlag() == false)
                {
                    // アイコンの移動処理
                    if (menuFlag.GetMenuFlag() == false)
                    {
                        if (Input.GetButtonDown("AButton") && controller == -1)
                        {
                            controlFlag2P = false;
                            CreateModel("player2");
                        }
                        // アイコンの移動処理
                        IconMove(controllerName, GamePad.Index.One, player_Two, name);
                    }
                }
            }
            else
            {
                sceneFlag1 = false;
                sceneFlag2 = false;
            }
        }
        if (enter.GetEnterFlag() != false)
        {
            if (!controlFlag2P)
            {

                if (GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.One))
                {
                    sceneFlag2 = true;
                    controlFlag2P = true;
                    pvcController = 1;
                    if (pos != iconPos2)
                        pos = iconPos2;
                    Destroy(GameObject.Find("player2"));
                }
            }
            else if (!controlFlag1P)
            {
                if (GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.One))
                {
                    sceneFlag1 = true;
                    controlFlag1P = true;
                    pvcController = 0;
                    if (pos != iconPos1)
                        pos = iconPos1;
                    Destroy(GameObject.Find("player1"));
                }
            }

        }

        if (controller == 2 && pvcController == 1)
        {
            controller = -1;
        }

    }
    /// <summary>
    /// PVPのコントローラーの操作
    /// </summary>
    /// <param name="name"></param>
    public void PVPcontrol(string name)
    {
        if (controller == 1)
        {
            if (controlFlag1P)
            {
                if (menuFlag.GetMenuFlag() == false)
                {
                    // "A"ボタンを押したとき
                    if (Input.GetButtonDown("AButton"))
                    {
                        controlFlag1P = false;
                        CreateModel("player1");
                        sceneFlag1 = false;
                        // 現在の座標を保存
                        iconPos1 = player_One.transform.localPosition;

                    }
                    // アイコンの移動処理
                    IconMove(controllerName, GamePad.Index.One, player_One, name);
                }
            }
        }
        else if (controller == 2)
        {

            if (controlFlag2P)
            {
                if (menuFlag.GetMenuFlag() == false)
                {
                    // "A"ボタンを押したとき
                    if (Input.GetButtonDown("AButton2"))
                    {
                        controlFlag2P = false;
                        CreateModel("player2");
                        sceneFlag2 = false;
                        // 現在の座標を保存
                        iconPos2 = player_Two.transform.localPosition;

                    }
                    // アイコンの移動処理
                    IconMove(controllerName, GamePad.Index.Two, player_Two, name);
                }

            }
        }
        if (enter.GetEnterFlag() != false)
        {
            if (!controlFlag1P)
            {
                // プレイヤー1のキャンセル
                if (GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.One))
                {
                    sceneFlag1 = true;
                    controlFlag1P = true;
                    Destroy(GameObject.Find("player1"));
                    if (pos != iconPos1)
                        pos = iconPos1;
                }
            }
            if (!controlFlag2P)
            {
                // プレイヤー2のキャンセル
                if (GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.Two))
                {
                    sceneFlag2 = true;
                    controlFlag2P = true;
                    if (pos != iconPos2)
                        pos = iconPos2;
                    Destroy(GameObject.Find("player2"));
                }
            }
        }
    }
}