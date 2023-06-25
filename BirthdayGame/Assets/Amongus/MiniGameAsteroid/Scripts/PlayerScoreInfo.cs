using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreInfo : MonoBehaviour
{
    public static PlayerScoreInfo objectInstance; // ссылка на собственный скрипт

    

    private int _experience = 0; // опыт и ниже валюта
    private int _currency = 0;

    public Text experienceText;
    public Text currencyText;
    
    private void Awake()
    {
        objectInstance = this;
    }
    public void AddCurrency(int currency)
    {
        _currency += currency;
        currencyText.text = _currency.ToString();


    }
    public void AddExperience()
    {
        _experience += 10;
        experienceText.text = _experience.ToString();
    }
}
