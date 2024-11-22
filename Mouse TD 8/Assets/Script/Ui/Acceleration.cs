using TMPro;
using UnityEngine;

public class Acceleration : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textAcceleration;
    
    public void SetTimeScale()
    {
        switch (Time.timeScale)
        {
            case 1:
                Time.timeScale = 2.0f;
                _textAcceleration.text = "2x";
                break;
            case 2:
                Time.timeScale = 3.0f;
                _textAcceleration.text = "3x";
                break;
            case 3:
                Time.timeScale = 4.0f;
                _textAcceleration.text = "4x";
                break;
            case 4:
                Time.timeScale = 1.0f;
                _textAcceleration.text = "1x";
                break;
        }
    }
}
