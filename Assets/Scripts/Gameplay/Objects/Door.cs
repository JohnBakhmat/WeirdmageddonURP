using UnityEngine;

public class Door : Interactable
{
  [SerializeField] private bool _isLocked = false;

  [SerializeField] private bool _isOpen = false;

  private bool _playerIsInRange = false;

  public override void Interact()
  {
    Debug.Log("Interacting with door");
  }

  private void DetectPlayer()
  {
    _playerIsInRange = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Player"));
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.cyan;
    Gizmos.DrawWireSphere(transform.position, 1.5f);
  }

  private void Update()
  {
    DetectPlayer();

    if (_playerIsInRange)
    {
      Debug.Log("Player is in range");
    }
  }
}
