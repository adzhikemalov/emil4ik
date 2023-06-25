using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractableObject : MonoBehaviour
{
    public enum InteractableType
    {
        MiniGame,
        Dialog,
        DeadBody
    }
    
    public enum MiniGameId
    {
        None,
        Asteroids,
        SimonSays,
        Electricity,
        CardMoving,
        EnterNumbers
    }

    public enum DialogId
    {
        None,
        Emine,
        EmilTot,
        EmilMav,
        Ismet,
        Nataly,
        Cat
    }

    public InteractableType ObjectType;
    public MiniGameId MiniGame;
    public DialogId Dialog;
}
