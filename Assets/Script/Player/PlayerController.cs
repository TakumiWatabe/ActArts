using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;
using System;

public class PlayerController : MonoBehaviour
{
    //コマンドのスクリプト
    private PlayerCommand playerCommand;

    private float speed = 0;
    [SerializeField]
    private bool isTest = false;
    //歩くスピード
    [SerializeField]
    private float walkSpeed = 0.03f;
    //走るスピード
    [SerializeField]
    private float dashSpeed = 0.08f;
    //重力
    private float gravity = 0.008f;
    //状態
    [SerializeField]
    private string state = "Stand";
    //必殺技の状態
    private string specialState = "";

    //ジャンプのスピード
    [SerializeField]
    private float jumpSpeed = 0.15f;
    //昇竜のスピード
    [SerializeField]
    private float syoryuSpeed = 0.175f;

    //キャラ分別
    [SerializeField]
    private string charName;

    private int damageCount = 0;
    private int damageTime = 0;

    //向き
    private int direction = 1;
    //相手
    [SerializeField]
    private GameObject enemy;

    //相手スクリプト
    private PlayerController enemyScript;

    //ガードする距離
    public float distanceToGuard = 0.7f;


    //アニメーター
    private Animator animator;

    //コントローラ操作が可能か
    [SerializeField]
    private bool canControll = true;

    //ガードクラッシュ時にひるむ時間
    [SerializeField]
    private int guardCrashTime = 120;
    private int guardCrashCount = 0;

    //ガードゲージ
    [SerializeField]
    private int guardGaugePoint = 5000;

    //波動コマンド成立時に飛び道具を飛ばすか
    [SerializeField]
    private bool isHadouCommandMissile = false;

    //昇竜コマンド成立時にジャンプするか
    [SerializeField]
    private bool isSyoryuCommandJump = false;

    //最終的な移動距離
    [SerializeField]
    private Vector3 finalMove = new Vector3(0, 0, 0);
    //ジャンプをしてから地面につかない間
    [SerializeField]
    private int jumpTime = 10;
    [SerializeField]
    private int jumpCount = 0;
    //縦方向の速度
    [SerializeField]
    private float ySpeed = 0;
    //今の重力
    [SerializeField]
    private float nowGravity;

    //キャラクター生成オブジェクト
    private GameObject contl;
    private InstanceScript InScript;

    private Camera mainCamera;

    [SerializeField]
    private GameObject jumpEffect;
    [SerializeField]
    private GameObject landEffect;
    [SerializeField]
    private GameObject guardEffect;
    [SerializeField]
    private GameObject hadouHitEffect;

    [SerializeField]
    private int guardEffectTime;
    private int guradEffectCount;

    private PlaySEScript playSEScript;

    private HPDirectorScript hpDirectorScript;

    void Awake()
    {
        if (!isTest)
        {
            contl = GameObject.Find("FighterComtrol");
            InScript = contl.GetComponent<InstanceScript>();
            playSEScript = GetComponent<PlaySEScript>();
            hpDirectorScript = GetComponent<HPDirectorScript>();
        }

    }

    // Use this for initialization
    void Start()
    {
        playerCommand = GetComponent<PlayerCommand>();

        GameObject obj = GameObject.Find("Main Camera");
        mainCamera = obj.GetComponent<Camera>();

        animator = GetComponent<Animator>();

        distanceToGuard = 2.2f;

        if (!isTest)
        {
            //キャラクター設定
            switch (this.gameObject.tag)
            {
                case "P1":
                    enemy = InScript.Fighter(1);
                    playerCommand.Controller = 1;
                    break;
                case "P2":
                    enemy = InScript.Fighter(0);
                    this.transform.position = new Vector3(1, this.transform.position.y, this.transform.position.z);
                    playerCommand.Controller = 2;

                    break;
                default:
                    break;
            }
        }


        enemyScript = enemy.GetComponent<PlayerController>();
        Debug.Log(enemy.tag);

        if (gameObject.GetComponent<EnemyAI>() != null) gameObject.GetComponent<EnemyAI>().Initialize();

        guradEffectCount = 0;

        SetDirection();

        
    }

    public void Initialize()
    {
        if (!isTest)
        {
            //キャラクター設定
            switch (this.gameObject.tag)
            {
                case "P1":
                    enemy = InScript.Fighter(1);
                    playerCommand.Controller = 1;
                    break;
                case "P2":
                    enemy = InScript.Fighter(0);
                    this.transform.position = new Vector3(1, this.transform.position.y, this.transform.position.z);
                    playerCommand.Controller = 2;

                    break;
                default:
                    break;
            }
        }

        playerCommand.Initialize();

        state = "Stand";
        animator.SetBool("GuardCrash", false);
        canControll = false;
        SetDirection();
        animator.SetInteger("Move", 0);
        animator.SetInteger("Special", 0);
        animator.SetBool("Guard", false);
        animator.SetBool("GuardCrash", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Punch", false);
        animator.SetBool("Kick", false);
        animator.SetBool("Dash", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Win", false);
        animator.SetBool("Down", false);
        animator.SetInteger("Damage", 0);


        guradEffectCount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //text.text = "";

        //gameObject.transform.position = new Vector3(gameObject.transform.position.x + speed, 0, 0);
        //Debug.Log(gameObject.name + "update1");

        if (NowHP <= 0 && canControll && transform.position.y == 0)
        {
            finalMove = Vector3.zero;
            playerCommand.HistoryClear();
            animator.SetBool("Down", true);
            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.LOSE);
            canControll = false;
        }

        if (enemyScript.NowHP <= 0 && NowHP > enemyScript.NowHP && canControll && transform.position.y == 0)
        {
            finalMove = Vector3.zero;
            playerCommand.HistoryClear();
            animator.SetBool("Win", true);
            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.WIN);
            canControll = false;
        }

        //if (animator.GetInteger("Damage") == 0 && transform.position.y > 0) state = "Jump";

        if (canControll) SetDirection();

        string name = playerCommand.controllerName;

        //コントローラーがAIじゃないなら入力を検知する
        if (name != "AI")
        {
            if (canControll) playerCommand.InputKey();
        }

        if (state != "GuardCrash") guardCrashCount = 0;


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
            case "BlowOff":
                BlowOff();
                break;
            case "GuardCrash":
                GuardCrash();
                break;
        }

        FinallyMove();
        CheckGuard();

        if (playerCommand.ControllerName == "AI") Debug.Log(gameObject.name + "STATE:" + state);

    }

    /// <summary>
    /// 吹っ飛ぶ
    /// </summary>
    private void BlowOff()
    {
        //animator.SetBool("Jump", true);
        animator.SetBool("Jump", true);
        animator.SetInteger("Damage", 1);
        jumpCount++;

        //animator.SetInteger("Damage", 0);
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
        bool jumping = gameObject.transform.position.y >= 0 && state == "BlowOff";
        if (jumping) nowGravity -= gravity;

        //ジャンプしたときのアニメ、ジャンプする動作
        if (state == "BlowOff")
        {
            //animator.SetBool("Jump", true);
            ySpeed = jumpSpeed;
            finalMove.x = walkSpeed * direction * -1;
        }
        else
        {
            //ジャンプ終わり
            animator.SetBool("Jump", false);
            animator.SetInteger("Damage", 0);
            ySpeed = 0;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
            Vector3 pos = transform.position;
            if (canControll) Instantiate(landEffect, pos, Quaternion.identity);
        }
        ySpeed = ySpeed + nowGravity;
        finalMove.y = ySpeed;
    }


    /// <summary>
    /// ガークラ
    /// </summary>
    private void GuardCrash()
    {
        animator.SetBool("GuardCrash", true);
        canControll = false;
        guardCrashCount++;
        animator.SetInteger("Move", 0);
        animator.SetInteger("Special", 0);
        animator.SetBool("Guard", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Punch", false);
        animator.SetBool("Kick", false);
        animator.SetBool("Dash", false);
        animator.SetInteger("Damage", 0);
        animator.SetBool("Jump", false);
        if (guardCrashCount > guardCrashTime)
        {
            state = "Stand";
            animator.SetBool("GuardCrash", false);
            guardCrashCount = 0;
            canControll = true;
        }
    }

    /// <summary>
    /// ダメージを受けている
    /// </summary>
    private void Damage()
    {
        float m = animator.GetInteger("Damage");
        state = "Damage";
        //if(direction == 1)finalMove = new Vector3(-0.2f, 0, 0);
        //else finalMove = new Vector3(0.2f, 0, 0);

        speed = 0.0f;

        animator.SetInteger("Move", 0);
        animator.SetInteger("Special", 0);
        animator.SetBool("Guard", false);
        //animator.SetBool("Sit", false);
        animator.SetBool("Punch", false);
        animator.SetBool("Kick", false);
        animator.SetBool("Dash", false);
        animator.SetBool("Jump", false);

        //Debug.Log(damageTime);

        if (damageCount >= damageTime)
        {
            damageCount = 0;
            animator.SetInteger("Damage", 0);
            if (animator.GetBool("Sit")) state = "Sit";
            else state = "Stand";
            canControll = true;
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

        //playSEScript.PlayVoice((int)PlaySEScript.VoiceData.GUARD);

        guardCrashCount++;
        if (guradEffectCount > guardEffectTime)
        {
            guardEffect.SetActive(false);
        }

        //if (direction == 1) finalMove = new Vector3(-0.075f, 0, 0);
        //else finalMove = new Vector3(0.075f, 0, 0);

        animator.SetInteger("Move", 0);
        animator.SetInteger("Special", 0);
        animator.SetBool("Guard", true);
        //animator.SetBool("Sit", false);
        animator.SetBool("Punch", false);
        animator.SetBool("Kick", false);
        animator.SetBool("Dash", false);
        animator.SetBool("Jump", false);
        animator.SetBool("StandGuard", true);

        if (playerCommand.InputDKey == 1) animator.SetBool("StandGuard", false);


        if (damageCount >= damageTime)
        {
            damageCount = 0;
            animator.SetInteger("Damage", 0);
            if(animator.GetBool("Sit")) state = "Sit";
            else    state = "Stand";
            Debug.Log("ガガガ戻れたぞ");
            canControll = true;
            guradEffectCount = 0;
            guardEffect.SetActive(false);
        }

        damageCount++;

    }

    /// <summary>
    /// 敵の位置を見て向きをセット
    /// </summary>
    public void SetDirection()
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

        //if (dirold != direction) Debug.Log("振り返った");
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
            if (playerCommand.InputDKey == 6 || playerCommand.InputDKey == 9) move = 1;
            if (playerCommand.InputDKey == 4 || playerCommand.InputDKey == 7) move = -1;
            //右向き
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);

        }
        else
        {
            if (playerCommand.InputDKey == 6 || playerCommand.InputDKey == 9) move = -1;
            if (playerCommand.InputDKey == 4 || playerCommand.InputDKey == 7) move = 1;

            //左向き
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1);
        }

        //下が押されたらしゃがみ
        if (playerCommand.InputDKey <= 3)
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
        animator.SetBool("Jump", false);
        animator.SetInteger("Damage", 0);

        //上が押されたらジャンプ
        if (playerCommand.InputDKey >= 7)
        {
            nowGravity = 0;
            state = "Jump";
            Vector3 pos = transform.position;
            Instantiate(jumpEffect, pos, Quaternion.identity);
            //animator.SetBool("Jump", true);
        }

        //立ち状態時にZを押すとパンチ
        if (playerCommand.PunchKey)
        {
            animator.SetBool("Punch", true);
            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.ATTACK1);
            playSEScript.PlaySE((int)PlaySEScript.SEData.ATTACK1);
            state = "Punch";
        }
        //立ち状態時にXを押すとキック
        if (playerCommand.KickKey)
        {
            animator.SetBool("Kick", true);
            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.ATTACK2);
            playSEScript.PlaySE((int)PlaySEScript.SEData.ATTACK2);
            state = "Kick";
        }

        playerCommand.Dash();
    }

    /// <summary>
    /// しゃがみ
    /// </summary>
    void Sit()
    {
        animator.SetBool("Sit", true);
        animator.SetBool("Punch", false);
        animator.SetBool("Kick", false);
        animator.SetBool("Jump", false);
        animator.SetInteger("Damage", 0);
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
        if (playerCommand.InputDKey <= 3)
        {
            state = "Sit";
        }
        else
        {
            state = "Stand";
        }

        //しゃがみ状態時にZを押すとパンチ
        if (playerCommand.PunchKey)
        {
            animator.SetBool("Punch", true);
            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.ATTACK1);
            playSEScript.PlaySE((int)PlaySEScript.SEData.ATTACK1);
            state = "SitPunch";
        }
        //しゃがみ状態時にXを押すとキック
        if (playerCommand.KickKey)
        {
            animator.SetBool("Kick", true);
            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.ATTACK2);
            playSEScript.PlaySE((int)PlaySEScript.SEData.ATTACK2);
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

        animator.SetBool("Punch", true);
        finalMove = new Vector3(0, 0, 0);

        //立ち状態時にZを押すとパンチ
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && (playerCommand.PunchKey))
        {
            animator.SetBool("Punch", true);
            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.ATTACK1);
            playSEScript.PlaySE((int)PlaySEScript.SEData.ATTACK1);
            state = "Punch";
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, -1, 0.0f);
        }

        //立ち状態時にXを押すとキック
        if (playerCommand.KickKey)
        {
            animator.SetBool("Kick", true);
            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.ATTACK2);
            playSEScript.PlaySE((int)PlaySEScript.SEData.ATTACK2);
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

        animator.SetBool("Punch", true);
        finalMove = new Vector3(0, 0, 0);

        //しゃがみ状態時にZを押すとパンチ
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && (playerCommand.PunchKey))
        {
            animator.SetBool("Punch", true);
            state = "SitPunch";
            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.ATTACK1);
            playSEScript.PlaySE((int)PlaySEScript.SEData.ATTACK1);
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, -1, 0.0f);
        }

        //しゃがみ状態時にXを押すとキック
        if (playerCommand.KickKey)
        {
            animator.SetBool("Kick", true);
            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.ATTACK2);
            playSEScript.PlaySE((int)PlaySEScript.SEData.ATTACK2);
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
                if (isSyoryuCommandJump)
                {
                    animator.SetInteger("Special", 2);
                    JumpSyoryu();
                    JumpingSyoryu();
                }
                else
                {
                    finalMove = new Vector3(0, 0, 0);
                    animator.SetInteger("Special", 2);
                }

                break;
        }

        if (isSyoryuCommandJump)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetInteger("Special") == 1)
            {
                float posX = transform.position.x + transform.GetChild(2).localPosition.z * direction;
                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
                animator.SetInteger("Special", 0);
                state = "Stand";
            }

        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetInteger("Special") != 0)
            {
                float posX = transform.position.x + transform.GetChild(2).localPosition.z * direction;
                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
                animator.SetInteger("Special", 0);
                state = "Stand";
            }

        }
    }

    /// <summary>
    /// ジャンプ中
    /// </summary>
    void Jumping()
    {
        animator.SetInteger("Damage", 0);
        //ジャンプしているときに地面に触っておらず一定時間経過していたら終了
        bool jumpEnd = gameObject.transform.position.y <= 0 && jumpCount > jumpTime;
        if (jumpEnd || jumpCount >= 90)
        {
            jumpCount = 0;
            state = "Stand";
            //freeze = true;
            //recoveryState = "JumpEnd";
        }
        //ジャンプしているときに重力をかける
        bool jumping = gameObject.transform.position.y >= 0 && state == "Jump";
        if (jumping) nowGravity -= gravity;

        //ジャンプしたときのアニメ、ジャンプする動作
        if (state == "Jump")
        {
            animator.SetBool("Jump", true);
            ySpeed = jumpSpeed;

            //ジャンプキック
            if (playerCommand.PunchKey && !animator.GetBool("Punch") && !animator.GetBool("Kick"))
            {
                //animator.SetBool("Punch", true);
                playSEScript.PlayVoice((int)PlaySEScript.VoiceData.ATTACK1);
                playSEScript.PlaySE((int)PlaySEScript.SEData.ATTACK1);
                animator.SetBool("Punch", true);
            }
            //ジャンプキック
            if (playerCommand.KickKey && !animator.GetBool("Punch") && !animator.GetBool("Kick"))
            {
                playSEScript.PlayVoice((int)PlaySEScript.VoiceData.ATTACK2);
                playSEScript.PlaySE((int)PlaySEScript.SEData.ATTACK2);
                animator.SetBool("Kick", true);
            }
        }
        else
        {
            //ジャンプ終わり
            animator.SetBool("Jump", false);
            ySpeed = 0;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
            Vector3 pos = transform.position;
            if (canControll) Instantiate(landEffect, pos, Quaternion.identity);
        }
        ySpeed = ySpeed + nowGravity;
        finalMove.y = ySpeed;
    }

    /// <summary>
    /// ダッシュ中
    /// </summary>
    void Dashing()
    {
        animator.SetBool("Dash", true);
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);

        int move = 0;
        speed = dashSpeed;

        //左右移動する
        if (direction == 1)
        {
            if (playerCommand.InputDKey == 6 || playerCommand.InputDKey == 9) move = 1;
            if (playerCommand.InputDKey == 4 || playerCommand.InputDKey == 7) move = -1;

            //右向き
            //if (!isModel) transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);

        }
        else
        {
            if (playerCommand.InputDKey == 6 || playerCommand.InputDKey == 9) move = -1;
            if (playerCommand.InputDKey == 4 || playerCommand.InputDKey == 7) move = 1;

            //左向き
            //if (!isModel) transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1);
        }

        //下が押されたらしゃがみ
        if (playerCommand.InputDKey <= 3)
        {
            state = "Sit";
        }
        //上が押されたらジャンプ
        if (playerCommand.InputDKey >= 7)
        {
            nowGravity = 0;
            state = "Jump";
            Vector3 pos = transform.position;
            Instantiate(jumpEffect, pos, Quaternion.identity);
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
        if (playerCommand.PunchKey)
        {
            animator.SetBool("Punch", true);
            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.ATTACK1);
            playSEScript.PlaySE((int)PlaySEScript.SEData.ATTACK1);
            state = "Punch";
        }
        //立ち状態時にXを押すとキック
        if (playerCommand.KickKey)
        {
            animator.SetBool("Kick", true);
            playSEScript.PlayVoice((int)PlaySEScript.VoiceData.ATTACK2);
            playSEScript.PlaySE((int)PlaySEScript.SEData.ATTACK2);
            state = "Kick";
        }

        if (playerCommand.InputDKey == 5 || playerCommand.InputDKey == 4)
        {
            state = "Stand";
            animator.SetBool("Dash", false);
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


        animator.SetInteger("Move", 0);
        animator.SetInteger("Special", 0);
        animator.SetBool("Guard", false);
        //animator.SetBool("Sit", false);
        animator.SetBool("Punch", false);
        animator.SetBool("Kick", false);
        animator.SetBool("Dash", false);
        animator.SetBool("Jump", false);


        //ジャンプしているときに地面に触っておらず一定時間経過していたら終了
        bool jumpEnd = gameObject.transform.position.y <= 0 && jumpCount > jumpTime && damageCount >= damageTime;
        if (jumpEnd || jumpCount >= 90)
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
            //animator.SetBool("Jump", true);
            ySpeed = jumpSpeed;
        }
        else
        {
            //ジャンプ終わり
            damageCount = 0;
            animator.SetInteger("Damage", 0);
            ySpeed = 0;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
            state = "Stand";
            Debug.Log("aaaaaa");
            canControll = true;
            Vector3 pos = transform.position;
            if (canControll) Instantiate(landEffect, pos, Quaternion.identity);
        }
        ySpeed = ySpeed + nowGravity;
        finalMove.y = ySpeed;
    }

    /// <summary>
    /// ジャンプ中の昇竜
    /// </summary>
    void JumpingSyoryu()
    {

        //ジャンプしているときに地面に触っておらず一定時間経過していたら終了
        bool jumpEnd = gameObject.transform.position.y <= 0 && jumpCount > jumpTime;
        if (jumpEnd || jumpCount >= 90)
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
            animator.SetInteger("Special", 0);
            ySpeed = 0;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
            Vector3 pos = transform.position;
            if (canControll) Instantiate(landEffect, pos, Quaternion.identity);
        }
        ySpeed = ySpeed + nowGravity;
        finalMove.y = ySpeed;
    }

    /// <summary>
    /// ガードできるかチェック
    /// </summary>
    void CheckGuard()
    {
        //敵との距離
        float distanceToEnemy = enemy.transform.position.x - transform.position.x;

        //立ちガード
        //if ((enemyScript.state == "Punch" || enemyScript.state == "Kick") && distanceToGuard > Mathf.Abs(distanceToEnemy) && playerCommand.InputDKey == 4 && (state == "Stand" || state == "Sit"))
        if ((enemyScript.AnimatorPlayer.GetBool("Punch") || enemyScript.AnimatorPlayer.GetBool("Kick") || enemyScript.AnimatorPlayer.GetInteger("Special") != 0) && distanceToGuard > Mathf.Abs(distanceToEnemy) && playerCommand.InputDKey == 4 && (state == "Stand" || state == "Sit"))
        {
            animator.SetBool("Guard", true);
            state = "StandGuard";
        }
        //しゃがみガード
        //if ((enemyScript.state == "Punch" || enemyScript.state == "Kick") && distanceToGuard > Mathf.Abs(distanceToEnemy) && playerCommand.InputDKey == 1 && (state == "Stand" || state == "Sit"))
        if ((enemyScript.AnimatorPlayer.GetBool("Punch") || enemyScript.AnimatorPlayer.GetBool("Kick") || enemyScript.AnimatorPlayer.GetInteger("Special") != 0) && distanceToGuard > Mathf.Abs(distanceToEnemy) && playerCommand.InputDKey == 1 && (state == "Stand" || state == "Sit"))
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
        if (playerCommand.InputDKey != 4) state = "Stand";
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
        if (playerCommand.InputDKey != 1) state = "Stand";
    }

    /// <summary>
    /// 最終的な移動
    /// </summary>
    void FinallyMove()
    {
        if (!canControll) return;

        SpecialMove();

        // 画面の右下を取得
        Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        // 上下反転させる
        bottomRight.Scale(new Vector3(1f, -1f, 1f));

        // 画面の左上を取得
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        // 上下反転させる
        topLeft.Scale(new Vector3(1f, -1f, 1f));

        //最終的な移動
        Vector3 finalPos = finalMove + gameObject.transform.position;
        finalPos.z = 0;
        if (finalPos.x <= bottomRight.x && finalPos.x >= topLeft.x) gameObject.transform.position = finalPos;
        else gameObject.transform.position = new Vector3(gameObject.transform.position.x, finalPos.y, 0);

        if (jumpCount == 0 && state != "JumpingDamage") gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);

        if (gameObject.transform.position.y < 0) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);

        if (gameObject.transform.position.x > bottomRight.x)
        {
            gameObject.transform.position = new Vector3(bottomRight.x - 0.01f, gameObject.transform.position.y, 0);
        }
        if (gameObject.transform.position.x < topLeft.x)
        {
            gameObject.transform.position = new Vector3(topLeft.x + 0.01f, gameObject.transform.position.y, 0);
        }
    }

    public void HitDamage(int dmg,int atkLevel,int hitStun)
    {
        damageCount = 0;

        {
            SpecialMove();
            animator.SetBool("GuardCrash", false);
            animator.SetInteger("Damage", dmg);

            if (jumpCount == 0)
            {
                state = "Damage";
            }
            else
            {
                //state = "JumpingDamage";
                state = "BlowOff";
                animator.SetBool("Jump", true);
                nowGravity = 0;
            }

            if (atkLevel >= 3)
            {
                state = "BlowOff";
                nowGravity = 0;
                animator.SetBool("Jump", true);
            }

            damageTime = hitStun;

            if (dmg > 600)
            {
                playSEScript.PlayVoice((int)PlaySEScript.VoiceData.DAMAGE2);
                playSEScript.PlaySE((int)PlaySEScript.SEData.DAMAGE2);
            }
            else
            {
                playSEScript.PlayVoice((int)PlaySEScript.VoiceData.DAMAGE2);
                playSEScript.PlaySE((int)PlaySEScript.SEData.DAMAGE2);
            }


        }

    }

    public void GuardDamage(int guardStun)
    {
        damageCount = 0;

        SpecialMove();
        Debug.Log(state);
        animator.SetBool("Guard", true);
        state = "Guard";

        damageTime = guardStun;

        playSEScript.PlayVoice((int)PlaySEScript.VoiceData.GUARD);
        //guardEffect.SetActive(true);
        guradEffectCount = 0;
    }

    /// <summary>
    /// 必殺技による移動調整の処理
    /// </summary>
    void SpecialMove()
    {
        if (state == "Special")
        {
            // 画面の右下を取得
            Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
            // 上下反転させる
            bottomRight.Scale(new Vector3(1f, -1f, 1f));

            // 画面の左上を取得
            Vector3 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
            // 上下反転させる
            topLeft.Scale(new Vector3(1f, -1f, 1f));

            Vector3 pos = transform.position;
            if (gameObject.transform.position.x + transform.GetChild(2).localPosition.z * direction >= bottomRight.x)
            {
                pos.x = bottomRight.x - transform.GetChild(2).localPosition.z * direction;
            }
            if (gameObject.transform.position.x + transform.GetChild(2).localPosition.z * direction <= topLeft.x)
            {
                pos.x = topLeft.x + transform.GetChild(2).localPosition.z;
            }
            transform.position = pos;
        }
    }

    public int InputDKey
    {
        get
        {
            return playerCommand.InputDKey;
        }
        set
        {
            playerCommand.InputDKey = value;
        }
    }

    public bool PunchKey
    {
        get
        {
            return playerCommand.PunchKey;
        }
        set
        {
            playerCommand.PunchKey = value;
        }
    }

    public bool KickKey
    {
        get
        {
            return playerCommand.KickKey;
        }
        set
        {
            playerCommand.KickKey = value;
        }
    }

    public string ControllerName
    {
        get
        {
            return playerCommand.ControllerName;
        }
        set
        {
            playerCommand.ControllerName = value;
        }
    }

    public int Controller
    {
        get
        {
            return playerCommand.Controller;
        }
        set
        {
            playerCommand.Controller = value;
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

    public bool CanControll
    {
        get
        {
            return canControll;
        }
        set
        {
            canControll = value;
        }
    }

    public int GuardGaugePoint
    {
        get
        {
            return guardGaugePoint;
        }
        set
        {
            guardGaugePoint = value;
        }
    }

    public float NowGravity
    {
        get
        {
            return nowGravity;
        }
        set
        {
            nowGravity = value;
        }
    }

    public GameObject fightEnemy { get { return enemy; } }

    public bool IsHadouCommandMissile { get { return isHadouCommandMissile; } }

    public bool IsSyoryuCommandJump { get { return isSyoryuCommandJump; } }

    public float thisSpeed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float jumpS { get { return ySpeed; } }

    public float thisGravity { get { return gravity; } }

    public string animState { get { return state; } }

    public GameObject GetHadou { get { return playerCommand.HadokenObject; } }

    public bool IsTest { get { return isTest; } }

    public int NowHP { get { return hpDirectorScript.NowHPState; } }

    public Animator AnimatorPlayer { get { return animator; } }
}