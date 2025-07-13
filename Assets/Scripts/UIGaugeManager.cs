using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGaugeManager : MonoBehaviour
{
    [SerializeField] Slider fuelGauge;

    private void OnEnable()
    {
        EventManager.FuelAmountChanged += UpdateFuelGauge;
    }

    private void OnDisable()
    {
        EventManager.FuelAmountChanged -= UpdateFuelGauge;
    }

    void UpdateFuelGauge(float fuelAmount)
    {
        fuelGauge.value = fuelAmount;
    }
}
