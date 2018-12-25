using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoEnemigo : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if(DataCtrl.instance.data.nivel <= 1){
            
                SistemaDejuego.instance.TutoEnemigo();
                this.gameObject.SetActive(false);
            
            }
           

        }
    }
}
