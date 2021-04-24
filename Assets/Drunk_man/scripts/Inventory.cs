using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance = null; // Экземпляр объекта

    [SerializeField]
    private AudioSource _audioSource;

    private List<string> _objectKeys;
    private string _selectedObjectKey;

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

        // Теперь нам нужно указать, чтобы объект не уничтожался
        // при переходе на другую сцену игры
        DontDestroyOnLoad(gameObject);

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
        _audioSource.Play();
    }

    public bool Has(string key)
    {
        return _objectKeys.Contains(key);
    }

    public string selectedObjectKey => _selectedObjectKey;
}
