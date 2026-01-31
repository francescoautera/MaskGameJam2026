using UnityEngine;

namespace GameJam
{
    
public class Character : MonoBehaviour
{
  public DialogueData currentDialogueData;

  public void StartDialogue()
  {
    FindFirstObjectByType<DialogueTest>().StartDialogue(currentDialogueData);
  }
}
}
