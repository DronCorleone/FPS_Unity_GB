using Geekbrains;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject _gamePanel;

    [SerializeField] private ButtonUI _resume;
    [SerializeField] private ButtonUI _save;
    [SerializeField] private ButtonUI _options;
    [SerializeField] private ButtonUI _mainMenu;
    [SerializeField] private ButtonUI _quit;

    private CharacterController _charController;
    private CharacterController CharController
    {
        get
        {
            return _charController ?? (_charController = FindObjectOfType<CharacterController>());
        }
    }

    //private AudioMixerSnapshot _paused;
    //private AudioMixerSnapshot Paused
    //{
    //    get
    //    {
    //        return _paused ?? (_paused = Resources.Load<AudioMixer>("MainAudioMixer").FindSnapshot("Paused"));
    //    }
    //}

    //private AudioMixerSnapshot _unPaused;
    //private AudioMixerSnapshot UnPaused
    //{
    //    get
    //    {
    //        return _unPaused ?? (_unPaused = Resources.Load<AudioMixer>("MainAudioMixer").FindSnapshot("UnPaused"));
    //    }
    //}


    private void Start()
    {
        _resume.GetText.text = LangManager.Instance.Text("MenuPause", "Resume");
        _resume.GetControl.onClick.AddListener(delegate
        {
            Hide();
        });

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

    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            if (IsShow)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    private void GoToMainMenu()
    {
        ServiceLocator.ClearDictionary();
        SceneManager.LoadScene(0);
    }

    public override void Hide()
    {
        if (!IsShow) return;

        //UnPaused.TransitionTo(0.001f);

        _pauseMenu.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(true);

        Time.timeScale = 1;

        CharController.enabled = true;
        ServiceLocator.Resolve<PlayerController>().On();
        ServiceLocator.Resolve<InputController>().On();
        ServiceLocator.Resolve<SelectionController>().On();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        IsShow = false;
    }

    public override void Show()
    {
        if (IsShow) return;

        //Paused.TransitionTo(0.001f);

        _pauseMenu.gameObject.SetActive(true);
        _gamePanel.gameObject.SetActive(false);

        Time.timeScale = 0;

        CharController.enabled = false;
        ServiceLocator.Resolve<PlayerController>().Off();
        ServiceLocator.Resolve<InputController>().Off();
        ServiceLocator.Resolve<SelectionController>().Off();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        IsShow = true;
    }
}
