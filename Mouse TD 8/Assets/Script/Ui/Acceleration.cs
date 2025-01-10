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
                _textAcceleration.text = "x2";
                break;
            case 2:
                Time.timeScale = 4.0f;
                _textAcceleration.text = "x4";
                break;
            case 4:
                Time.timeScale = 8.0f;
                _textAcceleration.text = "x8";
                break;
            case 8:
                Time.timeScale = 1.0f;
                _textAcceleration.text = "x1";
                break;
        }
    }
}
