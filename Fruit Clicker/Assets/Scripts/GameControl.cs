using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    // Scenemanager
    public string sceneToLoad;
    public void LoadScene() {

        if(sceneToLoad == "back") {
            // Start
            if (PlayerPrefs.GetString("lastLoadedScene") == SceneManager.GetActiveScene().name) {
                SceneManager.LoadScene("Start"); }
            // Back
            else {
                SceneManager.LoadScene(PlayerPrefs.GetString("lastLoadedScene")); }
        }            
        else {
            PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(sceneToLoad); }
    }

    // Gamesave
    public void Save() {

        string path = Application.persistentDataPath + "/playerInfo.dat";

        // create formatter and filestream
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        // convert SaveData to new PlayerData instance "data"
        PlayerData data = new PlayerData {
            fruits = SaveData.Fruits,
            shop = SaveData.Shop
        };

        // convert "data" to file via "stream" with help of "formatter"
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public void Load() {

        string path = Application.persistentDataPath + "/playerInfo.dat";

        if (File.Exists(path)) {
            // create formatter and filestream
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            // convert file to instance PlayerData "data"
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            // convert "data" to SaveData
            SaveData.Fruits = data.fruits;
            SaveData.Shop = data.shop;

            AutoClick.autoClick.RestartAuto(); // Restart autoclick
        }
    }
    public void Reset()
    {
        // ReStock
        SaveData.Fruits = SaveData.StockFruits();
        SaveData.Shop = SaveData.StockShop();

        AutoClick.autoClick.RestartAuto(); // Restart autoclick
    }

}

public static class SaveData {

    /*  New fruits:
            1. Stock values
            2. Shop.itemName
            Unity: 
                3. fruitID Clicker & Display
                4. Urn Display
                5. Map 
                6. save game */

    // Stock values
    public static float[,] StockFruits()
    {
        float[,] fruitStock = { // amount, perClick, perSec
            { 0, 1, 0 },    // apple
            { 0, 1, 0 },    // pear
            { 0, 1, 0 },    // banana
            { 0, 0.5f, 0 }     // coconut
        };
        return fruitStock;
    }
    public static float[,,] StockShop()
    {
        float[,,] shopStock = { // cost, level, add
            { // apple
                { 0,   0,  2 }, // multi
                { 0,   0,  0.1f },
                { 0,   0,  10 },
                { 0,   0,  100 },
                { 0,   0,  1000 }
            },
            { // pear
                { 5,    0,  2 },
                { 5,    0,  1 },
                { 10,   0,  3 },
                { 12,   0,  50 },
                { 1,   0,  1000 }
            },
            { // banana
                { 15,   0,  3 },
                { 5,    0,  2 },
                { 15,   0,  10 },
                { 25,   0,  50 },
                { 1,   0,  1000 }
            },
            { // coconut
                { 20,   0,  2 },
                { 30,   0,  10 },
                { 60,   0,  30 },
                { 100,  0,  40 },
                { 1,   0,  1000 }
            }
        };
        return shopStock;
    }
    public static string[] fruitName = { // .Length important
        "Apple", "Pear", "Banana", "Coconut" };

    // Start & Get Set
    private static float[,] fruits = StockFruits();
    private static float[,,] shop = StockShop();
    public static float[,] Fruits
    {
        get { return fruits; }
        set { fruits = value; }
    }
    public static float[,,] Shop
    {
        get { return shop; }
        set { shop = value; }
    }

    // Convert & Prefix
    public static string FruitFix (float fruits, int decimals)
    {
        string converted;
        string round = "f" + decimals.ToString();

        if (fruits >= Math.Pow(10, 24)) {
            converted = (fruits / Math.Pow(10, 24)).ToString(round) + "Y";
        } // kvadriljon
        else if (fruits >= Math.Pow(10, 21)) {
            converted = (fruits / Math.Pow(10, 21)).ToString(round) + "Z";
        } // triljard
        else if (fruits >= Math.Pow(10, 18)) {
            converted = (fruits / Math.Pow(10, 18)).ToString(round) + "E";
        } // triljon
        else if (fruits >= Math.Pow(10, 15)) {
            converted = (fruits / Math.Pow(10, 15)).ToString(round) + "P";
        } // biljard
        else if (fruits >= Math.Pow(10, 12)) {
            converted = (fruits / Math.Pow(10, 12)).ToString(round) + "T";
        } // biljon
        else if (fruits >= Math.Pow(10, 9)) {
            converted = (fruits / Math.Pow(10, 9)).ToString(round) + "G";
        } // miljard
        else if (fruits >= Math.Pow(10, 6)) {
            converted = (fruits / Math.Pow(10, 6)).ToString(round) + "M";
        } // miljon
        else if (fruits >= Math.Pow(10, 3)) {
            converted = (fruits / Math.Pow(10, 3)).ToString(round) + "k";
        } // tusen
        else if (fruits >= 1 || fruits == 0) {
            converted = fruits.ToString("f0");
        } // positiva heltal
        else {
            converted = fruits.ToString("f1");
        } // tiondel

        return converted;
    }

}

[Serializable]
class PlayerData {

    public float[,] fruits;
    public float[,,] shop;

}