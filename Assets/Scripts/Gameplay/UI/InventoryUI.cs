using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
  [SerializeField] private List<Item> inventory = new List<Item>();
  [SerializeField] private GameObject slot;
  [SerializeField] private GameObject border;
  [SerializeField] private Color emptyColor = Color.HSVToRGB(246, 38, 20);
  [SerializeField] private Color fullColor = Color.white;

  [Header("Sprites")]
  [SerializeField] private Sprite emptyBorder;
  [SerializeField] private Sprite fullBorder;

  private Image image;
  private Image borderImage;

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
      borderImage.sprite = emptyBorder;
    }
    else
    {
      image.color = fullColor;
      borderImage.sprite = fullBorder;
    }

    image.sprite = item.icon;
  }

  private void Start()
  {
    image = slot.GetComponent<Image>();
    borderImage = border.GetComponent<Image>();
    image.preserveAspect = true;
  }
}
