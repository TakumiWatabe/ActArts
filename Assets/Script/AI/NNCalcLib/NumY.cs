using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumY {

    #region 数値計算用行列を生成
    /// <summary>
    /// 数値計算用の行列を生成
    /// </summary>
    /// <param name="n">行列</param>
    /// <returns>生成した行列</returns>
    public static NumYArray NumYArray(List<List<float>> n)
    {
        return new NumYArray(n);
    }
    public static NumYArray NumYArray(List<float[]> n)
    {
        return new NumYArray(n);
    }
    public static NumYArray NumYArray(float[][] n)
    {
        return new NumYArray(n);
    }
    public static NumYArray NumYArray(NumYArray n)
    {
        return new NumYArray(n);
    }
    public static NumYArray NumYArray()
    {
        return new NumYArray();
    }
    #endregion

    #region シグモイド関数による計算
    /// <summary>
    /// シグモイド関数による計算
    /// </summary>
    /// <param name="a">自作配列</param>
    /// <returns>計算された自作配列</returns>
    public static NumYArray Sigmoid(NumYArray a)
    {
        NumYArray copyArray = new NumYArray(a);
        List<List<float>> array = copyArray.Get();

        for (int i = 0; i < array.Count; i++)
        {
            for (int j = 0; j < array[i].Count; j++)
            {
                array[i][j] = 1 / (1 + Mathf.Exp(-array[i][j]));
            }
        }
        
        NumYArray returnArray = new NumYArray(array);
        return returnArray;
    }
    #endregion

    #region シグモイド関数の微分
    /// <summary>
    /// シグモイド関数の微分
    /// </summary>
    /// <param name="a">自作配列</param>
    /// <returns>微分された自作配列</returns>
    public static NumYArray SigmoidDerivative(NumYArray a)
    {
        NumYArray copyArray = new NumYArray(a);

        return Sigmoid(copyArray) * (1 - Sigmoid(copyArray));
    }
    #endregion

    #region 行列の積を求める
    /// <summary>
    /// 行列の積を求める
    /// </summary>
    /// <param name="a">自作行列</param>
    /// <param name="b">自作行列</param>
    /// <returns>計算された自作行列</returns>
    public static NumYArray Dot(NumYArray a, NumYArray b)
    {
        int[] sizeA = a.Size;
        int[] sizeB = b.Size;

        NumYArray copyA = new NumYArray(a);
        NumYArray copyB = new NumYArray(b);

        List<List<float>> arrayA = copyA.Get();
        List<List<float>> arrayB = copyB.Get();
        List<List<float>> array = new List<List<float>>();

        //配列の要素確保
        for (int i = 0; i < sizeA[0]; i++)
        {
            array.Add(new List<float>());
            for (int j = 0; j < sizeB[1]; j++)
            {
                array[i].Add(0);
            }
        }

        //行列の内積計算
        if (sizeA[1] == sizeB[0])
        {
            for (int i = 0; i < array.Count; i++)
            {
                for (int j = 0; j < array[i].Count; j++)
                {
                    float term = 0;
                    for (int k = 0; k < sizeA[1]; k++)
                    {
                        term += arrayA[i][k] * arrayB[k][j];
                    }
                    array[i][j] = term;
                }
            }
        }
        else
        {
            //Debug.Log("Error : Argument size does not match.");
        }

        NumYArray returnArray = new NumYArray(array);
        return returnArray;
    }
    #endregion

    #region 行列の和を求める
    public static NumYArray Sum(NumYArray a, int axis = -1)
    {
        int[] sizeA = a.Size;

        NumYArray copyArray = new NumYArray(a);

        List<List<float>> copyList = copyArray.Get();
        List<List<float>> list = new List<List<float>>();

        //全ての数値を足す
        if (axis == -1) {
            float sumNum = 0;
            for (int i = 0; i < sizeA[0]; i++) {
                for (int j = 0; j < sizeA[1]; j++) {
                    sumNum += copyList[i][j];
                }
            }
            list.Add(new List<float>());
            list[0].Add(sumNum);
        }
        //指定された計算方法による加算
        else {
            //列の和を求める
            if(axis == 0) {
                list.Add(new List<float>());
                for (int i = 0; i < sizeA[1]; i++)
                {
                    float sumNum = 0;
                    for (int j = 0; j < sizeA[0]; j++)
                    {
                        sumNum += copyList[j][i];
                    }
                    list[0].Add(sumNum);
                }
            }
            //行の和を求める
            else if (axis == 1) {
                for (int i = 0; i < sizeA[0]; i++) {
                    list.Add(new List<float>());
                    float sumNum = 0;
                    for (int j = 0; j < sizeA[1]; j++) {
                        sumNum += copyList[i][j];
                    }
                    list[i].Add(sumNum);
                }
            }
            else {
                Debug.Log("Error : Argument size does not match.");
            }
        }

        NumYArray returnArray = new NumYArray(list);
        return returnArray;
    }
    #endregion

    #region ランダムな数値の配列を生成
    public static NumYArray RandomArray(int columnNum, int cellNum, float max, float min)
    {
        List<List<float>> list = new List<List<float>>();
        //ランダム配列の生成
        for (int i = 0; i < columnNum; i++)
        {
            list.Add(new List<float>());
            for (int j = 0; j < cellNum; j++)
            {
                list[i].Add(Random.Range(min, max));
            }
        }

        NumYArray returnArray = new NumYArray(list);
        return returnArray;
    }
    #endregion
}
