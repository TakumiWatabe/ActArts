using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChar : MonoBehaviour
{

    //攻撃ヒット時処理スクリプト

    //ヒット時攻撃判定ステータス取得用
    private struct HitState
    {
        public int damage;
        public string attri;
        public float startCorr;
        public float comboCorr;
        public int atkLev;
        public int blockStun;
        public int hitStun;
    };

    //敵取得用
    PlayerController Pcont;
    //コライダーイベント
    ColliderEvent CEvent;   //自身
    ColliderEvent CEventEne;   //敵
    //HPディレクター
    HPDirectorScript HPDir;
    //対戦相手取得用
    DebugGetEnemyScript DEGScript;
    //エフェクト発生用
    SetEffectScript SEScript;
    //ガード時の処理用
    GuardScript GScript;

    //あたり判定群
    List<GameObject> col = new List<GameObject>();
    List<ColliderReact> react = new List<ColliderReact>();

    //あたり判定ID
    private int collID;

    //操作キャラクター番号
    private int numID = 0;

    //のけぞり判定時間
    [SerializeField, Range(1, 60)]
    private int time = 5;
    int htime = 0;

    //経過時間
    private float timecCnt = 0;

    private bool hitatk = false;
    private bool guardatk = false;

    //技情報取得用
    private ArtsStateScript ASScript;
    private ArtsStateScript ASScriptEne;

    //技性能
    private HitState hitColSta;

    //搭載されているAI君
    EnemyAI AI;

    //ガードのエフェクト
    [SerializeField]
    private GameObject guardEffect;
    //波動が当たったときのエフェクト
    [SerializeField]
    private GameObject explodeHadouEffect;

    void Awake()
    {
        Pcont = this.GetComponent<PlayerController>();
        ASScript = this.GetComponent<ArtsStateScript>();
    }

    // Use this for initialization
    void Start()
    {
        //ディレクタースクリプト取得
        ASScriptEne = Pcont.fightEnemy.GetComponent<ArtsStateScript>();
        CEvent = this.GetComponent<ColliderEvent>();
        CEventEne = Pcont.fightEnemy.GetComponent<ColliderEvent>();
        HPDir = this.GetComponent<HPDirectorScript>();
        DEGScript = this.GetComponent<DebugGetEnemyScript>();
        SEScript = this.GetComponent<SetEffectScript>();
        GScript = this.GetComponent<GuardScript>();

        //コライダーのスクリプトを取得
        for (int i = 0; i < CEvent.HClid.Count; i++)
        {
            col.Add(CEvent.HClid[i]);
            react.Add(col[i].GetComponent<ColliderReact>());
        }

        AI = gameObject.GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        //攻撃くらい判定
        hitJudg();

        //ガード時判定
        if (Pcont.AnimatorPlayer.GetBool("Guard"))
        {
            guardatk = true;
        }
        else
        {
            guardatk = false;
        }

        //コルーチン処理
        waitRegene(30);
    }

    //攻撃ヒット判定
    private void hitJudg()
    {
        //くらい判定の数
        for (int i = 0; i < CEvent.HClid.Count; i++)
        {
            //攻撃が当たっているなら
            if (react[i].hiting/* && react[i].CObj != null*/)
            {
                hitatk = true;
                react[i].hiting = false;
                //ガードしているなら
                if (guardatk)
                {
                    bool successedGuard = true;
                    if(Pcont.AnimatorPlayer.GetBool("Guard"))
                    {
                        if (Pcont.AnimatorPlayer.GetBool("StandGuard"))
                        {
                            string hitArt = ASScriptEne.Attri((int)CEventEne.GetType);
                            if (hitArt == "Low")
                            {
                                successedGuard = false;
                                //エフェクト発生
                                SEScript.appearEffe(ASScriptEne.AtkLev((int)CEventEne.GetType), react[i].point);
                                //ダメージ分HPゲージを減らす
                                HPDir.hitDmage(ASScriptEne.Damage((int)CEventEne.GetType));
                                Pcont.HitDamage(ASScriptEne.Damage((int)CEventEne.GetType), ASScriptEne.AtkLev((int)CEventEne.GetType), ASScriptEne.HitStun((int)CEventEne.GetType));
                                if (Pcont.ControllerName == "AI")
                                {
                                    AI.JudgResult("Damage", Pcont.fightEnemy.GetComponent<PlayerController>().State);
                                }
                                else if (Pcont.fightEnemy.GetComponent<PlayerController>().ControllerName == "AI")
                                {
                                    Pcont.fightEnemy.GetComponent<EnemyAI>().JudgResult("Damaged", "");
                                }
                            }
                        }
                        else
                        {
                            string hitArt = ASScriptEne.Attri((int)CEventEne.GetType);
                            if (hitArt == "High")
                            {
                                successedGuard = false;
                                //エフェクト発生
                                SEScript.appearEffe(ASScriptEne.AtkLev((int)CEventEne.GetType), react[i].point);
                                //ダメージ分HPゲージを減らす
                                HPDir.hitDmage(ASScriptEne.Damage((int)CEventEne.GetType));
                                Pcont.HitDamage(ASScriptEne.Damage((int)CEventEne.GetType), ASScriptEne.AtkLev((int)CEventEne.GetType), ASScriptEne.HitStun((int)CEventEne.GetType));
                                if (Pcont.ControllerName == "AI")
                                {
                                    AI.JudgResult("Damage", Pcont.fightEnemy.GetComponent<PlayerController>().State);
                                }
                                else if (Pcont.fightEnemy.GetComponent<PlayerController>().ControllerName == "AI")
                                {
                                    Pcont.fightEnemy.GetComponent<EnemyAI>().JudgResult("Damaged", "");
                                }
                            }
                        }
                    }

                    if(successedGuard)
                    {
                        //ダメージ分ガードゲージを減らす
                        GScript.hitGuard(ASScriptEne.Damage((int)CEventEne.GetType));
                        Vector3 effectPos = transform.position;
                        effectPos.y += 1.0f;
                        Instantiate(guardEffect, effectPos, Quaternion.identity);
                        if (Pcont.ControllerName == "AI")
                        {
                            AI.JudgResult("Guard", "");
                        }
                        else if (Pcont.fightEnemy.GetComponent<PlayerController>().ControllerName == "AI")
                        {
                            Pcont.fightEnemy.GetComponent<EnemyAI>().JudgResult("WasGuarded", Pcont.State);
                        }
                    }

                }
                else
                {
                    bool successedGuardBullet = false;
                    string art = ASScriptEne.Attri((int)CEventEne.GetType);
                    if (art == "Bullet")
                    {
                        if (Pcont.InputDKey == 4 && (Pcont.State == "Stand" || Pcont.State == "Sit"))
                        {
                            successedGuardBullet = true;
                            Pcont.AnimatorPlayer.SetBool("Guard", true);
                            Pcont.State = "StandGuard";
                            //ダメージ分ガードゲージを減らす
                            GScript.hitGuard(ASScriptEne.Damage((int)CEventEne.GetType));
                            Vector3 effectPos = transform.position;
                            effectPos.y += 1.0f;
                            Instantiate(guardEffect, effectPos, Quaternion.identity);
                            if (Pcont.ControllerName == "AI")
                            {
                                AI.JudgResult("Guard", "");
                            }
                            else if (Pcont.fightEnemy.GetComponent<PlayerController>().ControllerName == "AI")
                            {
                                Pcont.fightEnemy.GetComponent<EnemyAI>().JudgResult("WasGuarded", Pcont.State);
                            }
                        }
                        else if (Pcont.InputDKey == 1 && (Pcont.State == "Stand" || Pcont.State == "Sit"))
                        {
                            successedGuardBullet = true;
                            Pcont.AnimatorPlayer.SetBool("Guard", true);
                            Pcont.State = "SitGuard";
                            //ダメージ分ガードゲージを減らす
                            GScript.hitGuard(ASScriptEne.Damage((int)CEventEne.GetType));
                            Pcont.GuardDamage(ASScriptEne.BlockStun((int)CEventEne.GetType));
                            Vector3 effectPos = transform.position;
                            effectPos.y += 1.0f;
                            Instantiate(guardEffect, effectPos, Quaternion.identity);
                            if (Pcont.ControllerName == "AI")
                            {
                                AI.JudgResult("Guard", "");
                            }
                            else if (Pcont.fightEnemy.GetComponent<PlayerController>().ControllerName == "AI")
                            {
                                Pcont.fightEnemy.GetComponent<EnemyAI>().JudgResult("WasGuarded", Pcont.State);
                            }
                        }
                    }
                    if(!successedGuardBullet)
                    {
                        //エフェクト発生
                        SEScript.appearEffe(ASScriptEne.AtkLev((int)CEventEne.GetType), react[i].point);
                        //ダメージ分HPゲージを減らす
                        HPDir.hitDmage(ASScriptEne.Damage((int)CEventEne.GetType));
                        Pcont.HitDamage(ASScriptEne.Damage((int)CEventEne.GetType), ASScriptEne.AtkLev((int)CEventEne.GetType), ASScriptEne.HitStun((int)CEventEne.GetType));
                        if (Pcont.ControllerName == "AI")
                        {
                            AI.JudgResult("Damage", Pcont.fightEnemy.GetComponent<PlayerController>().State);
                        }
                        else if (Pcont.fightEnemy.GetComponent<PlayerController>().ControllerName == "AI")
                        {
                            Pcont.fightEnemy.GetComponent<EnemyAI>().JudgResult("Damaged", "");
                        }
                    }


                }


                //攻撃を食らったあたり判定のIDを取得
                collID = i;
                //飛び道具消失
                toolVoid();

                //受けた攻撃のステータス
                CreateArtsSatet(ASScriptEne, (int)CEventEne.GetType);

            }

            //攻撃を食らっているなら
            if (react[i].CObj != null)
            {
                //react[i].CObj.SetActive(false);

                //のけぞり時間中ならあたり判定しない
                if (time >= timecCnt)
                {
                    timecCnt++;
                }
                else if (time < timecCnt)
                {
                    //のけぞり時間外になったらあたり判定する
                    timecCnt = 0;
                    collID = CEvent.HClid.Count;
                    react[i].CObj = null;
                }
            }
        }
        //エフェクトを止める
        SEScript.disappearEffe();
    }

    //飛び道具消失判定
    void toolVoid()
    {
        //攻撃判定が飛び道具なら
        if (CEventEne.GetType == ValueScript.AtkVal.HADOUKEN)
        {
            Instantiate(explodeHadouEffect, transform.position, Quaternion.identity);

            //飛び道具を消す
            Debug.Log(Pcont.fightEnemy.GetComponent<PlayerController>().GetHadou.name);
            Destroy(Pcont.fightEnemy.GetComponent<PlayerController>().GetHadou);
        }
    }

    //ガード値を徐々に回復させるための遅延時間
    private void waitRegene(int waitTime)
    {
        if (GScript.fFlag)
        {
            htime = 0;
        }

        if (waitTime > htime)
        {
            htime++;
        }
        else if (waitTime <= htime)
        {
            if (GScript.NowGuardVal < 1000 && GScript.NowGuardVal > 0)
            {

                //ガード値を徐々に回復
                GScript.guardRegene();
            }
        }
    }

    //攻撃時の性能を取得
    private void CreateArtsSatet(ArtsStateScript arts, int type)
    {
        hitColSta.damage = arts.Damage(type);
        hitColSta.attri = arts.Attri(type);
        hitColSta.startCorr = arts.StartCorr(type);
        hitColSta.comboCorr = arts.CombCorr(type);
        hitColSta.atkLev = arts.AtkLev(type);
        hitColSta.blockStun = arts.BlockStun(type);
        hitColSta.hitStun = arts.HitStun(type);
    }

    //変数取得関数
    public bool hitDamage
    {
        get { return hitatk; }
        set { hitatk = value; }
    }

    public int CID { get { return collID; } }

    //データ取得
    public int Damage { get { return hitColSta.damage; } }
    public string Attri { get { return hitColSta.attri; } }
    public float StartCorr { get { return hitColSta.startCorr; } }
    public float CombCorr { get { return hitColSta.comboCorr; } }
    public int AtkLev { get { return hitColSta.atkLev; } }
    public int BlockStun { get { return hitColSta.blockStun; } }
    public int HitStun { get { return hitColSta.hitStun; } }
}
