using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PriceHolder priceHolder;
    public GameData gameData;

    private void OnEnable()
    {
        EventManager.GetPriceHolder += () => priceHolder;
        EventManager.GetGameData += () => gameData;

    }
}
