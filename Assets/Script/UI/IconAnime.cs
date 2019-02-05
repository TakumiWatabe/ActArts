using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IconAnime : MonoBehaviour {
    private float scale;
    private float rotate;
    private float alpha;
    private bool scaleFlag;
    private bool alphaFlag;
    Image image;
    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
        scaleFlag = true;
        scale = 1;
    }

    // Update is called once per frame
    void Update () {
        if ((this.transform.name == "P1Image") || (this.transform.name == "P2Image"))
        {
            PlayerIconAnime();
        }
    }
    public void PlayerIconAnime()
    {
        // 拡縮
        if (scaleFlag)
            scale -= Time.deltaTime / 8;
        else
            scale += Time.deltaTime / 8;

        if (scale >= 1)
            scaleFlag = true;
        else if (scale <= 0.75)
            scaleFlag = false;

        // 透明度
        if (alphaFlag)
            alpha -= 1.0f;
        else
            alpha += 1.0f;

        if (alpha >= 1)
            alphaFlag = true;
        else if (alpha <= 0)
            alphaFlag = false;

        this.transform.localScale = new Vector3(scale, scale, scale);
        image.color = new Color(255, 255, 255, scale);
    }
}
