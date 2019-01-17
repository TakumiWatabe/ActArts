using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork {

    private NumYArray w1 = new NumYArray();
    private NumYArray b1 = new NumYArray();
    private NumYArray w2 = new NumYArray();
    private NumYArray b2 = new NumYArray();

    public NeuralNetwork(int inputUnits, int hiddenUnits, int outpuUnits, string folderName)
    {
        //学習値のデータがあるならそこから読み込む
        if (System.IO.File.Exists(@"Assets/Resources/AIData/" + folderName + "/LearningDataCSVw1.csv"))
        {
            w1 = NeuralReadCSV.readCSVData("AIData/" + folderName + "/LearningDataCSVw1");
            b1 = NeuralReadCSV.readCSVData("AIData/" + folderName + "/LearningDataCSVb1");
            w2 = NeuralReadCSV.readCSVData("AIData/" + folderName + "/LearningDataCSVw2");
            b2 = NeuralReadCSV.readCSVData("AIData/" + folderName + "/LearningDataCSVb2");
        }
        else
        {
            w1 = NumY.RandomArray(inputUnits, hiddenUnits, 1.0f, -1.0f);
            b1 = NumY.RandomArray(1, hiddenUnits, 1.0f, -1.0f);
            w2 = NumY.RandomArray(hiddenUnits, outpuUnits, 1.0f, -1.0f);
            b2 = NumY.RandomArray(1, outpuUnits, 1.0f, -1.0f);
        }
    }

    /// <summary>
    /// 入力されたデータを元にトレーニング
    /// </summary>
    /// <param name="xData">状況データ</param>
    /// <param name="yData">教師データ</param>
    /// <param name="epochs">トレーニング回数</param>
    /// <param name="learningRate">学習率</param>
    /// <returns></returns>
    public NumYArray Train(NumYArray xData, NumYArray yData,int epochs,float learningRate)
    {
        NumYArray layerZ1 = new NumYArray();
        NumYArray layerA1 = new NumYArray();
        NumYArray layerZ2 = new NumYArray();
        NumYArray layerA2 = new NumYArray();

        NumYArray dlayerZ1 = new NumYArray();
        NumYArray dlayerZ2 = new NumYArray();
        NumYArray dw1 = new NumYArray();
        NumYArray db1 = new NumYArray();
        NumYArray dw2 = new NumYArray();
        NumYArray db2 = new NumYArray();

        for (int i = 0; i < epochs; i++)
        {
            //許容値を超えているか判定する用出力結果
            //Array  a = Predict(xData);

            int m = xData.Get()[0].Count;

            //順伝播・フォワードプロパゲーション
            layerZ1 = NumY.Dot(xData, this.w1) + this.b1;
            layerA1 = NumY.Sigmoid(layerZ1);
            layerZ2 = NumY.Dot(layerA1, this.w2) + this.b2;
            layerA2 = NumY.Sigmoid(layerZ2);

            //誤差逆伝播法・バックプロパゲーション
            dlayerZ2 = (layerA2 - yData) / m;
            dw2 = NumY.Dot(layerA1.T, dlayerZ2);
            db2 = NumY.Sum(dlayerZ2, 0);

            dlayerZ1 = NumY.Dot(dlayerZ2, w2.T) * NumY.SigmoidDerivative(layerZ1);
            dw1 = NumY.Dot(xData.T, dlayerZ1);
            db1 = NumY.Sum(dlayerZ1, 0);

            //パラメータ更新
            w2 -= learningRate * dw2;
            b2 -= learningRate * db2;
            w1 -= learningRate * dw1;
            b1 -= learningRate * db1;
        }

        NumYArray returnArray = new NumYArray(layerA2);
        return returnArray;
    }

    /// <summary>
    /// データを元に結果を予測する
    /// </summary>
    /// <param name="xData">予測する状況</param>
    /// <returns>予測結果</returns>
    public NumYArray Predict(NumYArray xData)
    {
        //順伝播・フォワードプロパゲーション
        NumYArray layerZ1 = new NumYArray(NumY.Dot(xData, this.w1) + this.b1);
        NumYArray layerA1 = new NumYArray(NumY.Sigmoid(layerZ1));
        NumYArray layerZ2 = new NumYArray(NumY.Dot(layerA1, this.w2) + this.b2);
        NumYArray layerA2 = new NumYArray(NumY.Sigmoid(layerZ2));

        NumYArray returnArray = new NumYArray(layerA2);
        return returnArray;
    }

    /// <summary>
    /// 重みの保存(学習値の保存)
    /// </summary>
    public void SaveLearningData(string folderName)
    {
        NeuralSaveCSV.SaveCSVData(this.w1, folderName + "LearningDataCSVw1");
        NeuralSaveCSV.SaveCSVData(this.b1, folderName + "LearningDataCSVb1");
        NeuralSaveCSV.SaveCSVData(this.w2, folderName + "LearningDataCSVw2");
        NeuralSaveCSV.SaveCSVData(this.b2, folderName + "LearningDataCSVb2");
    }

}
