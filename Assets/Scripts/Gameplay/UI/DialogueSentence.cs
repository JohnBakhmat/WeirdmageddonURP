using UnityEngine;

[System.Serializable]
public class DialogueSentence
{
  public string narrator;

  [TextArea(3, 10)]
  public string sentence;
}
