﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 								 // Work with Serializable
using UnityEngine.UI;


/// <summary>
/// Todo lo que tiene que ver con la interfaz grafica
/// </summary>
[Serializable]
public class UI  {


	[Header("Textos de Informacion")]

	public Text txtPuntos;

	public Text txtBones;

	public Text txtCantEnemigos;

	public Text txtVidas;

	public Text textTimer;

	public Text texttimeOp;

	public Text textFallos;

    public Text textCheckPoint;

    public Text textMsjEnemigo;


	[Header("Textos de Habilidades")]

	public Text txtMsjgrlHabilidad;


	[Header("Imagenes de vida")]

	public GameObject[] vidasGo;


	[Header("Paneles de juego")]

	public GameObject pnMenuJuegoTerminado;

	public GameObject levelComplete;

	public GameObject panelPausa;


	[Header("Textos Zona intermedia")]

	public Text divisortxt;

	public Text restotxt;

	public Text resultadotxt;

	public Text dividendotxt;

	public Text msj;

    public Text operacionesRestantes;

	public GameObject panelMultiplo;

	public GameObject contenedorGrl;

	public GameObject contenedorGameOver;

	public GameObject contTryAgain;

}
