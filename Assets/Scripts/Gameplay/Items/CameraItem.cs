using UnityEngine;

public class CameraItem : Item
{

  public override void Use()
  {
    Debug.Log("CameraItem used");
  }

  public override void Drop()
  {
    Debug.Log("CameraItem dropped");
  }

  public override void Interact(Player player)
  {
    base.Interact(player);
    Debug.Log("CameraItem picked up");
  }
}