using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.UI;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _range;
    [SerializeField] private float _damage;

    [Header("Collider Parameters")]
    [SerializeField] private float _colliderDistance;
    [SerializeField] private BoxCollider2D _boxCollider;

    [Header("Enemy Layer")]
    [SerializeField] private LayerMask _enemyLayer;

    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Movement _player;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip _attackSound;

    private float _cooldownTimer = Mathf.Infinity;
    private EnemyHealth _enemyHealth;

    private void Update()
    {
        if(Input.GetButtonDown("Fire1") && _cooldownTimer > _attackCooldown)
        {
            Attack();
        } 
        _cooldownTimer += Time.deltaTime;
    }

    private bool EnemyInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z), 0, Vector2.left, 0, _enemyLayer);
        if(hit.collider != null)
        {
            _enemyHealth = hit.transform.GetComponent<EnemyHealth>(); 
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z));
    }

    private void DamageEnemy()
    {
        if (EnemyInSight())
            _enemyHealth.TakeDamage(_damage);
        AudioManager.instance.PlaySound(_attackSound);
    }
    private void Attack()
    {
        _cooldownTimer = 0;
        _player.isAttacking = true;
    }

    public void EndAttack()
    {
        _player.isAttacking = false;
    }
}
