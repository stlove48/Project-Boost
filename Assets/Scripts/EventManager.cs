using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    /// <summary>
    /// Event hook for when the ship has reached the landing pad.
    /// </summary>
    public static event UnityAction<landingRating> ShipLanded;
    /// <summary>
    /// Event hook for when the score has changed.
    /// </summary>
    public static event UnityAction<int> ScoreChanged;
    /// <summary>
    /// Event hook for when the amount of fuel has changed.
    /// </summary>
    public static event UnityAction<float> FuelAmountChanged;
    /// <summary>
    /// Event hook for when the level has been successfully completed.
    /// </summary>
    public static event UnityAction<int> LevelCompleted;
    /// <summary>
    /// Event hook for when the level has failed.
    /// </summary>
    public static event UnityAction<int> LevelFailed;
    /// <summary>
    /// Event hook for when a level has been successfully loaded.
    /// </summary>
    public static event UnityAction<bool> LevelLoaded;
    /// <summary>
    /// Event hook for when a fuel bonus has been earned for remaining fuel.
    /// </summary>
    public static event UnityAction<int> FuelBonusEarned;
    /// <summary>
    /// Event hook for a successful landing and the base score has been earned.
    /// </summary>
    public static event UnityAction<int> BaseScoreEarned;

    /// <summary>
    /// Invoke when the ship has reached the landing pad.
    /// </summary>
    /// <param name="landingRating">Takes in an enum based on the angle of the landing</param>
    public static void OnShipLanded(landingRating landingRating) => ShipLanded?.Invoke(landingRating);
    /// <summary>
    /// Invoke when the score has changed.
    /// </summary>
    /// <param name="changeAmount">Takes in an amount by which the score has changed as an integer.</param>
    public static void OnScoreChanged(int changeAmount) => ScoreChanged?.Invoke(changeAmount);
    /// <summary>
    /// Invoke when the amount of fuel has been changed.
    /// </summary>
    /// <param name="fuelAmount">Takes in the amount by which the fuel has been changed as a float.</param>
    public static void OnFuelAmountChanged(float fuelAmount) => FuelAmountChanged?.Invoke(fuelAmount);
    /// <summary>
    /// Invoke when the level has successfully been completed. Takes in the current scene index as an integer.
    /// </summary>
    /// <param name="sceneIndex">Takes in the index of the current scene as an integer.</param>
    public static void OnLevelCompleted(int sceneIndex) => LevelCompleted?.Invoke(sceneIndex);
    /// <summary>
    /// Invoke when the level is failed. Takes in the current scene index as an integer.
    /// </summary>
    /// <param name="sceneIndex"></param>
    public static void OnLevelFailed(int sceneIndex) => LevelFailed?.Invoke(sceneIndex);
    /// <summary>
    /// Invoke when the level is loaded. Takes in whether the scene was successfully loaded as a bool.
    /// </summary>
    /// <param name="levelLoaded"></param>
    public static void OnLevelLoaded(bool levelLoaded) => LevelLoaded?.Invoke(levelLoaded);
    /// <summary>
    /// Invoke when scoring and a fuel bonus was earned. Takes in the fuel bonus amount as an integer.
    /// </summary>
    /// <param name="fuelBonusEarned"></param>
    public static void OnFuelBonusEarned(int fuelBonusEarned) => FuelBonusEarned?.Invoke(fuelBonusEarned);
    /// <summary>
    /// Invoke when scoring. Takes in the base score earned as an integer.
    /// </summary>
    /// <param name="baseScoreEarned"></param>
    public static void OnBaseScoreEarned(int baseScoreEarned) => BaseScoreEarned?.Invoke(baseScoreEarned);
}
