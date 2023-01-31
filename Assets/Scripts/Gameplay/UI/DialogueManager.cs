using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
  Queue<DialogueSentence> _sentences = new Queue<DialogueSentence>();
  bool _isDialogueActive = false;


  //The UI stuff
  [SerializeField] GameObject _dialogueUI;
  [SerializeField] TextMeshProUGUI _narratorText;
  [SerializeField] TextMeshProUGUI _speachText;



  public void StartDialogue(Dialogue dialogue)
  {
    if (_isDialogueActive) return;

    _sentences = new Queue<DialogueSentence>(dialogue.sentences);
    _isDialogueActive = true;
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

    _narratorText.text = sentence.narrator;

    StopAllCoroutines();
    StartCoroutine(TypeSentence(sentence.sentence));

  }

  IEnumerator TypeSentence(string sentence)
  {
    var animationDuration = 2f;
    var length = sentence.Length;
    var animationSleepTime = 1f / (length * animationDuration);





    var ogArray = sentence.ToCharArray();
    var iteration = 0;

    while (iteration < length)
    {
      var text = new char[length];

      var randomArr = RandomLetterString(length).ToCharArray();

      for (int i = 0; i < length; i++)
      {
        if (ogArray[i] == ' ')
        {
          text[i] = ' ';
          continue;
        };

        if (i <= iteration)
        {
          text[i] = ogArray[i];
        }
        else
        {
          text[i] = randomArr[i];
        }

      }

      _speachText.text = new string(text);
      yield return new WaitForSeconds(animationSleepTime);

      iteration++;
    }

  }

  string RandomLetterString(int length)
  {
    var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var randomString = "";

    for (int i = 0; i < length; i++)
    {
      randomString += letters[Random.Range(0, letters.Length)];
    }

    return randomString;
  }


  void EndDialogue()
  {
    _isDialogueActive = false;

  }

  void Update()
  {
    _dialogueUI.SetActive(_isDialogueActive);

    if (Input.GetKeyDown(KeyCode.Space)) DisplayNextSentence();
  }
}
