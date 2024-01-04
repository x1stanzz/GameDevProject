using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] protected float _startingHealth;
    [SerializeField] protected Animator _animator;
    [SerializeField] private BoxCollider2D _boxCollider;

    [Header("Sounds")]
    [SerializeField] private AudioClip _hurtSound;
    [SerializeField] private AudioClip _deathSound;

    private bool _isDead;
    public float currentHealth;

    protected void Awake()
    {
        currentHealth = _startingHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, _startingHealth);
        if (currentHealth > 0)
        {
            _animator.SetTrigger("Hurt");
            AudioManager.instance.PlaySound(_hurtSound);
        }
        else
        {
            if (!_isDead)
            {
                _animator.SetTrigger("Die");

                if (GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if (GetComponent<BaseballPlayer>() != null)
                    GetComponent<BaseballPlayer>().enabled = false;
                _boxCollider.enabled = false;
                _isDead = true;
                AudioManager.instance.PlaySound(_deathSound);
            }
        }
    }
}
