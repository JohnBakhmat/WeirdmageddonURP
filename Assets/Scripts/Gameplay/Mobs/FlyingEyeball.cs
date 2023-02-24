using UnityEngine;

public class FlyingEyeball : MonoBehaviour
{
  private Rigidbody2D _rigidbody2D;
  private Animator _animator;
  private SpriteRenderer _spriteRenderer;

  [SerializeField] private float _floatSpeed = 2f;
  [SerializeField] private float _floatHeight = 1f;
  [SerializeField] private float _moveSpeed = 1f;
  [SerializeField] private FacingDirection _facingDirection = FacingDirection.Left;

  [Header("Trajectory")]
  [SerializeField] private Vector3 _startPosition;
  [SerializeField] private Vector3 _endPosition;


  private float _prevPingPong;
  private float _currentPingPong;

  private void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _startPosition = transform.position;
  }

  private void Update()
  {
    Turn();

    Move();
  }

  private void FixedUpdate()
  {

  }

  private void Move()
  {
    _currentPingPong = Mathf.PingPong(Time.time * _moveSpeed, 1);
    transform.position = Vector3.Lerp(_startPosition, _endPosition, _currentPingPong);
    // transform.position = new Vector3(transform.position.x, _startPosition.y, transform.position.z) + new Vector3(0, Mathf.Sin(Time.time * _floatSpeed), 0);
    var newY = Mathf.Sin(Time.time * _floatSpeed) * _floatHeight + _startPosition.y;
    transform.position = new Vector3(transform.position.x, newY, transform.position.z);


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
        break;
      case FacingDirection.Right:
        _spriteRenderer.flipX = false;
        break;
    }
  }


  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawLine(_startPosition, _endPosition);
    Debug.DrawRay(transform.position, Vector3.up * _floatHeight, Color.green, 10f);
  }
}