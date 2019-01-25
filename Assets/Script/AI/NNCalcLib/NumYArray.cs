using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumYArray {

    private List<List<float>> array = new List<List<float>>();

    #region コンストラクタ
    public NumYArray()
    {

    }
    public NumYArray(List<List<float>> n)
    {
        array = n;
    }
    public NumYArray(List<float[]> n)
    {
        for (int i = 0; i < n.Count; i++)
        {
            List<float> a = new List<float>();
            for (int j = 0; j < n[i].Length; j++)
            {
                a.Add(n[i][j]);
            }
            array.Add(a);
        }
    }
    public NumYArray(float[][] n)
    {
        for (int i = 0; i < n.Length; i++)
        {
            List<float> a = new List<float>();
            for (int j = 0; j < n[i].Length; j++)
            {
                a.Add(n[i][j]);
            }
            array.Add(a);
        }
    }
    public NumYArray(NumYArray n)
    {
        array = new List<List<float>>();
        List<List<float>> nArray = n.Get();
        
        for (int i = 0; i < nArray.Count; i ++)
        {
            array.Add(new List<float>());
            List<float> subList = nArray[i];
            for(int j = 0; j < subList.Count; j ++)
            {
                array[i].Add(subList[j]);
            }
        }
    }
    #endregion

    #region NumYArray内の数値を表示
    /// <summary>
    /// NumYArray内の数値を表示
    /// </summary>
    public void DisplayNum()
    {
        for(int i=0; i < array.Count; i++)
        {
            string str = "Array["+ i +"]:{";
            for (int j = 0; j < array[i].Count; j++)
            {
                if (j > 0)
                    str += ",";
                str += array[i][j];
            }
            str += "}";
            Debug.Log(str);
        }
    }
    #endregion

    #region NumYArray内の数値を返す
    public List<List<float>> Get()
    {
        return array;
    }
    public List<float> Get(int n)
    {
        return array[n];
    }
    public float Get(int n, int m)
    {
        return array[n][m];
    }
    #endregion

    #region float配列の追加
    /// <summary>
    /// float配列を追加
    /// </summary>
    /// <param name="n">追加する配列</param>
    public void Add(List<float> n)
    {
        array.Add(n);
    }
    public void Add(float[] n)
    {
        List<float> a = new List<float>();
        for (int i = 0; i < n.Length; i++)
        {
            a.Add(n[i]);
        }
        array.Add(a);
    }
    #endregion

    #region サイズを返す
    public int[] Size
    {
        get
        {
            int[] size = new int[2];
            size[0] = array.Count;
            size[1] = array[0].Count;

            return size;
        }
    }
    #endregion

    #region 四則演算
    #region 足し算
    public static NumYArray operator +(NumYArray a,NumYArray b)
    {
        int[] sizeA = a.Size;
        int[] sizeB = b.Size;

        NumYArray copyA = new NumYArray(a);
        NumYArray copyB = new NumYArray(b);

        List<List<float>> arrayA = copyA.Get();
        List<List<float>> arrayB = copyB.Get();

        //行列の足し算(配列の要素がすべて同じの場合)
        if (sizeA[0] == sizeB[0]) {
            for (int i = 0; i < sizeA[0]; i++)
            {
                for (int j = 0; j < sizeA[1]; j++)
                {
                    arrayA[i][j] += arrayB[i][j];
                }
            }
        }
        //(配列の要素の1次元目のみ違う場合)
        else if(sizeA[1] == sizeB[1]) {
            for (int i = 0; i < sizeA[0]; i++)
            {
                for (int j = 0; j < sizeA[1]; j++)
                {
                    arrayA[i][j] += arrayB[0][j];
                }
            }
        }
        else {
            Debug.Log("Error : Argument size does not match.");
        }

        NumYArray array = new NumYArray(arrayA);
        return array;
    }
    public static NumYArray operator +(NumYArray a, float b)
    {
        int[] sizeA = a.Size;

        NumYArray copyA = new NumYArray(a);

        List<List<float>> arrayA = copyA.Get();

        for (int i = 0; i < sizeA[0]; i++)
        {
            for (int j = 0; j < sizeA[1]; j++)
            {
                arrayA[i][j] += b;
            }
        }

        NumYArray array = new NumYArray(arrayA);
        return array;
    }
    public static NumYArray operator +(float a, NumYArray b)
    {
        int[] sizeB = b.Size;

        NumYArray copyB = new NumYArray(b);

        List<List<float>> arrayB = copyB.Get();

        for (int i = 0; i < sizeB[0]; i++)
        {
            for (int j = 0; j < sizeB[1]; j++)
            {
                arrayB[i][j] += a;
            }
        }

        NumYArray array = new NumYArray(arrayB);
        return array;
    }
    #endregion
    #region 引き算
    public static NumYArray operator -(NumYArray a, NumYArray b)
    {
        int[] sizeA = a.Size;
        int[] sizeB = b.Size;

        NumYArray copyA = new NumYArray(a);
        NumYArray copyB = new NumYArray(b);

        List<List<float>> arrayA = copyA.Get();
        List<List<float>> arrayB = copyB.Get();

        //行列の引き算(配列の要素がすべて同じの場合)
        if (sizeA[0] == sizeB[0])
        {
            for (int i = 0; i < sizeA[0]; i++)
            {
                for (int j = 0; j < sizeA[1]; j++)
                {
                    arrayA[i][j] -= arrayB[i][j];
                }
            }
        }
        //(配列の要素の1次元目のみ違う場合)
        else if (sizeA[1] == sizeB[1])
        {
            for (int i = 0; i < sizeA[0]; i++)
            {
                for (int j = 0; j < sizeA[1]; j++)
                {
                    arrayA[i][j] -= arrayB[0][j];
                }
            }
        }
        else
        {
            Debug.Log("Error : Argument size does not match.");
        }

        NumYArray array = new NumYArray(arrayA);
        return array;
    }
    public static NumYArray operator -(NumYArray a, float b)
    {
        int[] sizeA = a.Size;

        NumYArray copyA = new NumYArray(a);

        List<List<float>> arrayA = copyA.Get();

        for (int i = 0; i < sizeA[0]; i++)
        {
            for (int j = 0; j < sizeA[1]; j++)
            {
                arrayA[i][j] -= b;
            }
        }

        NumYArray array = new NumYArray(arrayA);
        return array;
    }
    public static NumYArray operator -(float a, NumYArray b)
    {
        int[] sizeB = b.Size;

        NumYArray copyB = new NumYArray(b);

        List<List<float>> arrayB = copyB.Get();

        for (int i = 0; i < sizeB[0]; i++)
        {
            for (int j = 0; j < sizeB[1]; j++)
            {
                arrayB[i][j] = a - arrayB[i][j];
            }
        }

        NumYArray array = new NumYArray(arrayB);
        return array;
    }
    #endregion
    #region 掛け算
    public static NumYArray operator *(NumYArray a, NumYArray b)
    {
        int[] sizeA = a.Size;
        int[] sizeB = b.Size;

        NumYArray copyA = new NumYArray(a);
        NumYArray copyB = new NumYArray(b);

        List<List<float>> arrayA = copyA.Get();
        List<List<float>> arrayB = copyB.Get();

        //行列の掛け算(配列の要素がすべて同じの場合)
        if (sizeA[0] == sizeB[0])
        {
            for (int i = 0; i < sizeA[0]; i++)
            {
                for (int j = 0; j < sizeA[1]; j++)
                {
                    arrayA[i][j] *= arrayB[i][j];
                }
            }
        }
        //(配列の要素の1次元目のみ違う場合)
        else if (sizeA[1] == sizeB[1])
        {
            for (int i = 0; i < sizeA[0]; i++)
            {
                for (int j = 0; j < sizeA[1]; j++)
                {
                    arrayA[i][j] *= arrayB[0][j];
                }
            }
        }
        else
        {
            Debug.Log("Error : Argument size does not match.");
        }

        NumYArray array = new NumYArray(arrayA);
        return array;
    }
    public static NumYArray operator *(NumYArray a, float b)
    {
        int[] sizeA = a.Size;

        NumYArray copyA = new NumYArray(a);

        List<List<float>> arrayA = copyA.Get();

        for (int i = 0; i < sizeA[0]; i++)
        {
            for (int j = 0; j < sizeA[1]; j++)
            {
                arrayA[i][j] *= b;
            }
        }

        NumYArray array = new NumYArray(arrayA);
        return array;
    }
    public static NumYArray operator *(float a, NumYArray b)
    {
        int[] sizeB = b.Size;

        NumYArray copyB = new NumYArray(b);

        List<List<float>> arrayB = copyB.Get();

        for (int i = 0; i < sizeB[0]; i++)
        {
            for (int j = 0; j < sizeB[1]; j++)
            {
                arrayB[i][j] *= a;
            }
        }

        NumYArray array = new NumYArray(arrayB);
        return array;
    }
    public static NumYArray operator -(NumYArray a)
    {
        int[] sizeA = a.Size;

        NumYArray copyA = new NumYArray(a);

        List<List<float>> arrayA = copyA.Get();

        for (int i = 0; i < sizeA[0]; i++)
        {
            for (int j = 0; j < sizeA[1]; j++)
            {
                arrayA[i][j] *= -1;
            }
        }

        NumYArray array = new NumYArray(arrayA);
        return array;
    }
    #endregion
    #region 割り算
    public static NumYArray operator /(NumYArray a, NumYArray b)
    {
        int[] sizeA = a.Size;
        int[] sizeB = b.Size;

        NumYArray copyA = new NumYArray(a);
        NumYArray copyB = new NumYArray(b);

        List<List<float>> arrayA = copyA.Get();
        List<List<float>> arrayB = copyB.Get();

        //行列の割り算(配列の要素がすべて同じの場合)
        if (sizeA[0] == sizeB[0])
        {
            for (int i = 0; i < sizeA[0]; i++)
            {
                for (int j = 0; j < sizeA[1]; j++)
                {
                    arrayA[i][j] /= arrayB[i][j];
                }
            }
        }
        //(配列の要素の1次元目のみ違う場合)
        else if (sizeA[1] == sizeB[1])
        {
            for (int i = 0; i < sizeA[0]; i++)
            {
                for (int j = 0; j < sizeA[1]; j++)
                {
                    arrayA[i][j] /= arrayB[0][j];
                }
            }
        }
        else
        {
            Debug.Log("Error : Argument size does not match.");
        }

        NumYArray array = new NumYArray(arrayA);
        return array;
    }
    public static NumYArray operator /(NumYArray a, float b)
    {
        int[] sizeA = a.Size;

        NumYArray copyA = new NumYArray(a);

        List<List<float>> arrayA = copyA.Get();

        for (int i = 0; i < sizeA[0]; i++)
        {
            for (int j = 0; j < sizeA[1]; j++)
            {
                arrayA[i][j] /= b;
            }
        }

        NumYArray array = new NumYArray(arrayA);
        return array;
    }
    public static NumYArray operator /(float a, NumYArray b)
    {
        int[] sizeB = b.Size;

        NumYArray copyB = new NumYArray(b);

        List<List<float>> arrayB = copyB.Get();

        for (int i = 0; i < sizeB[0]; i++)
        {
            for (int j = 0; j < sizeB[1]; j++)
            {
                arrayB[i][j] = a / arrayB[i][j];
            }
        }

        NumYArray array = new NumYArray(arrayB);
        return array;
    }
    #endregion
    #endregion

    #region 転置行列の生成
    public NumYArray T {
        get {
            int[] size = this.Size;

            NumYArray copy = new NumYArray(this);

            List<List<float>> copyList = copy.Get();
            List<List<float>> list = new List<List<float>>();

            //転置行列の生成
            for (int i = 0; i < size[1]; i++) {
                list.Add(new List<float>());
                for (int j = 0; j < size[0]; j++) {
                    list[i].Add(copyList[j][i]);
                }
            }

            return new NumYArray(list);
        }
    }
    #endregion
}
