using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Button menuButtonGamePanel;
    [SerializeField] Button menuButtonUpgradePanel;
    [SerializeField] Button menuButtonResearchesPanel;
  
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject startPos;
    [SerializeField] GameObject endPos;
    [SerializeField] GameObject backgroundMenu;
    
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject upgradePanel;
    GameObject researchesPanel;

    private bool moveMenuPanel;
    private bool moveMenuPanelBack;

    public float moveSpeed;
    private float tempMenuPos;
     
    // Start is called before the first frame update
    void Start()
    {
        gamePanel.SetActive(true);
        upgradePanel.SetActive(false);
        /*researchesPanel.SetActive(false);*/
        menuPanel.SetActive(true);
        backgroundMenu.SetActive(false);
        menuPanel.transform.position = startPos.transform.position;
        // Developing
        menuButtonResearchesPanel.interactable = true;
    }

    public void MovePanel()
    {
        moveMenuPanelBack = false;
        moveMenuPanel = true;
    }

    public void MovePanelBack()
    {
        moveMenuPanelBack = true;
        moveMenuPanel = false;
    }

    public void OnClickChangePanelToGame()
    {
        upgradePanel.SetActive(false);
        gamePanel.SetActive(true);
        /*researchesPanel.SetActive(false);*/
        MovePanelBack();
    }

    public void OnClickChangePanelToUpgrade()
    {
        upgradePanel.SetActive(true);
        gamePanel.SetActive(false);
        /*researchesPanel.SetActive(false);*/
        MovePanelBack();
    }

    public void OnClickChangePanleToResearches()
    {
        upgradePanel.SetActive(false);
        gamePanel.SetActive(false);
        researchesPanel.SetActive(true);
        MovePanelBack();
    }

    private void CheckButtons()
    {
        if (gamePanel.activeInHierarchy)
        {
            menuButtonGamePanel.interactable = false;
        }
        else
        {
            menuButtonGamePanel.interactable = true;
        }

        if (upgradePanel.activeInHierarchy)
        {
            menuButtonUpgradePanel.interactable = false;
        }
        else
        {
            menuButtonUpgradePanel.interactable = true;
        }

        /*if (researchesPanel.activeInHierarchy)
        {
            menuButtonResearchesPanel.interactable = false;
        }
        else
        {
            menuButtonResearchesPanel.interactable = true;
        }*/
    }

    private void CheckMoveMenuPanel()
    {
        if (moveMenuPanel)
        {
            backgroundMenu.SetActive(true);
            menuPanel.transform.position = Vector3.Lerp(menuPanel.transform.position, endPos.transform.position, moveSpeed * Time.deltaTime);

            if (menuPanel.transform.localPosition.x == tempMenuPos)
            {
                moveMenuPanel = false;
                menuPanel.transform.position = endPos.transform.position;
                tempMenuPos = -999999999.99f;
            }

            if (moveMenuPanel)
            {
                tempMenuPos = menuPanel.transform.position.x;
            }
        }

        if (moveMenuPanelBack)
        {
            backgroundMenu.SetActive(false);
            menuPanel.transform.position = Vector3.Lerp(menuPanel.transform.position, startPos.transform.position, moveSpeed * Time.deltaTime);

            if (menuPanel.transform.localPosition.x == tempMenuPos)
            {
                moveMenuPanelBack = false;
                menuPanel.transform.position = startPos.transform.position;
                tempMenuPos = -999999999.99f;
            }

            if (moveMenuPanelBack)
            {
                tempMenuPos = menuPanel.transform.position.x;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckButtons();
        CheckMoveMenuPanel();
    }
}