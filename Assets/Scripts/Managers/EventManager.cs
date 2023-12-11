using System;
using UnityEngine;


public static class EventManager
{
    
    

    #region InputSystem
    public static Func<Vector2> GetInput;
    public static Func<Vector2> GetInputDelta;
    public static Action InputStarted;
    public static Action InputEnded;
    public static Action<bool> CanMerge;
    public static Action<float> EarnMoney;
    public static Action UpdateButtons;

    public static Func<bool> IsTouching;
    public static Func<bool> IsPointerOverUI;
    #endregion

    public static Action<IncrementalButtonTypes,float> IdleButtonClicked;

    public static Action<Brick> BrickDestroyed;
    public static Func<PriceHolder> GetPriceHolder;
    public static Func<GameData> GetGameData;

 


}