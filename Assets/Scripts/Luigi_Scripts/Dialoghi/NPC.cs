using UnityEngine;

public class NPC : Interactable
{
    public Dialogue_Manager dialogueManager;
    public string characterName = "NPC";
    [TextArea(3, 10)]
    public string[] dialogueLines;

    public override void Interact()
    {
        base.Interact();
        dialogueManager.StartDialogue(characterName, dialogueLines);
    }
}
