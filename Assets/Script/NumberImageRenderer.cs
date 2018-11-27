using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class NumberImageRenderer : MonoBehaviour
{
    [System.Serializable]
    public struct TextNumRenderData
    {
        public TextNumRenderData(string fileName, Transform parentTrasform, bool ignoreFilledZero)
            : this()
        {
            this.fileName = fileName;
            this.parentTrasform = parentTrasform;
            this.ignoreFilledZero = ignoreFilledZero;
        }

        public string fileName;
        public Transform parentTrasform;
        public bool ignoreFilledZero;
    }

    struct Figure
    {
        public RectTransform rectTrans;
        public SpriteRenderer sr;
    }

    [SerializeField]
    TextNumRenderData data;
    public int maxDigit = 0;//表示最大桁数

    Sprite[] sprites = null;
    List<Figure> figureList = new List<Figure>();

    int digit = 0;//表示する数値の桁数

    // 文字（数字）表示用データの生成
    Figure Create(Sprite sprite)
    {
        var instance = new GameObject();
        instance.name = sprite.name;
        instance.transform.SetParent(data.parentTrasform);

        var rectTrans = instance.AddComponent<RectTransform>();
        instance.transform.localScale = Vector3.one;
        rectTrans.sizeDelta = data.parentTrasform.GetComponent<RectTransform>().sizeDelta;

        var spriteRenderer = instance.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        var figure = new Figure() { rectTrans = rectTrans, sr = spriteRenderer };

        return figure;
    }

    // 数字をSpriteで描画する
    void DrawSprite(string text)
    {
        for (int i = 0; i <= text.Length - 1; i++)
        {
            Sprite num_sp = null;
            //頭の０埋めを表示しない処理
            if (text[i].ToString().Equals("0") &&
                i <= text.Length - 1 - digit &&
                data.ignoreFilledZero)
            {
                num_sp = Resources.Load<Sprite>("clear");//Resourcesの下に"clear"ファイルがあること
            }
            else
            {
                var sp_name = data.fileName + "_" + text[i].ToString();
                num_sp = System.Array.Find(sprites, (s) => s.name.Equals(sp_name));
            }
            figureList[i].sr.sprite = num_sp;//Spriteを変更
        }
    }

    //空の数字Spriteを生成
    public void CreateEmpty(int digit)
    {
        if (sprites == null)
        {
            sprites = Resources.LoadAll<Sprite>(data.fileName);
            //for (int i = digit - 1 ; i >= 0 ; i--) {
            for (int i = 0; i < digit; i++)
            {
                Sprite sprite = null;
                var sp0_name = data.fileName + "_" + "0";
                sprite = System.Array.Find(sprites, (s) => s.name.Equals(sp0_name));

                var figure = Create(sprite);
                float spWidth = figure.rectTrans.sizeDelta.x / digit;
                float spStartx = -0.5f * figure.rectTrans.sizeDelta.x + spWidth * 0.5f;
                figure.rectTrans.anchoredPosition3D = new Vector3(i * spWidth + spStartx, 0, 0);

                //画像のサイズ拡大縮小
                float rateX = spWidth / sprite.bounds.size.x;
                float rateY = figure.rectTrans.sizeDelta.y / sprite.bounds.size.y;
                figure.sr.transform.localScale = new Vector3(rateX, rateY, 1.0f);

                if (data.ignoreFilledZero)
                {//頭の０埋めを表示しない設定の場合
                 //透明spriteで埋める
                    Sprite clear = Resources.Load<Sprite>("clear");//Resourcesの下に"clear"ファイルがあること
                    figure.sr.sprite = clear;
                }
                figureList.Add(figure);
            }
        }
    }

    // 数字をSpriteで描画する
    public void Draw(int num)
    {
        if (num <= -1) return;

        digit = (num == 0) ? 1 : (int)Mathf.Log10(num) + 1;
        if (digit > maxDigit)
        {
            Debug.Log(num + " exceed the max digit :" + maxDigit);
            return;
        }

        CreateEmpty(maxDigit);
        String form = null;
        if (maxDigit != 0)
        {
            form = "D" + maxDigit;
        }
        DrawSprite(num.ToString(form));
    }
}