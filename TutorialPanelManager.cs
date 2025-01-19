using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialPanelManager : MonoBehaviour
{
    public GameObject tutorialPanel; // Paneli atayın
    public Image[] images; // Panelde gösterilecek resimler
    public TMP_Text[] texts; // Panelde gösterilecek yazılar
    public Button triggerButton; // Paneli açmak için kullanılan buton

    private int currentIndex = 0; // Hangi grup içeriklerin gösterileceğini takip eder
    private const string TUTORIAL_SEEN_KEY = "TutorialSeen"; // PlayerPrefs anahtarı

    void Start()
    {
        // Panel başlangıçta kapalı
        tutorialPanel.SetActive(false);

        // Daha önce gösterildiyse butona işlev eklenmesin
        if (PlayerPrefs.GetInt(TUTORIAL_SEEN_KEY, 0) == 1)
        {
            triggerButton.onClick.AddListener(() => Debug.Log("Panel zaten gösterildi."));
            return;
        }

        // Butona tıklama işlevi ekle
        triggerButton.onClick.AddListener(ShowTutorialPanel);
    }

    void ShowTutorialPanel()
    {
        // Paneli göster
        tutorialPanel.SetActive(true);

        // İlk içerik grubunu göster
        UpdateContent();

        // Panel üzerindeki herhangi bir yere tıklama işlevi ekle
        tutorialPanel.AddComponent<Button>().onClick.AddListener(ChangeContent);

        // İlk defa gösterildiğini kaydet
        PlayerPrefs.SetInt(TUTORIAL_SEEN_KEY, 1);
        PlayerPrefs.Save();
    }

    void ChangeContent()
    {
        currentIndex += 2; // 2 resim ve 1 yazıyı temsil ediyor

        // Tüm içerik gösterildiyse paneli kapat
        if (currentIndex >= images.Length || currentIndex / 2 >= texts.Length)
        {
            tutorialPanel.SetActive(false);
            return;
        }

        // İçeriği güncelle
        UpdateContent();
    }

    void UpdateContent()
    {
        // Tüm resimleri ve yazıları kapat
        foreach (var img in images) img.gameObject.SetActive(false);
        foreach (var txt in texts) txt.gameObject.SetActive(false);

        // Sıradaki içerik grubunu göster
        if (currentIndex < images.Length) images[currentIndex].gameObject.SetActive(true);
        if (currentIndex + 1 < images.Length) images[currentIndex + 1].gameObject.SetActive(true);
        if (currentIndex / 2 < texts.Length) texts[currentIndex / 2].gameObject.SetActive(true);
    }
}