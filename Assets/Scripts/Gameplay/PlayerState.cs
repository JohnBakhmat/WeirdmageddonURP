using UnityEngine;


public interface PlayerState
{
  bool isVisible();
  bool canMove();
  bool canAttack();
  bool canCrouch();

  public float moveSpeed { get; }
}

public class IdleState : PlayerState
{
  public float moveSpeed => 0f;

  public bool isVisible() => true;
  public bool canMove() => true;
  public bool canAttack() => true;
  public bool canCrouch() => true;
}

public class CrouchState : PlayerState
{
  public float moveSpeed => 6f;

  public bool isVisible() => true;
  public bool canMove() => true;
  public bool canAttack() => false;
  public bool canCrouch() => false;
}

public class WalkState : PlayerState
{
  public float moveSpeed => 8f;

  public bool isVisible() => true;
  public bool canMove() => true;
  public bool canAttack() => true;
  public bool canCrouch() => true;
}

public class RunState : PlayerState
{
  public float moveSpeed => 200f;

  public bool isVisible() => true;
  public bool canMove() => true;
  public bool canAttack() => true;
  public bool canCrouch() => true;
}
