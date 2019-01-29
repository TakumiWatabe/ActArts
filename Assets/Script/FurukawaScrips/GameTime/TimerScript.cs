using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField, Range(1, 99)]
    private float gameTimer = 99;
    private float timer = 0;

    bool stopFlag = false;

    bool endFlag = false;
	// Use this for initialization
	void Start ()
    {

	}

    // Update is called once per frame
    void Update()
    {
        // ゲーム内の時間制限用タイマー
        timer += Time.unscaledDeltaTime;

        // ゲームが始まったら
        if (stopFlag)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer <= 0)
            {
                gameTimer = 0;
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
    public bool SwithGameTimer { set { stopFlag = value; } }

    public bool fightEnd { get { return endFlag; } }
}
