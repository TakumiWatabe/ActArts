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

    private InstantFighter[] fight = new InstantFighter[2];
    private string[] names = new string[3]
    {
        "Aoi" ,
        "Hikari" ,
        "none"
    };

    private DataRetention datas;
    private string[] receive = new string[2];


    // Use this for initialization
    void Awake ()
    {
        if (GameObject.Find("GameSystem") != null)
        {
            datas = GameObject.Find("GameSystem").GetComponent<DataRetention>();
        }


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
                        fight[i].fighter = Instantiate(charcter[0]);
                        fight[i].playerTag = i + 1;
                        Debug.Log("流法" + this.modes);
                        if (this.modes == 1 && fight[i].playerTag == 2) fight[i].fighter.AddComponent<EnemyAI>();
                        break;
                    case "Hikari":
                        fight[i].fighter = Instantiate(charcter[1]);
                        fight[i].playerTag = i + 1;
                        Debug.Log("流法" + this.modes);
                        if (this.modes == 1 && fight[i].playerTag == 2) fight[i].fighter.AddComponent<EnemyAI>();
                        break;
                    default:
                        fight[i].fighter = null;
                        fight[i].playerTag = 0;
                        break;
                }
            }
            else
            {
                //選択されたキャラクターの名前で検索
                switch (names[i])
                {
                    //一致したキャラを登録
                    case "Aoi":
                        fight[i].fighter = Instantiate(charcter[0]);
                        fight[i].playerTag = i + 1;
                        Debug.Log("流法" + this.modes);
                        if(this.modes==1 && fight[i].playerTag == 2) fight[i].fighter.AddComponent<EnemyAI>();
                        break;
                    case "Hikari":
                        fight[i].fighter = Instantiate(charcter[1]);
                        fight[i].playerTag = i + 1;
                        if (this.modes == 1 && fight[i].playerTag == 2) fight[i].fighter.AddComponent<EnemyAI>();
                        break;
                    default:
                        fight[i].fighter = null;
                        fight[i].playerTag = 0;
                        break;
                }
            }

            if (fight[i].fighter != null)
            {
                fight[i].fighter.tag = "P" + fight[i].playerTag.ToString();
                fight[i].fighter.transform.GetChild(0).tag = "P" + fight[i].playerTag.ToString();
            }
        }
    }

    //キャラクターネーム
    public string charName(int ID) { return names[ID]; }
    //キャラクター取得用
    public GameObject Fighter(int ID) { return fight[ID].fighter; }
    //キャラクターID
    public int FighterID(int ID) { return fight[ID].playerTag; }

    public int modes { get { return datas.Mode; } }
}
