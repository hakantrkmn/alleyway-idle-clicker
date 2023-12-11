using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IncrementalIdleButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public Button button;
    public IncrementalButtonTypes buttonType;
    public TextMeshProUGUI moneyText;
    private List<PriceAnodValue> priceList;

    [ShowIf("buttonType",IncrementalButtonTypes.Merge)]
    public bool canMerge;
    
    private void OnEnable()
    {
        EventManager.UpdateButtons += ControlButtonPrice;
        EventManager.CanMerge += CanMerge;
        EventManager.IdleButtonClicked += UpdateButton;
    }

    private void CanMerge(bool obj)
    {
        if (buttonType==IncrementalButtonTypes.Merge)
        {
            canMerge = obj;
            ControlButtonPrice();
        }
    }

    private void UpdateButton(IncrementalButtonTypes arg1, float arg2)
    {
        ControlButtonPrice();
    }

    private void OnDisable()
    {
        EventManager.UpdateButtons -= ControlButtonPrice;
        EventManager.CanMerge -= CanMerge;
        EventManager.IdleButtonClicked -= UpdateButton;
    }

    public void ControlButtonPrice()
    {
        foreach (var price in priceList)
        {
            if (!price.isReached)
            {
                if (EventManager.GetGameData().totalMoneyAmount>=price.requiredMoneyValue)
                {
                    ActivateButton(true);
                    moneyText.text = price.requiredMoneyValue.ToString();
                    return;
                }
                else
                {
                    ActivateButton(false);
                    moneyText.text = price.requiredMoneyValue.ToString();
                    return;
                }
                
            }
        }
        
        UpgradeIsFull();
        
        
    }

    void UpgradeIsFull()
    {
        ActivateButton(false);
        moneyText.text = "Full";
    }
    void ActivateButton(bool canActivate)
    {
        if (buttonType == IncrementalButtonTypes.Merge)
        {
            if (canMerge)
            {
                button.interactable = canActivate;

            }
            else
            {
                button.interactable = false;

            }
        }
        else
        {
            button.interactable = canActivate;

        }
    }

    [Button]
    public void IdleButtonClicked()
    {
        foreach (var price in priceList)
        {
            if (!price.isReached)
            {
                price.isReached = true;
                EventManager.GetGameData().totalMoneyAmount -= price.requiredMoneyValue;
                EventManager.IdleButtonClicked(buttonType, price.upgradeAmount);
                break;

            }
        }
    }

    private void Start()
    {
        foreach (var prices in EventManager.GetPriceHolder().priceValues)
        {
            if (prices.type==buttonType)
            {
                priceList = prices.priceAndValueList;
            }
        }

        buttonText.text = buttonType.ToString();
        
        ControlButtonPrice();
    }
}
