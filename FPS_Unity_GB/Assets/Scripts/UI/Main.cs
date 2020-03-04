using Helper;

public class Main : Singleton<Main>
{
    [System.Serializable]
    public struct SceneDate
    {
        public SceneField MainMenu;
        public SceneField Game;
    }

    public SceneDate Scenes;

    protected Main() { }


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

