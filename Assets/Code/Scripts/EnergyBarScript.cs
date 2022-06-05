using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarScript : MonoBehaviour
{
    public Slider EnergyBar;
    public CharacterStats Energy;
    public MenuController menu;
    public TimeController timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currEnergy =  Energy.GetEnergy();
        EnergyBar.value = currEnergy / 100f;

        if (currEnergy == 0) {
            menu.gameOver();
        }
    }
}
