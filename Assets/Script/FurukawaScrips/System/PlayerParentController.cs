//プレイヤー親のコントローラ
//2018/12/4
//入山奨
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class PlayerParentController : MonoBehaviour
{

    //移動速度
    private float speed = 0.03f;
    //移動速度(歩き)
    [SerializeField]
    private float walkSpeed = 0.03f;
    //移動速度(ダッシュ)
    [SerializeField]
    private float dashSpeed = 0.03f;
    //ジャンプの高さ
    [SerializeField]
    private float jumpSpeed = 0.15f;
    //重力
    [SerializeField]
    private float gravity = 0.008f;
    //状態
    [SerializeField]
    private string state = "Stand";
    //必殺技の状態
    [SerializeField]
    private string specialState = "";
    //コントローラー番号
    private int controller = 0;

    //最終的な移動距離
    private Vector3 finalMove = new Vector3(0, 0, 0);

    //方向キー
    private int inputDKey;
    private int inputDKeyOld;

    //コントローラーの名前
    [SerializeField]
    private string controllerName = "";

    //コマンド入力の猶予
    [SerializeField]
    private int commandCount = 20;
    //入力履歴の数
    [SerializeField]
    private int numInputHistory = 10;

    //ジャンプをしてから地面につかない間
    private int jumpTime = 10;
    private int jumpCount = 0;

    private int damageCount = 0;
    private int damageTime = 0;

    //キーの定義
    private float x = 0;
    private float y = 0;
    private bool punchKey = false;
    private bool kickKey = false;

    //向き
    private int direction = 1;
    //相手
    [SerializeField]
    private GameObject enemy;

    //縦方向の速度
    private float ySpeed = 0;
    //今の重力
    private float nowGravity;

    //操作ができない状態か
    [SerializeField]
    private bool isFreeze = false;

    //キャラクター生成オブジェクト
    private GameObject contl;
    private InstanceScript InScript;

    //入力履歴
    private List<string> history = new List<string>();
    //入力履歴(画面表示用)
    private List<string> inputHistory = new List<string>();

    void Awake()
    {
        //contl = GameObject.Find("FighterComtrol");
        //InScript = contl.GetComponent<InstanceScript>();
    }

    // Use this for initialization
    void Start()
    {
        //キャラクター設定
        switch (this.gameObject.tag)
        {
            case "P1":
                //enemy = InScript.Fighter(1);
                controller = 1;
                break;
            case "P2":
                //enemy = InScript.Fighter(0);
                controller = 2;
                this.transform.position = new Vector3(1, this.transform.position.y, this.transform.position.z);
                break;
            default:
                break;
        }

        Debug.Log(enemy.tag);

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

        if (controller > 0) controllerName = Input.GetJoystickNames()[controller - 1];

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetDirection();

        //コントローラーがAIじゃないなら入力を検知する
        if (controllerName != "AI")
        {
            InputKey();
        }

        switch (state)
        {
            case "Stand":
                //SetDirection();
                Stand();
                break;
            case "Dash":
                Dashing();
                break;
            case "Sit":
                //SetDirection();
                Sit();
                break;
            case "Jump":
                Jump();
                Jumping();
                break;
            case "Guard":
                Guard();
                break;
            case "StandGuard":
                StandGuard();
                break;
            case "SitGuard":
                SitGuard();
                break;
            case "Punch":
                Punch();
                break;
            case "Kick":
                Kick();
                break;
            case "SitPunch":
                SitPunch();
                break;
            case "SitKick":
                SitKick();
                break;
            case "Special":
                Special();
                break;
            case "Damage":
                Damage();
                break;
            case "JumpingDamage":
                JumpingDamage();
                break;
        }

        FinallyMove();

        inputDKeyOld = inputDKey;
    }

    /// <summary>
    /// 太刀状態
    /// </summary>
    void Stand()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);

        int move = 0;
        speed = walkSpeed;

        //左右移動する
        if (direction == 1)
        {
            if (inputDKey == 6 || inputDKey == 9) move = 1;
            if (inputDKey == 4 || inputDKey == 7) move = -1;
            //右向き
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);

        }
        else
        {
            if (inputDKey == 6 || inputDKey == 9) move = -1;
            if (inputDKey == 4 || inputDKey == 7) move = 1;

            //左向き
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1);
        }

        //下が押されたらしゃがみ
        if (inputDKey <= 3)
        {
            state = "Sit";
        }

        // 移動する向きを求める
        finalMove = new Vector3(move, 0, 0).normalized * speed;

        //上が押されたらジャンプ
        if (inputDKey >= 7)
        {
            nowGravity = 0;
            state = "Jump";
        }

        //立ち状態時にZを押すとパンチ
        if (punchKey)
        {
            state = "Punch";
        }
        //立ち状態時にXを押すとキック
        if (kickKey)
        {
            state = "Kick";
        }

        Dash();

    }

    /// <summary>
    /// しゃがみ
    /// </summary>
    void Sit()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        finalMove = new Vector3(0, 0, 0);

        if (direction == 1)
        {
            //右向き
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);

        }
        else
        {
            //左向き
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1);
        }

        //下が離されたら立つ
        if (inputDKey <= 3)
        {
            state = "Sit";
        }
        else
        {
            state = "Stand";
        }

        //しゃがみ状態時にZを押すとパンチ
        if (punchKey)
        {
            state = "SitPunch";
        }
        //しゃがみ状態時にXを押すとキック
        if (kickKey)
        {
            state = "SitKick";
        }
    }
    /// <summary>
    /// しゃがみパンチ
    /// </summary>
    void SitPunch()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        finalMove = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// しゃがみキック
    /// </summary>
    void SitKick()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        finalMove = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    void Jump()
    {
        jumpCount++;
    }

    /// <summary>
    /// パンチ
    /// </summary>
    void Punch()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        finalMove = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// キック
    /// </summary>
    void Kick()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        finalMove = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// 必殺技
    /// </summary>
    void Special()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        finalMove = new Vector3(0, 0, 0);
    }


    /// <summary>
    /// ジャンプ中
    /// </summary>
    void Jumping()
    {
        //ジャンプしているときに地面に触っておらず一定時間経過していたら終了
        bool jumpEnd = gameObject.transform.position.y <= 0 && jumpCount > jumpTime;
        if (jumpEnd)
        {
            jumpCount = 0;
            state = "Stand";
        }
        //ジャンプしているときに重力をかける
        bool jumping = gameObject.transform.position.y >= 0 && state == "Jump";
        if (jumping) nowGravity -= gravity;

        //ジャンプしたときのアニメ、ジャンプする動作
        if (state == "Jump")
        {
            ySpeed = jumpSpeed;
        }
        else
        {
            ySpeed = 0;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        }

        finalMove.y = ySpeed + nowGravity;
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
    /// ダッシュ
    /// </summary>
    void Dash()
    {
        //立ち状態だったらダッシュ
        if (state == "Stand")
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
                            state = "Dash";
                            history.Clear();
                            for (int j = 0; j < commandCount; j++)
                            {
                                history.Add("");
                            }

                        }
                        break;
                    }
                }
            }
            return;
        }
    }

    /// <summary>
    /// ダッシュ中
    /// </summary>
    void Dashing()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);

        int move = 0;
        speed = dashSpeed;

        //左右移動する
        if (direction == 1)
        {
            if (inputDKey == 6 || inputDKey == 9) move = 1;
            if (inputDKey == 4 || inputDKey == 7) move = -1;
        }
        else
        {
            if (inputDKey == 6 || inputDKey == 9) move = -1;
            if (inputDKey == 4 || inputDKey == 7) move = 1;
        }

        //下が押されたらしゃがみ
        if (inputDKey <= 3)
        {
            state = "Sit";
        }
        //上が押されたらジャンプ
        if (inputDKey >= 7)
        {
            nowGravity = 0;
            state = "Jump";
        }

        // 移動する向きを求める
        finalMove = new Vector3(move, 0, 0).normalized * speed;

        //立ち状態時にZを押すとパンチ
        if (punchKey)
        {
            state = "Punch";
        }
        //立ち状態時にXを押すとキック
        if (kickKey)
        {
            state = "Kick";
        }

        if (inputDKey == 5 || inputDKey == 4)
        {
            state = "Stand";
        }
    }

    /// <summary>
    /// ジャンプする昇竜
    /// </summary>
    void JumpSyoryu()
    {
        jumpCount++;
    }

    /// <summary>
    /// ジャンプ中にダメージ受けたとき
    /// </summary>++;
    void JumpingDamage()
    {
        jumpCount++;
        damageCount++;

        //ジャンプしているときに地面に触っておらず一定時間経過していたら終了
        bool jumpEnd = gameObject.transform.position.y <= 0 && jumpCount > jumpTime && damageCount >= damageTime;
        if (jumpEnd)
        {
            jumpCount = 0;
            state = "Stand";
        }
        //ジャンプしているときに重力をかける
        bool jumping = gameObject.transform.position.y >= 0 && state == "JumpingDamage";
        if (jumping) nowGravity -= gravity;

        //ジャンプしたときのアニメ、ジャンプする動作
        if (state == "JumpingDamage")
        {
            ySpeed = jumpSpeed;
        }
        else
        {
            //ジャンプ終わり
            damageCount = 0;
            ySpeed = 0;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
            state = "Stand";
        }

        finalMove.y = ySpeed + nowGravity;
    }

    /// <summary>
    /// ジャンプ中の昇竜
    /// </summary>
    void JumpingSyoryu()
    {

        //ジャンプしているときに地面に触っておらず一定時間経過していたら終了
        bool jumpEnd = gameObject.transform.position.y <= 0 && jumpCount > jumpTime;
        if (jumpEnd)
        {
            jumpCount = 0;
            state = "Stand";
            //freeze = true;
            //recoveryState = "JumpEnd";
        }
        //ジャンプしているときに重力をかける
        bool jumping = gameObject.transform.position.y >= 0 && state == "Special" && specialState == "Syoryuken";
        if (jumping) nowGravity -= gravity;

        //ジャンプしたときのアニメ、ジャンプする動作
        if (state == "Special" && specialState == "Syoryuken")
        {
            //animator.SetBool("Jump", true);
            ySpeed = jumpSpeed;
        }
        else
        {
            //ジャンプ終わり
            ySpeed = 0;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        }

        finalMove.y = ySpeed + nowGravity;
    }


    /// <summary>
    /// 立ちガードする
    /// </summary>
    void StandGuard()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        finalMove = new Vector3(0, 0, 0);
        if (inputDKey != 4) state = "Stand";
    }

    /// <summary>
    /// しゃがみガードする
    /// </summary>
    void SitGuard()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        finalMove = new Vector3(0, 0, 0);
        if (inputDKey != 1) state = "Stand";
    }

    /// <summary>
    /// 最終的な移動
    /// </summary>
    void FinallyMove()
    {
        //最終的な移動
        Vector3 finalPos = finalMove + gameObject.transform.position;
        if (finalPos.x <= 3.0f && finalPos.x >= -3.0f)
        {
            gameObject.transform.position = finalPos;

            if (jumpCount == 0 && state != "JumpingDamage") gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);

            if (gameObject.transform.position.y <= 0) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, finalPos.y, finalPos.z);

            if (jumpCount == 0 && state != "JumpingDamage") gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);

            if (gameObject.transform.position.y <= 0) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);

        }
    }

    /// <summary>
    /// ダメージを受けている
    /// </summary>
    private void Damage()
    {
        state = "Damage";

        speed = 0.0f;

        if (damageCount >= damageTime)
        {
            damageCount = 0;
            state = "Stand";
        }

        damageCount++;

    }

    /// <summary>
    /// ガードした
    /// </summary>
    private void Guard()
    {
        speed = 0.0f;

        if (damageCount >= damageTime)
        {
            damageCount = 0;
            state = "Stand";
        }

        damageCount++;

    }

    private void InputKey()
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
        if (direction == 1)
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
        else if (direction == -1)
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
    /// 敵の位置を見て向きをセット
    /// </summary>
    private void SetDirection()
    {
        int dirold = direction;

        if (enemy.transform.position.x >= transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
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

    public string State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
        }
    }

    public string SpecialState
    {
        get
        {
            return specialState;
        }
        set
        {
            specialState = value;
        }
    }

    public int Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }

    public bool IsFreeze
    {
        get
        {
            return isFreeze;
        }
        set
        {
            isFreeze = value;
        }
    }

    public GameObject fightEnemy { get { return enemy; } }
}
