using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GameLanguage : MonoBehaviour
{

    public static GameLanguage gl;
    public string currentLanguage = "tr";

    Dictionary<string, string> langEN;

    void Start()
    {
        gl = this;

        if (PlayerPrefs.HasKey("GameLanguage"))
        {
            currentLanguage = PlayerPrefs.GetString("GameLanguage");
        }
        else
        {
            ResetLanguage();
        }

        WordDefine();
    }

    public void Setlanguage(string langCode)
    {
        PlayerPrefs.SetString("GameLanguage", langCode);
        currentLanguage = langCode;
    }

    public void ResetLanguage()
    {
        Setlanguage("tr");
    }

    public string Say(string text)
    {
        switch (currentLanguage)
        {
            case "en":
                return FindInDict(langEN, text);
            default:
                return text;
        }
    }

    public string FindInDict(Dictionary<string, string> selectedLang, string text)
    {
        string normalizedText = text.Normalize(NormalizationForm.FormD);
        foreach (var key in selectedLang.Keys)
        {
            if (key.Normalize(NormalizationForm.FormD) == normalizedText)
            {
                return selectedLang[key];
            }
        }
        return "Untranslated";
    }

    public void WordDefine()
    {
        langEN = new Dictionary<string, string>()
        {
            {"OYNA", "PLAY"},
            {"Geri", "Back"},
            {"Müzik", "Music"},
            {"Dil", "Language"},
            {"Menüye Dön", "Back To Menu"},
            {"Ayarlar", "Settings"},
            {"Şöhret", "Reputation"},
            {"Gemi", "Ship"},
            {"Hazine", "Treasure"},
            {"Sağlık", "Health"},
            {"Tayfa", "Crew"},
            {"Şöhret: Korkusuz eylemlerde bulunduğunuzda, iyi bir mücadele verdiğinizde ve efsanevi olaylara bakışınızla artar.", "Reputation: Increases when you perform fearless actions, fight well, and approach legendary events with courage."},
            {"Gemi: Bakımlarını yaptırmanız, savaşlarda geminizi batırmayacak hamleler yapmanız gerekmektedir. Ya da tayfanızın üstünde çok tepinmemesi.", "Ship: Requires regular maintenance and strategic decisions in battles to prevent sinking. Or perhaps not pushing your crew too hard."},
            {"Hazine: Büyük savaşların ganimeti büyük olur. Ne kadar az kişiye bölünürse kazancın da o kadar artar.", "Treasure: The spoils of great battles are plentiful. The fewer people to share it with, the greater the gain."},
            {"Sağlık: Her savaşa girmek veya düelloya katılmak iyi değildir, sağlıklı bir korsan doğru kararları doğru zamanda vermeli.", "Health: It’s not wise to join every battle or duel. A healthy pirate makes the right decisions at the right time."},
            {"Tayfa: İstekleri yerine getirildiği ve maaşlarını zamanında aldıkları sürece çok mutlular. Güvenip güvenmemek senin ellerinde.", "Crew: They are happy as long as their needs are met and their wages are paid on time. Trusting them is up to you."},
            {"Devam etmek için dokun..", "Touch to continue.."},
            {"Unutulmuş bir kaptanla kim yolculuk yapmak ister? Belki de korsanlık bana göre değil. Emeklilik vakti geldi.", "Who would want to journey with a forgotten captain? Maybe piracy isn’t for me anymore. It’s time to retire."},
            {"Artık bayrağım direkte durmuyor, kamaramın içinde yosunlar geziniyor. Batan gemiden kurtulduğum için şanslı olmalıyım. Acaba kedi nerelerde?", "My flag no longer flies on the mast, and seaweed roams in my cabin. I should be lucky to have escaped the sinking ship. I wonder where the cat is."},
            {"Tayfamın maaşını ödeyemedim ve beni bir başıma bıraktılar. Gemiyi satıp bir tatile çıkmalıyım. Korsan gemi piyasası nasıldır bilen biri var mı?", "I couldn’t pay my crew, and they abandoned me. Maybe I should sell the ship and take a vacation. Does anyone know how the pirate ship market is doing?"},
            {"Beyaz ışığa doğru yürüdüm. Yolun sonunda bir handa şarkılar eşliğinde oturan eski efsaneleri görüyorum. Sanırım nerede olduğumu anlamaya başladım.", "I walked towards the white light. At the end of the path, I see old legends sitting in a tavern, singing songs. I think I’ve figured out where I am."},
            {"Bu sefiller beni gemiden aşağı attılar! En azından gaddar bir kaptan olarak tanınıyorum. Ya da bir cimri olarak…", "These scoundrels threw me off the ship! At least I’m known as a ruthless captain… or maybe just a miser."},
        };
    }
}