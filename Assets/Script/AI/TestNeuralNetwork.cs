using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNeuralNetwork : MonoBehaviour {

    [SerializeField]
    private string inputDataName = "";
    [SerializeField]
    private string outputDataName = "";

	// Use this for initialization
	void Start () {
        NumYArray xData = new NumYArray(NeuralReadCSV.readCSVData(inputDataName));
        NumYArray yData = new NumYArray(NeuralReadCSV.readCSVData(outputDataName));

        int epochs = 2000;
        float learningRate = 0.1f;

        //ニューラルネットワークの生成
        NeuralNetwork nn = new NeuralNetwork(xData.Size[1], 8, yData.Size[1], "Aoi");

        //時間計測
        float startTime = Time.realtimeSinceStartup;

        //データを基にトレーニング
        nn.Train(xData, yData, epochs * yData.Get().Count, learningRate, false);

        float endTime = Time.realtimeSinceStartup;
        //トレーニング時間の表示
        Debug.Log(endTime - startTime);

        //仮データによる結果の予測
        float[][] testList = { new float[] { 0.1f, 0.5f, 0.5f, 0.5f, 0.0f } };
        NumYArray testData = new NumYArray(testList);
        NumYArray result = new NumYArray(nn.Predict(testData));

        //予測結果の表示
        DisplayPredict(result);

        //学習した重みの保存
        nn.SaveLearningData("Aoi/");
	}

    //予測結果のデバッグ表示
    public void DisplayPredict(NumYArray data)
    {
        string[] name = new string[] {"立ち弱","立ち強","しゃがみ弱","しゃがみ強","波動","昇竜",
        "ガード","しゃがみガード","前進","後退","ダッシュ","ジャンプ","前ジャンプ","後ジャンプ"};

        for (int i = 0; i < name.Length; i++)
        {
            Debug.Log(name[i] + " : " + Mathf.Round(data.Get()[0][i] * 100) / 100);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
