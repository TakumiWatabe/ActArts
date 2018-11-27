using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.UI;

public class PlayMenuSystem : MonoBehaviour {

    [SerializeField]
    private Sprite[] selectMenuSprite;
    [SerializeField]
    private Sprite[] nonSelectMenuSprite;
    [SerializeField]
    private GameObject[] selectMenuComand;
    [SerializeField]
    private int choiceMenu = 0;
    private int selectMenuNum;
    private SceneManagement sm;
    private DataRetention dr;
    private string controllerName;
    private float oldStick = 0.0f;
    private float oldCuosorButton = 0.0f;

	// Use this for initialization
	void Start () {
        dr = GameObject.Find("GameSystem").GetComponent<DataRetention>();
        sm = GameObject.Find("GameSystem").GetComponent<SceneManagement>();
        selectMenuNum = (int)DataRetention.GameMode.SIZE;
        controllerName = Input.GetJoystickNames()[0];
        if (controllerName != "Arcade Stick (MadCatz FightStick Neo)")
            controllerName = "";
        ChangeSprite();
    }
	
	// Update is called once per frame
	void Update () {
        if (controllerName == "")
        {
            //上
            if((GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y >= 1.0f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y != oldStick) ||
                (GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y >= 1.0f && GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y != oldCuosorButton))
            {
                choiceMenu++;
                if(choiceMenu == selectMenuNum)
                {
                    choiceMenu = (int)DataRetention.GameMode.PvP;
                }
                ChangeSprite();
            }

            //下
            if ((GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y <= -1.0f && GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y != oldStick) ||
                (GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y <= -1.0f && GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y != oldCuosorButton))
            {
                choiceMenu--;
                if (choiceMenu == -1)
                {
                    choiceMenu = selectMenuNum - 1;
                }
                ChangeSprite();
            }

            ////ボタンを押すとシーンチェンジ
            //if(GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One))
            //{
            //    dr.Mode = choiceMenu;
            //    sm.SceneChange("select");
            //}

            //トリガー処理
            oldStick = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y;
            oldCuosorButton = GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y;
        }

        if (controllerName == "Arcade Stick (MadCatz FightStick Neo)")
        {
            //上
            if ((GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y <= -1.0f &&
                GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y != oldStick))
            {
                choiceMenu++;
                if (choiceMenu == selectMenuNum)
                {
                    choiceMenu = (int)DataRetention.GameMode.PvP;
                }
                ChangeSprite();
            }

            //下
            if ((GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y >= 1.0f &&
                GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y != oldStick))
            {
                choiceMenu--;
                if (choiceMenu == -1)
                {
                    choiceMenu = selectMenuNum - 1;
                }
                ChangeSprite();
            }

            ////ボタンを押すとシーンチェンジ
            //if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.Two))
            //{
            //    dr.Mode = choiceMenu;
            //    sm.SceneChange("select");
            //}

            //トリガー処理
            oldStick = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y;
            oldCuosorButton = GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y;
        }
    }
    
    /// <summary>
    /// スプライトの変更
    /// </summary>
    private void ChangeSprite()
    {
        for (int i = 0; i < selectMenuComand.Length; i++)
        {
            selectMenuComand[i].GetComponent<Image>().sprite = nonSelectMenuSprite[i];
        }

        selectMenuComand[choiceMenu].GetComponent<Image>().sprite = selectMenuSprite[choiceMenu];
    }

    public int menuType { get { return choiceMenu; } }
}
