using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
  public Dialogue dialogue;

  public void Trigger()
  {
    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
  }
}