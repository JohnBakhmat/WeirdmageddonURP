public abstract class Item : Interactable
{
  public abstract void Use();
  public abstract void Drop();

  public override void Interact(Player player)
  {
    player.PickUpItem(this);
    Destroy(gameObject);
  }
}


