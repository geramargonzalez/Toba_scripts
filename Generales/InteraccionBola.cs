using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionBola : MonoBehaviour {

	private Rigidbody2D rgbd2D;
	private GameObject vida;
	void Start(){
		vida = GameObject.Find ("Vida");
		rgbd2D = GetComponent<Rigidbody2D> ();
		//rgbd2D.gravityScale = 0;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Player") && this.gameObject.CompareTag("Bola")  &&  rgbd2D.velocity.y < -2 && rgbd2D.velocity.x < -2) {
			Debug.Log ("LO toca !!!!???????");
			vida.SendMessage ("TamageDamage", 20f);
		}

	}

}
