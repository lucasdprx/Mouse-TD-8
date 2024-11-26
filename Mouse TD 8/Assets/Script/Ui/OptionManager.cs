using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public AudioMixer _audioMixer;
    
    [SerializeField] private Toggle _toggleFullScreen;
    [SerializeField] private Slider _sliderSFX;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private TMP_Dropdown _dropdownResolution;
    
    private Resolution[] _resolutions;
    
    private void OnEnable()
    {
        if (_toggleFullScreen != null)
            _toggleFullScreen.isOn = Screen.fullScreen;

        if (PlayerPrefs.HasKey("SFX") && _sliderSFX != null)
            _sliderSFX.value = PlayerPrefs.GetFloat("SFX");

        if (PlayerPrefs.HasKey("Music") && _sliderMusic != null)
            _sliderMusic.value = PlayerPrefs.GetFloat("Music");
        
        if (_dropdownResolution != null)
            GetResolution(_resolutions, _dropdownResolution);
    }
    
    private void OnDisable()
    {
        if (_sliderMusic is not null)
            PlayerPrefs.SetFloat("Music", _sliderMusic.value);

        if (_sliderSFX is not null)
            PlayerPrefs.SetFloat("SFX", _sliderSFX.value);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    
    public void SetVolumeSFX(float volume)
    {
        if (_audioMixer == null) return;
        
        _audioMixer.SetFloat("SFX", volume);
    }
    
    public void SetVolumeMusic(float volume)
    {
        if (_audioMixer == null) return;
        
        _audioMixer.SetFloat("Music", volume);
    }
    
    private static void GetResolution(Resolution[] resolutions, TMP_Dropdown dropdownResolution)
    {
        if (resolutions == null || dropdownResolution == null) return;
        
        resolutions = Screen.resolutions.Select(resolution => 
            new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        dropdownResolution.ClearOptions();
        List<string> options = new List<string>();
        int currentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolution = i;
            }
        }
        
        dropdownResolution.AddOptions(options);
        dropdownResolution.value = currentResolution;
        dropdownResolution.RefreshShownValue();
    }
    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
