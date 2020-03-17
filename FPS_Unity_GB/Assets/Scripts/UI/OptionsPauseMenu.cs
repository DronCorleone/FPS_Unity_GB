using UnityEngine;
using UnityEngine.Audio;


namespace Geekbrains
{
    public class OptionsPauseMenu : BaseMenu
    {
        enum OptionsPauseMenuItems
        {
            Volume,
            Back
        }

        private AudioMixer _audioMixer;
        private AudioSettings _audioSettings;

        [SerializeField] private GameObject _optionsPausePanel;

        [SerializeField] private SliderUI _volume;
        [SerializeField] private ButtonUI _back;

        private void Start()
        {
            _optionsPausePanel.SetActive(true);

            _audioMixer = Resources.Load<AudioMixer>("MainAudioMixer");

            _volume.GetText.text = LangManager.Instance.Text("OptionsPauseMenu", "Volume");
            _volume.GetControl.minValue = -80;
            _volume.GetControl.maxValue = 20;
            _volume.GetControl.onValueChanged.AddListener(MasterVolume);

            _back.GetText.text = LangManager.Instance.Text("OptionsPauseMenu", "Back");
            _back.GetControl.onClick.AddListener(delegate
            {
                Back();
            });

            _optionsPausePanel.SetActive(false);

        }

        private void MasterVolume(float value)
        {
            _audioMixer.SetFloat("Master", value);
        }

        private void Save()
        {
            float master, effects, music;
            _audioMixer.GetFloat("Master", out master);
            _audioMixer.GetFloat("GameEffects", out effects);
            _audioMixer.GetFloat("Music", out music);

            _audioSettings = new AudioSettings
            {
                Master = master,
                Effects = effects,
                Music = music
            };
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
}
