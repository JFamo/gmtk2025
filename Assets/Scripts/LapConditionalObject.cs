using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class LapConditionalObject : StateChangeSubscriber {
    public bool appearsAfterLap;
    public int appearAfterLap;
    public bool disppearAfterLap;
    public int disappearAfterLap;
    
    private void Start() {
        LapStateController.GetInstance().Subscribe(this);
        if(appearsAfterLap) {
            if(LapStateController.GetInstance().GetLapNumber() > appearAfterLap) {
                gameObject.SetActive(true);
            } else {
                gameObject.SetActive(false);
            }
        } else {
            gameObject.SetActive(true);
        }
    }

    public override void OnLapChange(int newValue) {
        if (appearsAfterLap && newValue > appearAfterLap) {
            gameObject.SetActive(true);
        }
        else if (disppearAfterLap && newValue > disappearAfterLap) {
            gameObject.SetActive(false);
        }
    }
}
