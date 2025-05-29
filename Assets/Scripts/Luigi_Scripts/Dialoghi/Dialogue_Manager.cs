using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue_Manager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private Queue<Dialogue_Line> lines;
    private bool isTyping = false;
    private Dialogue_Line currentLine;

    void Start()
    {
        lines = new Queue<Dialogue_Line>();
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(Dialogue_Line[] dialogueLines)
    {
        lines.Clear();
        foreach (Dialogue_Line line in dialogueLines)
        {
            lines.Enqueue(line);
        }

        dialoguePanel.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentLine.sentence;
            isTyping = false;
            return;
        }

        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = lines.Dequeue();
        nameText.text = currentLine.speakerName;
        StartCoroutine(TypeSentence(currentLine.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        isTyping = false;
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
