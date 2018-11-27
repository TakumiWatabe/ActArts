using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueScript : MonoBehaviour {

    //キャラクター用変数
    public enum FightChar
    {
        CHARA_1,
        CHARA_2,

        CHARA_NUM,
    };

    //アクション用変数
    public enum AtkVal
    {
        PUNCH,
        KICK,
        PUNCH_SIT,
        KICK_SIT,
        PUNCH_JUMP,
        KICK_JUMP,
        HADOUKEN,
        SYORYUKEN,

        ATK_NUM,
    };

    //技の初段ダメージ計算
    public int ArtsDame(int damage, float FPro)
    {
        int proDamage;
        proDamage = (int)(damage * FPro);
        return proDamage;
    }

    //コンボ時のダメージ計算
    public int ComboDame(bool com, int damage,float CPro)
    {
        int combPro = 0;
        if(com)
        {
            combPro = (int)(damage * CPro);
        }
        else
        {
            combPro = 0;
        }
        return combPro;
    }

    //ゲーム内の時間操作関数
    public void GameTime(bool flag)
    {
        if(flag)
        {
            //時を止める
            Time.timeScale = 0;
        }
        else
        {
            //再び時を動かす
            Time.timeScale = 1;
        }
    }
}
