using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

enum FacingDirection
{
  Left = -1,
  Right = 1
}

public class Player : Character
{
  private PlayerState state = new IdleState();
  private float horizontalInput;
  private float verticalInput;
  private float height = 0f;
  private FacingDirection facingDirection = FacingDirection.Right;
  private List<Item> inventory = new List<Item>();
  private bool isDodging = false;

  [SerializeField] private Rigidbody2D rb;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private Transform ceilingCheck;
  [SerializeField] private LayerMask groundLayer;
  [SerializeField] private new CapsuleCollider2D collider;
  [SerializeField] private Healthbar healthbar;
  [SerializeField] private Animator animator;
  [SerializeField] private InventoryUI inventoryUI;

  [Header("Floats")]
  [SerializeField] private float verticalKnockback = 0.73f;
  [SerializeField] private float horizontalKnockback = 0.73f;
  [SerializeField] private float jumpForce = 5f;
  [SerializeField] private int inventorySize = 1;
  [SerializeField] private float dodgeDistance = 5f;
  [SerializeField] private float dodgeDuration = 0.2f;


  private void TakeDamage(int damage) => healthbar.TakeDamage(damage);


  private bool isGrounded => Physics2D.OverlapCircle(
    groundCheck.position,
    0.2f,
    groundLayer);

  private bool canUncrouch => !Physics2D.OverlapCircle(ceilingCheck.position,
    0.2f,
    groundLayer);

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

  private void ChangeState(PlayerState newState) => state = newState;

  private void Jump()
  {
    if (!state.canJump || !isGrounded) return;

    if (Input.GetKey(KeyCode.Space))
    {
      rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

  }

  IEnumerator DodgeCoroutine()
  {
    isDodging = true;
    var direction = (float)facingDirection;
    var newPos = new Vector2(transform.position.x
                             + direction
                             * dodgeDistance, transform.position.y);

    var startTime = Time.time;

    while (Time.time < startTime + dodgeDuration)
    {
      var t = (Time.time - startTime) / dodgeDuration;
      transform.position = Vector2.Lerp(transform.position, newPos, t);
      rb.AddForce(new Vector2(direction * 3f, 0), ForceMode2D.Impulse);
      yield return null;
    }

    transform.position = newPos;
    isDodging = false;
  }

  private void Dodge()
  {
    if (!state.canDodge) return;

    if (Input.GetKeyDown(KeyCode.Mouse1))
    {
      StartCoroutine(DodgeCoroutine());
    }
  }

  private void Move()
  {
    // If the player can't move, return
    if (!state.canMove) return;

    var velocity = new Vector2(horizontalInput * state.moveSpeed, rb.velocity.y);

    rb.AddForce(velocity - rb.velocity, ForceMode2D.Impulse);

  }

  private void Crouch()
  {
    if (!state.canCrouch) return;


    if (verticalInput < 0 && isGrounded)
    {
      ChangeState(new CrouchState());
      collider.size = new Vector2(collider.size.x, height / 2);
      collider.offset = new Vector2(collider.offset.x, -height / 4);
    }


    if (!canUncrouch) return;

    //Uncrouch
    if (verticalInput >= 0)
    {
      collider.size = new Vector2(collider.size.x, height);
      collider.offset = new Vector2(collider.offset.x, 0);
    }

  }

  private void Interact()
  {
    var objects = Physics2D.OverlapCircleAll(transform.position, 1.5f, LayerMask.GetMask("Interactable"));
    var interactable = objects.Length > 0 ? objects[0].GetComponent<Interactable>() : null;
    if (interactable == null) return;


    if (Input.GetKeyDown(KeyCode.E))
    {
      interactable.Interact(this);
    }
  }

  private void TouchTheEnemy()
  {

    var mask = LayerMask.GetMask("Enemy");

    if (!collider.IsTouchingLayers(mask)) return;

    var enemy = Physics2D.OverlapCircle(transform.position, 1.5f, mask)
    .GetComponent<Transform>();

    var directionVec = (enemy.position - transform.position).normalized;
    var direction = directionVec.x > 0 ? FacingDirection.Right : FacingDirection.Left;

    rb.AddForce(new Vector2(horizontalKnockback * (float)direction, verticalKnockback) - rb.velocity, ForceMode2D.Impulse);
  }

  public void PickUpItem(Item item)
  {
    if (inventory.Count >= inventorySize) return;

    inventory.Add(item);
    inventoryUI.SetInventory(inventory);
  }

  public void UseItem()
  {
    if (inventory.Count == 0) return;
    if (!Input.GetKeyDown(KeyCode.Mouse0)) return;

    var item = inventory[0];
    item.Use(this);

  }

  private void EffectTick(Effect[] effects)
  {
    foreach (var effect in effects)
    {
      effect.Update();
    }
  }

  private void HandleStateChange()
  {
    PlayerState newState = state;
    if (horizontalInput != 0)
    {
      if (Input.GetKey(KeyCode.LeftShift))
        newState = new RunningState();
      else
        newState = new WalkingState();
    }
    else
    {
      newState = new IdleState();
    }

    if (newState != null && newState != state)
    {
      ChangeState(newState);

      animator.SetBool("isRunning", newState is RunningState);
      animator.SetBool("isWalking", newState is WalkingState);
    }


  }

  #region Gameloop

  private void Start()
  {
    this.height = collider.size.y;

    animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    groundCheck = transform.Find("GroundCheck");
    ceilingCheck = transform.Find("CeilingCheck");
    collider = GetComponent<CapsuleCollider2D>();
  }

  private void Update()
  {


    //Debug
    var stateUi = GameObject.Find("StateUI").GetComponent<TextMeshProUGUI>();
    stateUi.text = effects.Count.ToString();
    //     

    EffectTick(effects.ToArray());
    Turn();
    Interact();
    Crouch();
    TouchTheEnemy();
    UseItem();
    Dodge();
  }



  private void FixedUpdate()
  {
    horizontalInput = Input.GetAxisRaw("Horizontal");
    verticalInput = Input.GetAxisRaw("Vertical");

    HandleStateChange();

    Move();
    Jump();
  }
  #endregion

}
