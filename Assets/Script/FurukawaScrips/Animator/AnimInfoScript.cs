using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimInfoScript : MonoBehaviour {

    private Animator anim;
    private AnimatorClipInfo animInfo;

    private string animName;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        animInfo = anim.GetCurrentAnimatorClipInfo(0)[0];
	}
	
	// Update is called once per frame
	void Update ()
    {
        animName = animInfo.clip.name;
        Debug.Log("現在のアニメーション：" + animName);
	}

    public string animationInfo { get { return animName; } }
}
