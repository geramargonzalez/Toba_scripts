using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnCtrl : MonoBehaviour {


	int levelNumber;
	Button btn;
	Image btnImage;
	Text btnTxt;
	Transform start1, start2, start3;

	public Sprite lockedBtn;
	public Sprite unlockedBtn;

	// Use this for initialization
	void Start () {

		// button called number, which representing 
		levelNumber = int.Parse (transform.gameObject.name);

		btn = transform.gameObject.GetComponent<Button> ();
		btnImage = btn.GetComponent<Image> ();
		btnTxt = btn.gameObject.transform.GetChild(0).GetComponent<Text> ();

		start1 = btn.gameObject.transform.GetChild (1);
		start2 = btn.gameObject.transform.GetChild (2);
		start3 = btn.gameObject.transform.GetChild (3);

		BtnStatus ();

	}


    public void BtnStatus()
    {

        GameData data = DataCtrl.instance.data;

        // Obtengo los niveles del juego
        bool unLocked = data.niveles[levelNumber].unlocked;
        int starsawarded = data.niveles[levelNumber].bonesStars;
       

      
		if (unLocked) {

			if (starsawarded == 3) {

				start1.gameObject.SetActive (true);
				start2.gameObject.SetActive (true);
				start3.gameObject.SetActive (true);

			}

			if (starsawarded == 2) {

				start1.gameObject.SetActive (true);
				start2.gameObject.SetActive (true);
				start3.gameObject.SetActive (false);

			}

			if (starsawarded == 1) {

				start1.gameObject.SetActive (true);
				start2.gameObject.SetActive (false);
				start3.gameObject.SetActive (false);

			}

			if (starsawarded == 0) {
				
				start1.gameObject.SetActive (false);
				start2.gameObject.SetActive (false);
				start3.gameObject.SetActive (false);
			}

			//Agrego la funcionalidad de Click
			btn.onClick.AddListener(LoadScene);


		} else {
		
			//Show the locked button image
			btnImage.overrideSprite = lockedBtn;

			//Dont show any text on the button
			btnTxt.text = "";


			//Hide 3 stars..
			start1.gameObject.SetActive (false);
			start2.gameObject.SetActive (false);
			start3.gameObject.SetActive (false);

		}
	
	}



	public void LoadScene(){
       
        GameData data = DataCtrl.instance.data;

        data.nivel = levelNumber;

        data.yaJugo = false;

        DataCtrl.instance.data = data;

        SceneManager.LoadScene ("Loading");
	}
}
