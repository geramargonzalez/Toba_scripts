﻿using System.Collections;
using System.Collections.Generic;
using System;


/// <summary>
///  Los datos de cada nivel, para las estadistica general
/// 
/// </summary>

[Serializable]
public class Nivel{

	// Variables generales del nivel
	public int nivel;
	public bool unlocked;
	public int bonesStars;

    //Contador global y por cada cuenta aritmetica
    public int promedio;
	public int fallosPorNivel;
	public int aciertosPorNivel;

	// Fallos por cuentas
	public int fallosMultiplicacion;
	public int fallosSuma;
	public int fallosResta;
	public int fallosDivision;

	// Aciertos Por cuentas
	public int aciertosMultiplicacion;
	public int aciertosSuma;
	public int aciertosResta;
	public int aciertosDivision;


    public Nivel (int pnivel, bool pUnlocked, int pbone, int ppromedio,int pfallosPorNivel,int paciertosPorNivel){

        nivel = pnivel;
        unlocked = pUnlocked;
        bonesStars = pbone;
        promedio = ppromedio;
        fallosPorNivel = pfallosPorNivel;
        aciertosPorNivel = paciertosPorNivel;
    
    }

 



}
