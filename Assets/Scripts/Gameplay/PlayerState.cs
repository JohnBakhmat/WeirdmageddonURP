


public interface PlayerState
{
  public bool canMove { get; }
  public bool canJump { get; }
  public bool canCrouch { get; }
  public float moveSpeed { get; }
}


public class IdleState : PlayerState
{
  public bool canMove => true;

  public bool canJump => true;

  public float moveSpeed => 0f;

  public bool canCrouch => true;

}
public class WalkingState : PlayerState
{
  public bool canMove => true;

  public bool canJump => true;

  public float moveSpeed => 5f;

  public bool canCrouch => true;


}
public class CrouchState : PlayerState
{
  public bool canMove => true;

  public bool canJump => false;

  public float moveSpeed => 2.5f;

  public bool canCrouch => false;


}
public class RunningState : PlayerState
{
  public bool canMove => true;

  public bool canJump => true;

  public float moveSpeed => 8f;

  public bool canCrouch => true;

}
