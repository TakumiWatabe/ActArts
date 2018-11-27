using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDirectorScript : MonoBehaviour {

    [SerializeField]
    Image hpImage;
    [SerializeField]
    Image dmageImage;

    [SerializeField]
    private int maxHP = 10000;
    private int nowHP;
    [SerializeField]
    private int moveHP = 10000;

    [SerializeField]
    private GameObject opponent;
    PlayerController script;

    // Use this for initialization
    void Start()
    {
        switch(this.tag)
        {
            case "P1":
                hpImage = GameObject.Find("1PHP").GetComponent<Image>();
                dmageImage= GameObject.Find("1PDamage").GetComponent<Image>();
                break;
            case "P2":
                hpImage = GameObject.Find("2PHP").GetComponent<Image>();
                dmageImage = GameObject.Find("2PDamage").GetComponent<Image>();
                break;
            default:
                break;
        }

        nowHP = maxHP;
        script = opponent.GetComponent<PlayerController>();
    }

    public void hitDmage(int dmage)
    {
        nowHP = nowHP - dmage;
        nowHP = Mathf.Clamp(nowHP, 0, maxHP);
        float HPgage = (float)nowHP / maxHP;

        hpImage.transform.localScale = new Vector3(HPgage, 1, 1);
    }

    private void moveDmage()
    {
        //HPが減っているなら
        if(nowHP<moveHP)
        {
            if (nowHP > 0)
            {
                //時間差で赤ゲージを減らす
                moveHP -= Mathf.FloorToInt(maxHP * Time.deltaTime * 0.2f);
            }
            else
            {
                //HPが0になったら赤ゲージを消す
                moveHP = 0;
            }
            moveHP = Mathf.Clamp(moveHP, 0, maxHP);
            float dmageGage = (float)moveHP / maxHP;

            dmageImage.transform.localScale = new Vector3(dmageGage, 1, 1);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        moveDmage();
    }

    public int NowHPState { get { return nowHP; } }

    //ゲージ取得関数
    public Vector3 HPScale
    {
        get { return hpImage.transform.localScale; }
        set { hpImage.transform.localScale = value; }
    }

    public Vector3 DamageScale
    {
        get { return dmageImage.transform.localScale; }
        set { dmageImage.transform.localScale = value; }
    }

    //初期化関数
    public void Initialise()
    {
        moveHP = 10000;
        nowHP = maxHP;
    }
}
