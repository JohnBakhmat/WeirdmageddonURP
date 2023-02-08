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

  public override bool Use(Player player)
  {
    if (!base.Use(player)) return false;

    // Do something with the camera
    Debug.Log("CameraItem used");

    return true;
  }



}