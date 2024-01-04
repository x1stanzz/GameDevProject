using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform _arrow;
    [SerializeField] private RectTransform[] _buttons;
    [SerializeField] private AudioClip _changeSound;
    [SerializeField] private AudioClip _interactSound;
    private int _currentPosition;

    private void Awake()
    {
        ChangePosition(0);
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
    public void ChangePosition(int change)
    {
        _currentPosition += change;
        if(change != 0)
        {
            AudioManager.instance.PlaySound(_changeSound);
        }

        if (_currentPosition < 0)
            _currentPosition = _buttons.Length - 1;
        else if (_currentPosition > _buttons.Length - 1)
            _currentPosition = 0;

        AssignPosition();
    }

    private void AssignPosition()
    {
        _arrow.position = new Vector3(_arrow.position.x, _buttons[_currentPosition].position.y);
    }
    private void Interact()
    {
        AudioManager.instance.PlaySound(_interactSound);

        if (_currentPosition == 0)
            SceneManager.LoadScene(PlayerPrefs.GetInt("SampleScene", 1));
        else if (_currentPosition == 1)
            Quit();
            
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
