using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatlhCOllectible : MonoBehaviour
{
    [SerializeField] private float _healthValue;
    [SerializeField] private AudioClip _pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            AudioManager.instance.PlaySound(_pickupSound);
            collision.GetComponent<PlayerHealth>().AddHealth(_healthValue);
            gameObject.SetActive(false);
        }
    }
}
