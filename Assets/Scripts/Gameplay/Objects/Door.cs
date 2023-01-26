using UnityEngine;

public class Door : Interactable
{
  [SerializeField] bool _isLocked = false;
  [SerializeField] bool _isOpen = false;
  [SerializeField] float _detectRadius = 1.5f;
  [SerializeField] GameObject _interactionBar;
  [SerializeField] GameObject _lock;



  bool _playerIsInRange = false;

  public override void Interact()
  {
    _isLocked = !_isLocked;
  }

  void DetectPlayer()
  {
    _playerIsInRange = Physics2D.OverlapCircle(transform.position, _detectRadius, LayerMask.GetMask("Player"));

    _interactionBar.SetActive(_playerIsInRange);
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.cyan;
    Gizmos.DrawWireSphere(transform.position, _detectRadius);
  }

  void Update()
  {
    DetectPlayer();

    if (_playerIsInRange)
    {
      Debug.Log("Player is in range");
    }

    _lock.SetActive(_isLocked);
  }


  void Start()
  {
    _lock.SetActive(_isLocked);
  }
}



