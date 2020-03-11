using UnityEngine;
using UnityEngine.UI;

public class DropdownUI : MonoBehaviour
{

    private Dropdown _dropDown;

    private void SetOptions()
    {
        Dropdown.OptionData data = new Dropdown.OptionData("cyka");
        _dropDown.options.Add(data);
        
    }
}
