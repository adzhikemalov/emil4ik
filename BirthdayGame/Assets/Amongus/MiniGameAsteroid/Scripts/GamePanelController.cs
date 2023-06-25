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
    private void OnEnable() // когда панель становится активной
    {
        StartCoroutine(CheckForClose()); // сопрограмма отвечающая за закрытие данной панели
    }
    private IEnumerator CheckForClose()
    {
        yield return new WaitForSeconds(30f);
        ClosePanel();
    }
    private void ClosePanel()
    {
        PlayerScoreInfo.objectInstance.AddExperience();
        gameObject.SetActive(false);
    }

    public void AddAsteroidScore() // инкременкция очков уничтоженных астероидов
    {
        _asteroidsScore += 1;
        asteroidScoreText.text = _asteroidsScore.ToString();
    }

}
