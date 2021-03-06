﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    public const float MINRANGE = 4.8f;

    public const float CAMERAPOS_X = 4f;
    private float cameraPos = 0f;
    private Camera cam;
    GameObject player1;
    GameObject player2;
    //private float num = 0;
    //private float maxCameraPosY = 1.3f;
    //private float minCameraPosY = 2.1f;
    //private float maxCameraSize = 2.25f;
    //private float minCameraSize = 1.45f;
    //private float playerRange = 0f;
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("P1");
        player2 = GameObject.FindGameObjectWithTag("P2");
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        float p1PosX = player1.transform.position.x + player1.transform.GetChild(2).localPosition.z * player1.GetComponent<PlayerController>().Direction;
        float p2PosX = player2.transform.position.x + player2.transform.GetChild(2).localPosition.z * player2.GetComponent<PlayerController>().Direction; ;
        if ((p1PosX + p2PosX) / 2f >= -3.6f && (p1PosX + p2PosX) / 2f <= 3.6f)
        {
            cameraPos = (p1PosX + p2PosX) / 2f;
        }

        if (cameraPos < -3.6f)
        {
            cameraPos = -3.59f;
        }
        if (cameraPos > 3.6f)
        {
            cameraPos = 3.59f;
        }

        //cameraPos = (player1.transform.position.x + player2.transform.position.x) / 2f;

        transform.position = new Vector3(cameraPos, 1.4f, -10f);


        //Debug.Log("カメラ：" + player1.transform.position.x + player2.transform.position.x);

        //if (player1.transform.position.x - player2.transform.position.x < 0)
        //{
        //    playerRange = (player1.transform.position.x - player2.transform.position.x) * -1f;
        //}
        //else
        //{
        //    playerRange = player1.transform.position.x - player2.transform.position.x;
        //}
        //if (playerRange <= MAXRANGE)
        //    if (Input.GetButton("AButton"))
        //    {
        //        num = 0.1f;
        //    }
        //    else if (Input.GetButton("B_Button"))
        //    {
        //        num = -0.1f;
        //    }
        //    else
        //    {
        //        num = 0f;
        //    }
        //float scroll = num;
        //float view = cam.orthographicSize - scroll;
        //cam.orthographicSize = Mathf.Clamp(value: view, min: 1.5f, max: 2.25f);
    }
}
