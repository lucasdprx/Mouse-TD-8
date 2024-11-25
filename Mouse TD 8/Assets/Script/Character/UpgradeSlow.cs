using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class UpgradeSlow : MonoBehaviour, IUpgrade
{
    public int _level { get; set; }
    public TextMeshProUGUI _descriptionLevel;
    public List<string> _listDescription { get; set; }
    public List<int> _listPrice { get; set; }

    private DefenseStat _defenseStat;
    private void Awake()
    {
        _listPrice = new List<int> { 100, 200, 300, 400 };
        _listDescription = new List<string> 
        {   "Slow time increase by 30%", 
            "Slow time increase by 50%", 
            "Upgrade 3", 
            "Upgrade 4",
            "Level Max"
        };
    }

    private void Start()
    {
        _defenseStat = GetComponent<DefenseStat>();
        _descriptionLevel.text = _listDescription[_level];
    }

    public void Upgrade(Button button)
    {
        if (Money.instance.GetMoney() < _listPrice[_level]) return;
        
        Money.instance.RemoveMoney(_listPrice[_level]);
        _level++;
        _descriptionLevel.text = _listDescription[_level];
        
        switch (_level)
        {
            case 1:
                _defenseStat._slowTime *= 1.3f;
                break;
            case 2:
                _defenseStat._slowTime *= 1.5f;
                break;
            case 3:
                break;
            case 4:
                button.interactable = false;
                break;
        }
    }
}
