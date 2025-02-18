using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScenarioManager : MonoBehaviour
{
    [System.Serializable]
    public class Scenario
    {
        public string id;
        public Texts texts;
        public Choices choices;
        public int img;

        [System.Serializable]
        public class Texts
        {
            public string tr;
            public string en;
        }

        [System.Serializable]
        public class Choices
        {
            public Option option1;
            public Option option2;
            public Option option3;

            [System.Serializable]
            public class Option
            {
                public Texts text;
                public List<Effect> effects;
                public string next_scenario;
            }
        }
    }

    [System.Serializable]
    public class Effect
    {
        public string key;
        public int value;
    }

    public TMP_Text scenarioText;
    public TMP_Text choice1Text;
    public TMP_Text choice2Text;
    public TMP_Text choice3Text;
    public TMP_Text yearText;
    public Button choice1Button;
    public Button choice2Button;
    public Button choice3Button;

    public EffectManager effectManager;
    public Image specialImage;
    public Image[] scenarioImages;

    private Dictionary<string, Scenario> scenarios;
    private Scenario currentScenario;

    private string currentLanguage = "tr"; 

    public int currentYear = 1612; 
    public int currentQuarter = 1; 

    void Start()
    {
        LoadScenarios();
        UpdateYearText(); 
        ShowScenario("1"); 
    }

    private void LoadScenarios()
    {
        scenarios = new Dictionary<string, Scenario>();
        TextAsset jsonData = Resources.Load<TextAsset>("seneryolar");
        ScenarioArray loadedScenarios = JsonUtility.FromJson<ScenarioArray>(jsonData.text);

        foreach (Scenario scenario in loadedScenarios.scenarios)
        {
            scenarios[scenario.id] = scenario;
        }
    }

    public void ShowScenario(string scenarioId)
    {
        if (!scenarios.ContainsKey(scenarioId))
        {
            Debug.LogError($"Senaryo bulunamadı: {scenarioId}");
            return;
        }

        currentScenario = scenarios[scenarioId];

        
        scenarioText.text = GetLocalizedText(currentScenario.texts);

        
        UpdateScenarioImage(currentScenario.img);

        
        ManageOption(choice1Button, choice1Text, currentScenario.choices.option1, 1);
        ManageOption(choice2Button, choice2Text, currentScenario.choices.option2, 2);
        ManageOption(choice3Button, choice3Text, currentScenario.choices.option3, 3);
    }

    private void ManageOption(Button button, TMP_Text text, Scenario.Choices.Option option, int choiceIndex)
    {
        if (option != null && option.text != null)
        {
            text.text = GetLocalizedText(option.text);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnChoiceSelected(choiceIndex));
            button.gameObject.SetActive(true);
        }
        else
        {
            text.text = "";
            button.gameObject.SetActive(false);
        }
    }

    private void UpdateScenarioImage(int imgIndex)
    {
       
        foreach (Image image in scenarioImages)
        {
            image.gameObject.SetActive(false);
        }

     
        if (imgIndex >= 0 && imgIndex < scenarioImages.Length)
        {
            scenarioImages[imgIndex].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Geçersiz img index: {imgIndex}");
        }
    }

    public void SetLanguage(string langCode)
    {
        currentLanguage = langCode;
        UpdateScenarioUI();
    }

    private void UpdateScenarioUI()
    {
        if (currentScenario != null)
        {
            scenarioText.text = GetLocalizedText(currentScenario.texts);

            ManageOption(choice1Button, choice1Text, currentScenario.choices.option1, 1);
            ManageOption(choice2Button, choice2Text, currentScenario.choices.option2, 2);
            ManageOption(choice3Button, choice3Text, currentScenario.choices.option3, 3);
        }
    }

    private string GetLocalizedText(Scenario.Texts texts)
    {
        if (texts == null)
        {
            Debug.LogError("Localized text is null. Returning empty string.");
            return ""; // Eğer texts null ise hata vermez, boş string döner
        }

        return currentLanguage == "tr" ? texts.tr ?? "" : texts.en ?? "";
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        Scenario.Choices.Option selectedOption = choiceIndex switch
        {
            1 => currentScenario.choices.option1,
            2 => currentScenario.choices.option2,
            3 => currentScenario.choices.option3,
            _ => null
        };

        if (selectedOption == null)
        {
            Debug.LogError("Seçilen seçenek mevcut değil.");
            return;
        }

        if (currentScenario.id == "5")
        {
            if (choiceIndex == 1)
            {
                specialImage.gameObject.SetActive(true); 
            }
            else if (choiceIndex == 2)
            {
                specialImage.gameObject.SetActive(false); 
            }
        }


        if (currentScenario.id == "5.1" && choiceIndex == 2)
        {
            specialImage.gameObject.SetActive(false); 
        }

        if (selectedOption.effects != null)
        {
            var effectsDict = new Dictionary<string, int>();
            foreach (var effect in selectedOption.effects)
            {
                effectsDict[effect.key] = effect.value;
            }

            Debug.Log($"Efektler uygulanıyor: {string.Join(", ", effectsDict)}");

            effectManager.ApplyEffects(
                effectsDict.ContainsKey("şöhret") ? effectsDict["şöhret"] : 0,
                effectsDict.ContainsKey("para") ? effectsDict["para"] : 0,
                effectsDict.ContainsKey("sağlık") ? effectsDict["sağlık"] : 0,
                effectsDict.ContainsKey("gemi") ? effectsDict["gemi"] : 0,
                effectsDict.ContainsKey("tayfa") ? effectsDict["tayfa"] : 0
            );
        }
        else
        {
            Debug.LogWarning("Efekt bulunamadı.");
        }

        IncrementQuarter();

        if (!string.IsNullOrEmpty(selectedOption.next_scenario))
        {
            ShowScenario(selectedOption.next_scenario);
        }
        else
        {
            ShowRandomMainScenario();
        }
    }

    private void IncrementQuarter()
    {
        currentQuarter++;
        if (currentQuarter > 4)
        {
            currentQuarter = 1;
            currentYear++;
        }
        UpdateYearText();
    }

    private void UpdateYearText()
    {
        string quarterText = currentLanguage == "tr" ? $"{currentQuarter}. Çeyrek" : $"Q{currentQuarter}";
        yearText.text = currentLanguage == "tr"
            ? $"{currentYear} - {quarterText}"
            : $"{quarterText} - {currentYear}";
    }

    private void ShowRandomMainScenario()
    {
        List<string> mainScenarioIds = new List<string>();

        foreach (var scenario in scenarios.Values)
        {
            // Ana senaryoları seç (ID'leri düz sayı olanlar)
            if (!scenario.id.Contains("."))
            {
                mainScenarioIds.Add(scenario.id);
            }
        }

        if (mainScenarioIds.Count > 0)
        {
            string randomScenarioId = mainScenarioIds[Random.Range(0, mainScenarioIds.Count)];
            ShowScenario(randomScenarioId);
        }
        else
        {
            Debug.LogError("Gösterilecek ana senaryo bulunamadı.");
        }
    }

    public void ResetTime()
    {
        currentYear = 1612; // Başlangıç yılına döndür
        currentQuarter = 1; // Başlangıç çeyreğine döndür
        UpdateYearText();
    }

    [System.Serializable]
    private class ScenarioArray
    {
        public Scenario[] scenarios;
    }
}
