using UnityEngine;

enum FacingDirection
{
  Left = -1,
  Right = 1
}

public class PlayerMovement : MonoBehaviour
{

  private float horizontalInput;
  private float walkSpeed = 8f;
  private FacingDirection facingDirection = FacingDirection.Right;


  [SerializeField] private Rigidbody2D rb;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private LayerMask groundLayer;

  /*   [SerializeField] private Transform ceilingCheck;
    [SerializeField] private LayerMask ceilingLayer; */

  private bool isGrounded()
  {
    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
  }

  private void Turn()
  {
    switch (horizontalInput)
    {
      case -1:
        facingDirection = FacingDirection.Left;
        break;
      case 1:
        facingDirection = FacingDirection.Right;
        break;
    }

    var facingMultiplier = (float)facingDirection;
    transform.localScale = new Vector3(facingMultiplier * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
  }


  // Loop
  void Update()
  {
    horizontalInput = Input.GetAxisRaw("Horizontal");

    Turn();
  }

  private void FixedUpdate()
  {
    rb.velocity = new Vector2(horizontalInput * walkSpeed, rb.velocity.y);
  }
}
