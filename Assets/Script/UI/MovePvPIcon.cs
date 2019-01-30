using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;
using System;
using UnityEngine.SceneManagement;

public class MovePvPIcon : MonoBehaviour {
    public const int SCREEN_TOP = 385;
    public const int SCREEN_SIDE = 210;

    [SerializeField]
    public int controller = 0;

    static int pvcController = 0;
    //385,210

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
    private GameObject aoiModel2;
    private GameObject hikariModel2;
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
    private CharacterSelect select;

    // Use this for initialization
    void Start () {
        // prefabから生成
        aoiModel = (GameObject)Resources.Load("Model/Aoi");
        aoiModel2 = (GameObject)Resources.Load("Model/Aoi2");
        hikariModel = (GameObject)Resources.Load("Model/Hikari");
        hikariModel2 = (GameObject)Resources.Load("Model/Hikari2");
        gameData = GameObject.Find("GameSystem").GetComponent<DataRetention>();
        sceneFlag1 = true;
        sceneFlag2 = true;
        controlFlag1P = true;
        controlFlag2P = true;
        rect = GetComponent<RectTransform>();
        //　オフセット値をアイコンのサイズの半分で設定
        offset = new Vector2(rect.sizeDelta.x / 2f, rect.sizeDelta.y / 2f);

        if (controller > 0) controllerName = Input.GetJoystickNames()[controller - 1];

        pvcController = 0;

    }

    // Update is called once per frame
    void Update () {

    }
    public void ArcadeControlIcon_PVP(int num)
    {
        //アケコン
        if (num == 1)
        {
            if (SceneManager.GetActiveScene().name == "SelectScene")
            {
                if (controlFlag1P)
                {
                    if (Input.GetButtonDown("AButton"))
                    {
                        controlFlag1P = false;
                        
                        if (select.GetCharName() == "Aoi")
                        {
                            aoiModel.transform.position = new Vector3(-modelPosX, modelPosY, 0);
                            Instantiate(aoiModel);
                        }
                        else if (select.GetCharName() == "Hikari")
                        {
                            hikariModel2.transform.position = new Vector3(-modelPosX, modelPosY, 0);
                            Instantiate(hikariModel);
                        }
                        else
                        {
                            controlFlag1P = true;
                        }
                    }
                    //　移動キーを押していなければ何もしない
                    if (GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x == 0.0f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y == 0.0f)
                    {
                        return;
                    }
                    //　移動先を計算
                    pos += new Vector2(GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x * iconSpeed, GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y * iconSpeed) * Time.deltaTime;
                    //　アイコン位置を設定
                    transform.localPosition = pos;
                }
                else
                {
                    sceneFlag1 = false;
                }

            }
        }
        else
        {
            if (controlFlag2P)
            {
                if (Input.GetButtonDown("AButton2"))
                {

                    controlFlag2P = false;
                    
                    if (select.GetCharName() == "Aoi")
                    {
                        aoiModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                        Instantiate(aoiModel2);
                        Debug.Log("2PAOI");
                        controlFlag2P = false;
                    }
                    else if (select.GetCharName() == "Hikari")
                    {
                        hikariModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                        Instantiate(hikariModel2);
                        Debug.Log("2PHIAKRI");
                        controlFlag2P = false;
                        Debug.Log(controlFlag2P);
                    }
                    else
                    {
                        controlFlag2P = true;
                    }
                }

                //　移動キーを押していなければ何もしない
                if (GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x == 0.0f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y == 0.0f)
                {
                    return;
                }
                //　移動先を計算
                pos += new Vector2(GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x * iconSpeed, GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y * iconSpeed) * Time.deltaTime;
                //　アイコン位置を設定
                transform.localPosition = pos;
            }
            else
            {
                sceneFlag2 = false;
            }
        }
    }

    public void XBoxControlIcon_PVP(int num)
    {
        if (num == 1)
        {
            if (controlFlag1P)
            {
                if (Input.GetButtonDown("AButton"))
                {
                    controlFlag1P = false;
                    //select.CreateModel("player1");
                }

                //　移動キーを押していなければ何もしない
                if (GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x == 0.0f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y == 0.0f)
                {
                    return;
                }
                //　移動先を計算
                pos += new Vector2(GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x * iconSpeed, GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y * iconSpeed) * Time.deltaTime;
                //　アイコン位置を設定
                transform.localPosition = pos;
            }
            else
            {
                sceneFlag1 = false;
            }

        }
        else if (num == 2)
        {
            if (controlFlag2P)
            {
                if (Input.GetButtonDown("AButton2"))
                {

                    controlFlag2P = false;
                    //select.CreateModel("player2");
                    //if (GetCharName() == "Aoi")
                    //{
                    //    aoiModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                    //    Instantiate(aoiModel2);
                    //    Debug.Log("2PAOI");
                    //    controlFlag2P = false;
                    //}
                    //else if (GetCharName() == "Hikari")
                    //{
                    //    hikariModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                    //    Instantiate(hikariModel2);
                    //    Debug.Log("2PHIAKRI");
                    //    controlFlag2P = false;
                    //    Debug.Log(controlFlag2P);
                    //}
                    //else
                    //{
                    //    controlFlag2P = true;
                    //}

                }

                //　移動キーを押していなければ何もしない
                if (GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x == 0.0f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y == 0.0f)
                {
                    return;
                }
                //　移動先を計算
                pos += new Vector2(GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x * iconSpeed, GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y * iconSpeed) * Time.deltaTime;
                //　アイコン位置を設定
                transform.localPosition = pos;
            }
            else
            {
                sceneFlag2 = false;
            }
        }

    }
}
