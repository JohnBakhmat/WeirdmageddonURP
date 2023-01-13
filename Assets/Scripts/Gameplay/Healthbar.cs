using UnityEngine;

public class Healthbar : MonoBehaviour
{
  [SerializeField] private Sprite fill25;
  [SerializeField] private Sprite fill50;
  [SerializeField] private Sprite fill75;
  [SerializeField] private Sprite fill100;

  [SerializeField] private SpriteRenderer spriteRenderer;


  public int maxHealth = 100;
  public int currentHealth = 100;

  public bool isAlive => currentHealth > 0;

  public void SetHealth(int health)
  {
    currentHealth = health;
    UpdateView();
  }

  public void UpdateView()
  {
    var sprite = getSprite(currentHealth);
    spriteRenderer.sprite = sprite;

  }

  public Sprite getSprite(int health)
  {

    if (health <= 25)
      return fill25;
    if (health <= 50)
      return fill50;
    if (health <= 75)
      return fill75;
    if (health <= 100)
      return fill100;

    return fill100;
  }

  void Update()
  {
    UpdateView();
  }
}
