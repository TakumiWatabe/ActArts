using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReadCSV : MonoBehaviour {

    //CSV読み込みオブジェクト
    [SerializeField]
    private GameObject dir;
    private ReadCSV rc;

    //プレイヤーネーム(大文字)
    [SerializeField]
    private string playerName;

    //技データ集
    private Dictionary<string, ReadCSV.CharaData> artsData;


	// Use this for initialization
	void Start ()
    {
        //CSV読み込みスクリプト取得
        rc = dir.GetComponent<ReadCSV>();

        //キャラクターの技データ集を取得
        artsData = rc.readCSVData(playerName);

        ////技データ集表示
        //rc.WriteCharaDatas(artsData, artsData.Count);

        for(int i=0;i<artsData.Count;i++)
        {
            Debug.Log(rc.Skills[i] + " : ダメージ　 : " + artsData[rc.Skills[i]].damage);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //Debug.Log(artsData["5A"].skill + " : ダメージ　 : " + artsData["5A"].damage);
    }
}
