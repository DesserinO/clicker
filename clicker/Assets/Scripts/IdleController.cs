using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.IO;

public class IdleController
{
    public Dictionary<string, Text[]> upgradeModule = new Dictionary<string, Text[]>();
    
    private ContentItem[] contentItems;

    /*private ulong idleAmount;*/

    private ulong[] currentValues;
    private ulong[] idleValues;

    public static IdleController Instance { get; private set; } = new IdleController();

    public void IdleInit(out ulong[] return1, out ulong[] return2, Dictionary<string, Text[]> return3)
    {
        // Get count of "ContentItem" PreFab
        var obj = Resources.FindObjectsOfTypeAll<GameObject>().LongCount(g => g.CompareTag("ItemContent"));
        
        GameObject[] temp = Resources.FindObjectsOfTypeAll<GameObject>().ToArray();
        GameObject[] contItem = new GameObject[obj - 1];

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

        try 
        {
            for (int i = 0; i < obj - 1; i++)
            {
                upgradeModule.Add(i.ToString(), contItem[i].GetComponentsInChildren<Text>());
            }
        } catch { }
        

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
        } catch { }

        currentValues = new ulong[obj - 1];
        idleValues = new ulong[currentValues.Length - 1];
        currentValues[0] = 5;
        idleValues[0] = 1;

        for (int i = 1; i < idleValues.Length; i++)
        {
            idleValues[i] = idleValues[i - 1] * 10;
        }

        ulong multiplier = 5;
        for (int i = 1; i < currentValues.Length; i++)
        {
            multiplier += (multiplier / 5);
            currentValues[i] = currentValues[i - 1] * multiplier;
        }

        return1 = currentValues;
        return2 = idleValues;
        return3 = upgradeModule;
    }

    public void UpdateText(ulong[] value)
    {
        for (int i = 0; i < upgradeModule.Keys.LongCount(); i++)
        {
            try { upgradeModule[i.ToString()][1].text = value[i].ToString(); } catch { }
        }
    }
}
