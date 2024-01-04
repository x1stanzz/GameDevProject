using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject[] _fireballs;

    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Movement _player;
    private float _cooldownTimer = Mathf.Infinity;

    [Header("Shoot Sound")]
    [SerializeField] private AudioClip _shootSound;

    private void Update()
    {
        if (Input.GetMouseButton(1) && _cooldownTimer > _attackCooldown)
            Shoot();
        _cooldownTimer += Time.deltaTime;
    }

    private void Shoot()
    {
        AudioManager.instance.PlaySound(_shootSound);
        _player.isShooting = true;
        _cooldownTimer = 0;

        _fireballs[FindFireball()].transform.position = _firePoint.position;
        _fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(_player._direction), _player.faceRight);
    }
    private void EndShoot()
    {
        _player.isShooting = false;
    }

    private int FindFireball()
    {
        for (int i = 0; i < _fireballs.Length; i++)
        {
            if (!_fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
