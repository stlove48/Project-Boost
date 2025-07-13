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

    public void ConsumeFuel()
    {
        Mathf.Clamp(remainingFuel -= (fuelConsumptionRate * Time.deltaTime), 0f, fuelMax);
        EventManager.OnFuelAmountChanged(remainingFuel);

        if (remainingFuel <= 0) 
        {
            outOfFuel = true;
        }
    }

    void RefillFuelFull(bool levelLoaded)
    {
        remainingFuel = fuelMax;
        EventManager.OnFuelAmountChanged(remainingFuel);
    }

    void RefillFuel(float refillAmount)
    {

    }

    public float DetermineRemainingFuel()
    {
        if (!outOfFuel)
        {
            return remainingFuel;
        }

        return 0f;
    }

}
