using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimtorDirector : MonoBehaviour {

    [SerializeField]
    private GameObject instance;
    private ResultChar RChar;
    //プレイヤー
    private GameObject winner;
    //アニメーター
    private Animator animator;
    //アニメーターの状態
    private const string WIN_ANIM_STATE = "Win";
    private bool stop = true;
    private float animTime = 0;

	// Use this for initialization
	void Start () {
        RChar = instance.GetComponent<ResultChar>();
        winner = RChar.GetWinner;
        //animatorをセットする
        animator = winner.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void PlayAnim(bool stop)
    {
        if (!stop) { animator.speed = 1; }
        else { animator.speed = 0; }
    }
}
