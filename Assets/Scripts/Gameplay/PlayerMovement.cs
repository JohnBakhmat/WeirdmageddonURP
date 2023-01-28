using UnityEngine;

enum FacingDirection
{
  Left = -1,
  Right = 1
}

public class PlayerMovement : MonoBehaviour
{
  PlayerState state = new IdleState();
  float horizontalInput;
  float height = 0f;
  FacingDirection facingDirection = FacingDirection.Right;

  [SerializeField] Rigidbody2D rb;
  [SerializeField] Transform groundCheck;
  [SerializeField] Transform ceilingCheck;
  [SerializeField] LayerMask groundLayer;
  [SerializeField] new CapsuleCollider2D collider;
  [SerializeField] Healthbar healthbar;
  [SerializeField] float verticalKnockback = 0.73f;
  [SerializeField] float horizontalKnockback = 0.73f;

  void TakeDamage(int damage) => healthbar.TakeDamage(damage);


  bool isGrounded()
  {
    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
  }

  bool canUncrouch()
  {
    return !Physics2D.OverlapCircle(ceilingCheck.position, 0.2f, groundLayer);
  }

  void Turn()
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

  void ChangeState(PlayerState newState)
  {
    state = newState;
  }

  void Move()
  {
    // If the player can't move, return
    if (!state.canMove()) return;

    if (Input.GetKey(KeyCode.LeftShift) && horizontalInput != 0)
      ChangeState(new RunState());
    else if (horizontalInput != 0)
      ChangeState(new WalkState());
    else if (horizontalInput == 0)
      ChangeState(new IdleState());

    var velocity = new Vector2(horizontalInput * state.moveSpeed, rb.velocity.y);

    rb.AddForce(velocity - rb.velocity, ForceMode2D.Impulse);
  }

  void Crouch()
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

  void Interact()
  {
    var objects = Physics2D.OverlapCircleAll(transform.position, 1.5f, LayerMask.GetMask("Interactable"));
    var interactable = objects.Length > 0 ? objects[0].GetComponent<Interactable>() : null;

    if (interactable != null && Input.GetKeyDown(KeyCode.E))
    {
      interactable.Interact();
    }
  }

  void TouchTheEnemy()
  {

    var mask = LayerMask.GetMask("Enemy");

    if (!collider.IsTouchingLayers(mask)) return;

    var enemy = Physics2D.OverlapCircle(transform.position, 1.5f, mask)
    .GetComponent<Transform>();

    var directionVec = (enemy.position - transform.position).normalized;
    var direction = directionVec.x > 0 ? FacingDirection.Right : FacingDirection.Left;

    rb.AddForce(new Vector2(horizontalKnockback * (float)direction, verticalKnockback) - rb.velocity, ForceMode2D.Impulse);
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
    Interact();
    Crouch();
    TouchTheEnemy();


  }

  void FixedUpdate()
  {
    Move();
  }

  #endregion
}
