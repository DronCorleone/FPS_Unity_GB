using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VideoOptions : BaseMenu
{
    enum VideoOptionsItems
    {
        GraphicsQuality,
        SoftParticles,
        ShadowQuality,
        SaveAndReturn,
        Back
    }

    [SerializeField] private GameObject _optionsPanel;

    [SerializeField] private SliderUI _graphicsQuality;
    [SerializeField] private ToggleUI _softParticles;
    [SerializeField] private SliderUI _shadowQuality; 
    [SerializeField] private ButtonUI _saveAndReturn;
    [SerializeField] private ButtonUI _back;


    private void Start()
    {
        
    }

    private void SetGraphics()
    {

    }

    private void SetSoftParticles()
    {

    }

    public override void Hide()
    {
        if (!IsShow) return;
        _optionsPanel.gameObject.SetActive(false);
        IsShow = false;
    }
    public override void Show()
    {
        if (IsShow) return;
        _optionsPanel.gameObject.SetActive(true);
        IsShow = true;
    }
}
