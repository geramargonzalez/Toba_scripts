﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

    List<string> descripciones = new List<string>();
    public List<Image> imagenes = new List<Image>();

    public Text txtDesc;

	// Use this for initialization
	void Start () {

        CargarTextos();
        ElegirTextoDescripcion();
        StartCoroutine(CambiadeEscena());

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator CambiadeEscena()
    {

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(DataCtrl.instance.data.nivel.ToString());

    }



    public void CargarTextos(){

        descripciones.Add("los perros pueden detectar las emociones de las personas");
        descripciones.Add("La nariz de los perros son únicas, como huellas dactilares de humanos ..");
        descripciones.Add("Tienen el mismo grado de inteligencia que posee un niño de dos años ....");
        descripciones.Add("El perro más viejo del mundo murió a los 29 años..");
        descripciones.Add("La temperatura del cuerpo de un perro es más alta que la de un humano.");
        descripciones.Add("Los perros de raza pequeña, viven más que los de raza grande.");
        descripciones.Add("No existe otra especie de animal en el mundo que tenga la misma diversidad de razas como el perro.");
        descripciones.Add("Un perro suele juzgar objetos la primera vez por su movimiento, después, por su brillo, y al final por su forma.");
        descripciones.Add("Los perros necesitan un olfato muy fuerte porque no tienen una visión muy aguda.");
        descripciones.Add("Los perros con un año de edad son tan maduros físicamente como un humano de 15 años de edad..");
 

        descripciones.Add("La propiedad cero (0) en la multiplicación significa que siempre que hay un cero en un problema, la respuesta es cero.");
        descripciones.Add("La palabra “multiplicar” viene del latín multiplicare y significa “aumentar el número de la misma cosa”");
        descripciones.Add("La inversa de la multiplicación es la división.");
        descripciones.Add("Los números que hay que multiplicar se llaman multiplicador y multiplicando, o a veces se llaman “factores”.");
        descripciones.Add("El resultado de la multiplicación se denomina “producto”.");
        descripciones.Add("Las tablas de multiplicación también se llaman la “Tabla de Pitágoras” ya que fue inventada por el matemático y filósofo Pitágoras en Grecia..");

    }

    public void ElegirTextoDescripcion(){
        
        float random = Random.Range(0f, (float)descripciones.Count-1);
        txtDesc.text = descripciones[(int)random];
    
    }



}