using UnityEngine;
using UnityEngine.UI;

public class FirstTimePanelManager : MonoBehaviour
{
    public GameObject firstTimePanel; // Paneli atayın
    public Button option1Button; // Birinci buton
    public Button option2Button; // İkinci buton

    private const string FIRST_TIME_KEY = "FirstTime"; // PlayerPrefs anahtarı

    void Start()
    {
        // Daha önce gösterildiyse paneli kapatma
        if (PlayerPrefs.GetInt(FIRST_TIME_KEY, 0) == 1)
        {
            firstTimePanel.SetActive(false);
            return;
        }

        // İlk defa gösterilmesi için paneli aç
        firstTimePanel.SetActive(true);

        // Butonlara tıklama işlevi ekle
        option1Button.onClick.AddListener(() => HandleButtonClick());
        option2Button.onClick.AddListener(() => HandleButtonClick());
    }

    void HandleButtonClick()
    {
        // Paneli kapat
        firstTimePanel.SetActive(false);

        // İlk defa gösterildiğini kaydet
        PlayerPrefs.SetInt(FIRST_TIME_KEY, 1);
        PlayerPrefs.Save();
    }
}