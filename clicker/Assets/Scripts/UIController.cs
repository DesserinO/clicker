using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Button menuButtonOpen;
    [SerializeField] Button menuButtonGamePanel;
    [SerializeField] Button menuButtonUpgradePanel;
    [SerializeField] Button menuButtonCloseMenu;
  
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject startPos;
    [SerializeField] GameObject endPos;
    [SerializeField] GameObject backgroundMenu;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject upgradePanel;
     
    // Start is called before the first frame update
    void Start()
    {
        gamePanel.SetActive(true);
        upgradePanel.SetActive(false);
        menuPanel.SetActive(false);
        backgroundMenu.SetActive(false);
        menuPanel.transform.position = startPos.transform.position;
    }

    public void OnClickMenuBtn()
    {
        menuPanel.SetActive(true);
        menuButtonOpen.gameObject.SetActive(false);
        backgroundMenu.gameObject.SetActive(true);
        menuPanel.transform.position = endPos.transform.position;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
    }

    public void OnClickCloseMenu()
    {
        menuPanel.transform.position = startPos.transform.position;
        menuButtonOpen.gameObject.SetActive(true);
        StartCoroutine(Wait());
        backgroundMenu.gameObject.SetActive(false);
        menuPanel.SetActive(false);
    }

    public void OnClickChangePanelToGame()
    {
        upgradePanel.SetActive(false);
        gamePanel.SetActive(true);
        OnClickCloseMenu();
    }

    public void OnClickChangePanelToUpgrade()
    {
        upgradePanel.SetActive(true);
        gamePanel.SetActive(false);
        OnClickCloseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePanel.activeInHierarchy)
        {
            /// Change button in menu to inactive
            menuButtonGamePanel.interactable = false;
        }
        else
        {
            /// Change to active statement
            menuButtonGamePanel.interactable = true;
        }

        if (upgradePanel.activeInHierarchy)
        {
            /// Change button in menu to inactive
            menuButtonUpgradePanel.interactable = false;
        }
        else
        {
            /// Change to active statement
            menuButtonUpgradePanel.interactable = true;
        }
    }
}
