using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private AudioClip _gameOverSound;


    [Header("Pause")]
    [SerializeField] private GameObject _pauseScreen;

    [Header("Win Screen")]
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private Movement _player;
    public bool isWin { private get; set; }

    private void Awake()
    {
        _gameOverScreen.SetActive(false);
        _pauseScreen.SetActive(false);
        _winScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }
    #region GameOver
    public void GameOver()
    {
        _gameOverScreen.SetActive(true);
        AudioManager.instance.PlaySound(_gameOverSound);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        //Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion
    #region Pause
    public void PauseGame(bool status)
    {
        _pauseScreen.SetActive(status);

        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void ChangeSoundVolume()
    {
        AudioManager.instance.ChangeSoundVolume(0.1f);
    }

    public void ChangeMusicVolume()
    {
        AudioManager.instance.ChangeMusicVolume(0.1f);
    }
    #endregion

    #region Win
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_player != null)
        {
            _winScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
    #endregion
}
