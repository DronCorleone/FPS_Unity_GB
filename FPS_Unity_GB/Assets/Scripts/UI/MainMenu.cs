using Geekbrains;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : BaseMenu
{
    #region Old

    enum MainMenuItems
    {
        NewGame,
        Continue,
        Options,
        Quit
    }

    private void CreateMenu(string[] menuItems)
    {
        _elementsOfInterface = new IControl[menuItems.Length];
        for (var index = 0; index < menuItems.Length; index++)
        {
            switch (index)
            {
                case (int)MainMenuItems.NewGame:
                    {
                        var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MainMenuItems", "NewGame"));
                        tempControl.GetControl.onClick.AddListener(delegate
                        {
                            LoadNewGame(Main.Instance.Scenes.Game.SceneAsset.name);
                        });

                        _elementsOfInterface[index] = tempControl;
                        break;
                    }
                case (int)MainMenuItems.Continue:
                    {
                        var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MainMenuItems", "Continue"));
                        tempControl.SetInteractable(false);

                        _elementsOfInterface[index] = tempControl;
                        break;
                    }
                case (int)MainMenuItems.Options:
                    {
                        var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MainMenuItems", "Options"));

                        tempControl.GetControl.onClick.AddListener(ShowOptions);
                        _elementsOfInterface[index] = tempControl;
                        break;
                    }
                case (int)MainMenuItems.Quit:
                    {
                        var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MainMenuItems", "Quit"));

                        tempControl.GetControl.onClick.AddListener(Interface.QuitGame);
                        _elementsOfInterface[index] = tempControl;
                        break;
                    }
            }
        }
        if (_elementsOfInterface.Length < 0) return;
        _elementsOfInterface[0].Control.Select();
        _elementsOfInterface[0].Control.OnSelect(new BaseEventData(EventSystem.current));
    }

    #endregion

    #region New

    [SerializeField] private GameObject _mainPanel;

    [SerializeField] private ButtonUI _newGame;
    [SerializeField] private ButtonUI _continue;
    [SerializeField] private ButtonUI _options;
    [SerializeField] private ButtonUI _quit;

    private void Start()
    {
        Time.timeScale = 1;
        _newGame.GetText.text = LangManager.Instance.Text("MainMenuItems", "NewGame");
        _newGame.GetControl.onClick.AddListener(delegate
        {
            LoadNewGame(Main.Instance.Scenes.Game.SceneAsset.name);
        });

        _continue.GetText.text = LangManager.Instance.Text("MainMenuItems", "Continue");
        _continue.SetInteractable(false);
        _options.GetText.text = LangManager.Instance.Text("MainMenuItems", "Options");
        _options.GetControl.onClick.AddListener(delegate
        {
            ShowOptions();
        });

        _quit.GetText.text = LangManager.Instance.Text("MainMenuItems", "Quit");
        _quit.GetControl.onClick.AddListener(delegate
        {
            Interface.QuitGame();
        });
    }

    #endregion

    public override void Hide()
    {
        if (!IsShow) return;
        _mainPanel.gameObject.SetActive(false);
        //Clear(_elementsOfInterface);
        IsShow = false;
    }

    public override void Show()
    {
        if (IsShow) return;
        _mainPanel.gameObject.SetActive(true);
        //var tempMenuItems = System.Enum.GetNames(typeof(MainMenuItems));
        //CreateMenu(tempMenuItems);
        IsShow = true;
    }

    private void ShowOptions()
    {
        Interface.Execute(InterfaceObject.OptionsMenu);
    }

    private void LoadNewGame(string lvl)
    {
        //SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
        Interface.LoadSceneAsync(lvl);
    }

    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //Main.Instance.InitGame();

        SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
    }
}