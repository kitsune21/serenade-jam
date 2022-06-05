using UnityEngine;
using System.Collections;

// track stats, data, and options for the character
public class CharacterStats : MonoBehaviour
{

    public float energy = 100f;
    private int maxEnergy = 100;
    public float energyModifier = 0f;
    private int maxEnergyModifier = 1;
    public bool isHavingFun = false;
    private bool isUsingDesk = false;
    private float changePerSecond;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float oldChange = changePerSecond;
        changePerSecond = 0.0001f * Mathf.Pow(energy, 2);
        if (!isHavingFun)
        {
            changePerSecond *= -1;
        }
        energy = Mathf.Clamp(energy + changePerSecond * Time.deltaTime, 0, maxEnergy);
    }

    public void SetEnergy(float amount)
    {
        energy += amount;
        NormalizeEnergy();
    }

    public void SetEnergyModifier(float amount)
    {
        energyModifier = amount;
        NormalizeEnergy();
    }

    public void SetHavingFun(bool newFun)
    {
        isHavingFun = newFun;
        if (isHavingFun)
        {
            energyModifier = 1;
        } else
        {
            energyModifier = 0;
        }
    }

    public bool GetHavingFun()
    {
        return isHavingFun;
    }

    public void SetUsingDesk(bool newDesk)
    {
        isUsingDesk = newDesk;
    }

    public bool GetUsingDesk()
    {
        return isUsingDesk;
    }

    public float GetEnergy()
    {
        return energy;
    }

    public float GetEnergyModifier()
    {
        return energyModifier;
    }

    // make sure health and magic don't go above max or below zero
    void NormalizeEnergy()
    {
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }
        if (energy < 0)
        {
            energy = 0;
        }
        if (energyModifier > maxEnergyModifier)
        {
            energyModifier = maxEnergyModifier;
        }
        if (energyModifier < maxEnergyModifier * -1)
        {
            energyModifier = maxEnergyModifier * -1;
        }
    }
}
