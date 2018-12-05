using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject BattleText;

    private float gameTimer = 99;
    private float timer = 0;
    //private Text text;

    bool stopFlag = false;

    bool endFlag = false;
	// Use this for initialization
	void Start ()
    {
        //text = GetComponent<Text>();
	}

    // Update is called once per frame
    void Update()
    {
        // ゲーム内の時間制限用タイマー
        timer += Time.unscaledDeltaTime;


        // ゲームが始まっていたら
        if (stopFlag && BattleText.activeSelf == false)
        {
            // 制限時間が0でなければ
            if (gameTimer >= 0)
            {
                // 制限時間を減少
                gameTimer -= Time.unscaledDeltaTime;
            }
            else
            {
                endFlag = true;
            }
        }

        //text.text = gameTimer.ToString("F0");
    }
    // 制限時間を取得
    public float GetGameTimer()
    {
        return gameTimer;
    }
    // 現在のゲーム時間を取得
    public float GetTimer()
    {
        return timer;
    }
    // 制限時間をリセット
    public void ResetGameTimer()
    {
        gameTimer = 99;
        endFlag = false;
    }
    // 制限時間を減らすかどうかの判断
    public void SwithGameTimer()
    {
        stopFlag = !stopFlag;
    }

    public bool fightEnd { get { return endFlag; } }
}
