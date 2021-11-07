using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ClickerController : MonoBehaviour
{
    [SerializeField] Text scoreText, idleText;
    [SerializeField] private ulong score;

    [SerializeField] private ulong scoreAmount, idleAmount;

    private ulong[] currentValues;
    private ulong[] idleValues;
    private ulong clickMultiplier = 10;

    float elapsed = 0f;

    public Dictionary<string, Text[]> upgradeModule = new Dictionary<string, Text[]>();

    public void OnClick()
    {
        score += scoreAmount;
    }

    // Idle function 
    private void idle() 
    { 
        score += idleAmount; 
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
        IdleController.Instance.IdleInit(out currentValues, out idleValues, upgradeModule);
    }

    // Update is called once per frame
    void Update()
    {
        IdleController.Instance.UpdateText(currentValues);

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