using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEffectScript : MonoBehaviour {

    //各種エフェクト
    [SerializeField]
    GameObject effectStr;
    [SerializeField]
    GameObject effectMid;
    [SerializeField]
    GameObject effectWeak;

    [SerializeField, Range(1, 60)]
    private float dissTime = 10;
    private float timeCnt = 0;

    private bool effe = false;

    private Vector3 nowPosition = Vector3.zero;
    private Vector3 attack = Vector3.zero;
    private Vector3 body;
    private Vector3 effectPos;

    private PlayerController PCon;

    // Use this for initialization
    void Start ()
    {
        //エフェクトを止めておく
        effectStr.GetComponent<ParticleSystem>().Stop();
        effectMid.GetComponent<ParticleSystem>().Stop();
        effectWeak.GetComponent<ParticleSystem>().Stop();

        PCon = this.GetComponent<PlayerController>();
    }

    void Update()
    {
        //左を向いている
        if (PCon.Direction == 1)
        {
            //effectWeak.transform.rotation = Quaternion.Euler(0, 90, 0);
            //effectMid.transform.rotation = Quaternion.Euler(0, 90, 0);
            //effectStr.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            //effectWeak.transform.rotation = Quaternion.Euler(180, 90, 0);
            //effectMid.transform.rotation = Quaternion.Euler(180, 90, 0);
            //effectStr.transform.rotation = Quaternion.Euler(180, 90, 0);
        }
    }

    //エフェクト発生関数
    public void appearEffe(int atkLev, Vector3 pos)
    {
        //エフェクト発生
        switch (atkLev)
        {
            case 1:
                effectWeak.transform.position = pos;
                effectWeak.GetComponent<ParticleSystem>().Play();
                break;
            case 2:
                effectMid.transform.position = pos;
                effectMid.GetComponent<ParticleSystem>().Play();
                break;
            case 3:
            case 4:
                effectStr.transform.position = pos;
                effectStr.GetComponent<ParticleSystem>().Play();
                break;
            default:
                //エラー終了
                Application.Quit();
                break;
        }
        effe = true;
    }

    //エフェクト停止関数
    public void disappearEffe()
    {
        if (dissTime >= timeCnt && effe)
        {
            timeCnt++;
        }
        else
        {
            effectWeak.GetComponent<ParticleSystem>().Stop();
            effectMid.GetComponent<ParticleSystem>().Stop();
            effectStr.GetComponent<ParticleSystem>().Stop();
            timeCnt = 0;
            effe = false;
        }
    }
}
