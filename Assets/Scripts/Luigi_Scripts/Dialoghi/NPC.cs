using UnityEngine;

public class NPC : Interactable
{
    public Dialogue_Manager dialogueManager;
    public Dialogue_Line[] dialogueLines;

    private bool dialogueStarted = false;

    public override void Interact()
    {
        base.Interact();

        if (!dialogueStarted)
        {
            dialogueManager.StartDialogue(dialogueLines);
            dialogueStarted = true;
        }
        else
        {
            dialogueManager.DisplayNextSentence();
        }
    }

    public new void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (other.CompareTag("Player"))
        {
            dialogueManager.EndDialogue();
            dialogueStarted = false;
        }
    }
}
