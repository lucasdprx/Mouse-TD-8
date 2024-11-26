using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _currentPanel;
    
    public void PlayScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void OpenPanel(GameObject panel)
    {
        if (_currentPanel == null) return;
        
        _currentPanel.SetActive(false);
        _currentPanel = panel;
        _currentPanel.SetActive(true);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
