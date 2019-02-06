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
    private float alpha;
    private bool alphaFlag;
    Image image;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        animeFlag = 0;
        ui = backImage.GetComponent<RectTransform>();
        ui2 = backImage2.GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update ()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            TitleAnime();
        }
        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            SelectAnime();
        }
    }
    public void TitleAnime()
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
    public void SelectAnime()
    {
        // 透明度
        if (alphaFlag)
            alpha -= Time.deltaTime;
        else
            alpha += Time.deltaTime;

        if (alpha >= 1)
            alphaFlag = true;
        else if (alpha <= 0)
            alphaFlag = false;

        image.color = new Color(255, 255, 255, alpha);

    }

}
