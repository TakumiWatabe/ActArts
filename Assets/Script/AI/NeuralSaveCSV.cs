using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class NeuralSaveCSV : MonoBehaviour {

	public static void SaveCSVData(NumYArray array, string csvName)
    {
        StreamWriter sw = new StreamWriter(@"Assets/Resources/AIData/" + csvName + ".csv", false, Encoding.GetEncoding("Shift_JIS"));
        //ヘッダー書き込み
        sw.WriteLine(csvName);
        string[] data = new string[array.Size[1]];
        //データ書き込み
        for (int i = 0; i < array.Size[0]; i++)
        {
            for (int j = 0; j < array.Size[1]; j++)
            {
                data[j] = array.Get()[i][j].ToString();
            }
            string str = string.Join(",", data);
            sw.WriteLine(str);
        }
        sw.Flush();
        sw.Close();
    }
}
