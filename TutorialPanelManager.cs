using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialPanelManager : MonoBehaviour
{
    public GameObject tutorialPanel; 
    public Image[] images; 
    public TMP_Text[] texts; 
    public Button triggerButton; 

    private int currentIndex = 0; 
    private const string TUTORIAL_SEEN_KEY = "TutorialSeen"; 

    void Start()
    {
        tutorialPanel.SetActive(false);

        if (PlayerPrefs.GetInt(TUTORIAL_SEEN_KEY, 0) == 1)
        {
            triggerButton.onClick.AddListener(() => Debug.Log("Panel zaten gösterildi."));
            return;
        }

        triggerButton.onClick.AddListener(ShowTutorialPanel);
    }

    void ShowTutorialPanel()
    {
        tutorialPanel.SetActive(true);

        UpdateContent();

        tutorialPanel.AddComponent<Button>().onClick.AddListener(ChangeContent);

        PlayerPrefs.SetInt(TUTORIAL_SEEN_KEY, 1);
        PlayerPrefs.Save();
    }

    void ChangeContent()
    {
        currentIndex += 2; // 2 resim ve 1 yazıyı temsil ediyor

        if (currentIndex >= images.Length || currentIndex / 2 >= texts.Length)
        {
            tutorialPanel.SetActive(false);
            return;
        }

        UpdateContent();
    }

    void UpdateContent()
    {
        foreach (var img in images) img.gameObject.SetActive(false);
        foreach (var txt in texts) txt.gameObject.SetActive(false);

        if (currentIndex < images.Length) images[currentIndex].gameObject.SetActive(true);
        if (currentIndex + 1 < images.Length) images[currentIndex + 1].gameObject.SetActive(true);
        if (currentIndex / 2 < texts.Length) texts[currentIndex / 2].gameObject.SetActive(true);
    }
}
