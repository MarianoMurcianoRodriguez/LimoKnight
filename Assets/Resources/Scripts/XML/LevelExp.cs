using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;					//acceso a XMLSerializer que gestiona los XML
using System.Xml;								//atributos basicos de XML
namespace LK{

	/// <summary>
	/// Level exp es una base de datos de cuanta experiencia es necesaria para subir, primero busca
	/// remos el siguiente al nivel del usuario, veremos que exp tiene y si hay que subirle de nivel
	/// se le resta y se realiza lo mismo hasta que la cantidad sea menor que la necesaria a subir.
	/// 
	/// Esta clase NO se tiene porque resetear en todo caso se ajustaran los valores a mano creando
	/// otro fichero (sobreescribiendo el XML que hubiese).
	/// </summary>
	[System.Serializable]
	public class LevelExp {
		public int Level;
		public int ExpRequired;
	}

	public class LevelExpDatabase{
		public List<LevelExp> DataBase = new List<LevelExp> ();
	}
}

