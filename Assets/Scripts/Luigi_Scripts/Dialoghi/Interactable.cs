using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactionMessage = "Premi F per interagire";
    public KeyCode interactionKey = KeyCode.F;

    private InteractionPromptUI promptUI;
    private bool playerInRange = false;

    void Start()
    {
        promptUI = FindFirstObjectByType<InteractionPromptUI>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            Interact();
        }
    }

    public virtual void Interact()
    {
        // Logica di interazione specifica per l'oggetto
        Debug.Log("Interazione con " + gameObject.name);
        promptUI.Hide();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            promptUI.Show(interactionMessage);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            promptUI.Hide();
        }
    }

}
