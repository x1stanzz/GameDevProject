using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider2D _boxCollider;

    private float _direction;
    private bool _hit;
    private float _lifetime;

    private void Update()
    {
        if (_hit) return;
        float movementSpeed = _speed * Time.deltaTime * _direction;
        transform.Translate(movementSpeed, 0, 0);

        _lifetime += Time.deltaTime;
        if(_lifetime > 5)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("IgnoreCollision"))
            return;
        _hit = true;
        _boxCollider.enabled = false;
        _animator.SetTrigger("Explode");

        if (collision.tag == "Enemy")
            collision.GetComponent<EnemyHealth>().TakeDamage(_damage);
    }

    public void SetDirection(float direction, bool faceRight)
    {
        _lifetime = 0;
        gameObject.SetActive(true);
        _direction = direction;
        _hit = false;
        _boxCollider.enabled = true;    

        float localScaleX = transform.localScale.x;
        if(!faceRight)
        {
            localScaleX *= -1;
            _direction *= -1;

        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
