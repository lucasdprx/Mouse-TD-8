using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Button = UnityEngine.UI.Button;

public class UpgradeInfo : MonoBehaviour
{
    [SerializeField, Multiline] private List<string> _listDescription = new List<string> {"","","",""};
    [SerializeField] private List<int> _listPrice = new List<int> {0,0,0,0};
    [SerializeField] private TextMeshProUGUI _descriptionLevel;
    [SerializeField] private TextMeshProUGUI _textPrice;
    
    private int _level;
    private DefenseStat _defenseStat;
    private void Start()
    {
        _defenseStat = GetComponent<DefenseStat>();
        _descriptionLevel.text = _listDescription[0];
        _textPrice.text = _listPrice[0].ToString();
    }

    public void Upgrade(Button button)
    {
        if (_level >= _listPrice.Count) return;
        if (Money.instance.GetMoney() < _listPrice[_level]) return;
        
        Money.instance.RemoveMoney(_listPrice[_level]);
        _level++;
        
        BaseUpgrade baseUpgrade = GetComponent<BaseUpgrade>();
        if (baseUpgrade == null) return;
        baseUpgrade.OnUpgrade(_defenseStat, _level);

        if (_level < _listDescription.Count)
        {
            _descriptionLevel.text = _listDescription[_level];
            _textPrice.text = _listPrice[_level].ToString();
            return;
        }
            
        _descriptionLevel.text = "Level max";
        _textPrice.transform.parent.gameObject.SetActive(false);
        button.interactable = false;
    }
}
