using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip _checkPointSound;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private UIManager _uiManager;
    private Transform _currentCheckpoint;

    public void CheckRespawn()
    {
        if(_currentCheckpoint == null)
        {
            _uiManager.GameOver();
            return;
        }
        transform.position = _currentCheckpoint.position;
        _playerHealth.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            _currentCheckpoint = collision.transform;
            AudioManager.instance.PlaySound(_checkPointSound);
            collision.GetComponent<Collider2D>().enabled = false;
        }
    }

}
