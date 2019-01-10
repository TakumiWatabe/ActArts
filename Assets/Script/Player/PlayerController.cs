using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;
using System;

public class PlayerController : MonoBehaviour
{
    //コマンドのスクリプト
    PlayerCommand playerCommand;

    float speed = 0;
    [SerializeField]
    bool isTest = false;
    //歩くスピード
    [SerializeField]
    float walkSpeed = 0.03f;
    //走るスピード
    [SerializeField]
    float dashSpeed = 0.08f;
    //重力
    float gravity = 0.008f;
    //状態
    [SerializeField]
    string state = "Stand";
    //必殺技の状態
    string specialState = "";

    //ジャンプのスピード
    [SerializeField]
    float jumpSpeed = 0.15f;
    //昇竜のスピード
    [SerializeField]
    float syoryuSpeed = 0.175f;

    //キャラ分別
    [SerializeField]
    string charName;

    //ダメージ受けたときに下がる距離
    float backDistance = 0;
    float backingDistance = 0;
    int damageDir = 0;

    int damageCount = 0;
    int damageTime = 0;

    //向き
    int direction = 1;
    //相手
    [SerializeField]
    private GameObject enemy;

    //相手スクリプト
    PlayerController enemyScript;

    //ガードする距離
    public float distanceToGuard = 0.7f;

    //効果音
    private AudioSource audio;

    public AudioClip largeDmg;
    public AudioClip midiumDmg;
    public AudioClip lowDmg;


    //アニメーター
    Animator animator;

    //コントローラ操作が可能か
    [SerializeField]
    bool canControll = true;

    //ガードクラッシュ時にひるむ時間
    [SerializeField]
    int guardCrashTime = 120;
    int guardCrashCount = 0;

    //ガードゲージ
    [SerializeField]
    int guardGaugePoint = 5000;

    //波動コマンド成立時に飛び道具を飛ばすか
    [SerializeField]
    bool isHadouCommandMissile = false;

    //昇竜コマンド成立時にジャンプするか
    [SerializeField]
    bool isSyoryuCommandJump = false;

    //最終的な移動距離
    [SerializeField]
    Vector3 finalMove = new Vector3(0, 0, 0);
    //ジャンプをしてから地面につかない間
    [SerializeField]
    int jumpTime = 10;
    [SerializeField]
    int jumpCount = 0;
    //縦方向の速度
    [SerializeField]
    float ySpeed = 0;
    //今の重力
    [SerializeField]
    float nowGravity;

    //キャラクター生成オブジェクト
    private GameObject contl;
    private InstanceScript InScript;

    void Awake()
    {
        if (!isTest)
        {
            contl = GameObject.Find("FighterComtrol");
            InScript = contl.GetComponent<InstanceScript>();
        }

    }

    // Use this for initialization
    void Start()
    {
        playerCommand = GetComponent<PlayerCommand>();
        


        animator = GetComponent<Animator>();

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


        

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //text.text = "";

        //gameObject.transform.position = new Vector3(gameObject.transform.position.x + speed, 0, 0);
        //Debug.Log(gameObject.name + "update1");

        SetDirection();

        string name = playerCommand.controllerName;

        //コントローラーがAIじゃないなら入力を検知する
        if (name != "AI")
        {
            if (canControll) playerCommand.InputKey();
        }

        if(state != "GuardCrash") guardCrashCount = 0;


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
            case "GuardCrash":
                GuardCrash();
                break;
        }

        FinallyMove();
        CheckGuard();

        //Debug.Log(gameObject.name + "update");

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
            state = "Stand";
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
            state = "Stand";
            Debug.Log("ガガガ戻れたぞ");
            canControll = true;
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
        animator.SetInteger("Damage", 0);

        //上が押されたらジャンプ
        if (playerCommand.InputDKey >= 7)
        {
            nowGravity = 0;
            state = "Jump";
            //animator.SetBool("Jump", true);
        }

        //立ち状態時にZを押すとパンチ
        if (playerCommand.PunchKey)
        {
            animator.SetBool("Punch", true);
            state = "Punch";
        }
        //立ち状態時にXを押すとキック
        if (playerCommand.KickKey)
        {
            animator.SetBool("Kick", true);
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
            state = "SitPunch";
        }
        //しゃがみ状態時にXを押すとキック
        if (playerCommand.KickKey)
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
            state = "Punch";
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, -1, 0.0f);
        }

        //立ち状態時にXを押すとキック
        if (playerCommand.KickKey)
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

        animator.SetBool("Punch", true);
        finalMove = new Vector3(0, 0, 0);

        //しゃがみ状態時にZを押すとパンチ
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && (playerCommand.PunchKey))
        {
            animator.SetBool("Punch", true);
            state = "SitPunch";
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, -1, 0.0f);
        }

        //しゃがみ状態時にXを押すとキック
        if (playerCommand.KickKey)
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
                animator.SetBool("Punch", true);
            }
            //ジャンプキック
            if (playerCommand.KickKey && !animator.GetBool("Punch") && !animator.GetBool("Kick"))
            {
                animator.SetBool("Kick", true);
            }
        }
        else
        {
            //ジャンプ終わり
            animator.SetBool("Jump", false);
            ySpeed = 0;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);
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
            state = "Punch";
        }
        //立ち状態時にXを押すとキック
        if (playerCommand.KickKey)
        {
            animator.SetBool("Kick", true);
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
        if ((enemyScript.state == "Punch" || enemyScript.state == "Kick") && distanceToGuard > Mathf.Abs(distanceToEnemy) && playerCommand.InputDKey == 4 && (state == "Stand" || state == "Sit"))
        {
            animator.SetBool("Guard", true);
            state = "StandGuard";
        }
        //しゃがみガード
        if ((enemyScript.state == "Punch" || enemyScript.state == "Kick") && distanceToGuard > Mathf.Abs(distanceToEnemy) && playerCommand.InputDKey == 1 && (state == "Stand" || state == "Sit"))
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
        SpecialMove();

        //最終的な移動
        Vector3 finalPos = finalMove + gameObject.transform.position;
        finalPos.z = 0;
        if (finalPos.x <= 3.0f && finalPos.x >= -3.0f) gameObject.transform.position = finalPos;
        else gameObject.transform.position = new Vector3(gameObject.transform.position.x, finalPos.y, 0);

        if (jumpCount == 0 && state != "JumpingDamage") gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);

        if (gameObject.transform.position.y < 0) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);

    }

    public void HitDamage(int dmg)
    {
        if ((state == "Stand" || state == "Sit" || state == "Guard" || state == "SitGuard" || state == "StandGuard") && state != "Jump" && (playerCommand.InputDKey == 1 || playerCommand.InputDKey == 4))
        {
            SpecialMove();
            Debug.Log(state);
            animator.SetBool("Guard", true);
            state = "Guard";

            damageTime = (dmg / 500 + 15) / 3;
            damageDir = direction * -1;

            guardGaugePoint -= dmg;
        }
        else
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
                state = "JumpingDamage";
            }

            backDistance = dmg;
            damageTime = dmg / 500 + 15;
            damageDir = direction * -1;

            AudioClip sound;

            sound = lowDmg;
            if (dmg > 500) sound = midiumDmg;
            if (dmg > 800) sound = largeDmg;

            audio.PlayOneShot(sound);
        }

    }

    /// <summary>
    /// 必殺技による移動調整の処理
    /// </summary>
    void SpecialMove()
    {
        if (state == "Special")
        {
            Vector3 pos = transform.position;
            if (gameObject.transform.position.x + transform.GetChild(2).localPosition.z * direction >= 3.0f)
            {
                pos.x = 3.0f - transform.GetChild(2).localPosition.z * direction;
            }
            if (gameObject.transform.position.x + transform.GetChild(2).localPosition.z * direction <= -3.0f)
            {
                pos.x = -3.0f + transform.GetChild(2).localPosition.z;
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
}