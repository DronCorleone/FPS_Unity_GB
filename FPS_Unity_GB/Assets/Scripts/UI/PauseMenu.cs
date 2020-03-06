using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : BaseMenu
{
    enum PauseMenuItems
    {
        Resume,
        Save,
        Options,
        MainMenu,
        Quit
    }

    [SerializeField] private GameObject _pauseMenu;

    [SerializeField] private ButtonUI _resume;
    [SerializeField] private ButtonUI _save;
    [SerializeField] private ButtonUI _options;
    [SerializeField] private ButtonUI _mainMenu;
    [SerializeField] private ButtonUI _quit;


    private void Start()
    {
        _resume.GetText.text = LangManager.Instance.Text("MenuPause", "Resume");
        _resume.SetInteractable(false);

        _save.GetText.text = LangManager.Instance.Text("MenuPause", "Save");
        _save.SetInteractable(false);

        _options.GetText.text = LangManager.Instance.Text("MenuPause", "Options");
        _options.SetInteractable(false);

        _mainMenu.GetText.text = LangManager.Instance.Text("MenuPause", "MainMenu");
        _mainMenu.GetControl.onClick.AddListener(delegate
        {
            GoToMainMenu();
        });

        _quit.GetText.text = LangManager.Instance.Text("MenuPause", "Quit");
        _quit.GetControl.onClick.AddListener(delegate
        {
            Interface.QuitGame();
        });
    }

    private void GoToMainMenu()
    {
        Interface.LoadSceneAsync(Main.Instance.Scenes.MainMenu.SceneAsset.name);
    }

    public override void Hide()
    {
        if (!IsShow) return;
        _pauseMenu.gameObject.SetActive(false);
        IsShow = false;
    }

    public override void Show()
    {
        if (IsShow) return;
        _pauseMenu.gameObject.SetActive(true);
        IsShow = true;
    }
}
