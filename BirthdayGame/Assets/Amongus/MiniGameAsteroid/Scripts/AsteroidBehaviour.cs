using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AsteroidBehaviour: MonoBehaviour, IPointerDownHandler
{
    

    public GameObject addingText; // объект отвечающий за всплывающий текст
    private AddingScoreText _addingScoreScript; // скрипт всплывающего текста

    public GameObject explosion; // экземпляр объект системы частиц взрыва
    private GameObject _tempExplosion; // временный объект взрыва отвечающий за появления множества взрывов на экране

    private int _movementX; // определение движения астероида по Х
    private int _movementY;// определение движения астероида по У
    private int _speed = 100; // скорость

    private int _currencyForDestruction; // сколько валюты дают за уничтожение астероида

    private int _asteroidType; // тип астероида

    private void Start()
    {

        _addingScoreScript = addingText.GetComponent<AddingScoreText>();
        switch (AsteroidController.objectInstance.asteroidSpawnSide) // определяем направление движения астероида исходя из того в какой части панели он появился
        {
            case 1:
                _movementX = 1;
                _movementY = Random.Range(-1, 2);
                break;
            case 2:
                _movementX = Random.Range(-1, 2);
                _movementY = -1;
                break;
            case 3:
                _movementX = -1;
                _movementY = Random.Range(-1, 2);
                break;
        }
        switch(gameObject.name) // определение типа астероида по его названию
        {
            case "BigAsteroid(Clone)":
                _asteroidType = 2;
                break;
            case "MediumAsteroid(Clone)":
                _asteroidType = 1;
                break;
            case "SmallAsteroid(Clone)":
                _asteroidType = 0;
                break;
        }
    }
    public void OnPointerDown(PointerEventData eventData) // при нажатии на астероид
    {
        _tempExplosion = Instantiate(explosion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -3), Quaternion.identity);// создание взрыва
        
        switch (_asteroidType) // определение сколько валюты дают за астероид
        {
            case 0:
                _currencyForDestruction = 10;
                break;
            case 1:
                _currencyForDestruction = 15;
                break;
            case 2:
                _currencyForDestruction = 25;
                break;
        }
        _addingScoreScript.ShowText("+" + _currencyForDestruction.ToString(), gameObject.transform.position); // передача валюты в функцию вывода на экран очков
        Destroy(gameObject);// уничтожение астероида и взрыва через 1 сек
        Destroy(_tempExplosion, 1f);
        PlayerScoreInfo.objectInstance.AddCurrency(_currencyForDestruction); // добавление валюты в панель очков
        GamePanelController.objectInstance.AddAsteroidScore(); // инкременция количества астероидов которые взорвал игрок
    }
    private void Update()// движение астероида
    {
        gameObject.transform.Translate(new Vector3(_movementX * _speed, _movementY * _speed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col) // на сцене присутствую стены которые отвечают за уничтожение астероидов если игрок их не взорвал
    {
        if(col.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
