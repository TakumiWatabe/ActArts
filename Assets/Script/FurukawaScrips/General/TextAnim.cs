using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnim : MonoBehaviour {

    [SerializeField]
    private List<string> description;
    [SerializeField]
    private Text text;

    private float disTime = 1;
    private float count = 0;

    private int displayScentences = 0;

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void DisplayAnim(int id)
    {
        if (description[id].Length > displayScentences)
        {
            count++;
            if (count > disTime)
            {
                displayScentences++;
                count = 0;
            }
        }

        //文字列反映
        text.text = description[id].Substring(0, displayScentences);
    }

    public void Initialize()
    {
        displayScentences = 0;
    }
}
