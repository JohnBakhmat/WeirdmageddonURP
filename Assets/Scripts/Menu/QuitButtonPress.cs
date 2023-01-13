using UnityEngine;

public class QuitButtonPress : ButtonPress
{
  protected new void OnMouseDown()
  {
    Debug.Log("Quit Button Pressed");
    Application.Quit();
  }
}

