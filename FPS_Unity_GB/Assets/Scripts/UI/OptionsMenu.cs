using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsMenu : BaseMenu
{
    enum OptionsMenuItems
    {
        Video,
        Sound,
        Game,
        Back
    }

    private void CreateMenu(string[] menuItems)
    {
        _elementsOfInterface = new IControl[menuItems.Length];
        for (var index = 0; index < menuItems.Length; index++)
        {
            switch (index)
            {
                case (int)OptionsMenuItems.Video:
                    {
                        var tempControl =
                        CreateControl(Interface.InterfaceResources.ButtonPrefab,
                            LangManager.Instance.Text("OptionsMenuItems", "Video"));
                        tempControl.GetControl.onClick.AddListener(LoadVideoOptions);
                        _elementsOfInterface[index] = tempControl;
                        break;
                    }
                case (int)OptionsMenuItems.Sound:
                    {
                        var tempControl =
                        CreateControl(Interface.InterfaceResources.ButtonPrefab,
                            LangManager.Instance.Text("OptionsMenuItems", "Sound"));
                        tempControl.GetControl.onClick.AddListener(LoadSoundOptions);
                        _elementsOfInterface[index] = tempControl;
                        break;
                    }
                case (int)OptionsMenuItems.Game:
                    {
                        var tempControl =
                        CreateControl(Interface.InterfaceResources.ButtonPrefab,
                            LangManager.Instance.Text("OptionsMenuItems", "Game"));
                        tempControl.GetControl.onClick.AddListener(LoadGameOptions);
                        _elementsOfInterface[index] = tempControl;
                        break;
                    }
                case (int)OptionsMenuItems.Back:
                    {
                        var tempControl =
                        CreateControl(Interface.InterfaceResources.ButtonPrefab,
                            LangManager.Instance.Text("OptionsMenuItems", "Back"));
                        tempControl.GetControl.onClick.AddListener(Back);
                        _elementsOfInterface[index] = tempControl;
                        break;
                    }
            }
        }
        if (_elementsOfInterface.Length < 0) return;
        _elementsOfInterface[0].Control.Select();
        _elementsOfInterface[0].Control.OnSelect(new BaseEventData(EventSystem.current));
    }


    [SerializeField] private GameObject _optionsPanel;

    [SerializeField] private ButtonUI _video;
    [SerializeField] private ButtonUI _sound;
    [SerializeField] private ButtonUI _game;
    [SerializeField] private ButtonUI _back;


    private void Start()
    {
        _video.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Video");
        _video.SetInteractable(false);

        _sound.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Sound");
        _sound.GetControl.onClick.AddListener(delegate
        {
            LoadSoundOptions();
        });

        _game.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Game");
        _game.SetInteractable(false);

        _back.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Back");
        _back.GetControl.onClick.AddListener(delegate
        {
            Back();
        });
    }

    private void LoadVideoOptions()
    {
        Interface.Execute(InterfaceObject.VideoOptions);

    }

    private void LoadSoundOptions()
    {
        Interface.Execute(InterfaceObject.AudioOptions);
    }

    private void LoadGameOptions()
    {
        Interface.Execute(InterfaceObject.GameOptions);
    }

    private void Back()
    {
        Interface.Execute(InterfaceObject.MainMenu);
    }

    public override void Hide()
    {
        if (!IsShow) return;
        //Clear(_elementsOfInterface);
        _optionsPanel.gameObject.SetActive(false);
        IsShow = false;
    }

    public override void Show()
    {
        if (IsShow) return;
        //var tempMenuItems = System.Enum.GetNames(typeof(OptionsMenuItems));
        //CreateMenu(tempMenuItems);
        _optionsPanel.gameObject.SetActive(true);
        IsShow = true;
    }
}