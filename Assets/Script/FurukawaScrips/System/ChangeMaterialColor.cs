using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColor : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }

    /// <summary>
    /// 色変更
    /// </summary>
    /// <param name="col">Color</param>
    public void ChangeColor(Color col,GameObject obj)
    {
        var materials = obj.GetComponent<SkinnedMeshRenderer>().materials;
        var materialsNum = obj.GetComponent<SkinnedMeshRenderer>().materials.Length;
        for (int i = 0; i < materialsNum; i++)
        {
            if (CountOf(materials[i].name, "SKIN") + CountOf(materials[i].name, "Eye") > 0) continue;
            materials[i].color = col;
        }
    } 

    /// <summary>
    /// 指定した文字列がいくつあるか
    /// </summary>
    private static int CountOf(string target, params string[] strArray)
    {
        int count = 0;

        foreach (string str in strArray)
        {
            int index = target.IndexOf(str, 0);
            while (index != -1)
            {
                count++;
                index = target.IndexOf(str, index + str.Length);
            }
        }

        return count;
    }
}
