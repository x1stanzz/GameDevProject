using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballPlayer : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _range;
    [SerializeField] private float _damage;

    [Header("Collider Parameters")]
    [SerializeField] private float _colliderDistance;
    [SerializeField] private BoxCollider2D _boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask _playerLayer;

    [Header("References")]
    [SerializeField] private Animator _animator;
    private EnemyPatrol _enemyPatrol;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip _attackSound;

    private float _cooldownTimer = Mathf.Infinity;
    private PlayerHealth _playerHealth;


    private void Awake()
    {
        _enemyPatrol = GetComponentInParent<EnemyPatrol>(); 
    }
    private void Update()
    {
        _cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if(_cooldownTimer > _attackCooldown)
            {
                _cooldownTimer = 0;
                _animator.SetTrigger("MeleeAttack");
                AudioManager.instance.PlaySound(_attackSound);
            }
        }

        if(_enemyPatrol != null)
            _enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z), 0, Vector2.left, 0, _playerLayer);
        if (hit.collider != null)
            _playerHealth = hit.transform.GetComponent<PlayerHealth>();
        return hit.collider != null && _playerHealth._isDead == false ;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z));
    }
    private void DamagePlayer()
    {
        if (PlayerInSight())
            _playerHealth.TakeDamage(_damage);
    }
}
