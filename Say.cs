using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Say : MonoBehaviour
{
    public string defaultText = "Default Text";

    private Text currentText;        
    private TextMeshProUGUI tmpText; 

    void Start()
    {
        currentText = GetComponent<Text>();
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        string translatedText = GameLanguage.gl.Say(defaultText);

        if (currentText != null)
        {
            currentText.text = translatedText;
        }
        else if (tmpText != null)
        {
            tmpText.text = translatedText;
        }
    }
}
