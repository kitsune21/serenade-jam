using UnityEngine;
using System.Collections;

// track stats, data, and options for the character
public class CharacterStats : MonoBehaviour
{

    public float energy = 100f;
    private int maxEnergy = 100;
    public float energyModifier = 0f;
    private int maxEnergyModifier = 1;
    public bool isHavingFun = true;

    // Use this for initialization
    void Awake()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

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
