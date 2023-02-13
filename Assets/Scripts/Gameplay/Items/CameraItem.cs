using UnityEngine;

public class CameraItem : Item
{

  public override void Drop()
  {
    Debug.Log("CameraItem dropped");
  }


  [SerializeField][Range(0, 180)] private float rayAngle = 20f;


  protected override void Action(Player player)
  {
    // When used, emit a conecast from the player's camera


    var start = player.transform.position;
    var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    var direction = target - start;


    var x = direction.x;
    var y = direction.y;


    var topVector = Quaternion.AngleAxis(rayAngle, Vector3.forward) * direction;
    var bottomVector = Quaternion.AngleAxis(-rayAngle, Vector3.forward) * direction;

    Debug.DrawRay(start, topVector, Color.red, 1f);
    Debug.DrawRay(start, direction, Color.green, 1f);
    Debug.DrawRay(start, bottomVector, Color.blue, 1f);

  }


}