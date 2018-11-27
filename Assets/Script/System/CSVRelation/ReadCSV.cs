using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadCSV : MonoBehaviour
{
    //技名変数
    private List<string> skillName = new List<string>();

    public struct CharaData
    {
        public string skill;
        public int damage;
        public string attri;
        public float startCorrec;
        public float conboCorrec;
        public int attackLevel;
        public int startUp;
        public int active;
        public int recovery;
        public int blockStun;
        public int hitStun;
    }

    // CSVデータを文字列型２次元配列に変換する
    // ファイルパス,変換される配列の値(参照渡し)
    public Dictionary<string, CharaData> readCSVData(string charaName)
    {
        TextAsset csv;
        string stagePath = "CharaData/" + charaName + "_SkillPerformance";
        csv = Resources.Load(stagePath) as TextAsset;

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
        int w = lines[0].Split(spliter, option).Length;

        // 返り値の2次元配列の要素数を設定
        Dictionary<string,CharaData> cdata = new Dictionary<string, CharaData>();
        
        // 行データを切り分けて,構造体配列へ変換する
        for (int i = 1; i < h; i++)
        {
            string[] splitedData = lines[i].Split(spliter, option);

            CharaData cd = new CharaData();
            cd.skill = splitedData[0];
            cd.damage = int.Parse(splitedData[1]);
            cd.attri = splitedData[2];
            cd.startCorrec = float.Parse(splitedData[3]);
            cd.conboCorrec = float.Parse(splitedData[4]);
            cd.attackLevel = int.Parse(splitedData[5]);
            cd.startUp = int.Parse(splitedData[6]);
            cd.active = int.Parse(splitedData[7]);
            cd.recovery = int.Parse(splitedData[8]);
            cd.blockStun = int.Parse(splitedData[10]);
            cd.hitStun = int.Parse(splitedData[11]);

            cdata.Add(cd.skill,cd);

            skillName.Add(cd.skill);
        }

        return cdata;
    }

    //確認表示用の関数
    //引数：2次元配列データ,行数,列数
    public void WriteCharaDatas(Dictionary<string, CharaData> array, int dataNum)
    {
        foreach (string key in array.Keys)
        {
            //行番号-列番号:データ値 と表示される
            Debug.Log(array[key].skill + " : ダメージ　 : " + array[key].damage);
            Debug.Log(array[key].skill + " : 属性　　　 : " + array[key].attri);
            Debug.Log(array[key].skill + " : 発生補正　 : " + array[key].startCorrec);
            Debug.Log(array[key].skill + " : コンボ補正 : " + array[key].conboCorrec);
            Debug.Log(array[key].skill + " : 攻撃レベル : " + array[key].attackLevel);
            Debug.Log(array[key].skill + " : 発生　　　 : " + array[key].startUp);
            Debug.Log(array[key].skill + " : 持続　　　 : " + array[key].active);
            Debug.Log(array[key].skill + " : 立ち直り　 : " + array[key].recovery);
            Debug.Log(array[key].skill + " : ガード硬直 : " + array[key].blockStun);
            Debug.Log(array[key].skill + " : ヒット硬直 : " + array[key].hitStun);
        }
    }

    public List<string> Skills { get{ return skillName; } }
}
