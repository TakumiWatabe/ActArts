using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboScript : MonoBehaviour {

    //[SerializeField]
    GameObject character1, character2;
    HPDirectorScript script1,script2;

    [SerializeField]
    Text text_P1, text_P2;

    int combo_P1 = 0, combo_P2 = 0;

    //キャラクター生成オブジェクト
    private GameObject contl;
    private InstanceScript InScript;

    void Awake()
    {
        contl = GameObject.Find("FighterComtrol");
        InScript = contl.GetComponent<InstanceScript>();
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
                    character1 = InScript.Fighter(0);
                    break;
                case "P2":
                    character2 = InScript.Fighter(1);
                    break;
                default:
                    break;
            }
        }

        script1 = character1.GetComponent<HPDirectorScript>();
        script2 = character2.GetComponent<HPDirectorScript>();
        text_P1.text = "";
        text_P2.text = "";
    }
	
	// Update is called once per frame
	void Update ()
    {
  //      //plyaer1コンボ
		//if(script1.hitFlag)
  //      {
  //          combo_P1++;
  //          text_P1.text= combo_P1.ToString() + "Combo!!";
  //      }

  //      //player2コンボ
  //      if(script2.hitFlag)
  //      {
  //          combo_P2++;
  //          text_P2.text = combo_P2.ToString() + "Combo!!";
  //      }
    }



    public void NoneCombo()
    {
        combo_P1 = 0;
        combo_P2 = 0;

        text_P1.text = "";
        text_P2.text = "";
    }
}
