using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAmount : MonoBehaviour {

    public UnityEngine.UI.Text display;
    public UnityEngine.UI.Text perClickDisplay;
    public UnityEngine.UI.Text perSecDisplay;

    public int fruitID;
    string type;

    void Start()
    {
        // Use lastLoadedScene if -1
        if (fruitID == -1) {
            for (int id = 0; id < SaveData.fruitName.Length; id++) {
                if (PlayerPrefs.GetString("lastLoadedScene") == SaveData.fruitName[id]) {
                    fruitID = id; break; }
            }
            if (fruitID == -1) { fruitID = 0; }
        }

        // Displayed fruit x apples
        type = " " + SaveData.fruitName[fruitID].ToLower() + "s";
    }

    void Update ()
    {
        // Display
        display.text = SaveData.FruitFix(SaveData.Fruits[fruitID, 0], 3) + type;
        perClickDisplay.text = SaveData.FruitFix(SaveData.Fruits[fruitID, 1], 3) + " per click";
        perSecDisplay.text = SaveData.FruitFix(SaveData.Fruits[fruitID, 2], 3) + " per second";
    }

}