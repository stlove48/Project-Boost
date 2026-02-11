using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelManager : MonoBehaviour
{
    float fuelMax = 100f;

    float remainingFuel = 100f;
    public float RemainingFuel { get { return remainingFuel; } }

    [Range(0, 10)]
    [SerializeField]private float fuelConsumptionRate;

    bool outOfFuel;

    public bool OutOfFuel { get { return outOfFuel; } }

    private void OnEnable()
    {
        EventManager.LevelLoaded += RefillFuelFull;
    }

    private void OnDisable()
    {
        EventManager.LevelLoaded -= RefillFuelFull;
    }

    /// <summary>
    /// While the throttle is held, reduce remainingFuel by the consumption rate multiplied by time between frames
    /// </summary>
    public void ConsumeFuel()
    {
        Mathf.Clamp(remainingFuel -= (fuelConsumptionRate * Time.deltaTime), 0f, fuelMax);
        EventManager.OnFuelAmountChanged(remainingFuel);

        if (remainingFuel <= 0) 
        {
            outOfFuel = true;
        }
    }

    /// <summary>
    /// Refill fuel completely on new level
    /// </summary>
    /// <param name="levelLoaded"></param>
    void RefillFuelFull(bool levelLoaded)
    {
        remainingFuel = fuelMax;
        EventManager.OnFuelAmountChanged(remainingFuel);
    }

    /// <summary>
    /// Add refillAmount, clamped between the current remaining fuel amount and the fuel maximum
    /// </summary>
    /// <param name="refillAmount"></param>
    void RefillFuel(float refillAmount)
    {
        remainingFuel = Mathf.Clamp(remainingFuel + refillAmount, remainingFuel, fuelMax);
        EventManager.OnFuelAmountChanged(remainingFuel);
    }

    /// <summary>
    /// Determines if there is still fuel left
    /// </summary>
    /// <returns>
    /// Returns either the amount of remaining fuel as a float or zero.
    /// </returns>
    public float DetermineRemainingFuel()
    {
        if (!outOfFuel)
        {
            return remainingFuel;
        }

        return 0f;
    }

}
