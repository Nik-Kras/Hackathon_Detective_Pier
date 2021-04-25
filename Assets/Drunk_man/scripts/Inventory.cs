using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance = null; // Экземпляр объекта

    [SerializeField] private StringEvent _onPickup;

    private List<string> _objectKeys;
    private string _selectedObjectKey;

    [SerializeField] private GameObject _evidenceInstanceUI;
    [SerializeField] private VerticalLayoutGroup _evidenceLayoutGroup;

    // Метод, выполняемый при старте игры
    void Start ()
    {
        _objectKeys = new List<string>();
        
        // Теперь, проверяем существование экземпляра
        if (instance == null) { // Экземпляр менеджера был найден
            instance = this; // Задаем ссылку на экземпляр объекта
        } else if(instance == this){ // Экземпляр объекта уже существует на сцене
            Destroy(gameObject); // Удаляем объект
        }

        // И запускаем собственно инициализатор
        InitializeManager();
    }

    // Метод инициализации менеджера
    private void InitializeManager(){
        /* TODO: Здесь мы будем проводить инициализацию */
    }

    public void OnPickup(string key)
    {
        _objectKeys.Add(key);
        AddEvedence(key);
        
        _onPickup.Invoke(key);
    }

    public bool Has(string key)
    {
        return _objectKeys.Contains(key);
    }
    
    GameObject AddEvedence(string evedenceText)
    {
        GameObject Instance = Instantiate(_evidenceInstanceUI, Vector3.zero, Quaternion.identity);
        Instance.GetComponentInChildren<Text>().text = evedenceText;
        Instance.transform.SetParent(_evidenceLayoutGroup.transform);
        Instance.transform.SetAsFirstSibling();
		
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_evidenceLayoutGroup.transform);

        return Instance;
    }
    
    public string selectedObjectKey => _selectedObjectKey;
}
