using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsCtrl : MonoBehaviour {

    Rigidbody2D rb;
    Vector3 vector;
    float vel;
    PlayerControllerAdvanced dogScript;

	
	void Start () {
       
        vel = 10f;
        vector = new Vector3(10f,0,0);
        rb = this.gameObject.GetComponent<Rigidbody2D>();
	
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        
        if (coll.gameObject.CompareTag("Player"))
        {
            dogScript = coll.gameObject.GetComponent<PlayerControllerAdvanced>();

            if(dogScript.isFacingRight){
            
                rb.AddForce(vector * vel); 
            
            } else {
            
                vector.x = -10f;
                rb.AddForce(vector * vel); 
            
            }

        }

    }


}
