using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _winText;
    [SerializeField] private Button _nextLevelButton;

    public static EndGame Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetEndGame(bool isWin)
    {
        Time.timeScale = 0.0f;
        _endGamePanel.SetActive(true);
        _waveText.text = "Wave " + PoolSpawner.Instance._waveIndex + " / " + PoolSpawner.Instance._waves.Count;
        _winText.text = isWin ? "You Win !" : "You Lose !";
        _nextLevelButton.interactable = isWin;
    }
    
    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void NextLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void MainMenu() => SceneManager.LoadScene(0);
}
