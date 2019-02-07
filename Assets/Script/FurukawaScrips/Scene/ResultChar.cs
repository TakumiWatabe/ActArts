using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultChar : MonoBehaviour {

    [SerializeField]
    private GameObject sys;
    private DataRetention datas;

    [SerializeField]
    private List<GameObject> charcter;
    [SerializeField]
    private List<Sprite> names;
    [SerializeField]
    private Image display;

    private string winName;
    private GameObject winner;

    void Awake()
    {
        datas = GameObject.Find("GameSystem").GetComponent<DataRetention>();

        //勝者キャラを生成
        CreateWinCharcter();
    }

    private void CreateWinCharcter()
    {
        if(datas!=null)
        {
            winName = datas.WinName;
        }

        if (winName != null)
        {
            switch (winName)
            {
                case "Aoi":
                    CreateChar(0);
                    break;
                case "Hikari":
                    CreateChar(1);
                    break;
                case "Xion":
                    CreateChar(2);
                    break;
                case "Shirogane":
                    CreateChar(3);
                    break;
                case "Chloe":
                    CreateChar(4);
                    break;
                case "Mari":
                    CreateChar(5);
                    break;
            }
        }
        else
        {
            CreateChar(5);
        }
    }

    private void CreateChar(int charID)
    {
        winner = Instantiate(charcter[charID]);
        display.sprite = names[charID];
    }

    public GameObject GetWinner { get { return winner; } }
}
