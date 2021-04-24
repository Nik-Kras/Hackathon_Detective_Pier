using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance = null; // Экземпляр объекта
    public List<string> objectKeys;

    // Метод, выполняемый при старте игры
    void Start () {
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
}
