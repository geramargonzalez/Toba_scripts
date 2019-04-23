    using System;

    /// <summary>
    ///  Los datos que se van a guardar del juego.
    /// </summary>

    [Serializable]
    public class GameData 
    {
    
    	//Contador de puntos, vidas, nivel actual, cantidad de enemigos que quedan
    	public int puntos;
    	public int vidas;
    	public int nivel;
        public int nivelMaximo;
    	public int bones;
        public int promedioGrl;
        public int cantidadOperacionesPorNivel;

        // Aciertos generales
        public int sumasGrl;
        public int restaGrl;
        public int divisionGrl;
        public int aciertosMultiGrl;
        public int aciertosGrl;
        

        // Fallos generales
        public int sumasFallosGrl;
        public int restaFallosGrl;
        public int divisionFallosGrl;
        public int multiFallosGrl;
        public int fallosGrl;

        // Cantidad de Trolls
         public bool audioOn = true;
    	// Cantidad de Trolls
    	public int cantidadTrolls;
    	public int numParaPromedio;
        public int cantidadAnimXpantalla;

    	// Tiempo 
    	public float tiempoActual;
    	public int posActualEnemigo = 0;
    	public int fallos;

    	//Estadisticas de los niveles ...
    	public Nivel[] niveles;

    	//Valida si el usurio ya jugo
    	public bool[] operaRealizadas;
    	public bool yaJugo = false;

    	//Posicion del personaje
    	public float x = 3.3f;
    	public float y = -53.0f;
    	public float z = 0f;

        // Personaje, guardo los incrementos
    	public float jumpSpeed = 900f;
    	public float speedBoost = 30f;

    	//Orquitos/Animales
    	public bool[] orcosPorAnimales;
    	public bool[] bonesBool;
        public bool okTutOrquito;
        public bool tutorial = true;
        public bool primeraVez = true;
    	public int cantAnimalesConvertidos;
        public int totalCuentas = 87;


}
