using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;					//acceso a XMLSerializer que gestiona los XML
using System.Xml;								//atributos basicos de XML


namespace LK{
	/// <summary>
	/// Info personaje. Esta clase es la representancion de los personajes
	/// </summary>
	[System.Serializable]
	public class InfoPersonaje{
		public string NameKnight;
		public TypeCharacter TypeKnight;
		public string History;
		public string Unlocked;
	}
	/// <summary>
	/// Info personaje database. Esta clase es la que se mete en el XML y contiene la anterior.
	/// </summary>
	[System.Serializable]
	public class InfoPersonajeDatabase{
		[XmlArray("InfoPersonajes")]
		public List<InfoPersonaje> DataBase = new List<InfoPersonaje>();
	}
}


