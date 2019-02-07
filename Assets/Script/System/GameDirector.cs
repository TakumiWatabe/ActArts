using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    //キャラクターの初期状態を設定するスクリプト

    //プレイヤー
    GameObject player1;
    GameObject player2;

    //HPスクリプト
    HPDirectorScript HP1, HP2;
    //初期位置
    Vector3 initPos1, initPos2;

    //ComboScript comboScript;
    GetGameScript gameScript;

    [SerializeField]
    private List<Sprite> playerIcon;
    [SerializeField]
    private List<Sprite> names;

    [SerializeField]
    Image P1Image, P2Image;
    [SerializeField]
    Image P1Name, P2Name;

    //PlayerController
    private PlayerController player1Controller;
    private PlayerController player2Controller;

    //キャラクター生成オブジェクト
    private GameObject contl;
    private InstanceScript InScript;

    void Awake()
    {
        contl = GameObject.Find("FighterComtrol");
        InScript = contl.GetComponent<InstanceScript>();
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            //キャラクター設定
            switch (InScript.Fighter(i).tag)
            {
                case "P1":
                    player1 = InScript.Fighter(0);
                    SetPlayerState(player1.name, P1Image, P1Name, player1);
                    player1Controller = player1.GetComponent<PlayerController>();
                    player1Controller.CanControll = false;
                    break;
                case "P2":
                    player2 = InScript.Fighter(1);
                    SetPlayerState(player2.name, P2Image, P2Name, player2);
                    player2Controller = player2.GetComponent<PlayerController>();
                    player2Controller.CanControll = false;
                    break;
            }
        }

        HP1 = player1.GetComponent<HPDirectorScript>();
        HP2 = player2.GetComponent<HPDirectorScript>();

        initPos1 = player1.transform.position;
        initPos2 = player2.transform.position;
        player1.transform.position = initPos1;
        player2.transform.position = initPos2;

        //comboScript = GetComponent<ComboScript>();
        gameScript = GetComponent<GetGameScript>();
    }

    public void Initialize()
    {
        HP1.Initialise();
        HP2.Initialise();

        HP1.HPScale = new Vector3(1, 1, 1);
        HP1.DamageScale = new Vector3(1, 1, 1);
        HP2.HPScale = new Vector3(1, 1, 1);
        HP2.DamageScale = new Vector3(1, 1, 1);

        player1.transform.position = initPos1;
        player2.transform.position = initPos2;

        player1Controller.CanControll = false;
        player2Controller.CanControll = false;

        player1Controller.Direction = 1;
        player2Controller.Direction = 2;

        //comboScript.NoneCombo();
    }

    //名前とアイコンの設定
    private void SetPlayerState(string name, Image icon, Image plate, GameObject charcter)
    {
        switch (name)
        {
            case "Aoi(Clone)":
                icon.sprite = playerIcon[0];
                plate.sprite = names[0];
                charcter.name = "Aoi";
                break;
            case "Hikari(Clone)":
                icon.sprite = playerIcon[1];
                plate.sprite = names[1];
                charcter.name = "Hikari";
                break;
            case "Xion(Clone)":
                icon.sprite = playerIcon[3];
                plate.sprite = names[3];
                charcter.name = "Xion";
                break;
            case "Shirogane(Clone)":
                icon.sprite = playerIcon[2];
                plate.sprite = names[2];
                charcter.name = "Shirogane";
                break;
            case "Chloe(Clone)":
                icon.sprite = playerIcon[4];
                plate.sprite = names[4];
                charcter.name = "Chloe";
                break;
            case "Mari(Clone)":
                icon.sprite = playerIcon[5];
                plate.sprite = names[5];
                charcter.name = "Mari";
                break;
        }
    }

    //プレイヤーを動かせる状態にする
    public void PlayersCanControlSet(bool move)
    {
        player1Controller.CanControll = move;
        player2Controller.CanControll = move;
    }
}
