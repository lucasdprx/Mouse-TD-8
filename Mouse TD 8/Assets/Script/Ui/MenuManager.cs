using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<Button> _levelList = new List<Button>();
    private void Start()
    {
        if (_levelList.Count <= 0) return;
        
        RefreshLevelList();
    }
    public void PlayScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        Time.timeScale = 1;
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (!Input.GetKeyUp(KeyCode.P))
            return;
        
        PlayerPrefs.DeleteAll();
        RefreshLevelList();
    }
    private void RefreshLevelList()
    {
        _levelList[0].interactable = true;
        for (int i = 1; i < _levelList.Count; i++)
        {
            _levelList[i].interactable = PlayerPrefs.GetInt("Level " + i) == 1;
        }
    }
}
