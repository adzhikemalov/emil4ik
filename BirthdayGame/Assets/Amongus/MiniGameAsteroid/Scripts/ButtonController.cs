using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerDownHandler
{

    public Sprite activeButton;
    public Sprite inactiveButton;

    private bool _isButtonActive = true;// активна ли кнопка в данный момент
    private bool _isDelayActive = false;// нужна ли задержка в данный момент

    public GameObject gamePanel; // объект игровой панели
    
    public void OnPointerDown(PointerEventData eventData)// при нажатии на кнопку начать мини игру панель мини игры становится активной
    {
        if(_isButtonActive)
        {
            GetComponent<Image>().sprite = inactiveButton;
            transform.GetChild(0).GetComponent<Text>().color = Color.black;
            _isButtonActive = false;
            _isDelayActive = true;
            gamePanel.SetActive(true);
            
        }
    }

    private void Update() 
    {
        if(_isDelayActive)// если задержка начата
        {
            if (!gamePanel.activeSelf && !_isButtonActive) // провека отключиласб ли игровая панель
            {
                StartCoroutine(WaitForActive());// запуска сопрограммы
                _isDelayActive = false;
            }
        }
        
        
    }
    private IEnumerator WaitForActive()
    {
        yield return new WaitForSeconds(20f);
        MakeActive();
    }
    private void MakeActive()
    {
        GetComponent<Image>().sprite = activeButton;
        transform.GetChild(0).GetComponent<Text>().color = Color.yellow;
        _isButtonActive = true;
    }
}
