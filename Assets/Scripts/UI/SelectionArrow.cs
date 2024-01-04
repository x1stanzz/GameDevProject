using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] _options;
    [SerializeField] private AudioClip _changeSound;
    [SerializeField] private AudioClip _interactSound;

    private RectTransform _rect;
    private int _currentPosition;
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }
    private void ChangePosition(int change)
    {
        _currentPosition += change;

        if (change != 0)
            AudioManager.instance.PlaySound(_changeSound);

        if (_currentPosition < 0)
            _currentPosition = _options.Length - 1;
        else if (_currentPosition > _options.Length - 1)
            _currentPosition = 0;
        _rect.position = new Vector3(_rect.position.x, _options[_currentPosition].position.y, 0);
    }
    
    private void Interact()
    {
        AudioManager.instance.PlaySound(_interactSound);

        _options[_currentPosition].GetComponent<Button>().onClick.Invoke();
    }

}
