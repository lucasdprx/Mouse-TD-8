using TMPro;
using UnityEngine;

public class Life : MonoBehaviour
{
    public static Life instance;

    [SerializeField] private int life;
    [SerializeField] private TextMeshProUGUI _textLife;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
        
        _textLife.text = life.ToString();
    }

    public int GetLife()
    {
        return life;
    }

    public void RemoveLife(int number)
    {
        life -= number;
        if (life <= 0)
        {
            life = 0; 
            Debug.Log("Game Over");
        }
        
        _textLife.text = life.ToString();
    }
}
