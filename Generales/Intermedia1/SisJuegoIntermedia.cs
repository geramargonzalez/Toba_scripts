
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 										


public class SisJuegoIntermedia : MonoBehaviour {


        	//INSTANCIAS Y TRAMAS
        	public static SisJuegoIntermedia instance = null;

        	// GAMEDATA
        	public GameData data;

        	//UI
        	public UI ui;
        		  
        	int divisor;
        	int dividendo;
        	int resto;
        	int resultado;

        	//Min y Max de los numeros para las operaciones
        	int min;
        	int max;
        	int minResp;
        	int maxResp;

           

        	//RespuestaCorrecta
        	int correcta;

        	// La cantidad de operaciones que necesito para poder terminar, y que no se repita un numero divisor.
        	int cantOperacionesParaTerminar;
        	int contMismoNumero;

        	//Tiempo
        	float timeLeft;
        	int tiempoStart;
        	int seteoTiempo;
        	bool sumandoParaPromedio;
            float timeRealiseOperation;
             
            bool contarParaCambio;
            float numeroParaCambio;

        	// PANEL DE FIN DE JUEGO
        	public GameObject panel;

        	public List<Text> txtOpciones = new List<Text>();
        	List<int> respuestas = new List<int>();
        	GameObject[] goRespuestas;


        	//Animaciones del juego.
        	Animator animDividendo;
        	Animator animResto;
        	Animator animResultado;
        	Animator animTroll;
        	Animator animDog;
            Animator animOp;

        	public GameObject troll;
                   Rigidbody2D trollRigi;

            public GameObject dog;
                   Rigidbody2D rigiDog;

            public GameObject plataformaDog;
                    Rigidbody2D platRigiDog; 
                
            public GameObject plataformaTroll;
                   Rigidbody2D platRigiTroll;



            public bool tirarTroll;

        int paraPromedio;



            //bool oki;
            float tiempoCambio = 1f;

        	int promedioPorNivel;

        	void Awake(){

        		if(instance == null){

        			instance = this;
        		}

        	}

        	// Use this for initialization
        	void Start () {
        
        		data = DataCtrl.instance.data;
        		ui.contenedorGameOver.SetActive (false);
        		animDividendo = ui.dividendotxt.GetComponent<Animator> ();
        		animResto = ui.restotxt.GetComponent<Animator> ();
        		animResultado = ui.resultadotxt.GetComponent<Animator> ();
        		animTroll = troll.GetComponent<Animator> ();
                trollRigi = troll.GetComponent<Rigidbody2D>();
                platRigiTroll = plataformaTroll.GetComponent<Rigidbody2D>();
                rigiDog = dog.GetComponent<Rigidbody2D>();
                platRigiDog = plataformaDog.GetComponent<Rigidbody2D>();
        		animDog = dog.GetComponent<Animator> ();
                animOp = ui.operacionesRestantes.GetComponent<Animator>();
                Comenzar();
               
        	}
        	

            void Update()
            {
                TimeParaPromedios();
                TimeDeCambio();
            }

            public void Comenzar(){
                
                contarParaCambio = false;
                SetearMinMax();
                sumandoParaPromedio = true;
        		cargarValoresDelasOperaciones ();
        		ReanudarTiempo ();
        		GenerarRespuestas ();
              
        	
            }

        	public void SetearMinMax(){

                data.fallos = 0;
              
            	 if (data.nivel == 5) {

                        cantOperacionesParaTerminar = DataCtrl.instance.cantidadEnemigoPorNivel();
            			min = 2;
            			max = 16;
            			minResp = 1;
            			maxResp = 151;
                       
                } else if(data.nivel == 5) {
                   
                    cantOperacionesParaTerminar = DataCtrl.instance.cantidadEnemigoPorNivel();
                    min = 2;
                    max = 11;
                    minResp = 1;
                    maxResp = 101;
                  
                }

                data.numParaPromedio = cantOperacionesParaTerminar;  
        		timeLeft = tiempoStart;

        	}

        	public void cargarValoresDelasOperaciones(){
             
            	ui.dividendotxt.text = "Dividendo ";
        		ui.restotxt.text = "Resto ";
        		ui.resultadotxt.text =  "Resultado ";
        		ui.divisortxt.text = "Divisor";

        	}

        	public void LimpiarValores(){

        		ui.dividendotxt.text = " ";
        		ui.restotxt.text = " ";
        		ui.resultadotxt.text =  " ";
        		ui.divisortxt.text = "";

        	}

            public void GenerarDivisor(){
                     
              divisor = GeneradorNumeroRandomMultiplo();
            
              ui.divisortxt.text = divisor.ToString();
          
            }

                

            public int GeneradorNumeroRandomMultiplo()
            {

                float random = Random.Range((float)min, (float)max);
                
                return (int)random;
            }


        	private void RestoDeOperacion(int num){
                
               
        		dividendo = respuestas[num];
        		resto = dividendo % divisor;
        		ui.dividendotxt.text = dividendo.ToString ();
        		ui.restotxt.text = resto.ToString ();
                ResultadoDelaOperacion();

        		if (resto == 0) {

                    DetenerTroll();
                    PararTiempo();
                    respuestas.Clear();
                    AudioCtrl.instance.AciertosMultiplo(this.gameObject.transform);
                    cantOperacionesParaTerminar--;
                    OperacionesRestantesUI();
                    ReciboTiempoParaPromedios(timeRealiseOperation);
                    timeRealiseOperation = 0;
        			SumarPuntos ();

        		} else {
                     
        			AudioCtrl.instance.ErrorMultiplo(this.gameObject.transform);
        			DescontarPuntosPorFallos ();
        		}

               
        	}

        	public void ResultadoDelaOperacion(){

        		resultado = dividendo / divisor;
        		ui.resultadotxt.text = resultado.ToString ();
        	}

            public void DetenerTroll(){
                if (animTroll.GetBool("entrar") == true)
                {
                    animTroll.SetBool("entrar", false);
                }
            }
            public void DetenerDog()
            {
                if (animDog.GetBool("entrar") == true)
                {
                    animDog.SetBool("entrar", false);
                }
            }

            public void OperacionesRestantesUI(){
                
               
                animOp.SetBool("entrar",true);
                ui.operacionesRestantes.text = "Faltan: " + cantOperacionesParaTerminar;
            
            }


                public void GenerarRespuestas()
            {

                GenerarDivisor();
                NumerosMultiplicos();

            }



            public void RecibirMultiplo(int numUsuario)
            {

                RestoDeOperacion(numUsuario);

            }
      
            public void NumerosMultiplicos()
            {
                
                do
                {
                    float random = Random.Range((float)minResp, (float)maxResp);




                       if (((int)random % divisor) == 0)
                        {
                        
                         respuestas.Add((int)random);

                        }              

                // Se va repetir el While mientras respuestas.Count sea menor a 1
                } while (respuestas.Count < 1);


                 if (data.nivel == 5){
                
                     NumerosNoMultiplicos(3);
                        
                  } else {
                           
                   NumerosNoMultiplicos(2);
                        
                }
                               
            }


                public void NumerosNoMultiplicos(int numeroRespuestasPorNivel){
                    
                    int cantNomulti = 0;
                    int nocorrecta = 0;
                    


               do
                    {


                    float random = Random.Range((float)minResp, (float)maxResp);

                    
                     
                     // Cuando no es divisible
                     if (((int)random % divisor) != 0)
                     {


                         nocorrecta = (int)random;                                 
                         respuestas.Add(nocorrecta);
                         cantNomulti++;

                      }

                       } while (cantNomulti < 2);

                      
                             BotonRespuestas();

                }



                public static List<T> DesordenarListados<T>(List<T> input)

                  {
                        List<T> arr = input;
                        List<T> arrDes = new List<T>();

                        while (arr.Count > 0)
                        {
                            int val = Random.Range(0, arr.Count);
                            arrDes.Add(arr[val]);
                            arr.RemoveAt(val);
                        }

                        return arrDes;
                }
            
                public void BotonRespuestas()
                {


                  respuestas = DesordenarListados<int>(respuestas);

                    for (int i = 0; i < respuestas.Count; i++)
                    {

                        txtOpciones[i].text = respuestas[i].ToString();

                    }

                   ReanudarTiempo();
                }


            	// Puntaje y Fin del Juego
                public void GameOver(){
                    StartCoroutine(TirarTroll());	
            	}

            	//Puntos por aciertos
            	public void SumarPuntos(){

                    data.puntos += 1000;
                    ui.txtPuntos.text = "Puntos: " + data.puntos.ToString ();
                    animDog.SetBool ("entrar", true);

                    if(cantOperacionesParaTerminar == 0){
                       
                        GameOver();

                    } else {

                        contarParaCambio = true;
                        
                    }

            	}


            	public void DescontarPuntosPorFallos(){

                    
                    data.puntos -= 1000;
                    ui.txtPuntos.text = "Puntos: " + data.puntos;
                    animTroll.SetBool("entrar", true);
            		Fallos ();

            	}


                public void Fallos()
                {
            
                    data.fallos++;
                    ui.textFallos.text = "Fallos: " + data.fallos.ToString();

                    if (data.fallos > 3)
                    {

                        StartCoroutine(TirarDog());

                    }

                }


             // Esta corrutina salta cuando se gana el juego
            IEnumerator TirarTroll()
            {
     
                AudioCtrl.instance.PararBSO();
                AudioCtrl.instance.TrollShout(gameObject.transform);
                trollRigi.isKinematic = false;
                trollRigi.mass = 100f;
                platRigiTroll.isKinematic = false;
                platRigiTroll.mass = 100f;
                sumandoParaPromedio = false;
                contarParaCambio = false;
                
                yield return new WaitForSeconds(2.5f);
                
                ui.contenedorGrl.SetActive(false);
                
                DataCtrl.instance.subirNivel();

                AudioCtrl.instance.GameOverMultiplo(this.gameObject.transform);
                ui.contenedorGameOver.SetActive(true);
                
          
            }


            IEnumerator TirarDog()
            {
                
                AudioCtrl.instance.PararBSO();
                platRigiDog.isKinematic = false;
                platRigiDog.mass = 100f;
                rigiDog.isKinematic = false;
                rigiDog.mass = 100f;
                AudioCtrl.instance.PararBSO();
                AudioCtrl.instance.TrollShout(gameObject.transform);
                sumandoParaPromedio = false;
                contarParaCambio = false;
                   
                yield return new WaitForSeconds(2.5f);
               
                AudioCtrl.instance.LoseMultiplo(this.gameObject.transform);
                AudioCtrl.instance.PararBSO();
                ui.contenedorGrl.SetActive (false);
                ui.contTryAgain.SetActive (true);

            }


           
        	public void PararTiempo(){
        		

                sumandoParaPromedio = false;

        	}

        	public void ReanudarTiempo(){
            
                sumandoParaPromedio = true;
        	}

        	

        	public int GetScore () {

        		return data.puntos;

        	}

        	public int obtenerFallos(){
        		return data.fallos;
        	}

        	public int SetStarsAwarded (int levelNumber, int stars){

        		return data.niveles[levelNumber].bonesStars = stars;

        	}


        	public void PuntosPorStars(int stars){

        		if(stars == 1){
        			
                    data.puntos += 2500;

        		} else if(stars == 2){

        			data.puntos += 5000;

        		} else if(stars == 3){

        			data.puntos += 10000;

        		}

        	}


        	public void ReciboTiempoParaPromedios(float tiempoOperacion){

        		data.niveles[data.nivel].promedio += (int)tiempoOperacion;

        	}


            public void TimeParaPromedios()
            {

                if (sumandoParaPromedio)
                {

                   
                    timeRealiseOperation += Time.deltaTime;

                    ui.textTimer.text = "Tiempo: " + (int)timeRealiseOperation;

                 } 

            }


            
            // Cada Tanto tiempo se tranca.
            public void TimeDeCambio()
            {

                if (contarParaCambio)
                {

                
                    numeroParaCambio += Time.deltaTime;
                    
                    if(numeroParaCambio > 1.5){

                        contarParaCambio = false;
                        numeroParaCambio = 0f;                      
                        AnimalRespuestas();
                    }
                }

             }


               public void AnimalRespuestas(){

                    cargarValoresDelasOperaciones();
                    GenerarRespuestas();  
              }



  

   

        }
