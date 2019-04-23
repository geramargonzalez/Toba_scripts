
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class EnemyScript : MonoBehaviour
    {

        Animator anim;
        float speed;

        public bool attack;
        bool move;
        bool ok = true;


        public GameObject tablas;

        //Tiempo entre cada operacion de la tabla
        public float maxtime;
        float timeLeft;

        bool contarParaPromedio;
        public float timeRealiseOperation;

        bool opHabilitada = false;
        public UI ui;

        int pos;

        public Text respuesta1;
        public Text respuesta2;
        public Text signoTroll;
        public Text resultado;
        public Text equal;

        Text[] textosOperaciones;

        bool oki;
        float tiempoCambio;

        public float visionRadius;
        GameObject player;
        bool aprobarCuenta;

        // Use this for initialization
        void Start()
        {

            TiempoMaximoOperaciones();
            move = false;
            attack = false;
            ui.texttimeOp = GameObject.Find("ResetOperacion").GetComponent<Text>();
            player = GameObject.Find("DogAdvanced");
            tablas.SetActive(false);
            anim = GetComponent<Animator>();
            agregarTextosParaRecorrerlos();
            timeLeft = 0;
            aprobarCuenta = true;

        }

        // Update is called once per frame
        void Update()
        {
            DogInsideRadius();

            if (SistemaDejuego.instance.matarTroll())
            {

                detenerOperacion();
                Death();

            }

            if (SistemaDejuego.instance.obtenerAttack())
            {

                Atacar();

            }

            Mover();

            finAttack();

            if (opHabilitada)
            {

                TimeOperaciones();

            }

            TimerChange();

           // TimeParaPromedios();
        }

        public void TiempoMaximoOperaciones()
        {

            int nivel = DataCtrl.instance.data.nivel;

            if (nivel <= 2)
            {
                
                maxtime = 15;

            }
            else if (nivel == 3)
            {

                maxtime = 12;

            }
            else if (nivel == 4)
            {

                maxtime = 9;

            }
        }


        void Mover()
        {

            if (move)
            {
                transform.Translate(-5f * Time.deltaTime, 0f, 0f);
            }

        }

        public void ocultarTroll()
        {

            StartCoroutine(DestruirTroll());
        }


        IEnumerator DestruirTroll()
        {

            AudioCtrl.instance.TrollDeath(gameObject.transform);
            SistemaDejuego.instance.SetDie(false);
            SistemaDejuego.instance.SetearCrearNuevoTroll(true);
            PararTiempoOperaciones();
            yield return new WaitForSeconds(0.1f);
            SistemaDejuego.instance.EnemyDerroted(this.gameObject.transform);
            Destroy(this.gameObject);

        }


        public void PararTiempoOperaciones()
        {
            if (contarParaPromedio)
            {
                contarParaPromedio = false;
                timeRealiseOperation += timeLeft;
                 timeLeft = 0;
                SistemaDejuego.instance.ReciboTiempoParaPromedios(timeRealiseOperation);
            }


          
        }


        public void finAttack()
        {
            anim.SetBool("Attack", SistemaDejuego.instance.obtenerAttack());
        }

        public void Atacar()
        {
            anim.SetBool("Attack", true);
        }

        public void Walk()
        {
            move = true;
            anim.SetBool("walk", move);

        }

        public void Idle()
        {
            move = false;
            anim.SetBool("walk", move);

        }

        public void Death()
        {

            anim.SetBool("Die", SistemaDejuego.instance.matarTroll());

        }


        void OnCollisionEnter2D(Collision2D coll)
        {

            if (coll.gameObject.tag == "Player")
                Atacar();

        }

        public void GenerarOperacionesAritmeticas(){

                restaurarValoresTiempo();
                SistemaDejuego.instance.radiusEnemigo = true;

        }





        public void TimerChange()
        {
            if (oki)
            {
                if (tiempoCambio > 0)
                {
                    tiempoCambio -= Time.deltaTime;

                    if(tiempoCambio < maxtime/2){
                       
                        AudioCtrl.instance.TimeEnd(this.gameObject.transform);
                    
                    }


                }
                else
                {

                    oki = false;
                    tiempoCambio = maxtime;
                    ok = true;
                    attack = SistemaDejuego.instance.obtenerAttack();

                }

            }
        }


    	public void DeactivateChildren(GameObject g, bool a) {

    		//g.activeSelf = a;

    		g.SetActive(a);

    		foreach (Transform child in g.transform) {
    			DeactivateChildren(child.gameObject, a);
    		}
    	
    	}

    	public void resetearTabla(){
            tiempoCambio = maxtime;
    	}
    		
    	public void restaurarValoresTiempo(){
    		ui.texttimeOp.color = Color.white;
    		timeLeft = 0;
    		opHabilitada = true;
    	}

    	public void TimeOperaciones(){
    		
    		timeLeft += Time.deltaTime;

            ui.texttimeOp.text = "Demorando: " + (int)timeLeft;
           	
            if(timeLeft >= maxtime){
    		
                resetearTabla ();

            } else if(timeLeft > maxtime - 4){
    			
                ui.texttimeOp.color = Color.red;
    	
            }
    	}




    	public void detenerOperacion(){
    		//StopCoroutine (tiempoDecambio ());
    		ui.texttimeOp.text = "";
    		//limpiarTextosDelasOperaciones ();
    		opHabilitada = false;
            oki = false;
            tiempoCambio = maxtime;
            DataCtrl.instance.SaveData();

    	}


    	public void limpiarTextosDelasOperaciones(){
    		respuesta1.text = " ";
    		signoTroll.text = " ";
    		respuesta2.text = " ";
    		resultado.text  = " ";
    		equal.text =  " ";

    	}

    	public void agregarTextosParaRecorrerlos(){
    		textosOperaciones = new Text[5];
    		textosOperaciones[0] = respuesta1;
    		textosOperaciones[1] = signoTroll;
    		textosOperaciones[2] = respuesta2;
    		textosOperaciones[3] = equal;
    		textosOperaciones[4] = resultado;

    	}

    	public void RecibirResultado(string resultadoOperacion){

    		if (respuesta1.text == "_") {
    			
                respuesta1.text = resultadoOperacion;
    			SistemaDejuego.instance.SetDie (true);
    		
            } else if (respuesta2.text ==  "_") {
    			
                respuesta2.text = resultadoOperacion;
    			SistemaDejuego.instance.SetDie (true);
    		
            } else if (signoTroll.text ==  "_") {
    			
                signoTroll.text = resultadoOperacion;
    			SistemaDejuego.instance.SetDie (true);
    		
            }else if (resultado.text ==  "_") {
    			
                resultado.text = resultadoOperacion;
    			SistemaDejuego.instance.SetDie (true);
    		}

    	}


    public void DogInsideRadius()
    {

        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist < visionRadius)
        {


            if (aprobarCuenta)
            {

                contarParaPromedio = true;
                aprobarCuenta = false;

                AudioCtrl.instance.TrollShout(gameObject.transform);

                SistemaDejuego.instance.ObtenerEnemigoActual(this.gameObject);

                Walk();


                oki = true;

                GenerarOperacionesAritmeticas();



            }

        }

    }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, visionRadius);
        }


    }
