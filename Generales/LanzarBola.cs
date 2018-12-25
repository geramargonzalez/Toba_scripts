using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzarBola : MonoBehaviour
{


    public GameObject bolaPrefab;
    public Transform[] positions;

    public void Lanzar(int pos){
		
        StartCoroutine ("lanzarBola", pos);
	
    }

		IEnumerator lanzarBola(int pos){
				
				Instantiate (bolaPrefab, positions[pos].position, Quaternion.identity);
				
				yield return new WaitForSeconds (0.5f);
		}
}
