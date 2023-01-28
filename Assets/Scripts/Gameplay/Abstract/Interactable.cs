using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
  public abstract void Interact();

  public bool IsPlayerInRadius(float radius) =>
      Physics2D
      .OverlapCircle(
        transform.position,
        radius,
        LayerMask.GetMask("Player"))
         != null;

  void Start()
  {
    // autoassign interactable layer
    gameObject.layer = LayerMask.NameToLayer("Interactable");
  }
}