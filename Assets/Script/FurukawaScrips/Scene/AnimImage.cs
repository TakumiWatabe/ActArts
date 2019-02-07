using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimImage : MonoBehaviour {

    [SerializeField]
    private Image effect, result;
    [SerializeField]
    private Image winname;
    [SerializeField]
    private Image win, winEffe;
    [SerializeField]
    private Image backEffect;

    [SerializeField]
    private GameObject maneger;
    private SceneFade Sfade;
    private AnimtorDirector ADir;

    private bool playFlag = true;

    private float moveSpeed = 25f;
    private float overLineR = 0;
    private float overLineW = 210;

    private Vector3 minScale;
    private Vector3 maxScale = new Vector3(3, 3, 3);
    private float baseTime = 1;
    private float timer = 0;

    // Use this for initialization
    void Start () {
        effect.transform.localPosition = new Vector3(-350, 0, 0);
        result.transform.localPosition = new Vector3(-350, 0, 0);
        winname.transform.localPosition = new Vector3(650, -30, 0);
        win.enabled = false;
        winEffe.enabled = false;
        minScale = winEffe.transform.localScale;

        Sfade = maneger.GetComponent<SceneFade>();
        ADir = this.GetComponent<AnimtorDirector>();	
	}

    // Update is called once per frame
    void Update()
    {
        ADir.PlayAnim(playFlag);
        if (Sfade.ImageAlpha <= 0)
        {
            if ((effect.transform.localPosition.x < overLineR) && (result.transform.localPosition.x < overLineR))
            {
                effect.transform.localPosition += new Vector3(moveSpeed, 0, 0);
                result.transform.localPosition += new Vector3(moveSpeed, 0, 0);
                playFlag = true;
            }

            if (winname.transform.localPosition.x > overLineW) { winname.transform.localPosition -= new Vector3(moveSpeed, 0, 0); }
            if ((effect.transform.localPosition.x >= overLineR) && (result.transform.localPosition.x >= overLineR)&& (winname.transform.localPosition.x <= overLineW)) { playFlag = false; }


            if (!playFlag)
            {
                StartCoroutine("expansion", winEffe);
            }
        }
    }

    //拡大
    private IEnumerator expansion(Image image)
    {
        yield return new WaitForSeconds(0.25f);

        win.enabled = true;
        winEffe.enabled = true;

        Vector3 vec;
        float rec;
        if (timer <= baseTime) { timer += 0.025f; }

        vec = Vector3.Lerp(minScale, maxScale, timer);
        image.transform.localScale = vec;
        rec = Mathf.Lerp(1, 0, timer);
        image.color = new Color(image.color.a, image.color.g, image.color.b, rec);
    }
}
