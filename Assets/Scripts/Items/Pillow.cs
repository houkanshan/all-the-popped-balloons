using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : MonoBehaviour
{
    InfoSpot infoSpot;
    bool prevIsClicked = false;
    public GameObject nextHotSpot;
    private void Awake() {
        infoSpot = GetComponent<InfoSpot>();
    }

    void Update()
    {
        if (
            infoSpot.isClicked && !prevIsClicked &&
            GameManager.i.loopedTimes.Value == 0
        ) {
            nextHotSpot.SetActive(true);
        }
        prevIsClicked = infoSpot.isClicked;
    }
}
