using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

    public int fruitID;
    int count = 0;

    public void Clicked() {

        if(SaveData.Fruits[fruitID, 1] < 1) {

            count++;
            float frequency = 1 / SaveData.Fruits[fruitID, 1];

            if (count >= frequency) {
                count = 0;
                SaveData.Fruits[fruitID, 0] += SaveData.Fruits[fruitID, 1] * frequency;
            }
            //else { count++; }

                


        }
        else {
            SaveData.Fruits[fruitID, 0] += SaveData.Fruits[fruitID, 1];
        }
    }
}