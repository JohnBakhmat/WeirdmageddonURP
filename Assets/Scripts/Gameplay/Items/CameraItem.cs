using UnityEngine;

public class CameraItem : Item
{

  public override void Drop()
  {
    Debug.Log("CameraItem dropped");
  }

  protected override void Action(Player player)
  {
    Debug.Log("CameraItem used");
  }


}