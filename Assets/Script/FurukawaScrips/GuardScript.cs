using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardScript : MonoBehaviour {

    private PlayerController PCont;

    //ガードゲージ画像
    [SerializeField]
    Image guardImage;

    //カード値
    [SerializeField]
    private int maxGuard;
    private int nowGuard;

    //ガード値自動回復判定
    private bool reGuard = false;
    private bool f = false;

	// Use this for initialization
	void Start ()
    {
        PCont = this.GetComponent<PlayerController>();
        nowGuard = maxGuard;

        //プレイヤーによって使用するゲージを設定
        switch (this.tag)
        {
            case "P1":
                guardImage = GameObject.Find("1PGuard").GetComponent<Image>();
                break;
            case "P2":
                guardImage = GameObject.Find("2PGuard").GetComponent<Image>();
                break;
        }
	}

    void Update()
    {
        f = false;
        //ゲージを増減させる
        transeGage();
    }

    //ダメージをガード値に反映
    public void hitGuard(int damage)
    {
        Debug.Log("防いでいます");
        f = true;
        nowGuard = nowGuard - damage;
        nowGuard = Mathf.Clamp(nowGuard, 0, maxGuard);
    }

    private void transeGage()
    {
        float GuardGage = (float)nowGuard / maxGuard;

        guardImage.transform.localScale = new Vector3(GuardGage, 1, 1);
    }

    //攻撃をガードしていないならガード値を徐々に回復させる関数
    public void guardRegene()
    {
        if (!(nowGuard >= maxGuard))
        {
            nowGuard++;
        }
        else
        {
            nowGuard = maxGuard;
        }
    }

    //現在のガード値を取得
    public int NowGuardVal
    {
        get { return nowGuard; }
        set { nowGuard = value; }
    }

    //ガード値の最大値を取得
    public int MaxGuardVal { get { return maxGuard; } }

    //ガードゲージの幅を取得
    public Vector3 GuardSca
    {
        get { return guardImage.transform.localScale; }
        set { guardImage.transform.localScale = value; }
    }

    public bool fFlag
    {
        get { return f; }
        set { f = value; }
    }
}
