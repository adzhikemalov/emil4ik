using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    private GameObject _currentMiniGame;
    private GameObject _currentGameContent;
    public GameObject Joystick;
    public GameObject MiniGameBackPrefab;
    private Button _closeButton;

    public GameObject MiniGameAsteroids;

    public void ShowMiniGame(InteractableObject.MiniGameId currentObjectMiniGame)
    {
        if (_currentMiniGame)
            Destroy(_currentMiniGame);
        
        Joystick.SetActive(false);
        _currentMiniGame = GameObject.Instantiate(MiniGameBackPrefab, this.transform);
        SetupButton();
        CreateGame(currentObjectMiniGame);
    }

    private void CreateGame(InteractableObject.MiniGameId currentObjectMiniGame)
    {
        switch (currentObjectMiniGame)
        {
            case InteractableObject.MiniGameId.Asteroids:
                _currentGameContent = Instantiate(MiniGameAsteroids, _currentMiniGame.transform);
                break;
        }
    }


    private void SetupButton()
    {
        _closeButton = _currentMiniGame.gameObject.GetComponentInChildren<Button>();
        if (_closeButton)
            _closeButton.onClick.AddListener(RemoveMiniGame);
    }

    public void RemoveMiniGame()
    {
        if (_currentMiniGame)
            Destroy(_currentMiniGame);
        if (_closeButton)
            _closeButton.onClick.RemoveAllListeners();
        Joystick.SetActive(true);
    }
}