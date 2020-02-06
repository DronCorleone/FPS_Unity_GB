using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;


namespace Geekbrains.Editor
{
    public class NewWindow : EditorWindow
    {
        public GameObject MedKit;

        private string _objName;
        private int _countOfObjects = 10;
        private float _minOfRange = -45;
        private float _maxOfRange = 45;
        private bool _groupEnabled;

        [MenuItem("Dron Corleone/Medkit Spawn Window")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(NewWindow));
        }

        public void OnGUI()
        {
            GUILayout.Label("Общие настройки", EditorStyles.boldLabel);
            MedKit = EditorGUILayout.ObjectField("Объект аптечки", MedKit, typeof(GameObject), true) as GameObject;
            _objName = EditorGUILayout.TextField("Имя объекта", _objName);
            _groupEnabled = EditorGUILayout.BeginToggleGroup("Дополнительные настройки", _groupEnabled);
            _countOfObjects = EditorGUILayout.IntSlider("Количество объектов", _countOfObjects, 1, 100);
            EditorGUILayout.EndToggleGroup();

            if (GUILayout.Button("Создать объекты"))
            {
                if (MedKit)
                {
                    GameObject root = new GameObject("Аптечки");

                    for (int i = 0; i < _countOfObjects; i++)
                    {
                        Vector3 position = new Vector3(Random.Range(_minOfRange, _maxOfRange), 0, Random.Range(_minOfRange, _maxOfRange));
                        GameObject tempObj = Instantiate(MedKit, position, Quaternion.identity) as GameObject;
                        tempObj.name = $"{_objName} ({i})";
                        tempObj.transform.parent = root.transform;
                    }
                }
            }
        }
    }
}