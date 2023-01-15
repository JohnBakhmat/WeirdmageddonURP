using UnityEngine;

public class SettingsButtonPress : ButtonPress
{
  protected new void OnMouseDown()
  {
    Debug.Log("Options Button Pressed");
  }
}

