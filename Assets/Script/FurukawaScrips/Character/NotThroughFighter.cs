using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotThroughFighter : MonoBehaviour
{

    //重なり判定処理(PlayerControllerの仕様によって変更あり)

    private OverlapScript OScript;
    private OverReact over;
    //自分コントローラー
    private PlayerController PContr;
    private GameObject enemyF;

    //壁と認識する範囲
    [SerializeField]
    private float rightWall;
    [SerializeField]
    private float leftWall;

    //速度補正
    private const float correctionSpd = 9f;
    //速度変換用
    private const float conversion = -1.0f;

    [SerializeField, Range(1, 0)]
    private float contactArea = 0.5f;

    private Camera mainCamera;

    void Awake()
    {
        OScript = this.GetComponent<OverlapScript>();
    }

    // Use this for initialization
    void Start()
    {
        GameObject obj = GameObject.Find("Main Camera");
        mainCamera = obj.GetComponent<Camera>();
        over = OScript.PClid.GetComponent<OverReact>();

        PContr = this.GetComponent<PlayerController>();
        enemyF = PContr.fightEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        rightWall = getScreenBottomRight().x;
        leftWall = getScreenTopLeft().x;
        //画面外に出ないようにする
        Mathf.Clamp(this.transform.position.x, leftWall, rightWall);
        //inStage();
        //Debug.Log(PContr.jumpS);

        //重なり判定に触れているなら
        if (over.contacting)
        {
            //押し出す
            //お互いが触れたときに速度を90%に下げる
            castumSpeed(PContr);

            //ジャンプ時に触れているなら左右に落とす
            if (PContr.jumpS < 0)
            {
                //落とす
                contactMove();
            }

            //貫通しない処理
            noThrough(PContr);
        }
    }

    //ジャンプ時に押し出す処理
    private void contactMove()
    {
        if (enemyF.transform.position.x >= leftWall || enemyF.transform.position.x <= -rightWall)
        {
            if (enemyF.transform.position.x >= leftWall)
            {
                this.transform.position += new Vector3(-0.05f, 0, 0);
            }
            if (enemyF.transform.position.x <= -rightWall)
            {
                this.transform.position += new Vector3(0.05f, 0, 0);
            }
        }
    }

    //画面外に行かない処理
    private void inStage()
    {
        if (this.transform.position.x >= leftWall || this.transform.position.x <= -rightWall)
        {
            if (this.transform.position.x > leftWall)
            {
                this.transform.position = new Vector3(leftWall, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.position.x < -rightWall)
            {
                this.transform.position = new Vector3(-rightWall, this.transform.position.y, this.transform.position.z);
            }
        }
    }

    //押し出す処理
    private void castumSpeed(PlayerController PcontrObj)
    {
        //速度に補正をかける
        PcontrObj.thisSpeed = PcontrObj.thisSpeed * correctionSpd;
    }

    //ダッシュ中に貫通しない処理
    private void noThrough(PlayerController PcontrObj)
    {
        if (PcontrObj.thisSpeed != 0)
        {
            //若干の速度対抗
            enemyF.GetComponent<PlayerController>().thisSpeed = PcontrObj.thisSpeed / correctionSpd;
        }
    }

    public float LWall { get { return leftWall; } }
    public float RWall { get { return rightWall; } }
    private Vector3 getScreenTopLeft()
    {
        // 画面の左上を取得
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        // 上下反転させる
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }

    private Vector3 getScreenBottomRight()
    {
        // 画面の右下を取得
        Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        // 上下反転させる
        bottomRight.Scale(new Vector3(1f, -1f, 1f));
        return bottomRight;
    }
}
