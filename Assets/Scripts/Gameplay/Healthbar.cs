using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
  [SerializeField] private Sprite fill25;
  [SerializeField] private Sprite fill50;
  [SerializeField] private Sprite fill75;
  [SerializeField] private Sprite fill100;

  [SerializeField]
  private GameObject fill;


  public int maxHealth = 100;
  public int currentHealth = 100;

  public bool isAlive => currentHealth > 0;

  public void SetHealth(int health) => currentHealth = health;

  public void TakeDamage(int damage)
  {
    if (!isAlive) return;

    SetHealth(currentHealth - damage);
  }


  public Sprite getSprite(int health) => health switch
  {
    <= 25 => fill25,
    <= 50 => fill50,
    <= 75 => fill75,
    <= 100 => fill100,
    _ => fill100
  };

  void Update()
  {
    fill.GetComponent<Image>().sprite = getSprite(currentHealth);
    fill.GetComponent<Transform>().localScale = new Vector3(currentHealth / 100f, 1, 1);
  }
}
