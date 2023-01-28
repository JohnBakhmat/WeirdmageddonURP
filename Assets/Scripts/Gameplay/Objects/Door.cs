using UnityEngine;

public class Door : Interactable
{
  [SerializeField] bool _isLocked = false;
  [SerializeField] bool _isOpen = false;
  [SerializeField] float _detectRadius = 1.5f;
  [SerializeField] GameObject _interactionBar;
  [SerializeField] GameObject _lock;
  [SerializeField] GameObject linkedDoor;


  bool _playerIsInRange = false;

  public override void Interact()
  {
    if (!_isLocked)
    {
      var outDoor = linkedDoor.GetComponent<Door>();
      if (outDoor == null) return;
      var player = GameObject.Find("Player");


      //z is the same as the current z

      var playerPos = player.transform.position;
      var outDoorPos = outDoor.transform.position;

      var doorHeight = outDoor.GetComponent<SpriteRenderer>().bounds.size.y;
      var playerHeight = player.GetComponent<SpriteRenderer>().bounds.size.y;

      var diffHeight = doorHeight - playerHeight;

      var offset = new Vector3(0, -diffHeight / 2, 0);
      var newPos = new Vector3(outDoorPos.x, outDoorPos.y, playerPos.z) + offset;


      player.transform.position = newPos;

      //move camera too

      GameObject.Find("Main Camera").GetComponent<Camera>().TeleportTo(newPos);
    }
  }



  void DetectPlayer()
  {
    _playerIsInRange = base.IsPlayerInRadius(_detectRadius);

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

    _lock.SetActive(_isLocked);
  }
}



