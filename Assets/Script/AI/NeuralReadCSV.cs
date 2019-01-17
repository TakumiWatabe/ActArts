using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NeuralReadCSV : MonoBehaviour
{
    // CSVデータを文字列型２次元配列に変換する
    // ファイルパス,変換される配列の値(参照渡し)
    public static NumYArray readCSVData(string path)
    {
        TextAsset csv;
        csv = Resources.Load(path) as TextAsset;

        StringReader reader = new StringReader(csv.text);
        string strStream = reader.ReadToEnd();

        // StringSplitOptionを設定(要はカンマとカンマに何もなかったら格納しないことにする)
        System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;

        // 行に分ける
        string[] lines = strStream.Split(new char[] { '\r', '\n' }, option);

        // カンマ分けの準備(区分けする文字を設定する)
        char[] spliter = new char[1] { ',' };

        // 行数設定
        int h = lines.Length;
        // 列数設定
        int w = lines[1].Split(spliter, option).Length;

        // 返り値の2次元配列の要素数を設定
        List<List<float>> sdata = new List<List<float>>();
        // 配列の要素確保
        for (int i = 0; i < h - 1; i++)
        {
            sdata.Add(new List<float>());
            for (int j = 0; j < w; j++)
            {
                sdata[i].Add(0);
            }
        }

        // 行データを切り分けて,2次元配列へ変換する
        for (int i = 1; i < h; i++)
        {
            string[] splitedData = lines[i].Split(spliter, option);

            for (int j = 0; j < w; j++)
            {
                sdata[i - 1][j] = float.Parse(splitedData[j]);
            }
        }

        NumYArray returnArray = new NumYArray(sdata);
        return returnArray;
    }

    // ２次元配列の型を文字列型から整数値型へ変換する
    public static int[,] convert2DArrayType(ref string[,] sarrays, int h, int w)
    {
        int[,] iarrays = new int[h, w];
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                iarrays[i, j] = int.Parse(sarrays[i, j]);
            }
        }

        return iarrays;
    }

    //確認表示用の関数
    //引数：2次元配列データ,行数,列数
    public static void WriteMapDatas(int[,] arrays, int hgt, int wid)
    {
        for (int i = 0; i < hgt; i++)
        {
            for (int j = 0; j < wid; j++)
            {
                //行番号-列番号:データ値 と表示される
                Debug.Log(i + "-" + j + ":" + arrays[i, j]);
            }
        }
    }
}
