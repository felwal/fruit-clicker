using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour {

    public UnityEngine.UI.Text costDisplay;
    public UnityEngine.UI.Text infoDisplay;
    public UnityEngine.Transform buyButton;

    bool infoShow = false;
    float costMultiplier = 1.3f;

    public int itemID;
    int fruitID;
    string itemDesc;
    string[,] itemName =
    { // [ fruitID, itemID ]
        { "PowerClick", "AutoClick", "Grandma", "Monster", "one small boi" }, // apple
        { "PowerClick", "AutoClick", "Grandpa", "Lunitic", "one small girl" }, // pear
        { "MasterClick", "AutoClick", "Old lady", "Chieftan", "one small monkeydude" }, // banana
        { "PowerCrack", "AutoMat", "Guy with spear", "Sawing Machine", "one small milk-sipper" } // coconut
    };

    // choose fruitID, itemDesc, displays
    void Start()
    {
        // fruitID based on lastLoadedScene
        for(int id = 0; id < SaveData.fruitName.Length; id++) {
            if (PlayerPrefs.GetString("lastLoadedScene") == SaveData.fruitName[id]) { fruitID = id; break; } }

        // itemDesc
        if (itemID == 0) { itemDesc = "x" + SaveData.Shop[fruitID, itemID, 2] + "/click"; }
        else { itemDesc = "+" + SaveData.FruitFix(SaveData.Shop[fruitID, itemID, 2], 0) + "/s"; }

        // Displays
        infoDisplay.text = itemName[fruitID, itemID];
        costDisplay.text = SaveData.FruitFix(SaveData.Shop[fruitID, itemID, 0], 3);
    }

    void Update()
    {
        if (SaveData.Fruits[fruitID, 0] >= SaveData.Shop[fruitID, itemID, 0])
            buyButton.GetComponent<Button>().interactable = true;
        else
            buyButton.GetComponent<Button>().interactable = false;
    }

    public void Info()
    {
        if (infoShow == false) {
            infoDisplay.text = itemDesc + ", level: " + SaveData.Shop[fruitID, itemID, 1];
        }
        else {
            infoDisplay.text = itemName[fruitID, itemID];
        }
        infoShow = !infoShow;
    }

    public void Purchase()
    {
        if (SaveData.Fruits[fruitID, 0] >= SaveData.Shop[fruitID, itemID, 0]) {
            // Pay
            SaveData.Fruits[fruitID, 0] -= SaveData.Shop[fruitID, itemID, 0];
            // Update item
            SaveData.Shop[fruitID, itemID, 0] = (int)(Math.Ceiling(SaveData.Shop[fruitID, itemID, 0] * costMultiplier));
            SaveData.Shop[fruitID, itemID, 1]++;
            // What you payed for
            if (itemID == 0)
                SaveData.Fruits[fruitID, 1] *= SaveData.Shop[fruitID, itemID, 2];
            else
                SaveData.Fruits[fruitID, 2] += SaveData.Shop[fruitID, itemID, 2];
        }
        // Update display
        costDisplay.text = SaveData.FruitFix(SaveData.Shop[fruitID, itemID, 0], 3);
        infoShow = !infoShow; Info();

        AutoClick.autoClick.RestartAuto(); // Restart autoclick
    }

}