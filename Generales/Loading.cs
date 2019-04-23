using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

    List<string> descripciones = new List<string>();
    public GameObject[] imagenes;
    public Text txtDesc;

	
	void Start () {

        CargarTextos();
        ElegirTextoDescripcion();
        StartCoroutine(CambiadeEscena());

	}

    IEnumerator CambiadeEscena()
    {

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(DataCtrl.instance.data.nivel.ToString());

    }



    public void CargarTextos(){

        descripciones.Add("Los perros pueden detectar las emociones de las personas");
        descripciones.Add("Animales Sin Hogar es un organización uruguaya que fue creada con el fin de brindar una base de datos a nivel nacional de animales perdidosy en adopción");
        descripciones.Add("La nariz de los perros son únicas, como las huellas dactilares de los humanos ..");
        descripciones.Add("Los perros tienen el mismo grado de inteligencia que posee un niño de dos años ....");
        descripciones.Add("El perro más viejo murió a los 29 años..");
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
        descripciones.Add("La suma y la multiplicación tienen la propiedad asociativa. Aunque se alteren el orden de los numeros el resultado es el mismo.");
        descripciones.Add("Cambiar la forma de asociar los números en la resta sí varía el resultado. Por lo tanto la resta no tiene la propiedad asociativa. ");
    
    }

    public void ElegirTextoDescripcion(){
        
        float random = Random.Range(0f, (float)descripciones.Count-1);
        txtDesc.text = descripciones[(int)random];
        random =  Random.Range(0f, (float)imagenes.Length-1);
        imagenes[(int)random].SetActive(true);
    }

  



}
