using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class UpgradeController
{
    public Dictionary<string, Text[]> upgradeModule = new Dictionary<string, Text[]>();
    private Dictionary<string, float> valueModule = new Dictionary<string, float>();
    private List<string> keys = new List<string>();

    private ContentItem[] contentItems;

    private string[] currentValues;
    private string[] idleValues;
    private ulong[] lvls;

    public static UpgradeController Instance { get; private set; } = new UpgradeController();

    public void SetValueModule(Dictionary<string, float> valueModule)
    {
        this.valueModule = valueModule;
    }

    public void SetKeys(List<string> keys)
    {
        this.keys = keys;
    }

    public Dictionary<string, Text[]> GetUpgradeText()
    {
        // Get count of "ContentItem" PreFab
        var obj = Resources.FindObjectsOfTypeAll<GameObject>().LongCount(g => g.CompareTag("ItemContent"));

        GameObject[] temp = Resources.FindObjectsOfTypeAll<GameObject>().ToArray();
        GameObject[] contItem = new GameObject[obj - 1];

        // Get ContentItem
        foreach (GameObject a in temp)
        {
            for (int i = 0; i < obj - 1; i++)
            {
                if (a.name == i.ToString())
                {
                    contItem[Int64.Parse(a.name)] = a;
                }
            }
        }

        // Add ContentItems to Dictionary
        try
        {
            for (int i = 0; i < obj - 1; i++)
            {
                upgradeModule.Add(i.ToString(), contItem[i].GetComponentsInChildren<Text>());
            }
        }
        catch { }

        // Set Names of "Name" text in ContentItem
        try
        {
            contentItems = Resources.LoadAll<ContentItem>("Data").ToArray();
            foreach (ContentItem g in contentItems)
            {
                for (int i = 0; i < g.Names.Length; i++)
                {
                    upgradeModule[i.ToString()][0].text = g.Names[i];
                }
            }
        }
        catch { }

        return upgradeModule;
    }

    public void UpgradeInit(out string[] return1, out string[] return2, out ulong[] return3)
    {
        currentValues = new string[NumSystem.Instance.GetCountContItem()];
        idleValues = new string[currentValues.Length];
        lvls = new ulong[currentValues.Length];

        currentValues[0] = "50";
        idleValues[0] = "1";

        float tempCoef = 3;
        double tempValue;
        float multiplier = 5;

        for (int i = 0; i < lvls.Length; i++)
        {
            lvls[i] = 0;
        }

        // Init current values for price
        for (int i = 1; i < NumSystem.Instance.GetCountContItem(); i++)
        {
            if (currentValues[i - 1].Split(' ').Length < 2)
            {
                tempValue = float.Parse(currentValues[i - 1]) * multiplier;
            } else
            {
                tempValue = float.Parse(currentValues[i - 1].Split(' ')[0]) * multiplier;
            }


            if (tempValue < Math.Pow(10, 6) - 1
                && tempCoef <= 3)
            {
                currentValues[i] = $"{tempValue}";
                multiplier += 5;
            }

            if (tempValue > Math.Pow(10, 6)
                && tempCoef == 3)
            {
                tempCoef += 3;
                tempValue /= Math.Pow(10, 6);
                currentValues[i] = NumSystem.Instance.SetCharCoef(tempValue.ToString(), tempCoef);
                multiplier += 5;
                continue;
            }

            if (tempValue > 0
                && tempValue < 1000
                && tempCoef > 3)
            {
                currentValues[i] = NumSystem.Instance.SetCharCoef($"{tempValue}", tempCoef);
                multiplier += 5;
            }

            if (tempValue > 999
                && tempCoef > 3)
            {
                tempCoef += 3;
                currentValues[i] = NumSystem.Instance.SetCharCoef((tempValue / 1000).ToString(), tempCoef);
                multiplier += 5;
                continue;
            }
        }

        // Init idle values for idle 
        for (int i = 1; i < currentValues.Length; i++)
        {
            if (currentValues[i - 1].Split(' ').Length < 2)
            {
                idleValues[i] = (float.Parse(currentValues[i - 1]) / 50).ToString();
            }
            else
            {
                idleValues[i] = $"{float.Parse(currentValues[i - 1].Split(' ')[0]) / 50} {currentValues[i - 1].Split(' ')[1]}";
            }
        }

        return1 = currentValues;
        return2 = idleValues;
        return3 = lvls;
    }

    public void UpdateText(string[] value, ulong[] lvl)
    {
        upgradeModule = GetUpgradeText();
        for (int i = 0; i < upgradeModule.Keys.LongCount(); i++)
        {
            try { upgradeModule[i.ToString()][1].text = value[i]; } catch { }
            try { upgradeModule[i.ToString()][2].text = $"LVL: {lvl[i]}"; } catch { }
        }
    }
}   