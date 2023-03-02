using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : Character
{
  protected bool _playerDetected = false;
  protected abstract void DetectPlayer();

};


public class FlyingEyeball : Enemy
{
  private Rigidbody2D _rigidbody2D;
  private Animator _animator;
  private SpriteRenderer _spriteRenderer;
  private GameObject _lightSource;
  private float _time;
  private float _prevPingPong;
  private float _currentPingPong;

  [SerializeField] private float _lightRotationAngle = 133f;
  [SerializeField] private float _floatSpeed = 2f;
  [SerializeField] private float _floatHeight = 1f;
  [SerializeField] private float _moveSpeed = 1f;
  [SerializeField] private FacingDirection _facingDirection = FacingDirection.Left;

  [Header("Trajectory")]
  [SerializeField] private Vector3 _startPosition;
  [SerializeField] private Vector3 _endPosition;

  [Header("Raycast")]
  [SerializeField] private float _rayAngle = 15f;
  [SerializeField] private float _rayLength = 5f;
  [SerializeField] private float _rayCount = 5f;
  [SerializeField] private LayerMask _rayLayerMask;





  private void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _startPosition = transform.position;
    _lightSource = transform.Find("Light").gameObject;
  }

  private void Update()
  {
    Patrol();
    CheckTurn();
    Turn();
    DetectPlayer();



  }


  protected override void DetectPlayer()
  {
    var start = transform.position;

    var dirMultiplyer = _facingDirection == FacingDirection.Left ? -1 : 1;
    var target = Vector3.up;
    target = Quaternion.Euler(0, 0, _lightRotationAngle * dirMultiplyer) * target;

    var vectors = new List<Vector3>();

    var splitWidth = _rayAngle / _rayCount * 2;

    for (int i = 0; i <= _rayCount; i++)
    {
      var v = Quaternion.AngleAxis(splitWidth * i - _rayAngle, Vector3.forward) * target;
      vectors.Add(v);
      Debug.DrawRay(start, v * _rayLength, Color.cyan, 0.01f);
    }

    foreach (var v in vectors)
    {
      var hit = Physics2D.Raycast(start, v, _rayLength, _rayLayerMask);
      if (hit.collider != null)
      {
        if (hit.collider.gameObject.tag == "Player")
        {
          _playerDetected = true;
          return;
        }


        _playerDetected = false;
      }
    }
  }

  private void Patrol()
  {
    if (_playerDetected) return;
    Move();
    FlyUp();

  }

  private void Move()
  {
    _currentPingPong = Mathf.PingPong(Time.time * _moveSpeed, 1);
    transform.position = Vector3.Lerp(_startPosition, _endPosition, _currentPingPong);
  }
  private void FlyUp()
  {
    var newY = Mathf.Sin(Time.time * _floatSpeed) * _floatHeight + _startPosition.y;
    transform.position = new Vector3(transform.position.x, newY, transform.position.z);
  }

  private void CheckTurn()
  {
    if (_currentPingPong == _prevPingPong) return;

    if (_currentPingPong < _prevPingPong)
      _facingDirection = FacingDirection.Left;
    else
      _facingDirection = FacingDirection.Right;

    _prevPingPong = _currentPingPong;
  }



  private void Turn()
  {

    switch (_facingDirection)
    {
      case FacingDirection.Left:
        _spriteRenderer.flipX = true;
        _lightSource.transform.rotation = Quaternion.Euler(0, 0, -_lightRotationAngle);
        break;
      case FacingDirection.Right:
        _spriteRenderer.flipX = false;
        _lightSource.transform.rotation = Quaternion.Euler(0, 0, _lightRotationAngle);
        break;
    }
  }


}