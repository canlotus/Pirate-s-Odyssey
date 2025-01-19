using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Say : MonoBehaviour
{
    public string defaultText = "Default Text";

    private Text currentText;           // Normal Unity Text bile?eni
    private TextMeshProUGUI tmpText;     // TextMeshPro bile?eni

    void Start()
    {
        // Hem Text hem de TextMeshPro bile?enlerini kontrol et
        currentText = GetComponent<Text>();
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // ?evirilmi? metni al?n
        string translatedText = GameLanguage.gl.Say(defaultText);

        // E?er normal Text bile?eni varsa, ona ?eviriyi uygula
        if (currentText != null)
        {
            currentText.text = translatedText;
        }
        // E?er TextMeshPro bile?eni varsa, ona ?eviriyi uygula
        else if (tmpText != null)
        {
            tmpText.text = translatedText;
        }
    }
}
