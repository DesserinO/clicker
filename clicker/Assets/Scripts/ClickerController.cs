using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerController : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] private ulong score;
    
    [SerializeField] private ulong scoreAmount, idleAmount;

    float elapsed = 0f;
    

    public void OnClick()
    {
        score += scoreAmount;
        scoreText.text = $" {score} Money";
    }

    void Start()
    {
        scoreText.GetComponent<Text>();
    }

    // Idle function 
    private void idle()
    {
        score += idleAmount;
        scoreText.text = $" {score} Money";
    }

    // Update is called once per frame
    void Update()
    {
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