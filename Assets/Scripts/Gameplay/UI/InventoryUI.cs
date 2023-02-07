using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
  [SerializeField] private List<Item> inventory = new List<Item>();
  [SerializeField] private GameObject slot;
  [SerializeField] private Color emptyColor = Color.HSVToRGB(246, 38, 20);

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

    image.color = Color.white;
    image.sprite = inventory[0].icon;
  }

  private void Start()
  {
    image = slot.GetComponent<Image>();
    image.preserveAspect = true;
  }
}
