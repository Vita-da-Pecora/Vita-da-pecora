using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    public GameObject promptUI;
    public TMP_Text promptText;

    public void Show(string message)
    {
        promptText.text = message;
        promptUI.SetActive(true);
    }

    public void Hide()
    {
        promptUI.SetActive(false);
    }
}
