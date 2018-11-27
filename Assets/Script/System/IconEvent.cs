using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IconEvent : MonoBehaviour
{
    //　このスロットのアイテム名
    [SerializeField]
    private string itemName;
    GameObject frame1p;
    GameObject frame2p;
    GameObject frame2p2;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        frame1p = GameObject.Find("1Pframe");
        frame2p = GameObject.Find("2Pframe");
        frame2p2 = GameObject.Find("2Pframe2");
    }

    //　マウスアイコンが自分のアイコン上に入った時
    void OnTriggerEnter2D(Collider2D col)
    {
        CheckEvent(col);
    }

    //　マウスアイコンが自分のアイコン上にいる間
    void OnTriggerStay2D(Collider2D col)
    {
        CheckEvent(col);
    }

    void CheckEvent(Collider2D col)
    {
        //　アイコンを検知する
        if (col.tag == "icon1")
        {
            frame1p.transform.localPosition = col.gameObject.GetComponent<CharacterSelect>().GetFramePos(itemName);
        }
        else if(col.tag=="icon2")
        {
            frame2p.transform.localPosition = col.gameObject.GetComponent<CharacterSelect>().GetFramePos(itemName);
            frame2p2.transform.localPosition = col.gameObject.GetComponent<CharacterSelect>().GetFramePos(itemName);
        }
    }
}