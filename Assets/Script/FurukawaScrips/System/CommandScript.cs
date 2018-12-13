using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandScript : MonoBehaviour {

    //入力からコマンドを生成するスクリプト

    private InputInfoScript IIScript;

    //次入力判定の猶予
    [SerializeField, Range(0, 60)]
    private int graceInput = 15;
    private int graceTime = 0;

    //入力ID
    private string inputID = " ";
    //入力履歴
    private string command = "";
    //コマンド記憶変数
    private string lastComm = "";


    // Use this for initialization
    void Start ()
    {
        IIScript = this.GetComponent<InputInfoScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        inputID = IIScript.motionState.ToString();
        //コマンド入力履歴記憶
        createCommand(inputID);
    }

    //コマンド取得関数
    public void createCommand(string id)
    {
        //フレーム加算
        graceTime++;

        //コマンド入力猶予内なら
        if (graceTime < graceInput)
        {
            //前回のコマンドと入力したコマンドが異なっていれば
            if (lastComm != inputID)
            {
                //入力値を記憶
                lastComm = inputID;
                if (lastComm != "5")
                {
                    //コマンド履歴に文字列として追加
                    command += inputID;
                }
                graceTime = 0;
            }
        }
        if (graceTime >= graceInput)
        {
            //変数初期化
            lastComm = "";
            command = "";
            graceTime = 0;
        }
    }

    //画面にデバック表示
    private void OnGUI()
    {
        GUILayout.Label("Command: " + command);
    }

    //コマンドを返す
    public string getCom { get { return inputID; } }
    public string getincom { get { return command; } }
}
