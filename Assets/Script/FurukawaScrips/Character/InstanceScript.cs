using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceScript : MonoBehaviour {

    struct InstantFighter
    {
        public GameObject fighter;
        public int playerTag;
    }

    [SerializeField]
    private List<GameObject> charcter;

    //キャラクターの名前を格納する変数
    private InstantFighter[] fight = new InstantFighter[2];
    private string[] names = new string[3]
    {
        "Aoi" ,
        "Hikari" ,
        "none"
    };

    //キャラクターの名前を取得する用
    private DataRetention datas;
    private int dataState;
    //エラー変数
    private const int exceptionData = 100;

    //取得したキャラクターの名前を格納する変数
    private string[] receive = new string[2];

    // Use this for initialization
    void Awake ()
    {
        if (GameObject.Find("GameSystem") != null)
        {
            datas = GameObject.Find("GameSystem").GetComponent<DataRetention>();
        }

        //キャラクター生成
        JudgeCreateChar();
    }

    //キャラクター生成関数
    private void CreateChar(int charID,int InputID)
    {
        fight[charID].fighter = Instantiate(charcter[InputID]);
        fight[charID].playerTag = charID + 1;
        if (this.modes() == 1 && fight[charID].playerTag == 2) fight[charID].fighter.AddComponent<EnemyAI>();
    }

    //生成キャラ判別関数
    private void JudgeCreateChar()
    {
        for (int i = 0; i < fight.Length; i++)
        {
            if (datas != null)
            {
                receive[i] = datas.fighterName[i];
            }

            if (receive[i] != null)
            {
                switch (receive[i])
                {
                    //一致したキャラを登録
                    case "Aoi":
                        CreateChar(i, 0);
                        break;
                    case "Hikari":
                        CreateChar(i, 1);
                        break;
                    default:
                        fight[i].fighter = null;
                        fight[i].playerTag = 0;
                        break;
                }
            }
            else
            {
                //デバッグ用に先頭のキャラを登録
                CreateChar(i, 0);
            }

            if (fight[i].fighter != null)
            {
                fight[i].fighter.tag = "P" + fight[i].playerTag.ToString();
                fight[i].fighter.transform.GetChild(0).tag = "P" + fight[i].playerTag.ToString();
            }
            else
            {
                //エラー終了
                Application.Quit();
            }
        }


        if (Fighter(0).name == Fighter(1).name)
        {
            var change = gameObject.GetComponent<ChangeMaterialColor>();
            change.ChangeColor(Color.red, Fighter(1).transform.GetChild(3).GetChild(0).gameObject);
            change.ChangeColor(Color.black, Fighter(1).transform.GetChild(1).gameObject);
            change.ChangeColor(Color.black, Fighter(1).transform.GetChild(0).gameObject);
        }
    }

    //キャラクターネーム
    public string charName(int ID) { return names[ID]; }
    //キャラクター取得用
    public GameObject Fighter(int ID) { return fight[ID].fighter; }
    //キャラクターID
    public int FighterID(int ID) { return fight[ID].playerTag; }

    //対戦モード取得用変数
    public int modes()
    {
        if (datas != null)
        {
            return datas.Mode;
        }
        return exceptionData;
    }
}
