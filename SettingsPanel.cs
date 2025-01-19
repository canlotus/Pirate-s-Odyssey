using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public GameObject languagePanel; // Dil ayar paneli
    public Button openPanelButton; // Paneli açma butonu
    public Button closePanelButton; // Paneli kapatma butonu
    public Button trButton; // Türkçe dil seçme butonu
    public Button enButton; // İngilizce dil seçme butonu
    public Text statusText; // Dil durumunu göstermek için bir Text

    public Button musicOnButton; // Müziği açma butonu
    public Button musicOffButton; // Müziği kapama butonu

    public ScenarioManager scenarioManager; // Senaryo yöneticisine erişim
    public MusicManager musicManager; // Müzik yöneticisine erişim

    void Start()
    {
        // Panel başlangıçta kapalı
        languagePanel.SetActive(false);

        // Dil butonlarını bağlayın
        openPanelButton.onClick.AddListener(OpenPanel);
        closePanelButton.onClick.AddListener(ClosePanel);
        trButton.onClick.AddListener(() => ChangeLanguage("tr"));
        enButton.onClick.AddListener(() => ChangeLanguage("en"));

        // Müzik butonlarını bağlayın
        musicOnButton.onClick.AddListener(() => ToggleMusic(true));
        musicOffButton.onClick.AddListener(() => ToggleMusic(false));

        // Başlangıç ayarlarını yükle
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
        // Dil ayarını değiştir
        GameLanguage.gl.Setlanguage(langCode);

        // Senaryo yöneticisinin dilini güncelle
        scenarioManager.SetLanguage(langCode);

        // Yeni dil durumunu göster
        UpdateStatusText();
    }

    void UpdateStatusText()
    {
        // Oyun dilini statusText'te göster
        if (statusText != null)
        {
            statusText.text = $"Dil: {(GameLanguage.gl.currentLanguage == "tr" ? "Türkçe" : "İngilizce")}";
        }
    }

    void ToggleMusic(bool isMusicOn)
    {
        // Müzik durumunu ayarla ve PlayerPrefs'e kaydet
        PlayerPrefs.SetInt("MusicState", isMusicOn ? 1 : 0);

        if (isMusicOn)
        {
            musicManager.PlayMusic();
        }
        else
        {
            musicManager.StopMusic();
        }

        // Buton durumlarını güncelle
        UpdateMusicButtons();
    }

    void UpdateMusicButtons()
    {
        // PlayerPrefs'ten müzik durumunu al ve butonları etkinleştir/deaktif et
        bool isMusicOn = PlayerPrefs.GetInt("MusicState", 1) == 1;

        musicOnButton.interactable = !isMusicOn; // Eğer müzik açıksa açma butonu pasif
        musicOffButton.interactable = isMusicOn; // Eğer müzik kapalıysa kapama butonu pasif
    }
}