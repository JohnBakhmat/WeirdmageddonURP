using UnityEngine;

public class CameraItem : Item
{

  public override void Drop()
  {
    Debug.Log("CameraItem dropped");
  }

  public override void Interact(Player player)
  {
    base.Interact(player);
    Debug.Log("CameraItem picked up");
  }

  protected override void Action(Player player)
  {
    Debug.Log("CameraItem used");
  }


}