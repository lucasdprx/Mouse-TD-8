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
    [SerializeField] private Toggle _toggleWaveAutoPlay;
    [SerializeField] private Slider _sliderSFX;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private TMP_Dropdown _dropdownResolution;
    
    private Resolution[] _resolutions;
    
    private void Start()
    {
        if (_toggleFullScreen != null)
            _toggleFullScreen.isOn = Screen.fullScreen;

        if (_toggleWaveAutoPlay != null)
            _toggleWaveAutoPlay.isOn = PlayerPrefs.GetInt("AutoPlay") == 1;

        if (PlayerPrefs.HasKey("SFX") && _sliderSFX != null)
            _sliderSFX.value = PlayerPrefs.GetFloat("SFX");
            

        if (PlayerPrefs.HasKey("Music") && _sliderMusic != null)
            _sliderMusic.value = PlayerPrefs.GetFloat("Music");
        
        if (_dropdownResolution != null)
            GetResolution();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SetAutoPlay(bool isFullScreen)
    {
        PlayerPrefs.SetInt("AutoPlay", isFullScreen ? 1 : 0);
    }
    
    public void SetVolumeSFX(float volume)
    {
        if (_audioMixer == null) return;
        
        _audioMixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFX", _sliderSFX.value);
    }
    
    public void SetVolumeMusic(float volume)
    {
        if (_audioMixer == null) return;
        
        _audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("Music", _sliderMusic.value);
    }
    
    private Resolution[] GetResolution()
    {
        _resolutions = Screen.resolutions.Select(resolution => 
            new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        _dropdownResolution.ClearOptions();
        List<string> options = new List<string>();
        int currentResolution = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height;
            options.Add(option);
            
            if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
            {
                currentResolution = i;
            }
        }
        
        _dropdownResolution.AddOptions(options);
        _dropdownResolution.value = currentResolution;
        _dropdownResolution.RefreshShownValue();
        
        return _resolutions;
    }
    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
