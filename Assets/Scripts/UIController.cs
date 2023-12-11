using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public float moneyAmount;
    void SetCanvas()
    {
        UpdateMoney();
    }

    void UpdateMoney()
    {
        moneyAmount = EventManager.GetGameData().totalMoneyAmount;
        moneyText.text = ((int)moneyAmount).ToString();
    }

    private void OnEnable()
    {
        EventManager.EarnMoney += EarnMoney;
        EventManager.IdleButtonClicked += IdleButtonClicked;
    }

    private void EarnMoney(float obj)
    {
        EventManager.GetGameData().totalMoneyAmount += obj;
        UpdateMoney();
        EventManager.UpdateButtons();
    }

    private void OnDisable()
    {
        EventManager.EarnMoney -= EarnMoney;
        EventManager.IdleButtonClicked -= IdleButtonClicked;

    }

    private void IdleButtonClicked(IncrementalButtonTypes type, float amount)
    {
        UpdateMoney();
    }

    private void Start()
    {
        SetCanvas();
    }
}
