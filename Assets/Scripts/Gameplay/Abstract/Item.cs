using UnityEngine;

public abstract class Item : Interactable
{
  public Sprite icon = null;
  public float cooldownTime = 0f;
  public float cooldownTimer = 0f;

  public bool IsOnCooldown = false;


  public void Use(Player player)
  {
    if (IsOnCooldown) return;

    Action(player);
    IsOnCooldown = true;
  }

  protected abstract void Action(Player player);

  public abstract void Drop();

  public virtual void CoolDown()
  {
    if (IsOnCooldown)
    {
      cooldownTimer += Time.deltaTime;
    }

    if (cooldownTimer >= cooldownTime)
    {
      IsOnCooldown = false;
      cooldownTimer = 0f;
    }
  }

  public override void Interact(Player player)
  {
    player.PickUpItem(this);

    //Hide object
    gameObject.GetComponent<SpriteRenderer>().enabled = false;
    gameObject.GetComponent<Collider2D>().enabled = false;
    gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    gameObject.transform.position = player.transform.position;
    interactionBar.GetComponent<SpriteRenderer>().enabled = false;
    Debug.Log(interactionBar);


    //Transform to child of player
    var inventoryObj = player.transform.Find("Inventory");
    if (inventoryObj == null)
    {
      inventoryObj = new GameObject("Inventory").transform;
      inventoryObj.SetParent(player.transform);
    }
    gameObject.transform.SetParent(inventoryObj);
  }

  protected override void Update()
  {
    base.Update();
    CoolDown();
  }
}




