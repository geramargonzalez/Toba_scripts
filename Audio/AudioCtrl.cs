using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour {

	public static AudioCtrl instance;
	public PlayerAudio playerAudio;
	public EnemyAudio enemyAudio;
	public MultiploAudio multAudio;

	public AudioEffects audioEffects;
	public Transform player;
	public GameObject bsos;
    GameData data;

	public bool audioOn;


	// Use this for initialization
	void Start () {

		if(instance == null){
		
			instance = this;

		}

        data = DataCtrl.instance.data;

		if (data.audioOn) {

			bsos.SetActive (true);

		} else {

			bsos.SetActive (false);
		
		}

	}

	public void EnemyHit(Transform playerPos){

        if(data.audioOn){

			Vector3 pos = new Vector3 (playerPos.position.x, playerPos.position.y, -3f);
			AudioSource.PlayClipAtPoint(playerAudio.enemyHit1, pos,0.5f);
			
        }

	}


	public void PlayerDied(Transform playerPos){

        if(data.audioOn){

			AudioSource.PlayClipAtPoint (playerAudio.playerDied, playerPos.position);
		}

	}

	public void PickUpHealth(Transform playerPos){

        if(data.audioOn){

            AudioSource.PlayClipAtPoint (playerAudio.pickUp, playerPos.position,1f);

			
		}

	}

	public void CheckPoint(Transform playerPos){

        if(data.audioOn){

            AudioSource.PlayClipAtPoint(playerAudio.checkPoint, playerPos.position, 1f);

		}

	}

	public void TrollShout(Transform enemyPos){
        
        if(data.audioOn){

            Vector3 pos = new Vector3(enemyPos.position.x, enemyPos.position.y, -3f);
			AudioSource.PlayClipAtPoint (enemyAudio.trollShoutSound , pos,1f);
			

		}

	}


    public void TrollShoutPantalla()
    {

        if (data.audioOn)
        {
            
            Vector3 pos = new Vector3(0, 0, -3f);
            AudioSource.PlayClipAtPoint(enemyAudio.trollDeathSound, pos, 1f);


        }

    }



	public void TrollDeath(Transform enemyPos){

        if(data.audioOn){

			Vector3 pos = new Vector3 (enemyPos.position.x, enemyPos.position.y, -3f);

			AudioSource.PlayClipAtPoint (enemyAudio.trollDeathSound, pos,1f);
			

		}

	}


	public void AciertosMultiplo(Transform enemyPos){
        
        if(data.audioOn){

            Vector3 pos = new Vector3(enemyPos.position.x, enemyPos.position.y, -3f);
            AudioSource.PlayClipAtPoint (multAudio.aciertoA, pos,1f);
            

		}

	}

	public void GameOverMultiplo(Transform enemyPos){

        if(data.audioOn){

            Vector3 pos = new Vector3(enemyPos.position.x, enemyPos.position.y, -9f);
            AudioSource.PlayClipAtPoint(multAudio.winA, pos);
		
		}

	}

	public void LoseMultiplo(Transform enemyPos){

        if(data.audioOn){
            
            Vector3 pos = new Vector3(enemyPos.position.x, enemyPos.position.y, -9f);
            AudioSource.PlayClipAtPoint(multAudio.loseA, pos);
           
		}

	}




	public void ErrorMultiplo(Transform enemyPos){

        if(data.audioOn){

            Vector3 pos = new Vector3(enemyPos.position.x, enemyPos.position.y, -9f);
            AudioSource.PlayClipAtPoint(multAudio.loseA, pos);

		}

	}


    public void TimeEnd(Transform enemyPos)
    {

        if (data.audioOn)
        {

            Vector3 pos = new Vector3(enemyPos.position.x, enemyPos.position.y, -9f);
            AudioSource.PlayClipAtPoint(audioEffects.alarmTimeOut, pos);

        }

    }

    public void MonedaSound(Transform enemyPos)
    {

        if (data.audioOn)
        {

            Vector3 pos = new Vector3(enemyPos.position.x, enemyPos.position.y, -9f);
            AudioSource.PlayClipAtPoint(audioEffects.monedaSound, pos);

        }

    }


    public void PararBSO(){

        if(bsos.activeSelf){
            bsos.SetActive(false);
        } else {
            bsos.SetActive(true);
        }
		
	
	}


    public void toggleAudio()
    {
        if(data.audioOn){
            data.audioOn = false;
        } else {
            data.audioOn = true;
        }
    }




   




}
