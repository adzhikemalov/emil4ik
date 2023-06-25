using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddingScoreText : MonoBehaviour
{
    private GameObject _tempAddingScore;
    private Text _scoreText;
    private Transform _parent;
  
    
    public void ShowText(string text, Vector3 position) // вывод текста на экран
    {
        _parent = transform.Find("/Canvas/GamePanelCanvas/GamePanel").GetComponent<Transform>(); // находим объект GamePanel
        _tempAddingScore = Instantiate(gameObject, position, Quaternion.identity, _parent); // создаем текст
        _tempAddingScore.transform.GetChild(0).GetComponent<Text>().text = text; // присваиваем ему нужный текст
        Destroy(_tempAddingScore, 1.5f); // уничтожение объекта через 1.5 сек
    }
}
