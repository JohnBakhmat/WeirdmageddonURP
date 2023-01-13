using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonPress : ButtonPress
{
  protected new void OnMouseDown()
  {
    Debug.Log("Play Button Pressed");
    SceneManager.LoadScene("Gameplay");
  }
}

