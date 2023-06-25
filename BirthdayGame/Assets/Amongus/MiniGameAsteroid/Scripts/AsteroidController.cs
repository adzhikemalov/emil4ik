using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidController : MonoBehaviour
{
    public static AsteroidController objectInstance;// ссылка на собственный скрипт

    public GameObject[] asteroids; // все доступные астероиды
    public List<GameObject> allAsteroids = new List<GameObject>(); // активные астероиды

    private GameObject _asteroid; // шаблон астероида
    private GameObject _tempAsteroid; // временный объект астероида отвечающий за появления множества астероидов на экране

    private Vector2 _asteroidPosition; // позиция астероида
    public Transform asteroidParent; // его родитель для Instantiate

    public int asteroidSpawnSide { get; private set; } // свойство для определения в какой части панели появился астероид
    

    private void Awake()
    {
        objectInstance = this;
    }
    private void Update()
    {
        SpawnAsteroidIfRequired();
    }
    private void OnDisable() // при отключении игровой панели удаляются все активные астероиды
    {
        foreach (GameObject asteroid in allAsteroids.ToList())
        {
            allAsteroids.Remove(asteroid);
            Destroy(asteroid);
        }
    }
    private void SpawnAsteroidIfRequired()
    {
      
        if (CheckForAsteroid() == 5) // рандом для генерации астероидов
        {
            switch (asteroidSpawnSide = Random.Range(1, 4))
            {
                case 1:
                    _asteroidPosition = new Vector2(-250f, Random.Range(-250f, 190f));
                    break;
                case 2:
                    _asteroidPosition = new Vector2(Random.Range(-190, 250f), 250f);
                    break;
                case 3:
                    _asteroidPosition = new Vector2(250f, Random.Range(-190f, 190f));
                    break;
            }

            int _tempInt = Random.Range(0, 11); // рандом для появления разных астероидов с разной вероятностью
            if (_tempInt <= 4)
            {
                _asteroid = asteroids[0];
            }
            else if (_tempInt <= 7 && _tempInt > 4)
            {
                _asteroid = asteroids[1];
            }
            else if (_tempInt > 7)
            {
                _asteroid = asteroids[2];
            }
            _tempAsteroid = Instantiate(_asteroid, _asteroidPosition, Quaternion.identity, asteroidParent);
            allAsteroids.Add(_tempAsteroid);
        }
    }
    private int CheckForAsteroid()
    {
        return Random.Range(0, 100);
    }
}
