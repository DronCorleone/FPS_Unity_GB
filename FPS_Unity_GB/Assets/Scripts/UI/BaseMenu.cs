using Object = UnityEngine.Object;
using UnityEngine;
using System;


public abstract class BaseMenu : MonoBehaviour
{
    protected IControl[] _elementsOfInterface;

    protected bool IsShow { get; set; }
    protected Interface Interface;
    protected virtual void Awake()
    {
        Interface = FindObjectOfType<Interface>();
    }

    public abstract void Hide();
    public abstract void Show();

    protected void Clear(IControl[] controls)
    {
        foreach (var t in controls)
        {
            if (t == null) continue;
            Destroy(t.Instance);
        }
    }

    protected T CreateControl<T>(T prefab, string text) where T : Object, IControl
    {
        T tempControl = CreateControl(prefab);

        if (tempControl is IControlText tempControlText)
        {
            tempControlText.GetText.text = text;
        }
        return tempControl;
    }

    protected T CreateControl<T>(T prefab, Sprite sprite) where T : Object, IControl
    {
        T tempControl = CreateControl(prefab);

        if (tempControl is IControlImage tempControlImage)
        {
            tempControlImage.GetImage.sprite = sprite;
        }
        return tempControl;
    }

    private T CreateControl<T>(T prefab) where T : Object, IControl
    {
        if (!prefab) throw new Exception(string.Format("Отсутствует ссылка на {0}", typeof(T)));
        var root = Interface.InterfaceResources.MainPanel.transform;
        var tempControl = Instantiate(prefab, root.position, Quaternion.identity, root);
        return tempControl;
    }
}