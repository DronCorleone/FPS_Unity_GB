using UnityEngine;


/// <summary>
/// Отвечает за создания управляющих классов на сцене
/// </summary>
public class CreateInterface : MonoBehaviour
{
    #region Editor
    /// <summary>
    /// Создание главного меню
    /// </summary>
    public void CreateMainMenu()
    {
        CreateComponent();
        Clear();
    }
    /// <summary>
    /// Создание игрового UI
    /// </summary>
    public void CreateGameMenu()
    {
        CreateComponent();
        Clear();
    }

    private void Clear()
    {
        DestroyImmediate(this);
    }

    private void CreateComponent()
    {
    }
    #endregion
}