using UnityEngine;
using System.Collections.Generic;

public class SpacecraftSystem : MonoBehaviour
{
    [System.Serializable]
    public class Spacecraft
    {
        public string name;
        public int maxHealth = 100;
        public int currentHealth;
        public float maxFuel = 100f;
        public float currentFuel;
        public float maxOxygen = 100f;
        public float currentOxygen;
        public float enginePower = 1f;
        public float shieldStrength = 1f;
        public List<string> equipment = new List<string>();
        public int level = 1;
    }

    [SerializeField] private Spacecraft currentSpacecraft;
    private List<Spacecraft> unlockedSpacecrafts = new List<Spacecraft>();

    private void Start()
    {
        InitializeSpacecrafts();
    }

    private void InitializeSpacecrafts()
    {
        // Explorer-1 (Starting Spacecraft)
        unlockedSpacecrafts.Add(new Spacecraft
        {
            name = "Explorer-1",
            maxHealth = 100,
            currentHealth = 100,
            maxFuel = 100f,
            currentFuel = 100f,
            maxOxygen = 100f,
            currentOxygen = 100f,
            enginePower = 1f,
            shieldStrength = 1f,
            equipment = new List<string> { "BasicCommunications", "SensorArray", "LifeSupport" }
        });

        currentSpacecraft = unlockedSpacecrafts[0];
    }

    public void ConsumeFuel(float amount)
    {
        currentSpacecraft.currentFuel = Mathf.Max(0, currentSpacecraft.currentFuel - amount);
    }

    public void ConsumOxygen(float amount)
    {
        currentSpacecraft.currentOxygen = Mathf.Max(0, currentSpacecraft.currentOxygen - amount);
    }

    public void TakeDamage(int damageAmount)
    {
        currentSpacecraft.currentHealth = Mathf.Max(0, currentSpacecraft.currentHealth - damageAmount);
        if (currentSpacecraft.currentHealth <= 0)
        {
            Debug.Log("Spacecraft destroyed!");
        }
    }

    public void Repair(int repairAmount)
    {
        currentSpacecraft.currentHealth = Mathf.Min(currentSpacecraft.maxHealth, currentSpacecraft.currentHealth + repairAmount);
    }

    public void Refuel(float fuelAmount)
    {
        currentSpacecraft.currentFuel = Mathf.Min(currentSpacecraft.maxFuel, currentSpacecraft.currentFuel + fuelAmount);
    }

    public void RefillOxygen(float oxygenAmount)
    {
        currentSpacecraft.currentOxygen = Mathf.Min(currentSpacecraft.maxOxygen, currentSpacecraft.currentOxygen + oxygenAmount);
    }

    public void UpgradeEngines()
    {
        currentSpacecraft.enginePower *= 1.2f;
        Debug.Log($"Engines upgraded! Power: {currentSpacecraft.enginePower}");
    }

    public void UpgradeShields()
    {
        currentSpacecraft.shieldStrength *= 1.2f;
        Debug.Log($"Shields upgraded! Strength: {currentSpacecraft.shieldStrength}");
    }

    public Spacecraft GetCurrentSpacecraft() => currentSpacecraft;
    public List<Spacecraft> GetUnlockedSpacecrafts() => unlockedSpacecrafts;
}
