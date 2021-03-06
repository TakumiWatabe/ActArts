﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapScript : MonoBehaviour {
    //--------------------------------------
    //あたり判定設定
    //--------------------------------------

    //押し合い判定
    private BoxCollider PushBox;

    private const float CSizeZ = 0.25f;

    // Use this for initialization
    void Start ()
    {
        PushBox = GetComponent<BoxCollider>();
    }

    //-------------------------------------------------------------------------------------------------------------------
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Aoiあたり判定
    //
    //-------------------------------------------------------
    //立ち押し合い判定
    //
    //使用モーション:立ち系
    //-------------------------------------------------------
    void IdleOver()
    {
        //基本押し合い判定
        SetBoxState(PushBox,
            new Vector3(0, 0.8f, 0.08f),
            new Vector3(CSizeZ, 1.5f, 0.35f));
    }

    //-------------------------------------------------------
    //しゃがみ押し合い判定
    //
    //使用モーション:しゃがみ系
    //-------------------------------------------------------
    void SitOver()
    {
        //基本押し合い判定
        SetBoxState(PushBox,
            new Vector3(0, 0.55f, 0.08f),
            new Vector3(CSizeZ, 1.1f, 0.35f));
    }

    //-------------------------------------------------------
    //ジャンプ押し合い判定
    //
    //使用モーション:ジャンプ系
    //-------------------------------------------------------
    void JumpOver()
    {
        //基本押し合い判定
        SetBoxState(PushBox,
            new Vector3(0, 1f, 0.05f),
            new Vector3(CSizeZ, 1.1f, 0.35f));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //-------------------------------------------------------------------------------------------------------------------

    //-------------------------------------------------------------------------------------------------------------------
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Hikariあたり判定
    //
    //-------------------------------------------------------
    //立ち押し合い判定
    //
    //使用モーション:立ち系
    //-------------------------------------------------------
    void HStandOver()
    {
        //基本押し合い判定
        SetBoxState(PushBox,
            new Vector3(0, 0.75f, 0.05f),
            new Vector3(CSizeZ, 1.45f, 0.4f));
    }

    //---------------------------------------------------------------
    //しゃがみ押し合い判定
    //
    //使用モーション:しゃがみ系
    //---------------------------------------------------------------
    void HSitOver()
    {
        //基本押し合い判定
        SetBoxState(PushBox,
            new Vector3(0, 0.55f, 0.05f),
            new Vector3(CSizeZ, 1.1f, 0.4f));
    }

    //--------------------------------
    //ジャンプ押し合い判定
    //
    //使用モーション:ジャンプ系
    //--------------------------------
    void HJumpOver()
    {
        //基本押し合い判定
        SetBoxState(PushBox,
            new Vector3(0, 0.85f, 0.05f),
            new Vector3(CSizeZ, 1.1f, 0.4f));
    }
     /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //-------------------------------------------------------------------------------------------------------------------

    //あたり判定設定関数
    private void SetBoxState(BoxCollider box, Vector3 pos, Vector3 size)
    {
        box.center = pos;
        box.size = size;
    }

    public BoxCollider PClid { get { return PushBox; } }
}
