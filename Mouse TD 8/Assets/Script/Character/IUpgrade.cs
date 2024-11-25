using System.Collections.Generic;
using TMPro;
using Button = UnityEngine.UI.Button;
public interface IUpgrade
{
    public int _level { get; set; }
    public List<string> _listDescription { get; set; }
    public List<int> _listPrice { get; set; }
    public void Upgrade(Button button);
    
}