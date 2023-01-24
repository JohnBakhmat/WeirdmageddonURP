using UnityEngine;

enum FacingDirection
{
  Left = -1,
  Right = 1
}

public class PlayerMovement : MonoBehaviour
{
  private PlayerState state = new IdleState();
  private float horizontalInput;
  private float height = 0f;
  private FacingDirection facingDirection = FacingDirection.Right;

  [SerializeField] private Rigidbody2D rb;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private Transform ceilingCheck;
  [SerializeField] private LayerMask groundLayer;
  [SerializeField] private new CapsuleCollider2D collider;

  private bool isGrounded()
  {
    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
  }

  private bool canUncrouch()
  {
    return !Physics2D.OverlapCircle(ceilingCheck.position, 0.2f, groundLayer);
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

  private void ChangeState(PlayerState newState)
  {
    state = newState;
  }

  private void Move()
  {
    // If the player can't move, return
    if (!state.canMove()) return;

    if (Input.GetKey(KeyCode.LeftShift) && horizontalInput != 0)
      ChangeState(new RunState());
    else if (horizontalInput != 0)
      ChangeState(new WalkState());
    else if (horizontalInput == 0)
      ChangeState(new IdleState());

    rb.velocity = new Vector2(horizontalInput * state.moveSpeed, rb.velocity.y);
  }

  private void Crouch()
  {
    if (!state.canCrouch()) return;


    if (Input.GetKey(KeyCode.LeftControl) && isGrounded())
    {
      ChangeState(new CrouchState());
      collider.size = new Vector2(collider.size.x, height / 2);
      collider.offset = new Vector2(collider.offset.x, -height / 4);
    }


    if (!canUncrouch()) return;

    //Uncrouch
    if (!Input.GetKey(KeyCode.LeftControl))
    {
      ChangeState(new IdleState());
      collider.size = new Vector2(collider.size.x, height);
      collider.offset = new Vector2(collider.offset.x, 0);
    }

  }


  #region Gameloop

  void Start()
  {
    this.height = collider.size.y;
  }


  void Update()
  {
    horizontalInput = Input.GetAxisRaw("Horizontal");

    Turn();
  }

  private void FixedUpdate()
  {
    Move();
    Crouch();

  }

  #endregion
}
