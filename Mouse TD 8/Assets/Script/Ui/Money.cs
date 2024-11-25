using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMoney;
    [SerializeField] private int _money;
    public static Money instance;

    private void Awake()
    {
        _textMoney.text = _money.ToString();
        
        if (instance != null) return;
        instance = this;
    }
    public int GetMoney() => _money;

    public void RemoveMoney(int money)
    {
        _money -= money;
        _textMoney.text = _money.ToString();
    }
    public void AddMoney(int money)
    {
        _money += money;
        _textMoney.text = _money.ToString();
    }
}
