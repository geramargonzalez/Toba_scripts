using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHeadCtrl : MonoBehaviour {

	public GameObject enemy;

	
    void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.CompareTag ("PlayerFeet")) 
		{
			AudioCtrl.instance.EnemyHit (coll.gameObject.transform);

            SistemaDejuego.instance.MsjAnimalConvetido();

			SistemaDejuego.instance.EnemyStompsEnemy(enemy);

			SFXCtrl.instance.showPlayerLanding(coll.gameObject.transform.position);

		}
	
	}


}
