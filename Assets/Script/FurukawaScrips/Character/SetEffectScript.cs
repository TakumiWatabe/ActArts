using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetEffectScript : MonoBehaviour {

    //各種エフェクト
    [SerializeField]
    GameObject effectStr;
    [SerializeField]
    GameObject effectMid;
    [SerializeField]
    GameObject effectWeak;

    private GameObject dirSys;
    private TextGenerator textGene;
    private GameObject BattleText;

    private Vector3 nowPosition = Vector3.zero;
    private Vector3 attack = Vector3.zero;
    private Vector3 body;
    private Vector3 effectPos;

    // Use this for initialization
    void Start ()
    {
        //dirSys = GameObject.Find("TextFactory");
        //textGene = dirSys.GetComponent<TextGenerator>();
        //BattleText = GameObject.Find("GameText");

        //エフェクトを止めておく
        effectStr.GetComponent<ParticleSystem>().Stop();
        effectMid.GetComponent<ParticleSystem>().Stop();
        effectWeak.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update ()
    {
        
	}

    //エフェクト計算関数
    public void caluclation(BoxCollider hitBox, BoxCollider atkBox)
    {
        nowPosition = this.transform.position;

        //あたり判定の計算
        body = hitBox.center + nowPosition;
        attack = atkBox.center + nowPosition;

        //エフェクト発生場所
        effectPos = (body + attack) / 2;
    }

    //エフェクト発生関数
    public void appearEffe(int atkLev)
    {
        //エフェクト発生
        switch (atkLev)
        {
            case 1:
                effectWeak.transform.position = effectPos;
                effectWeak.GetComponent<ParticleSystem>().Play();
                break;
            case 2:
                effectMid.transform.position = effectPos;
                effectMid.GetComponent<ParticleSystem>().Play();
                break;
            case 3:
                effectStr.transform.position = effectPos;
                effectStr.GetComponent<ParticleSystem>().Play();
                break;
            default:
                //エラー終了
                EditorApplication.Exit(0);
                break;
        }
    }

    //エフェクト停止関数
    public void disappearEffe()
    {
        effectWeak.GetComponent<ParticleSystem>().Stop();
        effectMid.GetComponent<ParticleSystem>().Stop();
        effectStr.GetComponent<ParticleSystem>().Stop();
    }
}
