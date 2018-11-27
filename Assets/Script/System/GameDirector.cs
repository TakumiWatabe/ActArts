using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour {
    public const int NONE = 0;
    public const int PLAYER1_WIN = 1;
    public const int PLAYER2_WIN = 2;

    //[SerializeField]
    GameObject player1;
    //[SerializeField]
    GameObject player2;

    HPDirectorScript HP1, HP2;
    Vector3 initPos1, initPos2;

    ComboScript comboScript;
    GetGameScript gameScript;
    TextGenerator textScript;
    SceneManagement sceneManager;
    [SerializeField]
    Sprite image;
    [SerializeField]
    Sprite AoiImage;
    [SerializeField]
    Sprite HikariImage;
    [SerializeField]
    Image P1Image;
    [SerializeField]
    Image P2Image;


    float timer;
    bool hp;
    // ゲームの状態(0 = ゲーム開始時 , 1 = ゲームプレイ中 , 2 = ゲーム終了)
    int gameState;

    //キャラクター生成オブジェクト
    private GameObject contl;
    private InstanceScript InScript;

    void Awake()
    {
        contl = GameObject.Find("FighterComtrol");
        InScript = contl.GetComponent<InstanceScript>();

        gameState = 0;
    }

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < 2; i++)
        {
            //キャラクター設定
            switch (InScript.Fighter(i).tag)
            {
                case "P1":
                    player1 = InScript.Fighter(0);
                    switch(player1.transform.GetChild(0).gameObject.name)
                    {
                        case "Aoi":
                            P1Image.sprite = AoiImage;
                            break;
                        case "Hikari":
                            P1Image.sprite = HikariImage;
                            break;
                    }
                    break;
                case "P2":
                    player2 = InScript.Fighter(1);
                    switch (player2.transform.GetChild(0).gameObject.name)
                    {
                        case "Aoi":
                            P2Image.sprite = AoiImage;
                            break;
                        case "Hikari":
                            P2Image.sprite = HikariImage;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        timer = 0;
        textScript = GameObject.Find("TextFactory").GetComponent<TextGenerator>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagement>();

        HP1 = player1.transform.GetChild(0).gameObject.GetComponent<HPDirectorScript>();
        HP2 = player2.transform.GetChild(0).gameObject.GetComponent<HPDirectorScript>();

        initPos1 = player1.transform.position;
        initPos2 = player2.transform.position;
        player1.transform.position = initPos1;
        player2.transform.position = initPos2;

        comboScript = GetComponent<ComboScript>();
        gameScript = GetComponent<GetGameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textScript.gameState == false && gameScript.GetPlayerWin() == 0)
        {
            timer += Time.unscaledDeltaTime;
            if (timer >= 3.0f)
            {
                sceneManager.SceneChange("play");
            }
        }
        if (gameScript.GetPlayerWin() == 1 || gameScript.GetPlayerWin() == 2)
        {
            timer += Time.unscaledDeltaTime;
            if (timer >= 5.0f)
            {
                gameScript.ResetGame(gameScript.winImages.sprite);
                textScript.gameStateNum = 1;
                sceneManager.SceneChange("title");
            }
        }
        //情報をリセット
        if (Input.GetKey(KeyCode.R))
        {
            HP1.Initialise();
            HP2.Initialise();

            HP1.HPScale = new Vector3(1, 1, 1);
            HP1.DamageScale = new Vector3(1, 1, 1);
            HP2.HPScale = new Vector3(1, 1, 1);
            HP2.DamageScale = new Vector3(1, 1, 1);

            player1.transform.position = initPos1;
            player2.transform.position = initPos2;

            comboScript.NoneCombo();
            gameScript.ResetGame(image);
        }

    }
    // 決着が着いた(どちらが勝ったか)
    public int GameSet()
    {
        int setNum = NONE;
        // Player1のHPが0以下になったら
        // Player2の勝利
        if (HP1.NowHPState <= 0)
        {
            setNum = PLAYER2_WIN;
        }
        // Player2のHPが0以下になったら
        // Player1の勝利
        if (HP2.NowHPState <= 0)
        {
            setNum = PLAYER1_WIN;
        }
        return setNum;
    }
    public void GameState(int state)
    {
        gameState = state;
    }
    public int GetGameState()
    {
        return gameState;
    }
    public void ResetState()
    {
        gameState = 0;
    }

    public int NowP1HP { get { return HP1.NowHPState; } }
    public int NowP2HP { get { return HP2.NowHPState; } }
}
