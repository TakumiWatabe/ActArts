using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArray : MonoBehaviour {

    private NumYArray arrayA = NumY.NumYArray();

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 2; i++)
        {
            List<float> a = new List<float>();
            for (int j = 0; j < 3; j++)
            {
                a.Add(0);
            }
            arrayA.Add(a);
        }

        //float[][] b = { new float[] { 1, 2 }, new float[] { 3, 4 }, new float[] { 5, 6 } };
        //NumYArray arrayB = NumY.NumYArray(b);

        //float[][] c = { new float[] { 6, 5, 4, }, new float[] { 3, 2, 1 } };
        //NumYArray arrayC = NumY.NumYArray(c);

        //行列内の表示
        //arrayA.DisplayNum();
        //arrayB.DisplayNum();
        //arrayC.DisplayNum();
        //行列の四則演算
        //Array arrayD = NumY.Array(arrayC + arrayB);
        //arrayD = -arrayD;
        //arrayD.DisplayNum();
        //Array arrayE = NumY.Array(arrayC - arrayB);
        //arrayE.DisplayNum();
        //Array arrayF = NumY.Array(arrayC * arrayB);
        //arrayF.DisplayNum();
        //Array arrayG = NumY.Array(arrayC / arrayB);
        //arrayG.DisplayNum();

        //行列の内積計算
        //Array arrayH = NumY.Dot(arrayB, arrayC);
        //arrayH.DisplayNum();

        //行列の和を求めるテスト
        //float[][] o = { new float[] { 1, 2, 3, 4 }, new float[] { 1, 2, 3, 4 }, new float[] { 1, 2, 3, 4 } };
        //Array arrayO = NumY.Array(o);
        //arrayO.DisplayNum();
        //Array arrayO1 = new Array(NumY.Sum(arrayO));
        //arrayO1.DisplayNum();
        //Array arrayO2 = new Array(NumY.Sum(arrayO,0));
        //arrayO2.DisplayNum();
        //Array arrayO3 = new Array(NumY.Sum(arrayO,1));
        //arrayO3.DisplayNum();

        //転置行列を求めるテスト
        //float[][] p = { new float[] { 1, 2, 3, 4 }, new float[] { 1, 2, 3, 4 }, new float[] { 1, 2, 3, 4 } };
        //Array arrayP = NumY.Array(p);
        //arrayP = arrayP.T;
        //arrayP.DisplayNum();
    }
}
