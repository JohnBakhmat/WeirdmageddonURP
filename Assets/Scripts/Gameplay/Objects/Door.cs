using UnityEngine;

public class Door : Interactable
{
  [SerializeField] private bool _isLocked = false;
  [SerializeField] private bool _isOpen = false;
  [SerializeField] private float _detectRadius = 1.5f;
  [SerializeField] private GameObject _interactionBar = null;


  [SerializeField] private float _detectRadius = 1.5f;
  [SerializeField] private GameObject _interactionBar = null;

  private bool _playerIsInRange = false;

  public override void Interact()
  {
    Debug.Log("Interacting with door");
  }

  private void DetectPlayer()
  {
    _playerIsInRange = Physics2D.OverlapCircle(transform.position, _detectRadius, LayerMask.GetMask("Player"));

    _interactionBar.SetActive(_playerIsInRange);
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.cyan;
    Gizmos.DrawWireSphere(transform.position, _detectRadius);
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
