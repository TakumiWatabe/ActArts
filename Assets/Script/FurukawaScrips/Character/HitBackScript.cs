using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBackScript : MonoBehaviour
{
    //攻撃を食らった際のノックバックを行うスクリプト
    private TestChar charState;
    private PlayerController PCon;
    private NotThroughFighter NT;
    private ColliderEvent CEventEne;

    private Vector3 startPos;
    private Vector3 endPos;

    //ノックバック量
    [SerializeField]
    private float weakBack = 0.08f;
    [SerializeField]
    private float middleBack = 0.2f;
    [SerializeField]
    private float strongBack = 0.5f; 

    private float backNum;

    //補間用変数
    private float time = 1;
    private float diff = 0;
    [SerializeField, Range(0, 1)]
    private float count = 0.1f;

	// Use this for initialization
	void Start ()
    {
        charState = this.GetComponent<TestChar>();
        PCon = this.GetComponent<PlayerController>();
        NT = this.GetComponent<NotThroughFighter>();
        CEventEne = PCon.fightEnemy.GetComponent<ColliderEvent>();
    }

    // Update is called once per frame
    void Update ()
    {
        //攻撃を受けたなら
        if (charState.hitDamage)
        {
            setBack(charState.AtkLev);

            //ノックバック位置設定
            if (diff == 0) { setPos(); }
            //経過時間を取得
            diff += count;

            //ノックバックを行う
            if (diff < time && !(this.transform.position.x >= NT.LWall || this.transform.position.x <= -NT.RWall))
            {
                knockBack();
            }
            else if (diff < time && (this.transform.position.x >= NT.LWall || this.transform.position.x <= -NT.RWall) && CEventEne.GetType != ValueScript.AtkVal.HADOUKEN)
            {
                apWallDam();
            }
            else if (diff > time)
            {
                diff = 0;
                charState.hitDamage = false;
            }
        }
	}

    //ノックバック距離を設定する関数
    private void setBack(int aLev)
    {
        //攻撃レベルによって距離を設定
        switch(aLev)
        {
            case 1:
                backNum = weakBack;
                break;
            case 2:
                backNum = middleBack;
                break;
            case 3:
                backNum = strongBack;
                break;
            default:
                break;
        }
    }

    private void setPos()
    {
        //壁に触れているなら
        if (this.transform.position.x >= NT.LWall || this.transform.position.x <= -NT.RWall)
        {
            //相手の座標を参照
            startPos = PCon.fightEnemy.transform.position;
            if (this.transform.position.x >= NT.LWall)
            {
                endPos = new Vector3(PCon.fightEnemy.transform.position.x - backNum, PCon.fightEnemy.transform.position.y, PCon.fightEnemy.transform.position.z);
            }
            if (this.transform.position.x <= -NT.RWall)
            {
                endPos = new Vector3(PCon.fightEnemy.transform.position.x + backNum, PCon.fightEnemy.transform.position.y, PCon.fightEnemy.transform.position.z);
            }
        }
        //壁に触れていないなら
        else if (!(this.transform.position.x >= NT.LWall || this.transform.position.x <= -NT.RWall))
        {
            //自分の座標を参照
            startPos = this.transform.position;
            if (PCon.Direction == 1)
            {
                endPos = new Vector3(this.transform.position.x - backNum, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                endPos = new Vector3(this.transform.position.x + backNum, this.transform.position.y, this.transform.position.z);
            }
        }
    }

    //通常ノックバック
    private void knockBack()
    {
        this.transform.position = Vector3.Lerp(startPos, endPos, diff);
        Debug.Log("危険を感知。後退します。");
    }

    //ダメージ時自分が壁にいる際の処理
    private void apWallDam()
    {
        PCon.fightEnemy.transform.position = Vector3.Lerp(startPos, endPos, diff);
        Debug.Log("壁際を確認。相手を押し出します。");
    }
}
