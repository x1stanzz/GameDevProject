using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Movement _player;
    [SerializeField] private UIManager _manager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        UIManager _manager = gameObject.GetComponent<UIManager>();
        if (_player != null)
            _manager.isWin = true;
    }
}
