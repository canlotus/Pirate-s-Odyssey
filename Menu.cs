using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject gamePanel; // Oyun ekranını temsil eden panel

    void Start()
    {
        // Oyun ekranını başlangıçta kapalı tut
        gamePanel.SetActive(false);
    }

    public void StartGame()
    {
        // Play butonuna basıldığında oyun ekranını aç
        gamePanel.SetActive(true);
    }

    public void CloseGamePanel()
    {
        // Gerekirse oyun ekranını kapatmak için bir fonksiyon
        gamePanel.SetActive(false);
    }
}