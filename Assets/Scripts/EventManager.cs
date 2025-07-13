using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<landingRating> ShipLanded;
    public static event UnityAction<int> ScoreChanged;
    public static event UnityAction<float> FuelAmountChanged;
    public static event UnityAction<int> LevelCompleted;
    public static event UnityAction<int> LevelFailed;
    public static event UnityAction<bool> LevelLoaded;
    public static event UnityAction<int> FuelBonusEarned;
    public static event UnityAction<int> BaseScoreEarned;

    public static void OnShipLanded(landingRating landingRating) => ShipLanded?.Invoke(landingRating);
    public static void OnScoreChanged(int changeAmount) => ScoreChanged?.Invoke(changeAmount);
    public static void OnFuelAmountChanged(float fuelAmount) => FuelAmountChanged?.Invoke(fuelAmount);
    public static void OnLevelCompleted(int sceneIndex) => LevelCompleted?.Invoke(sceneIndex);
    public static void OnLevelFailed(int sceneIndex) => LevelFailed?.Invoke(sceneIndex);
    public static void OnLevelLoaded(bool levelLoaded) => LevelLoaded?.Invoke(levelLoaded);
    public static void OnFuelBonusEarned(int fuelBonusEarned) => FuelBonusEarned?.Invoke(fuelBonusEarned);
    public static void OnBaseScoreEarned(int baseScoreEarned) => BaseScoreEarned?.Invoke(baseScoreEarned);
}
