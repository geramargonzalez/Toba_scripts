using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class btnCertificate : MonoBehaviour {

    int levelNumber;
    Button btn;
    Image btnImage;
    Text btnTxt;
   

    public Sprite lockedBtn;


    // Use this for initialization
    void Start()
    {

        // button called number, which representing 
        levelNumber = int.Parse(transform.gameObject.name);

        btn = transform.gameObject.GetComponent<Button>();
        btnImage = btn.GetComponent<Image>();
        btnTxt = btn.gameObject.transform.GetChild(0).GetComponent<Text>();

      
        BtnStatus();

    }

  
    public void BtnStatus()
    {
        
        GameData data = DataCtrl.instance.data;

        // Getting the local status of button IsUnlocked (levelNumber);
        bool unLocked = data.niveles[levelNumber].unlocked;
        int starsawarded = data.niveles[levelNumber].bonesStars;
        ///Debug.Log(unLocked);

        if(unLocked){
            //Agrego la funcionalidad de Click
            btn.onClick.AddListener(LoadScene);
        } else {
            
            //Show the locked button image
            btnImage.overrideSprite = lockedBtn;

            //Dont show any text on the button
            btnTxt.text = "";

        }
        }

            

    public void LoadScene()
    {

        GameData data = DataCtrl.instance.data;

        data.nivel = levelNumber;

        data.yaJugo = false;

        SceneManager.LoadScene(data.nivel.ToString());
    }
}
