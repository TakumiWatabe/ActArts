﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;
using System;

public class PlayerControllerTest : MonoBehaviour {
    float speed = 0;
    //歩くスピード
    public float walkSpeed = 0.03f;
    //走るスピード
    public float dashSpeed = 0.08f;
    //重力
    public float gravity = 0.008f;
    //状態
    public string state = "Stand";
    //必殺技の状態
    public string specialState = "";
    //コマンド入力の猶予
    public int commandCount = 20;
    //入力履歴の数
    public int numInputHistory = 10;
    //コントローラー番号
    private int controller = 0;
    //ダメージ
    public int damage = 0;
    //ダメージ受けたときに下がる距離
    float backDistance = 0;
    float backingDistance = 0;
    int damageDir = 0;

    int damageCount = 0;
    int damageTime = 0;

    //コントローラーの名前
    [SerializeField]
    public string controllerName = "";

    //向き
    int direction = 1;
    //相手
    private GameObject enemy;
    //相手スクリプト
    PlayerControllerTest enemyScript;

    //ガードする距離
    public float distanceToGuard = 0.7f;

    //方向キー
    int inputDKey;
    int inputDKeyOld;

    //これがモデルかどうか
    public bool isModel = false;

    //public Text text;
    //入力履歴
    public List<string> history = new List<string>();
    //入力履歴(画面表示用)
    List<string> inputHistory = new List<string>();
    //アニメーター
    Animator animator;
    //最終的な移動距離
    Vector3 finalMove = new Vector3(0, 0, 0);
    //ジャンプをしてから地面につかない間
    int jumpTime = 10;
    int jumpCount = 0;
    //縦方向の速度
    float ySpeed = 0;
    //今の重力
    float nowGravity;

    //キーの定義
    float x = 0;
    float y = 0;

    bool punchKey = false;
    bool kickKey = false;

    ColliderEvent CEvent = null;
    TestChar TChar = null;

    GameObject parent;

    //キャラクター生成オブジェクト
    private GameObject contl;
    private InstanceScript InScript;

    void Awake()
    {
        contl = GameObject.Find("FighterComtrol");
        InScript = contl.GetComponent<InstanceScript>();
    }

    // Use this for initialization
    void Start()
    {
        //キャラクター設定
        switch (this.gameObject.tag)
        {
            case "P1":
                enemy = InScript.Fighter(1);
                controller = 1;
                break;
            case "P2":
                enemy = InScript.Fighter(0);
                controller = 2;
                this.transform.position = new Vector3(1, this.transform.position.y, this.transform.position.z);
                break;
            default:
                break;
        }

        Debug.Log(enemy.tag);
        enemyScript = enemy.GetComponent<PlayerControllerTest>();

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

        if (isModel)
        {
            CEvent = this.GetComponent<ColliderEvent>();
            TChar = this.GetComponent<TestChar>();
            parent = this.transform.parent.gameObject;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetDirection();
        InputKey();

        switch (state)
        {
            case "Stand":
                Stand();
                break;
            case "Dash":
                Dashing();
                break;
            case "Sit":
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

        }


        FinallyMove();
        InputCommand();
        InputKeyHistory();
        SyoryukenCommand();
        HadokenCommand();
        CheckGuard();
        inputDKeyOld = inputDKey;
    }

    /// <summary>
    /// ダメージを受けている
    /// </summary>
    private void Damage()
    {
        float m = animator.GetInteger("Damage");
        state = "Damage";
        speed = 0.0f;

        animator.SetInteger("Move", 0);
        animator.SetInteger("Special", 0);
        animator.SetBool("Guard", false);
        animator.SetBool("Punch", false);
        animator.SetBool("Kick", false);
        animator.SetBool("Dash", false);
        animator.SetBool("Jump", false);
        if (damageCount >= damageTime)
        {
            damageCount = 0;
            animator.SetInteger("Damage", 0);
            state = "Stand";
            Debug.Log("ダダダ戻れたぞ");
        }

        damageCount++;

    }

    /// <summary>
    /// ガードした
    /// </summary>
    private void Guard()
    {
        float m = animator.GetInteger("Damage");

        speed = 0.0f;

        animator.SetInteger("Move", 0);
        animator.SetInteger("Special", 0);
        animator.SetBool("Guard", true);
        animator.SetBool("Punch", false);
        animator.SetBool("Kick", false);
        animator.SetBool("Dash", false);
        animator.SetBool("Jump", false);
        animator.SetBool("StandGuard", true);

        if (inputDKey == 1) animator.SetBool("StandGuard", false);

        if (damageCount >= damageTime)
        {
            damageCount = 0;
            animator.SetInteger("Damage", 0);
            state = "Stand";
            Debug.Log("ガガガ戻れたぞ");
        }

        damageCount++;

    }

    /// <summary>
    /// 敵の位置を見て向きをセット
    /// </summary>
    void SetDirection()
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

    /// <summary>
    /// コントローラの入力を入れる
    /// </summary>
    void InputKey()
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
            if (!isModel) transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);

        }
        else
        {
            if (inputDKey == 6 || inputDKey == 9) move = -1;
            if (inputDKey == 4 || inputDKey == 7) move = 1;

            //左向き
            if (!isModel) transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1);
        }

        //下が押されたらしゃがみ
        if (inputDKey <= 3)
        {
            state = "Sit";
        }

        // 移動する向きを求める
        finalMove = new Vector3(move, 0, 0).normalized * speed;

        if (direction == 1) animator.SetInteger("Move", move);
        else animator.SetInteger("Move", move * -1);
        animator.SetInteger("Special", 0);
        animator.SetBool("Guard", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Punch", false);
        animator.SetBool("Kick", false);
        animator.SetBool("Dash", false);

        //上が押されたらジャンプ
        if (inputDKey >= 7)
        {
            nowGravity = 0;
            state = "Jump";
        }

        //立ち状態時にZを押すとパンチ
        if (punchKey)
        {
            animator.SetBool("Punch", true);
            state = "Punch";
        }
        //立ち状態時にXを押すとキック
        if (kickKey)
        {
            animator.SetBool("Kick", true);
            state = "Kick";
        }

        Dash();

    }

    /// <summary>
    /// しゃがみ
    /// </summary>
    void Sit()
    {
        animator.SetBool("Sit", true);
        animator.SetBool("Punch", false);
        animator.SetBool("Kick", false);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        finalMove = new Vector3(0, 0, 0);

        if (direction == 1)
        {
            //右向き
            if (!isModel) transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);

        }
        else
        {
            //左向き
            if (!isModel) transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1);
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
            animator.SetBool("Punch", true);
            state = "SitPunch";
        }
        //しゃがみ状態時にXを押すとキック
        if (kickKey)
        {
            animator.SetBool("Kick", true);
            state = "SitKick";
        }
    }

    /// <summary>
    /// しゃがみキック
    /// </summary>
    void SitKick()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        //キックしているアニメーションで終わったら戻す
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetBool("Kick"))
        {
            animator.SetBool("Kick", false);
            state = "Sit";
        }
        damage = 600;
        animator.SetBool("Kick", true);
        finalMove = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    void Jump()
    {
        animator.SetBool("Jump", true);
        jumpCount++;
    }

    /// <summary>
    /// パンチ
    /// </summary>
    void Punch()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);

        damage = 300;
        animator.SetBool("Punch", true);
        finalMove = new Vector3(0, 0, 0);

        //立ち状態時にZを押すとパンチ
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && (punchKey))
        {
            animator.SetBool("Punch", true);
            state = "Punch";
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, -1, 0.0f);
        }

        //立ち状態時にXを押すとキック
        if (kickKey)
        {
            animator.SetBool("Kick", true);
            state = "Kick";
        }

        //キックしているアニメーションで終わったら戻す
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetBool("Punch"))
        {
            animator.SetBool("Punch", false);
            state = "Stand";
        }


    }

    /// <summary>
    /// しゃがみパンチ
    /// </summary>
    void SitPunch()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);

        damage = 300;
        animator.SetBool("Punch", true);
        finalMove = new Vector3(0, 0, 0);

        //しゃがみ状態時にZを押すとパンチ
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && (punchKey))
        {
            animator.SetBool("Punch", true);
            state = "SitPunch";
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, -1, 0.0f);
        }

        //しゃがみ状態時にXを押すとキック
        if (kickKey)
        {
            animator.SetBool("Kick", true);
            state = "SitKick";
        }

        //キックしているアニメーションで終わったら戻す
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetBool("Punch"))
        {
            animator.SetBool("Punch", false);
            state = "Sit";
        }


    }

    /// <summary>
    /// キック
    /// </summary>
    void Kick()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        //キックしているアニメーションで終わったら戻す
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetBool("Kick"))
        {
            animator.SetBool("Kick", false);
            state = "Stand";
        }
        damage = 600;
        animator.SetBool("Kick", true);
        finalMove = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// 必殺技
    /// </summary>
    void Special()
    {
        switch (specialState)
        {
            //必殺
            case "Hadoken":

                finalMove = new Vector3(0, 0, 0);
                animator.SetInteger("Special", 1);
                break;
            //必殺
            case "Syoryuken":

                finalMove = new Vector3(0, 0, 0);
                animator.SetInteger("Special", 2);
                break;
        }
        //アニメ終了でもどる
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetInteger("Special") != 0)
        {
            animator.SetInteger("Special", 0);
            state = "Stand";
        }

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
            animator.SetBool("Jump", true);
            ySpeed = 0.15f;

            //ジャンプキック
            if (punchKey && !animator.GetBool("Punch") && !animator.GetBool("Kick"))
            {
                animator.SetBool("Punch", true);
                damage = 300;
            }
            //ジャンプキック
            if (kickKey && !animator.GetBool("Punch") && !animator.GetBool("Kick"))
            {
                animator.SetBool("Kick", true);
                damage = 500;
            }
        }
        else
        {
            //ジャンプ終わり
            animator.SetBool("Jump", false);
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
        if (state != "Special")
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
                        if (state != "Jump" && state != "Special")
                        {
                            specialState = "Hadoken";
                            state = "Special";
                            history.Clear();
                            for (int j = 0; j < commandCount; j++)
                            {
                                history.Add("");
                            }
                            Debug.Log("波動拳");

                        }
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
        if (state != "Special")
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
                        if (state != "Jump" && state != "Special")
                        {
                            specialState = "Syoryuken";
                            state = "Special";
                            history.Clear();
                            for (int j = 0; j < commandCount; j++)
                            {
                                history.Add("");
                            }
                            Debug.Log("昇龍拳");
                        }
                        return;
                    }
                }
            }
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
                        state = "Dash";
                        history.Clear();
                        for (int j = 0; j < commandCount; j++)
                        {
                            history.Add("");
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
        animator.SetBool("Dash", true);
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

        if (direction == 1) animator.SetInteger("Move", move);
        else animator.SetInteger("Move", move * -1);
        animator.SetInteger("Special", 0);
        animator.SetBool("Sit", false);
        animator.SetBool("Punch", false);
        animator.SetBool("Kick", false);

        //立ち状態時にZを押すとパンチ
        if (punchKey)
        {
            animator.SetBool("Punch", true);
            state = "Punch";
        }
        //立ち状態時にXを押すとキック
        if (kickKey)
        {
            animator.SetBool("Kick", true);
            state = "Kick";
        }

        if (inputDKey == 5)
        {
            state = "Stand";
            animator.SetBool("Dash", false);
        }
    }

    /// <summary>
    /// ガードできるかチェック
    /// </summary>
    void CheckGuard()
    {
        //敵との距離
        float distanceToEnemy = enemy.transform.position.x - transform.position.x;

        //立ちガード
        if ((enemyScript.state == "Punch" || enemyScript.state == "Kick") && distanceToGuard > Mathf.Abs(distanceToEnemy) && inputDKey == 4 && (state == "Stand" || state == "Sit"))
        {
            animator.SetBool("Guard", true);
            state = "StandGuard";
        }
        //しゃがみガード
        if ((enemyScript.state == "Punch" || enemyScript.state == "Kick") && distanceToGuard > Mathf.Abs(distanceToEnemy) && inputDKey == 1 && (state == "Stand" || state == "Sit"))
        {
            animator.SetBool("Guard", true);
            state = "SitGuard";
        }
    }

    /// <summary>
    /// 立ちガードする
    /// </summary>
    void StandGuard()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        finalMove = new Vector3(0, 0, 0);
        animator.SetBool("StandGuard", true);
        if (enemyScript.state != "Punch" || enemyScript.state != "Kick") state = "Stand";
        if (inputDKey != 4) state = "Stand";
    }

    /// <summary>
    /// しゃがみガードする
    /// </summary>
    void SitGuard()
    {

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
        finalMove = new Vector3(0, 0, 0);
        animator.SetBool("StandGuard", false);
        if (enemyScript.state != "Punch" || enemyScript.state != "Kick") state = "Stand";
        if (inputDKey != 1) state = "Stand";
    }

    /// <summary>
    /// 最終的な移動
    /// </summary>
    void FinallyMove()
    {
        //最終的な移動
        Vector3 finalPos = finalMove + gameObject.transform.position;
        gameObject.transform.position = finalPos;

        if (state != "Jump") gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);

    }

    /// <summary>
    /// ダメージ受けるかチェック
    /// </summary>
    public void HitDamage(int dmg)
    {
        if ((state == "Stand" || state == "Sit" || state == "Guard" || state == "SitGuard" || state == "StandGuard") && state != "Jump" && (inputDKey == 1 || inputDKey == 4))
        {
            animator.SetBool("Guard", true);
            state = "Guard";

            parent.GetComponent<PlayerControllerTest>().state = "Guard";
            damageTime = (dmg / 500 + 15) / 3;
            damageDir = direction * -1;
            parent.GetComponent<PlayerControllerTest>().damageTime = (dmg / 500 + 15) / 3;
            parent.GetComponent<PlayerControllerTest>().damageDir = direction * -1;
        }
        else
        {
            animator.SetInteger("Damage", dmg);
            state = "Damage";

            parent.GetComponent<PlayerControllerTest>().state = "Damage";
            backDistance = dmg;
            damageTime = dmg / 500 + 15;
            damageDir = direction * -1;
            parent.GetComponent<PlayerControllerTest>().damageTime = dmg / 500 + 15;
            parent.GetComponent<PlayerControllerTest>().damageDir = direction * -1;
        }

    }
}