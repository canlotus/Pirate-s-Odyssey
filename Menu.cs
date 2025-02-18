using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject gamePanel; 

    void Start()
    {
        
        gamePanel.SetActive(false);
    }

    public void StartGame()
    {
        
        gamePanel.SetActive(true);
    }

    public void CloseGamePanel()
    {
        
        gamePanel.SetActive(false);
    }
}
