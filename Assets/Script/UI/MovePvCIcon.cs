using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;
using System;
using UnityEngine.SceneManagement;

public class MovePvCIcon : MonoBehaviour
{
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
    void Start()
    {

        // prefabから生成
        aoiModel = (GameObject)Resources.Load("Model/Aoi");
        aoiModel2 = (GameObject)Resources.Load("Model/Aoi2");
        hikariModel = (GameObject)Resources.Load("Model/Hikari");
        hikariModel2 = (GameObject)Resources.Load("Model/Hikari2");

        select = gameObject.GetComponent<CharacterSelect>();
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
    void Update()
    {
    }
    public void ArcadeControlIcon_PVC(int cont)
    {
        //アケコン
        if (cont == 1)
        {
            if (SceneManager.GetActiveScene().name == "SelectScene")
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
                    pos += new Vector2(GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x * iconSpeed, GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y * iconSpeed * -1) * Time.deltaTime;
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
                    //select.CreateModel("player2");
                    pvcController = 2;
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
    public void XBoxControlIcon_PVC(int pvc,int cont)
    {
        if (pvc == 0 && cont == 1)
        {
            if (controlFlag1P)
            {
                if (Input.GetButtonDown("AButton"))
                {
                    controlFlag1P = false;
                    //select.CreateModel("player1");
                    pvcController = 1;
                    transform.GetComponent<Image>().enabled = false;
                }

                //　移動キーを押していなければ何もしない
                if (GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x >= -0.5f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y >= -0.5f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x <= 0.5f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y <= 0.5f)
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
                Debug.Log("sceneFlag1" + sceneFlag1);
            }
            pvcCount = 0;
        }
        else if (pvc == 1)
        {
            //transform.GetComponent<Image>().enabled = false;
            sceneFlag1 = false;
            if (controlFlag2P)
            {

                if (Input.GetButtonDown("AButton") && cont == -1)
                {
                    controlFlag2P = false;
                    //select.CreateModel("player2");
                    pvcController = 2;
                }

                //　移動キーを押していなければ何もしない
                if (GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x >= -0.5f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y >= -0.5f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x <= 0.5f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y <= 0.5f)
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
                sceneFlag2 = false;
            }
        }
        else if (pvc == 2)
        {
            sceneFlag1 = false;
            sceneFlag2 = false;
        }
        if (cont == 2 && pvc == 1)
        {
            controller = -1;
        }
    }
}

