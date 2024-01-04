using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float _startingHealth;

    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Movement _player;

    [Header("Sounds")]
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _hurtSound;
    public bool _isDead;
    public float currentHealth { get; private set; }

    protected void Awake()
    {
        currentHealth = _startingHealth;
        _player = GetComponent<Movement>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, _startingHealth);
        if (currentHealth > 0)
        {
            _player.PlayAnimation(Movement.AnimationState.Hurt, true);
            AudioManager.instance.PlaySound(_hurtSound);
        }
        else
        {
            if (!_isDead)
            {
                _player.PlayAnimation(Movement.AnimationState.Die, true);
                _player.enabled = false;
                _isDead = true;
                AudioManager.instance.PlaySound(_deathSound);
            }
        }
    }

    public void Respawn()
    {
        AddHealth(_startingHealth);
        _animator.SetInteger(_player._animatorParameterName, (int)Movement.AnimationState.Idle);
        _player._currentState = Movement.AnimationState.Idle;
        _player.enabled = true;
        _isDead = false;
    }
    private void EndHurt()
    {
        _player.PlayAnimation(Movement.AnimationState.Hurt, false);
    }
    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, _startingHealth);
    }
}
