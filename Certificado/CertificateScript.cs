using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CertificateScript : MonoBehaviour
{
    
    public GameObject certificado;
    public GameObject contInput;
    public Text nombre;
    public Text apellido;
    public Text infoCert;
    public Text infoCert2;
    GameData data;
  
    public Text msjInicio;
    int nivel;
    int datos;

    private bool isProcessing;
    private bool isFocus;

    // Use this for initialization
    void Start()
    {
        
        data = DataCtrl.instance.data;
        isProcessing = false;
        //nivel = data.nivel-1;
        datos = 30;
        nivel = datos;
        msjInicio.text = "Superaste los " + nivel + " niveles del juego";
       

    }

    public void GenerarCertificado()
    {

        certificado.SetActive(true);

        contInput.SetActive(false);
       
        AudioCtrl.instance.GameOverMultiplo(this.gameObject.transform);

        infoCert.text = "Certficamos que el estudiante " + nombre.text + " " + apellido.text + " supero los  " +
            nivel + " niveles ";

        infoCert2.text = " y resolvio " + nivel + " operaciones matemáticas";

    }

    public void ShareMedia(){

        if(!isProcessing){

            StartCoroutine(ShareScreenShoot());
        
        }
    }


    IEnumerator ShareScreenShoot(){

        isProcessing = true;

        yield return new WaitForEndOfFrame();

        string fileName = "certificado.png";

        //Toma una captura del pantall
        ScreenCapture.CaptureScreenshot(fileName, 2);

        string destination = Path.Combine(Application.persistentDataPath,fileName);


        yield return new WaitForSecondsRealtime(0.3f);

        // Pregunta si no esta en el editor
        if(!Application.isEditor){

            //Instancia una clase de Java
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaClass("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION SEND"));
           
            AndroidJavaClass  uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);

            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Some text");

            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");

            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject> ("createChooser", intentObject);

            currentActivity.Call("starActivity", chooser);

            yield return new WaitForSecondsRealtime(1f);

            yield return new WaitUntil(() => isFocus);

            certificado.SetActive(false);

            isProcessing = false;


        }



    }


    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }



}
