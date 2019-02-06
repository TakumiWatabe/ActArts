using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStage : MonoBehaviour {
    [SerializeField]
    GameObject stage1;
    [SerializeField]
    GameObject stage2;
    [SerializeField]
    GameObject stage3;

    int rnd;
    GameObject mainStage;
	// Use this for initialization
	void Start () {
        rnd = Random.Range(1, 4);
        if (GameObject.FindGameObjectWithTag("stage") == null)
        {
            ChoiceStage(rnd);
            Instantiate(mainStage);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void ChoiceStage(int num)
    {
        switch(num)
        {
            case 1:
                mainStage = stage1;
                break;
            case 2:
                mainStage = stage2;
                break;
            case 3:
                mainStage = stage3;
                break;
        }
    }
}
