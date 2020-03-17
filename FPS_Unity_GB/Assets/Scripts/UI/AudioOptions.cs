using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

namespace Geekbrains
{
    public class AudioOptions : BaseMenu
    {
        enum AudioMenuItems
        {
            Master,
            Music,
            Effects,
            Back
        }

        private AudioMixer _audioMixer;
        private AudioSettings _audioSettings;

        [SerializeField] private GameObject _audioPanel;

        [SerializeField] private SliderUI _masterVolume;
        [SerializeField] private SliderUI _musicVolume;
        [SerializeField] private SliderUI _effectsVolume;
        [SerializeField] private ButtonUI _back;


        private void Start()
        {
            //_audioPanel.SetActive(true);

            _audioMixer = Resources.Load<AudioMixer>("MainAudioMixer");

            //_masterVolume.GetText.text = LangManager.Instance.Text("AudioMenuItems", "Master");
            //_masterVolume.GetControl.minValue = -80;
            //_masterVolume.GetControl.maxValue = 20;
            //_masterVolume.GetControl.onValueChanged.AddListener(MasterVolume);

            //_musicVolume.GetText.text = LangManager.Instance.Text("AudioMenuItems", "Music");
            //_musicVolume.GetControl.minValue = -80;
            //_musicVolume.GetControl.maxValue = 20;
            //_musicVolume.GetControl.onValueChanged.AddListener(MusicVolume);

            //_effectsVolume.GetText.text = LangManager.Instance.Text("AudioMenuItems", "Effects");
            //_effectsVolume.GetControl.minValue = -80;
            //_effectsVolume.GetControl.maxValue = 20;
            //_effectsVolume.GetControl.onValueChanged.AddListener(EffectsVolume);

            //_back.GetText.text = LangManager.Instance.Text("AudioMenuItems", "Back");
            //_back.GetControl.onClick.AddListener(delegate
            //{
            //    Back();
            //});

            _audioPanel.SetActive(false);
        }

        private void MasterVolume(float value)
        {
            _audioMixer.SetFloat("Master", value);
        }

        private void MusicVolume(float value)
        {
            _audioMixer.SetFloat("Music", value);
        }

        private void EffectsVolume(float value)
        {
            _audioMixer.SetFloat("GameEffects", value);
        }

        private void Back()
        {
            Save();
            Interface.Execute(InterfaceObject.OptionsMenu);
        }

        public override void Hide()
        {
            if (!IsShow) return;
            _audioPanel.SetActive(false);
            IsShow = false;
        }

        public override void Show()
        {
            if (IsShow) return;
            _audioPanel.SetActive(true);
            IsShow = true;

            _masterVolume.GetText.text = LangManager.Instance.Text("AudioMenuItems", "Master");
            _masterVolume.GetControl.minValue = -80;
            _masterVolume.GetControl.maxValue = 20;
            _masterVolume.GetControl.onValueChanged.AddListener(MasterVolume);

            _musicVolume.GetText.text = LangManager.Instance.Text("AudioMenuItems", "Music");
            _musicVolume.GetControl.minValue = -80;
            _musicVolume.GetControl.maxValue = 20;
            _musicVolume.GetControl.onValueChanged.AddListener(MusicVolume);

            _effectsVolume.GetText.text = LangManager.Instance.Text("AudioMenuItems", "Effects");
            _effectsVolume.GetControl.minValue = -80;
            _effectsVolume.GetControl.maxValue = 20;
            _effectsVolume.GetControl.onValueChanged.AddListener(EffectsVolume);

            _back.GetText.text = LangManager.Instance.Text("AudioMenuItems", "Back");
            _back.GetControl.onClick.AddListener(delegate
            {
                Back();
            });
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
    }
}