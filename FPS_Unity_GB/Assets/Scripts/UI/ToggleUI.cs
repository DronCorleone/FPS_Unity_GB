using UnityEngine.UI;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    private Toggle _toggle;
    private Text _text;

    private void Awake()
    {
        _toggle = transform.GetComponentInChildren<Toggle>();
        _text = transform.GetComponentInChildren<Text>();
    }

    public bool GetValue()
    {
        return _toggle.isOn;
    }

    public void SetValue(bool value)
    {
        _toggle.isOn = value;
    }
}
