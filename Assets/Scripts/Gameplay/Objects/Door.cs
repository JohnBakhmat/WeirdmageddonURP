using UnityEngine;

public class Door : Interactable
{
  [SerializeField] bool _isLocked = false;
  [SerializeField] bool _isOpen = false;


  [SerializeField] GameObject _lock;
  [SerializeField] GameObject linkedDoor;


  bool _playerIsInRange = false;

  public override void Interact(Player player)
  {
    if (!_isLocked)
    {
      var outDoor = linkedDoor.GetComponent<Door>();
      if (outDoor == null) return;

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


  protected override void Update()
  {
    base.Update();
    _lock.SetActive(_isLocked);
  }
}



