using UnityEngine;

public class Camera : MonoBehaviour
{
  public Transform player;
  public float smoothSpeed = 0.0f;
  public Vector3 offset;

  void FixedUpdate()
  {
    Vector3 desiredPosition = player.position + offset;
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    transform.position = smoothedPosition;
  }
}
