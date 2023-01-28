using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
  [SerializeField] public List<DialogueSentence> sentences;

  public Queue<DialogueSentence> GetDialogueQueue()
  {
    var queue = new Queue<DialogueSentence>();
    foreach (var sentence in sentences)
    {
      queue.Enqueue(sentence);
    }

    return queue;
  }
}
