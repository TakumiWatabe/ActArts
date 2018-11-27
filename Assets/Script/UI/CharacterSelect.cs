using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;
using System;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour {
    [SerializeField]
    public int controller=0;

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

    void Start()
    {
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

    void Update()
    {
        if (gameData.Mode == 0)
        {
            if (controllerName == "Arcade Stick (MadCatz FightStick Neo)")
            {
                if (controller == 1)
                {
                    if (SceneManager.GetActiveScene().name == "SelectScene")
                    {
                        if (controlFlag1P)
                        {
                            if (Input.GetButtonDown("AButton"))
                            {
                                controlFlag1P = false;
                                if (GetCharName() == "Aoi")
                                {
                                    aoiModel.transform.position = new Vector3(-modelPosX, modelPosY, 0);
                                    Instantiate(aoiModel);
                                }
                                else if (GetCharName() == "Hikari")
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
                            if (Input.GetAxis("Horizontal") == 0.0f && Input.GetAxis("Vertical") == 0.0f)
                            {
                                return;
                            }
                            //　移動先を計算
                            pos += new Vector2(Input.GetAxis("Horizontal") * iconSpeed, Input.GetAxis("Vertical") * iconSpeed) * Time.deltaTime;
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
                            if (GetCharName() == "Aoi")
                            {
                                aoiModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                                Instantiate(aoiModel2);
                                Debug.Log("2PAOI");
                                controlFlag2P = false;
                            }
                            else if (GetCharName() == "Hikari")
                            {
                                hikariModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                                Instantiate(hikariModel2);
                                Debug.Log("2PHIAKRI");
                                controlFlag2P = false;
                                Debug.Log(controlFlag2P);
                            }
                            else//if (GetCharName() == "None")
                            {
                                controlFlag2P = true;
                                //Debug.Log("TURURUになったｙｐ");
                            }

                        }

                        //　移動キーを押していなければ何もしない
                        if (Input.GetAxis("Horizontal2") == 0.0f && Input.GetAxis("Vertical2") == 0.0f)
                        {
                            return;
                        }
                        //　移動先を計算
                        pos += new Vector2(Input.GetAxis("Horizontal2") * iconSpeed, Input.GetAxis("Vertical2") * iconSpeed) * Time.deltaTime;
                        //　アイコン位置を設定
                        transform.localPosition = pos;
                    }
                    else
                    {
                        sceneFlag2 = false;
                    }
                }
            }
            else
            {
                if (controller == 1)
                {
                    if (controlFlag1P)
                    {
                        if (Input.GetButtonDown("AButton"))
                        {
                            controlFlag1P = false;
                            if (GetCharName() == "Aoi")
                            {
                                aoiModel.transform.position = new Vector3(-modelPosX, modelPosY, 0);
                                Instantiate(aoiModel);
                            }
                            else if (GetCharName() == "Hikari")
                            {
                                hikariModel2.transform.position = new Vector3(-modelPosX, modelPosY, 0);
                                Instantiate(hikariModel);
                            }
                            else//if(GetCharName()== "None")
                            {
                                controlFlag1P = true;
                            }
                        }

                        //　移動キーを押していなければ何もしない
                        if (Input.GetAxis("Horizontal") >= -0.5f && Input.GetAxis("Vertical") >= -0.5f && Input.GetAxis("Horizontal") <= 0.5f && Input.GetAxis("Vertical") <= 0.5f)
                        {
                            return;
                        }
                        //　移動先を計算
                        pos += new Vector2(Input.GetAxis("Horizontal") * iconSpeed, -Input.GetAxis("Vertical") * iconSpeed) * Time.deltaTime;
                        //　アイコン位置を設定
                        transform.localPosition = pos;
                    }
                    else
                    {
                        sceneFlag1 = false;
                    }

                }
                else if (controller == 2)
                {
                    
                    if (controlFlag2P)
                    {
                        if (Input.GetButtonDown("AButton2"))
                        {

                            controlFlag2P = false;
                            if (GetCharName() == "Aoi")
                            {
                                aoiModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                                Instantiate(aoiModel2);
                                Debug.Log("2PAOI");
                                controlFlag2P = false;
                            }
                            else if (GetCharName() == "Hikari")
                            {
                                hikariModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                                Instantiate(hikariModel2);
                                Debug.Log("2PHIAKRI");
                                controlFlag2P = false;
                                Debug.Log(controlFlag2P);
                            }
                            else//if (GetCharName() == "None")
                            {
                                controlFlag2P = true;
                                //Debug.Log("TURURUになったｙｐ");
                            }

                        }

                        //　移動キーを押していなければ何もしない
                        if (Input.GetAxis("Horizontal2") >= -0.5f && Input.GetAxis("Vertical2") >= -0.5f && Input.GetAxis("Horizontal2") <= 0.5f && Input.GetAxis("Vertical2") <= 0.5f)
                        {
                            return;
                        }
                        //　移動先を計算
                        pos += new Vector2(Input.GetAxis("Horizontal2") * iconSpeed, -Input.GetAxis("Vertical2") * iconSpeed) * Time.deltaTime;
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
                            if (GetCharName() == "Aoi")
                            {
                                aoiModel.transform.position = new Vector3(-modelPosX, modelPosY, 0);
                                Instantiate(aoiModel);
                                pvcController = 1;
                            }
                            else if (GetCharName() == "Hikari")
                            {
                                hikariModel2.transform.position = new Vector3(-modelPosX, modelPosY, 0);
                                Instantiate(hikariModel);
                                pvcController = 1;
                            }
                            else//if(GetCharName()== "None")
                            {
                                controlFlag1P = true;
                            }
                            pvcCount = 0;
                        }

                        //　移動キーを押していなければ何もしない
                        if (Input.GetAxis("Horizontal") == 0.0f && Input.GetAxis("Vertical") == 0.0f)
                        {
                            return;
                        }
                        //　移動先を計算
                        pos += new Vector2(Input.GetAxis("Horizontal") * iconSpeed, Input.GetAxis("Vertical") * iconSpeed) * Time.deltaTime;
                        //　アイコン位置を設定
                        transform.localPosition = pos;
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
                            if (GetCharName() == "Aoi")
                            {
                                aoiModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                                Instantiate(aoiModel2);
                                Debug.Log("2PAOI");
                                controlFlag2P = false;
                            }
                            else if (GetCharName() == "Hikari")
                            {
                                hikariModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                                Instantiate(hikariModel2);
                                Debug.Log("2PHIAKRI");
                                controlFlag2P = false;
                                Debug.Log(controlFlag2P);
                            }
                            else//if (GetCharName() == "None")
                            {
                                controlFlag2P = true;
                                //Debug.Log("TURURUになったｙｐ");
                            }

                        }

                        //　移動キーを押していなければ何もしない
                        if (Input.GetAxis("Horizontal2") == 0.0f && Input.GetAxis("Vertical2") == 0.0f)
                        {
                            return;
                        }
                        //　移動先を計算
                        pos += new Vector2(Input.GetAxis("Horizontal2") * iconSpeed, Input.GetAxis("Vertical2") * iconSpeed) * Time.deltaTime;
                        //　アイコン位置を設定
                        transform.localPosition = pos;
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
                            if (GetCharName() == "Aoi")
                            {
                                aoiModel.transform.position = new Vector3(-modelPosX, modelPosY, 0);
                                Instantiate(aoiModel);
                                pvcController = 1;
                                transform.GetComponent<Image>().enabled = false;
                                //enabled = false;
                                //if (controller == 2) transform.localPosition = new Vector2(0, 0);
                            }
                            else if (GetCharName() == "Hikari")
                            {
                                hikariModel2.transform.position = new Vector3(-modelPosX, modelPosY, 0);
                                Instantiate(hikariModel);
                                pvcController = 1;
                                transform.GetComponent<Image>().enabled = false;
                                //enabled = false;
                                //if (controller == 2) transform.localPosition = new Vector2(0, 0);

                            }
                            else//if(GetCharName()== "None")
                            {
                                controlFlag1P = true;
                            }
                        }

                        //　移動キーを押していなければ何もしない
                        if (Input.GetAxis("Horizontal") >= -0.5f && Input.GetAxis("Vertical") >= -0.5f && Input.GetAxis("Horizontal") <= 0.5f && Input.GetAxis("Vertical") <= 0.5f)
                        {
                            return;
                        }
                        //　移動先を計算
                        pos += new Vector2(Input.GetAxis("Horizontal") * iconSpeed, -Input.GetAxis("Vertical") * iconSpeed) * Time.deltaTime;
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
                else if (pvcController == 1)
                {
                    //transform.GetComponent<Image>().enabled = false;
                    sceneFlag1 = false;
                    if (controlFlag2P)
                    {

                        if (Input.GetButtonDown("AButton") && controller == -1)
                        {

                            controlFlag2P = false;
                            if (GetCharName() == "Aoi")
                            {
                                aoiModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                                Instantiate(aoiModel2);
                                //Debug.Log("2PAOI");
                                controlFlag2P = false;
                                pvcController = 2;
                            }
                            else if (GetCharName() == "Hikari")
                            {
                                hikariModel2.transform.position = new Vector3(modelPosX, modelPosY, 0);
                                Instantiate(hikariModel2);
                                //Debug.Log("2PHIAKRI");
                                controlFlag2P = false;
                                Debug.Log(controlFlag2P);
                                pvcController = 2;
                            }
                            else//if (GetCharName() == "None")
                            {
                                controlFlag2P = true;
                                //Debug.Log("TURURUになったｙｐ");
                            }

                        }

                        //　移動キーを押していなければ何もしない
                        if (Input.GetAxis("Horizontal") >= -0.5f && Input.GetAxis("Vertical") >= -0.5f && Input.GetAxis("Horizontal") <= 0.5f && Input.GetAxis("Vertical") <= 0.5f)
                        {
                            return;
                        }
                        //　移動先を計算
                        pos += new Vector2(Input.GetAxis("Horizontal") * iconSpeed, -Input.GetAxis("Vertical") * iconSpeed) * Time.deltaTime;
                        //　アイコン位置を設定
                        transform.localPosition = pos;
                    }
                    else
                    {
                        sceneFlag1 = false;
                        sceneFlag2 = false;
                    }
                }
                else if (pvcController == 2)
                {
                    sceneFlag1 = false;
                    sceneFlag2 = false;
                }
                if (controller == 2 && pvcController == 1)
                {
                    controller = -1;
                }
            }

        }

        if(Input.GetButtonDown("AButton"))
        {
            if (charName != "None" && selectName == null)
            {
                selectName = charName;
            }
        }

        //Debug.Log("sceneFlag1" + sceneFlag1 + "sceneFlag2" + sceneFlag2);
    }
    public Vector2 GetFramePos(string name)
    {
        Vector2 pos = new Vector2(0, 0);
        switch(name)
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
        //Debug.Log("1Pフラグ" + controlFlag1P);
        return sceneFlag1;
    }
    public bool GetP2Frag()
    {
        //Debug.Log("2Pフラグ" + controlFlag2P);
        return sceneFlag2;
    }

    public string GetSelectName { get { return selectName; } }
}
