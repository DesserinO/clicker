using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class ClickerController : MonoBehaviour
{
    [SerializeField] Text scoreText, idleText;
    [SerializeField] private ulong score;
    
    [SerializeField] private ulong scoreAmount, idleAmount;

    [SerializeField] GameObject clickX2;
    [SerializeField] GameObject upgrade1;
    [SerializeField] GameObject upgrade2;
    [SerializeField] GameObject upgrade3;
    [SerializeField] GameObject upgrade4;
    [SerializeField] GameObject upgrade5;
    [SerializeField] GameObject upgrade6;
    [SerializeField] GameObject upgrade7;
    [SerializeField] GameObject upgrade8;
    [SerializeField] GameObject upgrade9;

    private ulong[] defaultValues;
    private ulong[] currentValues;
    private ulong[] idleValues;
    private ulong multiplier = 5;
    private ulong clickMultiplier = 10;

    float elapsed = 0f;

    public Dictionary<string, Text[]> upgradeModule = new Dictionary<string, Text[]>();
    public List<string> keyList = new List<string>();

    public void OnClick()
    {
        score += scoreAmount;
    }

    // Idle function 
    private void idle() 
    { 
        score += idleAmount; 
    }

    void UpgradeInit()
    {
        var obj = Resources.FindObjectsOfTypeAll<GameObject>().LongCount(g => g.CompareTag("ItemContent"));
        defaultValues = new ulong[obj - 1];
        currentValues = new ulong[defaultValues.Length];
        idleValues = new ulong[defaultValues.Length - 1];
        defaultValues[0] = 5;
        idleValues[0] = 1;

        for (int i = 1; i < idleValues.Length; i++)
        {
            idleValues[i] = idleValues[i - 1] * 10;
        }
        
        for (int i = 1; i < defaultValues.Length; i++)
        {
            multiplier *= 2;
            defaultValues[i] = defaultValues[i - 1] * multiplier;
        }

        currentValues = defaultValues;

        // Initialization
        upgradeModule.Add("Click x2", clickX2.GetComponentsInChildren<Text>());
        upgradeModule.Add("Upgrade1", upgrade1.GetComponentsInChildren<Text>());
        upgradeModule.Add("Upgrade2", upgrade2.GetComponentsInChildren<Text>());
        upgradeModule.Add("Upgrade3", upgrade3.GetComponentsInChildren<Text>());
        upgradeModule.Add("Upgrade4", upgrade4.GetComponentsInChildren<Text>());
        upgradeModule.Add("Upgrade5", upgrade5.GetComponentsInChildren<Text>());
        upgradeModule.Add("Upgrade6", upgrade6.GetComponentsInChildren<Text>());
        upgradeModule.Add("Upgrade7", upgrade7.GetComponentsInChildren<Text>());
        upgradeModule.Add("Upgrade8", upgrade8.GetComponentsInChildren<Text>());
        upgradeModule.Add("Upgrade9", upgrade9.GetComponentsInChildren<Text>());

        Dictionary<string, Text[]>.KeyCollection keyL = upgradeModule.Keys;
        foreach (string k in keyL)
        {
            keyList.Add(k);
        }

        for (int i = 0; i < currentValues.Length; i++)
        {
            upgradeModule[keyList[i]][0].text = keyList[i];
            upgradeModule[keyList[i]][1].text = currentValues[i].ToString();
        }
    }

    public void UpgradeClick()
    {
        if (score >= currentValues[0])
        {
            score -= currentValues[0];
            scoreAmount *= 2;
            currentValues[0] *= clickMultiplier;
        }
    }

    public void UpgradeIdle()
    {
        // EventSystem.current.currentSelectedGameObject.name;
        int num = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);
        if (score >= currentValues[num])
        {
            score -= currentValues[num];
            idleAmount += idleValues[num - 1];
            currentValues[num] += (currentValues[num] / 10);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpgradeInit();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < currentValues.Length; i++)
        {
            upgradeModule[keyList[i]][1].text = currentValues[i].ToString();
        }

        idleText.text = $"IDLE: {idleAmount}";
        scoreText.text = $" {score}";

        if (idleAmount != 0)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 1f)
            {
                elapsed = elapsed % 1f;
                idle();
            }
        }
    }
}