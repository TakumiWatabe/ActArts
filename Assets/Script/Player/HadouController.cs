using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadouController : MonoBehaviour
{

    // Use this for initialization

    [SerializeField]
    float speed = 1.0f;
    public int direction = 1;

    private int player = 1;

    private Vector3 pos;

    Vector3 defaultScale = Vector3.zero;

    void Start()
    {
        pos = transform.position;
        defaultScale = transform.lossyScale;
        //transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        pos.x = transform.position.x + speed * direction;
        transform.position = pos;

        Vector3 lossScale = transform.lossyScale;
        Vector3 localScale = transform.localScale;

        transform.localScale = new Vector3(
                localScale.x / lossScale.x * defaultScale.x,
                localScale.y / lossScale.y * defaultScale.y,
                localScale.z / lossScale.z * defaultScale.z
        );
    }

    public int Player { set { player = value; } }
}
