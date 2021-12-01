using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NumSystem : MonoBehaviour
{
    private List<string> keys = new List<string>();
    private List<char> alpha = new List<char>();
    private Dictionary<string, float> valueModule = new Dictionary<string, float>();

    public static NumSystem Instance { get; private set; } = new NumSystem();

    public void NumInit(out Dictionary<string, float> return1, out List<string> return2)
    {
        keys.Add("M");
        keys.Add("B");
        keys.Add("T");

        for (char c = 'a'; c <= 'z'; c++)
        {
            alpha.Add(c);
            keys.Add(c.ToString());
        }

        for (int i = 0; i < alpha.Count; i++)
        {
            for (int j = 0; j < alpha.Count; j++)
            {
                keys.Add(alpha[i].ToString() + alpha[j].ToString());
            }
        }

        float multiplier = 6;
        for (int i = 0; i < keys.Count; i++)
        {
            valueModule.Add(keys[i], multiplier);
            multiplier += 3;
        }

        return1 = valueModule;
        return2 = keys;
    }

    public string SetCharCoef(string str, float coef)
    {
        foreach (string k in valueModule.Keys)
        {
            if (valueModule[k] == coef)
            {
                str = $"{str} {k}";
                break;
            }
        }

        return str;
    }

    public float GetCoef(string str)
    {
        float temp = 0;
        foreach (string k in valueModule.Keys)
        {
            if (str.Split(' ')[1].Contains(k))
            {
                temp = valueModule[k];
            }
        }

        return temp;
    }

    public long GetCountContItem()
    {
        return Resources.FindObjectsOfTypeAll<GameObject>().LongCount(g => g.CompareTag("ItemContent")) - 2;
    }

    public bool IsContainChar(string str)
    {
        bool temp = false;
        foreach (string k in valueModule.Keys)
        {
            if (str.Contains(k))
            {
                temp = true;
            } 
        }

        return temp;
    }

    public float GetDiff(string coef1, string coef2)
    {
        float value1 = GetCoef(coef1);
        float value2 = GetCoef(coef2);
        float temp = 0;
        if (value1 > value2)
        {
            temp += value1 - value2;
        }

        if (value1 < value2)
        {
            temp = -1;
        }

        return temp;
    }

    public float GetDegree(float value)
    {
        return value <= 12 ? value : 12;
    }
}
