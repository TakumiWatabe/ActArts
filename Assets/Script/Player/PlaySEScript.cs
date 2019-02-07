using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySEScript : MonoBehaviour {

    private AudioSource audio;

    //通常攻撃SE
    [Header("------各種効果音------")]
    [SerializeField, Header("通常攻撃")]
    private AudioClip attack1;
    [SerializeField]
    private AudioClip attack2;
    //波動拳SE
    [SerializeField, Header("波動拳")]
    private AudioClip hadou;
    //昇竜拳SE
    [SerializeField, Header("昇竜拳")]
    private AudioClip syoryu;
    //ダメージSE
    [SerializeField, Header("ダメージ")]
    private AudioClip damage1;
    [SerializeField]
    private AudioClip damage2;
    //ガードSE
    [SerializeField, Header("ガード")]
    private AudioClip guard;
    //勝利SE
    [SerializeField, Header("勝利")]
    private AudioClip win;
    //負けSE
    [SerializeField, Header("敗北")]
    private AudioClip lose;
    //ガークラSE
    [SerializeField, Header("ガードクラッシュ")]
    private AudioClip guardCrash;


    [Header("------各種ボイス------")]
    //通常攻撃
    [SerializeField, Header("通常攻撃")]
    private AudioClip attackVoice1;
    [SerializeField]
    private AudioClip attackVoice2;
    //波動拳
    [SerializeField, Header("波動拳")]
    private AudioClip hadouVoice;
    //昇竜拳
    [SerializeField, Header("昇竜拳")]
    private AudioClip syoryuVoice;
    //ダメージ
    [SerializeField, Header("ダメージ")]
    private AudioClip damageVoice1;
    [SerializeField]
    private AudioClip damageVoice2;
    //ガード
    [SerializeField, Header("ガード")]
    private AudioClip guardVoice;
    //勝利
    [SerializeField, Header("勝利")]
    private AudioClip winVoice;
    //負け
    [SerializeField, Header("敗北")]
    private AudioClip loseVoice;
    //ガークラ
    [SerializeField, Header("ガードクラッシュ")]
    private AudioClip guardCrashVoice;

    [SerializeField]
    private List<AudioClip> se;
    [SerializeField]
    private List<AudioClip> voice;

    public enum SEData
    {
        ATTACK1 = 0,
        ATTACK2,
        HADOU,
        SYORYU,
        DAMAGE1,
        DAMAGE2,
        GUARD,
        WIN,
        LOSE,
        GUARDCRASH,
        NUM
    }

    public enum VoiceData
    {
        ATTACK1 = 0,
        ATTACK2,
        HADOU,
        SYORYU,
        DAMAGE1,
        DAMAGE2,
        GUARD,
        WIN,
        LOSE,
        GUARDCRASH,
        NUM
    }

    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();

        se.Add(attack1);
        se.Add(attack2);
        se.Add(hadou);
        se.Add(syoryu);
        se.Add(damage1);
        se.Add(damage2);
        se.Add(guard);
        se.Add(win);
        se.Add(lose);
        se.Add(guardCrash);

        voice.Add(attackVoice1);
        voice.Add(attackVoice2);
        voice.Add(hadouVoice);
        voice.Add(syoryuVoice);
        voice.Add(damageVoice1);
        voice.Add(damageVoice2);
        voice.Add(guardVoice);
        voice.Add(winVoice);
        voice.Add(loseVoice);
        voice.Add(guardCrashVoice);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySE(int num)
    {
        audio.PlayOneShot(se[num],1.0f);
    }

    public void PlayVoice(int num)
    {
        //必ず鳴らすボイス以外は一定の確率で鳴らす
        if(num != (int)VoiceData.HADOU && num != (int)VoiceData.SYORYU && num != (int)VoiceData.WIN && num != (int)VoiceData.LOSE)
        {
            if (Random.Range(0, 2) != 0) return;
            else audio.Stop();
        }
        else audio.Stop();
        audio.PlayOneShot(voice[num],1.0f);
    }
}
