using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaidaBarril : MonoBehaviour {

	public GameObject[] barril;

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			 StartCoroutine(tirarBarriles());
		}
	}

	IEnumerator tirarBarriles(){
		for (int i = 0; i < barril.Length; i++) {
			barril[i].GetComponent<Rigidbody2D> ().gravityScale = 2;
			yield return new WaitForSeconds (0.2f);
		}
	 }
			
}
