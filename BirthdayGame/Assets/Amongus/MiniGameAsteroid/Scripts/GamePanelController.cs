using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelController : MonoBehaviour
{
    public static GamePanelController objectInstance;// ссылка на собственный скрипт

    private int _asteroidsScore;
    public Text asteroidScoreText;

    private void Awake()
    {
        objectInstance = this;
    }
    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void AddAsteroidScore() // инкременкция очков уничтоженных астероидов
    {
        _asteroidsScore += 1;
        asteroidScoreText.text = _asteroidsScore.ToString();
    }

}
