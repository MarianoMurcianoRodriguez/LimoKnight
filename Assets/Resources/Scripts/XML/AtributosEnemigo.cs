using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;					//acceso a XMLSerializer que gestiona los XML
using System.Xml;								//atributos basicos de XML

namespace LK {
	/// <summary>
	/// Esta clase almacena la informacion relativa a los enemigos, a fin de cuentas solo necesitan
	/// sus atributos, y el IncreaseByLevel, ellos calcularan su nivel como 'nivel_personaje + nivel_usuario
	/// * factor_piso * factor_dificultad' esto permite que si el jugador encuentra algun objeto
	/// que sume estadisticas dentro del juego NO se sienta menos poderoso.
	/// 
	/// El atributo de Dexterity será transformado en defense, pues esto bloqueará tantos puntos de daño
	/// por golpe. Este atributo tendrá un escalado -el que sea- pero nunca bloqueara mas del 80%
	/// 
	/// La experiencia que ofrece se incrementa por el mismo factor, asi un bicho 10 veces mas poderoso
	/// es 10 veces mas worth.
	/// </summary>
	[System.Serializable]
	public class AtributosEnemigo{
		public TypeEnemy EnemyType;
		public int Constitution;	//los atributos iniciales son 20, ninguno puede ser +10
		public int Power;
		public int Agility;
		public int Defense;
		public int Experience;
		public float[] IncreaseByLevel = new float[4];	//6 puntos a repartir, nunca mayor que 2 uno
	}

	/// <summary>
	/// Info personaje database. Esta clase es la que se mete en el XML y contiene la anterior.
	/// </summary>
	[System.Serializable]
	public class AtributosEnemigoDatabase {
		[XmlArray("EstadisticasEnemigo")]
		public List<AtributosEnemigo> DataBase = new List<AtributosEnemigo>();
	}

}
