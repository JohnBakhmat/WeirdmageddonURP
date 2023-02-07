using UnityEngine;

public abstract class Item : Interactable
{
  public abstract void Use();
  public abstract void Drop();

  public Sprite icon = null;

  public override void Interact(Player player)
  {
    player.PickUpItem(this);
    gameObject.SetActive(false);
  }
}


