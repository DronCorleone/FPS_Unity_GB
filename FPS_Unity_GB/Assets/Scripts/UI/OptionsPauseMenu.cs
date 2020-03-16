using UnityEngine;

public class OptionsPauseMenu : BaseMenu
{
    enum OptionsPauseMenuItems
    {
        Volume,
        Back
    }

    [SerializeField] private GameObject _optionsPausePanel;

    [SerializeField] private SliderUI _volume;
    [SerializeField] private ButtonUI _back;

    private void Start()
    {
        _optionsPausePanel.SetActive(true);

        _volume.GetText.text = LangManager.Instance.Text("OptionsPauseMenu", "Volume");

        _back.GetText.text = LangManager.Instance.Text("OptionsPauseMenu", "Back");
        _back.GetControl.onClick.AddListener(delegate
        {
            Back();
        });

        _optionsPausePanel.SetActive(false);

    }

    private void Back()
    {
        Interface.Execute(InterfaceObject.MenuPause);
    }

    public override void Hide()
    {
        if (!IsShow) return;
        _optionsPausePanel.SetActive(false);
        IsShow = false;
    }

    public override void Show()
    {
        if (IsShow) return;
        _optionsPausePanel.SetActive(true);
        IsShow = true;
    }
}
