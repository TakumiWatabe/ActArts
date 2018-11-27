using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIAnime : MonoBehaviour {
    public const int MAXSIZE =2000;

    private RectTransform ui;
    private RectTransform ui2;
    float uiSize1;
    float uiSize2;
    [SerializeField]
    GameObject backImage;
    [SerializeField]
    GameObject backImage2;
    int animeFlag;
    // Use this for initialization
    void Start()
    {
        animeFlag = 0;
        ui = backImage.GetComponent<RectTransform>();
        ui2 = backImage2.GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update ()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {

            if (uiSize1 <= MAXSIZE)
            {
                uiSize1 += 2.0f;
            }
            else
            {
                uiSize1 = 0.0f;
            }
            if (uiSize1 >= MAXSIZE / 2.0f)
            {
                animeFlag = 1;
            }

            if (uiSize2 <= MAXSIZE && animeFlag != 0)
            {
                uiSize2 += 2.0f;
            }
            else
            {
                uiSize2 = 0.0f;
            }

            ui.sizeDelta = new Vector2(uiSize1, uiSize1);
            ui2.sizeDelta = new Vector2(uiSize2, uiSize2);
        }
    }
}
