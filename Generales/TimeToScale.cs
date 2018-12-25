using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToScale : MonoBehaviour {


    private void Awake()
    {

        if (Time.timeScale == 0)
        {

            Time.timeScale = 1f;

        }
    }

 
}
