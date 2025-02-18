using UnityEngine;
using UnityEngine.UI;

public class FirstTimePanelManager : MonoBehaviour
{
    public GameObject firstTimePanel; 
    public Button option1Button; 
    public Button option2Button; 

    private const string FIRST_TIME_KEY = "FirstTime"; 

    void Start()
    {
        
        if (PlayerPrefs.GetInt(FIRST_TIME_KEY, 0) == 1)
        {
            firstTimePanel.SetActive(false);
            return;
        }

        
        firstTimePanel.SetActive(true);

        
        option1Button.onClick.AddListener(() => HandleButtonClick());
        option2Button.onClick.AddListener(() => HandleButtonClick());
    }

    void HandleButtonClick()
    {
        
        firstTimePanel.SetActive(false);

        
        PlayerPrefs.SetInt(FIRST_TIME_KEY, 1);
        PlayerPrefs.Save();
    }
}
