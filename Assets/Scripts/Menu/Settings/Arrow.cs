using UnityEngine;

public class Arrow : MonoBehaviour
{
  public enum Direction
  {
    Left,
    Right
  }

  public Direction direction;
  public GameObject target;

  private void OnMouseDown()
  {
    target.SendMessage("OnArrowClick", direction);
  }
}