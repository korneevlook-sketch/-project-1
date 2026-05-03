using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private string[] lines;
    [SerializeField] private KeyCode nextKey = KeyCode.E;

    private int currentLine;
    private bool isDialogueActive;

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!isDialogueActive)
            return;

        if (Input.GetKeyDown(nextKey))
        {
            ShowNextLine();
        }
    }

    public void StartDialogue(string[] newLines)
    {
        lines = newLines;
        currentLine = 0;
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = lines[currentLine];
    }

    public void ShowNextLine()
    {
        currentLine++;

        if (currentLine >= lines.Length)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = lines[currentLine];
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
    }
}
