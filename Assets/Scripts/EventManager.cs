using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<landingRating> ShipLanded;
    public static event UnityAction<int> ScoreChanged;

    public static void OnShipLanded(landingRating landingRating) => ShipLanded?.Invoke(landingRating);
    public static void OnScoreChanged(int changeAmount) => ScoreChanged?.Invoke(changeAmount);
}
