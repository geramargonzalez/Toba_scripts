using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcultarGraficosTuto : MonoBehaviour {

    public GameObject contenedor;

	// Use this for initialization
	void Start () {
		
	}
	
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            contenedor.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
