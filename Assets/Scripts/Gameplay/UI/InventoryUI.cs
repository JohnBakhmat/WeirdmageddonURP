using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
  [SerializeField] private List<Item> inventory = new List<Item>();
  [SerializeField] private GameObject slot;
  [SerializeField] private Color emptyColor = Color.HSVToRGB(246, 38, 20);
  [SerializeField] private Color fullColor = Color.white;

  private Image image;

  public void SetInventory(List<Item> inventory)
  {
    this.inventory = inventory;
  }

  private void Update()
  {
    if (inventory.Count <= 0)
    {
      image.color = emptyColor;
      return;
    }
    var item = inventory[0];

    if (item.IsOnCooldown)
    {
      var cooldown = item.cooldownTimer / item.cooldownTime;
      image.color = Color.Lerp(emptyColor, fullColor, cooldown);
    }
    else
    {
      image.color = fullColor;
    }

    image.sprite = item.icon;
  }

  private void Start()
  {
    image = slot.GetComponent<Image>();
    image.preserveAspect = true;
  }
}
