using UnityEngine;

public class OptionsButtonPress : ButtonPress
{
  protected new void OnMouseDown()
  {
    Debug.Log("Options Button Pressed");
  }
}

