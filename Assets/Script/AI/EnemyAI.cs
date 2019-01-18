////////////////////////////////
// Creater : Masato Yamagishi //
// Data    : 10/30            //
////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum BEHAVE
    {
        wATTACK = 0,
        sATTACK,
        swATTACK,
        ssATTACK,
        HADOU,
        SHOURYU,
        GUARD,
        sGUARD,
        WOLK,
        BACK,
        DUSH,
        JUMP,
        fJUMP,
        bJUMP
    }
    private GameObject enemy;
    private float enemyDis = 0.0f;
    private int elapsedTime = 0;
    private Animator animator;
    private PlayerController pc;
    private PlayerCommand pcc;

    private AIIntention intention;

    [SerializeField, Header("移動系行動の判定間隔")]
    private int judgTime = 20;
    [SerializeField, Header("距離の最大値")]
    private float maxDis;

    private bool isLearning = new bool();
    private bool isLearn = true;

    // Use this for initialization
    void Start()
    {
        //pc = gameObject.GetComponent<PlayerController>();
        //pcc = gameObject.GetComponent<PlayerCommand>();
        //enemy = pc.fightEnemy;
        //pc.ControllerName = "AI";
        //pcc.controllerName = "AI";
        //animator = GetComponent<Animator>();
    }

    public void Initialize()
    {
        pc = gameObject.GetComponent<PlayerController>();
        pcc = gameObject.GetComponent<PlayerCommand>();
        enemy = pc.fightEnemy;
        if (pcc.ControllerName == "")
        {
            pc.ControllerName = "AI";
            pcc.controllerName = "AI";

            //片方がAIだったら学習しない
            if (enemy.GetComponent<PlayerCommand>().controllerName == "AI")
            {
                isLearn = false;
            }
        }
        else
        {
            this.enabled = false;
        }
        animator = GetComponent<Animator>();

        intention = GameObject.Find(gameObject.name.Replace("(Clone)", "") + "IntentionObj").GetComponent<AIIntention>();

        intention.Initialize(gameObject, enemy, 3);
        isLearning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLearning)
        {
            elapsedTime++;

            pc.PunchKey = false;
            pc.KickKey = false;

            //時間経過でニューラルネットワークによる意思決定を行う
            if (elapsedTime >= judgTime)
            {
                JudgResult("", "");
                DecideBehavior();
            }

            if (Input.GetKeyDown(KeyCode.Space) && isLearn) isLearning = intention.Learning(isLearning);
        }
        else
        {
            isLearning = intention.Learning(isLearning);
        }
    }

    /// <summary>
    /// 行動決定をする
    /// </summary>
    private void DecideBehavior()
    {
        //相手との距離を計算
        enemyDis = gameObject.transform.position.x - enemy.transform.position.x;
        if (enemyDis < 0.0f)
            enemyDis *= -1.0f;
        enemyDis /= maxDis;

        //状況を数値化
        intention.JudgSituation(enemyDis);

        //数値化された状況を基に意思を決定し行動する
        switch (intention.DecideIntention())
        {
            case (int)BEHAVE.wATTACK:
                pc.InputDKey = 5;
                pc.PunchKey = true;
                break;
            case (int)BEHAVE.sATTACK:
                pc.InputDKey = 5;
                pc.KickKey = true;
                break;
            case (int)BEHAVE.swATTACK:
                pc.InputDKey = 2;
                pc.PunchKey = true;
                break;
            case (int)BEHAVE.ssATTACK:
                pc.InputDKey = 2;
                pc.KickKey = true;
                break;
            case (int)BEHAVE.HADOU:
                pcc.history[0] = "2";
                pcc.history[1] = "3";
                pcc.history[2] = "6";
                pcc.history[3] = "P";
                break;
            case (int)BEHAVE.SHOURYU:
                pcc.history[0] = "6";
                pcc.history[1] = "2";
                pcc.history[2] = "3";
                pcc.history[3] = "P";
                break;
            case (int)BEHAVE.GUARD:
                pc.InputDKey = 4;
                break;
            case (int)BEHAVE.sGUARD:
                pc.InputDKey = 1;
                break;
            case (int)BEHAVE.WOLK:
                pc.InputDKey = 6;
                break;
            case (int)BEHAVE.BACK:
                pc.InputDKey = 4;
                break;
            case (int)BEHAVE.DUSH:
                pc.InputDKey = 6;
                pc.State = "Dash";
                break;
            case (int)BEHAVE.JUMP:
                pc.InputDKey = 8;
                break;
            case (int)BEHAVE.fJUMP:
                pc.InputDKey = 9;
                break;
            case (int)BEHAVE.bJUMP:
                pc.InputDKey = 7;
                break;
            default:
                pc.InputDKey = 5;
                break;
        }

        elapsedTime = 0;
    }

    /// <summary>
    /// 結果の判定
    /// </summary>
    /// <param name="result">行動の結果</param>
    public void JudgResult(string result,string Estate)
    {
        //相手との距離を計算
        enemyDis = gameObject.transform.position.x - enemy.transform.position.x;
        if (enemyDis < 0.0f)
            enemyDis *= -1.0f;
        enemyDis /= maxDis;

        intention.CalcTeachData(result, Estate, enemyDis);
    }
}