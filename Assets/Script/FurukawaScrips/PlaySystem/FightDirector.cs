using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightDirector : MonoBehaviour
{

    //戦闘開始を宣言するスクリプト
    //戦闘開始オブジェクト
    [SerializeField]
    private GameObject dirctor;
    private TimerScript timer;

    private FadeImage fade;

    // 戦闘開始表示用
    private float baseTime = 1;
    private float time = 0;
    private float count = 0.05f;

    private bool anim = false;
    private bool flag = true;

    //画像
    [SerializeField]
    private Image fight;

    private Vector3 scale;
    private Color clear = new Color(1, 1, 1, 0);

    private GameDirector gameDirector;

    private AudioSource audio;
    [SerializeField]
    private AudioClip fightVoice;

    // Use this for initialization
    void Start()
    {
        audio = this.GetComponent<AudioSource>();

        gameDirector = this.GetComponent<GameDirector>();

        timer = dirctor.GetComponent<TimerScript>();
        fade = this.GetComponent<FadeImage>();

        timer.SwithGameTimer = false;

        //拡大しておく
        fight.transform.localScale = new Vector3(3, 3, 3);
        scale = fight.transform.localScale;

        //設定
        fight.color = clear;
    }

    public void Initialize()
    {
        baseTime = 1;
        time = 0;

        timer.SwithGameTimer = false;
        count = 0.05f;

        anim = false;
        flag = true;

        //拡大しておく
        fight.transform.localScale = new Vector3(3, 3, 3);
        scale = fight.transform.localScale;

        //設定
        fight.color = clear;

        gameDirector.PlayersCanControlSet(false);
    }

    public void FightDisplay()
    {
        //戦闘開始画像表示
        if (flag)
        {
            if (!fade.Flag && fight.color.a <= 0)
            {
                fade.Flag = true;
                gameDirector.PlayersCanControlSet(true);
                audio.PlayOneShot(fightVoice, 1.0f);
            }
            FightAnim();
        }
        if (!fade.Flag && fight.color.a < 0)
        {
            flag = false;
        }

        if (fight.transform.localScale.x <= 1) { anim = true; }
        if (fight.color.a <= 0 && anim) { timer.SwithGameTimer = true; }
    }

    //戦闘開始画像のアニメーション
    private void FightAnim()
    {
        //フェードイン/アウトを行う
        fade.doFade(fight, 5);
        //縮小
        shrink(fight);
    }

    //拡縮
    private void shrink(Image image)
    {
        Vector3 vec;
        if (time <= baseTime) { time += count; }

        //縮小
        vec = Vector3.Lerp(scale, Vector3.one, time);
        image.transform.localScale = vec;
    }
}
