using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
    
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
