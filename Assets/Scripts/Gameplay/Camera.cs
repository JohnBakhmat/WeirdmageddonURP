using UnityEngine;

public class Camera : MonoBehaviour
{
  [SerializeField] Transform player;
  [SerializeField] float smoothSpeed;
  [SerializeField] Vector3 offset;

  public void MoveTo(Vector3 position)
  {

    var desiredPosition = position + offset;
    var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    transform.position = smoothedPosition;
  }

  public void TeleportTo(Vector3 position)
  {
    transform.position = position + offset;
  }

  void Update()
  {
    MoveTo(player.position);
  }


}
