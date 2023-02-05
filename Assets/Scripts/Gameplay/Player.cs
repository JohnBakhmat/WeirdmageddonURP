using UnityEngine;

enum FacingDirection
{
  Left = -1,
  Right = 1
}

public class Player : MonoBehaviour
{
  private PlayerState state = new IdleState();
  private float horizontalInput;
  private float verticalInput;
  private float height = 0f;
  private FacingDirection facingDirection = FacingDirection.Right;

  [SerializeField] private Rigidbody2D rb;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private Transform ceilingCheck;
  [SerializeField] private LayerMask groundLayer;
  [SerializeField] private new CapsuleCollider2D collider;
  [SerializeField] private Healthbar healthbar;
  [SerializeField] private Animator animator;


  [Header("Floats")]
  [SerializeField] private float verticalKnockback = 0.73f;
  [SerializeField] private float horizontalKnockback = 0.73f;
  [SerializeField] private float jumpForce = 5f;

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

  void Jump()
  {
    if (!state.canJump() || !isGrounded()) return;

    if (verticalInput > 0)
    {
      rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

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



    if (state is RunState)
      animator.SetBool("isRunning", true);
    else
      animator.SetBool("isRunning", false);
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

    animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    groundCheck = transform.Find("GroundCheck");
    ceilingCheck = transform.Find("CeilingCheck");
    collider = GetComponent<CapsuleCollider2D>();
  }

  void Update()
  {
    horizontalInput = Input.GetAxisRaw("Horizontal");
    verticalInput = Input.GetAxisRaw("Vertical");

    Turn();
    Interact();
    Crouch();
    TouchTheEnemy();


  }

  void FixedUpdate()
  {
    Move();
    Jump();
  }


  #endregion
}
