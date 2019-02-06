using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class CancelScript : MonoBehaviour {

    CharacterSelect selectPlayer1;
    CharacterSelect selectPlayer2;
    bool enterFlag;
    bool timerFlag;
    float timer;
    // Use this for initialization
    void Start () {
        selectPlayer1 = GameObject.Find("P1Image").GetComponent<CharacterSelect>();
        selectPlayer2 = GameObject.Find("P2Image").GetComponent<CharacterSelect>();
        enterFlag = true;
    }

    // Update is called once per frame
    void Update () {
        // 両プレイヤーがキャラクターを選択した
        if ((selectPlayer1.GetP1Frag() == false) && (selectPlayer2.GetP2Frag() == false))
        {
            if (GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.One))
            {
                enterFlag = false;
            }

        }
        // どちらかがまだ選択していない
        else
        {
            enterFlag = true;
        }
    }
    public bool GetEnterFlag()
    {
        return enterFlag;
    }
}
