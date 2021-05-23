using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClick : MonoBehaviour {

    public static AutoClick autoClick; // create static instance of class AutoClick

    void Awake ()
    {
        // DontDestroyOnLoad
        if (autoClick == null) {
            DontDestroyOnLoad(gameObject);
            autoClick = this;
            RestartAuto();
        }
        else if (autoClick != this) {
            Destroy(gameObject);
        }
    }

    // ReStart Coroutines
    public void RestartAuto()
    {
        StopAllCoroutines();    // end current
        AutoUpdate();           // start new
    }

    // Start Coroutines
    public void AutoUpdate()
    {
        for (int id = 0; id < SaveData.fruitName.Length; id++) {
            if (SaveData.Fruits[id, 2] != 0) {

                if (SaveData.Fruits[id, 2] < 10) {
                    float repeatRate = 1 / SaveData.Fruits[id, 2];
                    StartCoroutine(EveryUpdate(id, repeatRate));
                }
                else {
                    StartCoroutine(Every10(id)); }
            }
        }
    }

    // Coroutine
    IEnumerator EveryUpdate(int id, float repeatRate)
    {
        while (true) {
            yield return new WaitForSeconds(repeatRate);
            SaveData.Fruits[id, 0]++;
        }
    }

    // Coroutine
    IEnumerator Every10(int id)
    {
        // bara generellt för långsam
        while (true) {
            yield return new WaitForSeconds(0.1f);
            SaveData.Fruits[id, 0] += (SaveData.Fruits[id, 2] / 10);
        }
    }

}