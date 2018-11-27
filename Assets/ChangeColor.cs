using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public const float MAX_COLOR = 255.0f;
    [SerializeField]
    ParticleSystem[] particls2;
    ParticleSystem.MainModule[] par;
    Color randomColor;
    float red;
    float green;
    float blue;
    float alpha;
    Color[] startColor =
    {
        new Color(127.0f / MAX_COLOR,217.0f / MAX_COLOR,255.0f / MAX_COLOR,50.0f / MAX_COLOR),       //水色
        new Color(255.0f / MAX_COLOR,  0.0f / MAX_COLOR,  0.0f / MAX_COLOR,50.0f / MAX_COLOR),       //赤色
        new Color(174.0f / MAX_COLOR, 66.0f / MAX_COLOR,255.0f / MAX_COLOR,50.0f / MAX_COLOR),       //黄色
        new Color(255.0f / MAX_COLOR,174.0f / MAX_COLOR,  0.0f / MAX_COLOR,50.0f / MAX_COLOR),       //橙色
        new Color(124.0f / MAX_COLOR,255.0f / MAX_COLOR,125.0f / MAX_COLOR,50.0f / MAX_COLOR)        //緑色
    };
    bool[] colorChangeFlag = new bool[3]
    {
        false,
        false,
        false
    };
    bool[] colorUseFlag =new bool[5]
    {
        false,
        false,
        false,
        false,
        false,
    };
    // Use this for initialization
    void Start ()
    {
        par = new ParticleSystem.MainModule[1];
        red = 255 / MAX_COLOR;
        green = 0 / MAX_COLOR;
        blue = 0 / MAX_COLOR;
        alpha = 40 / MAX_COLOR;
        randomColor = new Color(red, green, blue,alpha);
        for (int i = 0; i < par.Length; i++)
        {
            par[i] = particls2[i].main;
            par[i].startColor = randomColor;
        }

    }

    // Update is called once per frame
    void Update ()
    {
        if (red >= MAX_COLOR / MAX_COLOR && blue == green)
        {
            blue += 1.0f / MAX_COLOR;
        }
        if  (blue >= MAX_COLOR && red == green)
        {
            green += 1.0f;
        }
        if (green >= MAX_COLOR && blue == red)
        {
            red += 1.0f;
        }
        if (red >= MAX_COLOR && blue >= MAX_COLOR)
        {
            red -= 1.0f;
        }
        if (green >= MAX_COLOR && red >= MAX_COLOR)
        {
            green -= 1.0f;
        }
        if (blue >= MAX_COLOR && green >= MAX_COLOR)
        {
            blue -= 1.0f;
        }
        for (int i = 0; i < par.Length; i++)
        {
            randomColor = new Color(red, green, blue, alpha);

            par[i] = particls2[i].main;
            par[i].startColor = randomColor;
        }

    }

    /// <summary>
    /// 動的にエフェクトのカラー変更(2色で比較)
    /// </summary>
    /// <param name="color1">薄くする色</param>
    /// <param name="color2">color1と比較するための色</param>
    /// <param name="maxColor">Colorの最大値(MAX_COLOR)</param>
    void ColorControl(float color1, float color2,float maxColor)
    {
        int flagNum = 0;
        if (color1 >= maxColor && color2 >= maxColor)
        {
            colorChangeFlag[0] = true;
        }
        if (colorChangeFlag[0] == true)
        {
            red -= 1.0f / maxColor;
        }
        if (colorChangeFlag[1] == true)
        {
            green -= 1.0f / maxColor;
        }
        if (colorChangeFlag[2] == true)
        {
            blue -= 1.0f / maxColor;
        }
    }
    /// <summary>
    /// 動的にカラーを変更(3色で比較)
    /// </summary>
    /// <param name="color1">今の色</param>
    /// <param name="color2">値が0の色</param>
    /// <param name="color3">値が0の色</param>
    /// <param name="maxColor">Colorの最大値(MAX_COLOR)</param>
    void ColorControl2(float color1, float color2, float color3, float maxColor)
    {
        int flagNum = 0;
        if (color1 >= maxColor && color2 == color3)
        {

        }

    }
    void AddColor(bool[] flags)
    {
        if (flags[0] == true)
        {
            red -= 1.0f / MAX_COLOR;
        }
        if (flags[1] == true)
        {
            green -= 1.0f / MAX_COLOR;
        }
        if (flags[2] == true)
        {
            blue -= 1.0f / MAX_COLOR;
        }
        if (red <= 0.0f || green <= 0.0f || blue <= 0.0f)
        {
            for (int i = 0; i < colorChangeFlag.Length; i++)
            {
                colorChangeFlag[i] = false;
            }
        }
    }
    void SubColor(bool[] flags)
    {
        if (flags[0] == true)
        {
            red += 1.0f / MAX_COLOR;
        }
        if (flags[1] == true)
        {
            green += 1.0f / MAX_COLOR;
        }
        if (flags[2] == true)
        {
            blue += 1.0f / MAX_COLOR;
        }
        if ((red >= 1.0f && green >= 1.0f) || (red >= 1.0f && blue >= 1.0f) || (green >= 1.0f && blue >= 1.0f))
        {
            for (int i = 0; i < colorChangeFlag.Length; i++)
            {
                colorChangeFlag[i] = false;
            }
        }
    }
    void ChangeRandomColor()
    {
        par = new ParticleSystem.MainModule[4];
        for (int i = 0; i < 4; i++)
        {
            par[i] = particls2[i].main;
            int cnt = 0;
            while (cnt < 4)
            {
                int rnd = Random.Range(0, 4);
                if (colorUseFlag[rnd] == false)
                {
                    colorUseFlag[rnd] = true;
                    par[i].startColor = startColor[rnd];
                    cnt++;
                    break;
                }
            }
        }
    }

}
