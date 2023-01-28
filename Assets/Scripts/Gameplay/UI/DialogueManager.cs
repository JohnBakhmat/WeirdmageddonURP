using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
  private Queue<DialogueSentence> _sentences = new Queue<DialogueSentence>();

  public void StartDialogue(Dialogue dialogue)
  {
    _sentences = new Queue<DialogueSentence>(dialogue.sentences);

    DisplayNextSentence();
  }

  public void DisplayNextSentence()
  {
    if (_sentences.Count == 0)
    {
      EndDialogue();
      return;
    }

    var sentence = _sentences.Dequeue();
    Debug.Log(sentence.narrator + ": " + sentence.sentences[0]);
  }

  void EndDialogue()
  {
    throw new NotImplementedException();
  }
}
