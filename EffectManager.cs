using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EffectManager : MonoBehaviour
{
    public TMP_Text reputationText;
    public TMP_Text reputationValueText;
    public Slider reputationSlider;

    public TMP_Text moneyText;
    public TMP_Text moneyValueText;
    public Slider moneySlider;

    public TMP_Text healthText;
    public TMP_Text healthValueText;
    public Slider healthSlider;

    public TMP_Text shipText;
    public TMP_Text shipValueText;
    public Slider shipSlider;

    public TMP_Text crewText;
    public TMP_Text crewValueText;
    public Slider crewSlider;

    public GameObject gameOverPanel;

    // Game Over Metinleri
    public TMP_Text reputationGameOverText;
    public TMP_Text moneyGameOverText;
    public TMP_Text healthGameOverText;
    public TMP_Text shipGameOverText;
    public TMP_Text crewGameOverText;

    // Game Over Görselleri
    public Image reputationGameOverImage;
    public Image moneyGameOverImage;
    public Image healthGameOverImage;
    public Image shipGameOverImage;
    public Image crewGameOverImage;

    public TMP_Text gameOverYearText; // Geçen yılları gösterecek metin
    public ScenarioManager scenarioManager; // ScenarioManager referansı

    public Button closeButton;

    private int reputation = 50;
    private int money = 50;
    private int health = 50;
    private int ship = 50;
    private int crew = 50;

    private readonly Color positiveColor = new Color(0.6f, 0.8f, 0.6f); // Pastel yeşil
    private readonly Color negativeColor = new Color(0.9f, 0.5f, 0.5f); // Pastel kırmızı
    private readonly Color defaultColor = Color.white;

    void Start()
    {
        UpdateEffectsUI();
        gameOverPanel.SetActive(false);
    }

    public void ApplyEffects(int reputationChange, int moneyChange, int healthChange, int shipChange, int crewChange)
    {
        Debug.Log($"Gelen Değerler -> Şöhret: {reputationChange}, Para: {moneyChange}, Sağlık: {healthChange}, Gemi: {shipChange}, Tayfa: {crewChange}");

        UpdateSlider(reputationSlider, reputationValueText, reputation, reputationChange);
        reputation = Mathf.Clamp(reputation + reputationChange, 0, 100);

        UpdateSlider(moneySlider, moneyValueText, money, moneyChange);
        money = Mathf.Clamp(money + moneyChange, 0, 100);

        UpdateSlider(healthSlider, healthValueText, health, healthChange);
        health = Mathf.Clamp(health + healthChange, 0, 100);

        UpdateSlider(shipSlider, shipValueText, ship, shipChange);
        ship = Mathf.Clamp(ship + shipChange, 0, 100);

        UpdateSlider(crewSlider, crewValueText, crew, crewChange);
        crew = Mathf.Clamp(crew + crewChange, 0, 100);

        CheckGameOver();
    }

    private void UpdateSlider(Slider slider, TMP_Text valueText, int currentValue, int change)
    {
        int newValue = Mathf.Clamp(currentValue + change, 0, 100);
        slider.value = newValue;

        valueText.text = newValue.ToString();

        if (change > 0)
        {
            StartCoroutine(FlashColor(slider, valueText, positiveColor));
        }
        else if (change < 0)
        {
            StartCoroutine(FlashColor(slider, valueText, negativeColor));
        }
    }

    private IEnumerator FlashColor(Slider slider, TMP_Text valueText, Color flashColor)
    {
        Image fillImage = slider.fillRect.GetComponent<Image>();
        Color originalFillColor = fillImage.color;
        Color originalTextColor = valueText.color;

        fillImage.color = flashColor;
        valueText.color = flashColor;

        yield return new WaitForSeconds(1f);

        fillImage.color = originalFillColor;
        valueText.color = defaultColor;
    }

    private void UpdateEffectsUI()
    {
        reputationSlider.value = reputation;
        reputationValueText.text = reputation.ToString();

        moneySlider.value = money;
        moneyValueText.text = money.ToString();

        healthSlider.value = health;
        healthValueText.text = health.ToString();

        shipSlider.value = ship;
        shipValueText.text = ship.ToString();

        crewSlider.value = crew;
        crewValueText.text = crew.ToString();
    }

    private void CheckGameOver()
    {
        if (reputation <= 0)
        {
            ShowGameOverPanel(reputationGameOverText, reputationGameOverImage);
        }
        else if (money <= 0)
        {
            ShowGameOverPanel(moneyGameOverText, moneyGameOverImage);
        }
        else if (health <= 0)
        {
            ShowGameOverPanel(healthGameOverText, healthGameOverImage);
        }
        else if (ship <= 0)
        {
            ShowGameOverPanel(shipGameOverText, shipGameOverImage);
        }
        else if (crew <= 0)
        {
            ShowGameOverPanel(crewGameOverText, crewGameOverImage);
        }
    }

    private void ShowGameOverPanel(TMP_Text reasonText, Image reasonImage)
    {
        gameOverPanel.SetActive(true);
        DisableAllGameOverTexts();
        DisableAllGameOverImages();

        reasonText.gameObject.SetActive(true);
        reasonImage.gameObject.SetActive(true);

        // Yıl ve çeyrek bilgisi ekle
        int yearsPassed = scenarioManager.currentYear - 1612; // Geçen yılları hesapla
        gameOverYearText.text = $"{yearsPassed} year(s), {scenarioManager.currentQuarter}. Quarter.";

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() => ResetGame());
    }

    private void DisableAllGameOverTexts()
    {
        reputationGameOverText.gameObject.SetActive(false);
        moneyGameOverText.gameObject.SetActive(false);
        healthGameOverText.gameObject.SetActive(false);
        shipGameOverText.gameObject.SetActive(false);
        crewGameOverText.gameObject.SetActive(false);
    }

    private void DisableAllGameOverImages()
    {
        reputationGameOverImage.gameObject.SetActive(false);
        moneyGameOverImage.gameObject.SetActive(false);
        healthGameOverImage.gameObject.SetActive(false);
        shipGameOverImage.gameObject.SetActive(false);
        crewGameOverImage.gameObject.SetActive(false);
    }

    private void ResetGame()
    {
        gameOverPanel.SetActive(false);

        // Değerleri sıfırla
        reputation = 50;
        money = 50;
        health = 50;
        ship = 50;
        crew = 50;

        scenarioManager.ResetTime(); // Tarihi sıfırla

        UpdateEffectsUI();
    }
}