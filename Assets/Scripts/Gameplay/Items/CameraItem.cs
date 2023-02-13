using UnityEngine;

public class CameraItem : Item
{

  public override void Drop()
  {
    Debug.Log("CameraItem dropped");
  }


  [SerializeField][Range(0, 180)] private float rayAngle = 20f;
  [SerializeField][Range(0, 10)] private float rayLength = 5f;


  protected override void Action(Player player)
  {
    // When used, emit a conecast from the player's camera


    var start = player.transform.position;
    var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    var direction = target - start;

    direction = direction.normalized * rayLength;

    var topVector = Quaternion.AngleAxis(rayAngle, Vector3.forward) * direction;
    var bottomVector = Quaternion.AngleAxis(-rayAngle, Vector3.forward) * direction;

    // var topRay = Physics2D.Raycast(start, topVector, rayLength, LayerMask.GetMask("Walls"));
    // var bottomRay = Physics2D.Raycast(start, bottomVector, rayLength, LayerMask.GetMask("Walls"));
    // var centerRay = Physics2D.Raycast(start, direction, rayLength, LayerMask.GetMask("Walls"));

    // var rays = new List<RaycastHit2D> { topRay, bottomRay, centerRay };

    // foreach (var ray in rays)
    // {
    //   if (ray.collider != null)
    //   {
    //     var hitpoint = ray.point;

    //     var mark = new GameObject("Mark");
    //     mark.transform.position = hitpoint;
    //     mark.AddComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;

    //   }
    // }


    // Debug.DrawRay(start, topVector, Color.red, 0.5f);
    // Debug.DrawRay(start, direction, Color.blue, 0.5f);
    // Debug.DrawRay(start, bottomVector, Color.red, 0.5f);

  }


}