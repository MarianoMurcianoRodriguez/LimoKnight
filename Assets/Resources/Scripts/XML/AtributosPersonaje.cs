using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;					//acceso a XMLSerializer que gestiona los XML
using System.Xml;								//atributos basicos de XML

using UnityEngine;
namespace LK {

	/// <summary>
	/// AtributosPersonaje contiene las estadisticas de cada personaje notese que los atributos 
	/// basicos no se cambian una vez se establecen. Los multiplicadores se aplican al resultado del 
	/// dado, los IncreaseByLevel son cuanto debe de incrementarse los atributos en base al nivel
	/// antes de empezar el juego. En el caso mas extremo algun atributo llegara a 10+(2.0*100+5*100) = 700
	/// </summary>
	[System.Serializable]
	public class AtributosPersonaje{
		public TypeCharacter TypeKnight;
		public int Experience;
		public int ActualLevel;
		public int Constitution;	//los atributos iniciales son 35, ninguno puede ser +10
		public int Dexterity;
		public int Power;
		public int Agility;
		public int Luck;
		public float[] IncreaseByLevel = new float[5];	//10 puntos a repartir, nunca mayor que 5 uno
	}
	/// <summary>
	/// Info personaje database. Esta clase es la que se mete en el XML y contiene la anterior.
	/// </summary>
	[System.Serializable]
	public class AtributosPersonajeDatabase{
		[XmlArray("AtributosPersonajes")]
		public List<AtributosPersonaje> DataBase = new List<AtributosPersonaje>();
	}
}
