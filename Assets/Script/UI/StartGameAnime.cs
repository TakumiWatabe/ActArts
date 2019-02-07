using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartGameAnime : MonoBehaviour {
    CharacterSelect selectPlayer1;
    CharacterSelect selectPlayer2;
    CancelScript enter;
    float speed;
    float alpha;
    Image image;
    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
        selectPlayer1 = GameObject.Find("P1Image").GetComponent<CharacterSelect>();
        selectPlayer2 = GameObject.Find("P2Image").GetComponent<CharacterSelect>();
        enter = GameObject.Find("SelectSceneObj").GetComponent<CancelScript>();
        speed = 0;
        alpha = 0;
    }

    // Update is called once per frame
    void Update () {
        if ((selectPlayer1.GetP1Frag() == false) && (selectPlayer2.GetP2Frag() == false))
        {
            if (transform.localPosition.x < 0f)
                speed = 100f;
            else
                speed = 0;

            if (alpha <= 255)
                alpha += 20f / 255f;
            this.transform.localPosition += new Vector3(speed, 0, 0);
            image.color = new Color(255, 255, 255, alpha);
        }
        else
        {
            this.transform.localPosition = new Vector3(-800, -125, 0);
            image.color = new Color(255, 255, 255, 0);
            speed = 0;
            alpha = 0;
        }
        if (enter.GetEnterFlag() == false)
        {
            alpha = 255;
        }

    }
}
