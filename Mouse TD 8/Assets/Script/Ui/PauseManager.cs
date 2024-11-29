using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuUI;
    private bool _isPaused;
    private float _timeScale;
    private void Update()
    {
        if (!_isPaused)
        {
            _timeScale = Time.timeScale;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Resume();
        }
    }
    public void Resume()
    {
        Time.timeScale = _isPaused ? _timeScale : 0.0f;
        _pauseMenuUI.SetActive(!_pauseMenuUI.activeSelf);
        _isPaused = !_isPaused;
    }
}
