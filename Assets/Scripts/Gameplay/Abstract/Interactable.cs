using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
  public abstract void Interact(Player player);

  public float detectRadius = 0.5f;
  public GameObject interactionBar = null;

  public bool IsPlayerInRadius() =>
      Physics2D
      .OverlapCircle(
        transform.position,
        detectRadius,
        LayerMask.GetMask("Player"))
         != null;

  void Start()
  {
    gameObject.layer = LayerMask.NameToLayer("Interactable");

    if (interactionBar == null)
    {
      var existentBar = transform.Find("InteractionBar").gameObject;

      if (existentBar != null)
        interactionBar = existentBar;
      else
      {
        var newBar = Instantiate(Resources.Load("Prefabs/UI/InteractionUI.prefab")) as GameObject;
        newBar.transform.SetParent(transform);
      }
    }

  }

  protected virtual void DetectPlayer()
  {
    interactionBar.SetActive(IsPlayerInRadius());
  }

  protected virtual void Update()
  {
    DetectPlayer();
  }


  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.cyan;
    Gizmos.DrawWireSphere(transform.position, detectRadius);
  }
}