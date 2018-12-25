using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstadisticasCtrl : MonoBehaviour {

	GameData data;
    GameObject[] nivel;
    Text[] nivelesText;
    GameObject[] promedio;
	Text[] promediostxt;
	GameObject[] aciertos;
	Text[] aciertostxt;
	GameObject[] fallos;
	Text[] fallostxt;
	GameObject[] totales;
	Text[] totalestxt;

	// Use this for initialization
	void Start () {
        
        //Le paso la informacion del juego
		Data (DataCtrl.instance.data);
		ObtenerGameObjectTextos ();
	
	}

	public void Data(GameData dat){

		data = dat;
	
	}

	public void ObtenerGameObjectTextos(){
       
        NivelesActive();
        nivelActualJuego();
		SeleccionarTextPromedio();
		SeleccionarTextFallos();
		SeleccionarTextAciertos();
		SetearValoresDelJuego ();
	
	}

	public void SeleccionarTextPromedio(){

        promedio = new GameObject[data.niveles.Length];
        promediostxt = new Text[data.niveles.Length];

		for(int i = 0; i < promedio.Length-1; i++){

            promedio[i] = GameObject.Find ("promedio" + i);
			promediostxt[i] = promedio[i].GetComponent<Text> ();
		}

	}

	public void SeleccionarTextFallos(){

        fallos = new GameObject[data.niveles.Length];
        fallostxt = new Text[data.niveles.Length];

		for(int i = 0; i < fallos.Length-1; i++){

			fallos[i] = GameObject.Find ("fallos" + i);
			fallostxt[i] = fallos[i].GetComponent<Text> ();


		}
	}

    public void NivelesActive()
    {
        
        nivel = new GameObject[data.niveles.Length];
        nivelesText = new Text[data.niveles.Length];

        //Debug.Log(data.niveles.Length);
        for (int i = 0; i < data.niveles.Length-1; i++)
        {

          nivel[i] = GameObject.Find("nivel" + i);   
          nivelesText[i] = nivel[i].GetComponent<Text>();
  
        }

        DataCtrl.instance.SaveData(data);

    }

	public void SeleccionarTextAciertos(){

        aciertos = new GameObject[data.niveles.Length];
        aciertostxt = new Text[data.niveles.Length];

		for(int i = 0; i < aciertos.Length-1; i++){

			aciertos[i] = GameObject.Find ("aciertos" + i);
			aciertostxt[i] = aciertos[i].GetComponent<Text> ();
           
		}

       

	}

	public void SeleccionarTextTotales(){

		totales = new GameObject[3];
		totalestxt = new Text[6];

		for(int i = 0; i < totales.Length-1; i++){

			int tmp = i;
			totales[i] = GameObject.Find ("total" + tmp);
			totalestxt[i] = totales[i].GetComponent<Text> ();

		}

	}



	public void SetearValoresDelJuego(){

        for(int i = 0; i < data.niveles.Length-1; i++) {

			if (i <= data.nivel) {

				promediostxt[i].text = data.niveles[i].promedio.ToString();
				fallostxt [i].text = data.niveles [i].fallosPorNivel.ToString();
				aciertostxt [i].text = data.niveles [i].aciertosPorNivel.ToString();

			} 
		}

	}

    public void nivelActualJuego()
    {

        for (int i = 0; i < data.niveles.Length-1; i++)
        {
            if (i <= data.nivel)
            {
                int tmp = data.niveles[i].nivel + 1;
                nivelesText[i].text = tmp.ToString();

            }
        }
    }

	public void TotalValoresAciertos(){

		int total = 0;

        for(int i = 0; i < data.niveles.Length-1; i++){

			if (i <= data.nivel) {
			
				total += data.niveles [i].aciertosPorNivel;
			
			}

		}

		totalestxt [1].text = total.ToString ();


	}

	public void TotalValoresFallos(){

		int total = 0;

        for(int i = 0; i < data.niveles.Length-1; i++){

			if (i <= data.nivel) {

				total += data.niveles [i].fallosPorNivel;

			}

		}

		totalestxt [2].text = total.ToString ();
	}


	public void TotalPromedioPorNivel(){

		int total = 0;
		int promeNivel = 0;

        for(int i = 0; i < data.niveles.Length-1; i++){

			if (i <= data.nivel) {

				total += data.niveles [i].promedio;

			}
		}

		if (total == 0) {

			totalestxt [0].text = " 0 ";

		} else {

			promeNivel = total / data.nivel+1;
			totalestxt [0].text = promeNivel.ToString ();
		}

	}


   



}
