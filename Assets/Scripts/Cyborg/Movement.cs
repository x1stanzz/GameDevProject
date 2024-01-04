using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("HorizontalMovement")]
    [SerializeField] private float _speed;
    public bool faceRight;

    [Header("Jump")]
    [SerializeField] private float _jumpPower;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _whatIsGround;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    public AnimationState _currentState;
    public string _animatorParameterName { get; private set; }

    [Header("Jump Sound")]
    [SerializeField] private AudioClip _jumpSound;

    private Rigidbody2D _rigidbody;
    public float _direction { get; private set; }
    private float _verticalDirection;
    private bool _jump;
    public bool CanClimb { private get; set; }
    public bool isAttacking {  private get; set; }
    public bool isShooting { private get; set; }
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animatorParameterName = _animator.GetParameter(0).name;
    }

    private void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal");
        _verticalDirection = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump"))
            _jump = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckRadius);
    }
    private void FixedUpdate()
    {
        bool isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _whatIsGround);

        Move(_direction);
        SetDirection();
        if (_jump && isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpPower);
            AudioManager.instance.PlaySound(_jumpSound);
        }
        _jump = false;

        if (CanClimb)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _verticalDirection * _speed);
            _rigidbody.gravityScale = 0;
        }
        else
        {
            _rigidbody.gravityScale = 3;
        }

        PlayAnimation(AnimationState.Jump, !isGrounded && _rigidbody.velocity.y > 0f);
        PlayAnimation(AnimationState.Run, _direction != 0);
        PlayAnimation(AnimationState.ClimbIdle, CanClimb && _verticalDirection == 0);
        PlayAnimation(AnimationState.Climb, CanClimb && _verticalDirection != 0);
        PlayAnimation(AnimationState.Attack, isAttacking);
        PlayAnimation(AnimationState.Shoot, isShooting);
    }
    public void PlayAnimation(AnimationState animationState, bool active)
    {
        if (animationState < _currentState)
            return;

        if (!active)
        {
            if(animationState == _currentState)
            {
                _animator.SetInteger(_animatorParameterName, (int)AnimationState.Idle);
                _currentState = AnimationState.Idle;
            }
            return;
        }
        _animator.SetInteger(_animatorParameterName, (int)animationState);
        _currentState = animationState;
    }
    private void Move(float direction)
    {
        _rigidbody.velocity = new Vector2(_speed * _direction, _rigidbody.velocity.y);
    }

    private void SetDirection()
    {
        if(faceRight && _direction < 0)
        {
            Flip();
        }
        else if(!faceRight && _direction > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
    }

    public enum AnimationState
    {
        Idle = 0,
        Run = 1,
        Attack = 2,
        Shoot = 3,
        Jump = 4,
        ClimbIdle = 5,
        Climb = 6,
        Hurt = 7, 
        Die = 8
    }
}
