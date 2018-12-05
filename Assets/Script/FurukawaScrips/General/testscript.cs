using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour {


    [SerializeField]
    private GameObject[] cursor = new GameObject[2];
    private CharacterSelect[] sele = new CharacterSelect[2];
    private string[] Charname = new string[2];

    private DataRetention datas;

    // Use this for initialization
    void Start ()
    {
        for(int i=0;i<sele.Length;i++)
        {
            sele[i] = cursor[i].GetComponent<CharacterSelect>();
        }
        datas = GameObject.Find("GameSystem").GetComponent<DataRetention>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < sele.Length; i++)
        {
            Charname[i] = sele[i].GetCharName();
            datas.fighterName[i] = Charname[i];
        }
    }
}
