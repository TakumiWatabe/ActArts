using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class PlayerCommand : MonoBehaviour {

    //PlayerController
    PlayerController playerController;
    Animator animator;

    [SerializeField]
    GameObject hadokenObject;
    private GameObject hado;

    //コマンド入力の猶予
    public int commandCount = 20;
    //入力履歴の数
    public int numInputHistory = 10;
    //コントローラー番号
    [SerializeField]
    private int controller = 0;

    //コントローラーの名前
    [SerializeField]
    public string controllerName = "";

    //方向キー
    int inputDKey;
    int inputDKeyOld;

    //入力履歴
    public List<string> history = new List<string>();
    //入力履歴(画面表示用)
    List<string> inputHistory = new List<string>();

    //キーの定義
    float x = 0;
    float y = 0;

    bool punchKey = false;
    bool kickKey = false;

    int specialDirection;

    private PlaySEScript playSEScript;

    // Use this for initialization
    void Start () {
        playSEScript = GetComponent<PlaySEScript>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        //入力履歴の設定
        history.Clear();
        for (int i = 0; i < commandCount; i++)
        {
            history.Add("N");
        }

        inputHistory.Clear();
        for (int i = 0; i < numInputHistory; i++)
        {
            inputHistory.Add("");
        }

        inputDKey = 5;
        inputDKeyOld = inputDKey;

        //キャラクター設定
        switch (this.gameObject.tag)
        {
            case "P1":
                controller = 1;
                break;
            case "P2":
                controller = 2;
                break;
            default:
                break;
        }

        controllerName = Input.GetJoystickNames()[controller - 1];
    }

    public void Initialize()
    {

        //入力履歴の設定
        history.Clear();
        for (int i = 0; i < commandCount; i++)
        {
            history.Add("N");
        }

        inputHistory.Clear();
        for (int i = 0; i < numInputHistory; i++)
        {
            inputHistory.Add("");
        }

        inputDKey = 5;
        inputDKeyOld = inputDKey;

        //キャラクター設定
        switch (this.gameObject.tag)
        {
            case "P1":
                controller = 1;
                break;
            case "P2":
                controller = 2;
                break;
            default:
                break;
        }

        //controllerName = Input.GetJoystickNames()[controller - 1];
    }

    // Update is called once per frame
    void Update () {

        if (!playerController.CanControll) return;

        InputCommand();

        InputKeyHistory();

        SyoryukenCommand();

        HadokenCommand();

        inputDKeyOld = inputDKey;
    }

    /// <summary>
    /// コントローラの入力を入れる
    /// </summary>
    public void InputKey()
    {
        //キーの定義
        x = 0;
        y = 0;

        punchKey = false;
        kickKey = false;

        if (controller == 1)
        {
            // 右・左
            x = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x;
            x += GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).x * 1000;

            // 上・下
            y = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y * 1;
            y += GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y * 1000;

            //パンチ
            punchKey = GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One);
            //キック
            kickKey = GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.One);

            if (controllerName == "Arcade Stick (MadCatz FightStick Neo)")
            {
                // 上・下
                y = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y * 1 * -1;
                y += GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y * 1000 * -1;

                //パンチ
                punchKey = GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.One);
                //キック
                kickKey = GamePad.GetButtonDown(GamePad.Button.Y, GamePad.Index.One);
            }
        }
        else if (controller == 2)
        {
            // 右・左
            x = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Two).x;
            x += GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.Two).x * 1000;

            // 上・下
            y = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Two).y * 1;
            y += GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.Two).y * 1000;

            //パンチ
            punchKey = GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Two);
            //キック
            kickKey = GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.Two);

            if (controllerName == "Arcade Stick (MadCatz FightStick Neo)")
            {
                // 上・下
                y = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Two).y * 1 * -1;
                y += GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.Two).y * 1000 * -1;

                //パンチ
                punchKey = GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.Two);
                //キック
                kickKey = GamePad.GetButtonDown(GamePad.Button.Y, GamePad.Index.Two);
            }
        }
        else
        {
            // 右・左
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                x = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                x = 1;
            }

            // 上・下
            if (Input.GetKey(KeyCode.DownArrow))
            {
                y = -1;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                y = 1;
            }

            //パンチ
            punchKey = Input.GetKeyDown(KeyCode.Z);
            //キック
            kickKey = Input.GetKeyDown(KeyCode.X);
        }

        //取得
        if (playerController.Direction == 1)
        {
            if (x < -0.4f && y < -0.5f) inputDKey = 1;
            if (x < 0.4f && x > -0.4f && y < -0.5f) inputDKey = 2;
            if (x > 0.4f && y < -0.5f) inputDKey = 3;
            if (y < 0.4f && y > -0.5f && x < -0.4f) inputDKey = 4;
            if (x < 0.4f && x > -0.4f && y < 0.4f && y > -0.5f) inputDKey = 5;
            if (y < 0.4f && y > -0.5f && x > 0.4f) inputDKey = 6;
            if (x < -0.4f && y > 0.4f) inputDKey = 7;
            if (x < 0.4f && x > -0.4f && y > 0.4f) inputDKey = 8;
            if (x > 0.4f && y > 0.4f) inputDKey = 9;
        }
        else if (playerController.Direction == -1)
        {
            if (x < -0.4f && y < -0.5f) inputDKey = 3;
            if (x < 0.4f && x > -0.4f && y < -0.5f) inputDKey = 2;
            if (x > 0.4f && y < -0.5f) inputDKey = 1;
            if (y < 0.4f && y > -0.5f && x < -0.4f) inputDKey = 6;
            if (x < 0.4f && x > -0.4f && y < 0.4f && y > -0.5f) inputDKey = 5;
            if (y < 0.4f && y > -0.5f && x > 0.4f) inputDKey = 4;
            if (x < -0.4f && y > 0.4f) inputDKey = 9;
            if (x < 0.4f && x > -0.4f && y > 0.4f) inputDKey = 8;
            if (x > 0.4f && y > 0.4f) inputDKey = 7;
        }
    }

    /// <summary>
    /// コマンド入力
    /// </summary>
    void InputCommand()
    {
        //入力
        switch (inputDKey)
        {
            case 1:
                history.RemoveAt(0);
                history.Add("1");
                break;
            case 2:
                history.RemoveAt(0);
                history.Add("2");
                break;
            case 3:
                history.RemoveAt(0);
                history.Add("3");
                break;
            case 4:
                history.RemoveAt(0);
                history.Add("4");
                break;
            case 5:
                history.RemoveAt(0);
                history.Add("5");
                break;
            case 6:
                history.RemoveAt(0);
                history.Add("6");
                break;
            case 7:
                history.RemoveAt(0);
                history.Add("7");
                break;
            case 8:
                history.RemoveAt(0);
                history.Add("8");
                break;
            case 9:
                history.RemoveAt(0);
                history.Add("9");
                break;

        }

        if (punchKey)
        {
            history.RemoveAt(0);
            history.Add("P");
        }
        if (kickKey)
        {
            history.RemoveAt(0);
            history.Add("K");
        }

        for (int i = 0; i < commandCount; i++)
        {
            //text.text += history[i];
        }
    }

    /// <summary>
    /// 入力履歴
    /// </summary>
    void InputKeyHistory()
    {
        //入力履歴
        if (history[commandCount - 1] != "5" && history[commandCount - 1] != history[commandCount - 2])
        {
            inputHistory.RemoveAt(0);
            inputHistory.Add(history[commandCount - 1]);
        }
    }

    /// <summary>
    /// は同県コマンド
    /// </summary>
    void HadokenCommand()
    {
        //は同県
        if (playerController.State != "Special" && animator.GetInteger("Damage") == 0)
        {
            string[] hadoken = new string[4];
            hadoken[0] = "2";
            hadoken[1] = "3";
            hadoken[2] = "6";
            hadoken[3] = "P";

            int hadokenCount = 0;

            for (int i = 0; i < commandCount; i++)
            {
                if (hadoken[hadokenCount] == history[i])
                {
                    hadokenCount++;
                    if (hadokenCount > 3)
                    {
                        if (playerController.State != "Jump" && playerController.State != "Special")
                        {
                            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.HADOU);
                            playerController.SpecialState = "Hadoken";
                            playerController.State = "Special";
                            specialDirection = playerController.Direction;
                            //startSpecialPos = transform.position;
                            HistoryClear();

                            if (playerController.IsHadouCommandMissile)
                            {
                                Vector3 hadoPos = transform.position;
                                hadoPos.y += 0.5f;
                                hado = Instantiate(hadokenObject, hadoPos, Quaternion.identity);
                                //GameObject hado = Instantiate(hadokenObject, GetComponent<ColliderEvent>().GetHitBoxs[9].center + this.transform.parent.transform.position, Quaternion.identity);
                                if (playerController.Direction == 1) hado.transform.Rotate(0, 0, 0);
                                else hado.transform.Rotate(0, 180, 0);
                                hado.name = "HadoukenA";
                                hado.transform.SetParent(this.transform);

                                hado.GetComponent<HadouController>().direction = playerController.Direction;
                                //Instantiate(hadokenObject, GetComponent<ColliderEvent>().GetHitBoxs[9].center + this.transform.parent.transform.position, Quaternion.identity);

                            }
                        }
                        playerController.SetDirection();
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 昇竜拳コマンド
    /// </summary>
    void SyoryukenCommand()
    {
        //昇竜拳
        if (playerController.State != "Special" && animator.GetInteger("Damage") == 0)
        {
            string[] syoryu = new string[4];
            syoryu[0] = "6";
            syoryu[1] = "2";
            syoryu[2] = "3";
            syoryu[3] = "P";

            int syoryuCount = 0;

            for (int i = 0; i < commandCount; i++)
            {
                if (syoryu[syoryuCount] == history[i])
                {
                    syoryuCount++;
                    if (syoryuCount > 3)
                    {
                        if (playerController.State != "Jump" && playerController.State != "Special")
                        {
                            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.SYORYU);
                            playerController.SpecialState = "Syoryuken";
                            playerController.State = "Special";
                            specialDirection = playerController.Direction;
                            HistoryClear();
                            Debug.Log("昇龍拳");
                            playerController.SetDirection();
                            playerController.NowGravity = 0.0f;
                            //nowGravity = 0;
                            //break;
                        }
                        return;
                    }
                }
            }
        }
    }


    ///// <summary>
    ///// コマンド入力
    ///// </summary>
    ///// <param name="command">コマンド内容</param>
    ///// <param name="setSpecialState">成立時にどの技を出すか</param>
    //void Command(List<string> command, string setSpecialState)
    //{

    //    int commandNum = command.Count;

    //    if (playerController.State != "Special" && animator.GetInteger("Damage") == 0)
    //    {

    //        int commandEstablishmentCount = 0;

    //        for (int i = 0; i < commandCount; i++)
    //        {
    //            if (command[commandEstablishmentCount] == history[i])
    //            {
    //                commandEstablishmentCount++;
    //                if (commandEstablishmentCount > commandNum)
    //                {
    //                    if (playerController.State != "Jump" && playerController.State != "Special")
    //                    {
    //                        if (playerController.IsHadouCommandMissile)

    //                        {
    //                            hado = Instantiate(hadokenObject, transform.position, Quaternion.identity);
    //                            //GameObject hado = Instantiate(hadokenObject, GetComponent<ColliderEvent>().GetHitBoxs[9].center + this.transform.parent.transform.position, Quaternion.identity);
    //                            if (playerController.Direction == 1) hado.transform.Rotate(0, 0, 0);
    //                            else hado.transform.Rotate(0, 180, 0);
    //                            hado.name = "HadoukenA";
    //                            hado.transform.SetParent(this.transform);

    //                            hado.GetComponent<HadouController>().direction = playerController.Direction;
    //                        }

    //                        playerController.SpecialState = setSpecialState;
    //                        playerController.State = "Special";
    //                        HistoryClear();
    //                        Debug.Log(setSpecialState);
    //                        //nowGravity = 0;
    //                    }
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //}

    /// <summary>
    /// ダッシュ
    /// </summary>
    public void Dash()
    {
        //立ち状態だったらダッシュ
        if (playerController.State == "Stand")
        {
            string[] dash = new string[3];
            dash[0] = "6";
            dash[1] = "5";
            dash[2] = "6";

            int dashCount = 0;

            for (int i = commandCount / 2; i < commandCount; i++)
            {
                if (dash[dashCount] == history[i])
                {
                    dashCount++;
                    if (dashCount > 2)
                    {
                        //if (state != "Jump")
                        {
                            playerController.State = "Dash";
                            HistoryClear();
                        }
                        break;
                    }
                }
            }
            return;
        }
    }


    public void HistoryClear()
    {
        history.Clear();
        for (int i = 0; i < commandCount; i++)
        {
            history.Add("");
        }
    }

    public int InputDKey
    {
        get
        {
            return inputDKey;
        }
        set
        {
            inputDKey = value;
        }
    }

    public bool PunchKey
    {
        get
        {
            return punchKey;
        }
        set
        {
            punchKey = value;
        }
    }

    public bool KickKey
    {
        get
        {
            return kickKey;
        }
        set
        {
            kickKey = value;
        }
    }

    public string ControllerName
    {
        get
        {
            return controllerName;
        }
        set
        {
            controllerName = value;
        }
    }

    public int Controller
    {
        get
        {
            return controller;
        }
        set
        {
            controller = value;
        }
    }

    public int SpecialDirection { get { return specialDirection; } }

    public GameObject HadokenObject { get { return hado; } }
}
