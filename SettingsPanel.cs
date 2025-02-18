using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public GameObject languagePanel; 
    public Button openPanelButton; 
    public Button closePanelButton; 
    public Button trButton; 
    public Button enButton; 
    public Text statusText; 

    public Button musicOnButton; 
    public Button musicOffButton;

    public ScenarioManager scenarioManager; 
    public MusicManager musicManager; 

    void Start()
    {
        languagePanel.SetActive(false);

        openPanelButton.onClick.AddListener(OpenPanel);
        closePanelButton.onClick.AddListener(ClosePanel);
        trButton.onClick.AddListener(() => ChangeLanguage("tr"));
        enButton.onClick.AddListener(() => ChangeLanguage("en"));

        musicOnButton.onClick.AddListener(() => ToggleMusic(true));
        musicOffButton.onClick.AddListener(() => ToggleMusic(false));

        UpdateStatusText();
        UpdateMusicButtons();
    }

    void OpenPanel()
    {
        languagePanel.SetActive(true);
    }

    void ClosePanel()
    {
        languagePanel.SetActive(false);
    }

    public void ChangeLanguage(string langCode)
    {
        GameLanguage.gl.Setlanguage(langCode);

        scenarioManager.SetLanguage(langCode);

        UpdateStatusText();
    }

    void UpdateStatusText()
    {
        if (statusText != null)
        {
            statusText.text = $"Dil: {(GameLanguage.gl.currentLanguage == "tr" ? "Türkçe" : "İngilizce")}";
        }
    }

    void ToggleMusic(bool isMusicOn)
    {
        PlayerPrefs.SetInt("MusicState", isMusicOn ? 1 : 0);

        if (isMusicOn)
        {
            musicManager.PlayMusic();
        }
        else
        {
            musicManager.StopMusic();
        }

        UpdateMusicButtons();
    }

    void UpdateMusicButtons()
    {
        bool isMusicOn = PlayerPrefs.GetInt("MusicState", 1) == 1;

        musicOnButton.interactable = !isMusicOn; 
        musicOffButton.interactable = isMusicOn; 
    }
}
