using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRetention : MonoBehaviour {

    //ゲームモードの列挙型
    public enum GameMode
    {
        PvP,
        PvC,
        SIZE
    }

    private int mode = 0;

    private string[] fightName = new string[2];
    private string winner;

    //propaty
    public int Mode
    {
        get
        {
            return mode;
        }
        set
        {
            mode = value;
        }
    }

    public string[] fighterName
    {
        get { return fightName; }
        set { fightName = value; }
    }

    public string WinName
    {
        get { return winner; }
        set { winner = value; }
    }

}
