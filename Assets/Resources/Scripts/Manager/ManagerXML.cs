using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml.Serialization;					//acceso a XMLSerializer que gestiona los XML
using System.Xml;								//atributos basicos de XML
using System.IO;								//Operacion de entrada y salida para fichero


namespace LK {
	/// <summary>
	/// Manager XMl. Esta clase contiene las operaciones para guardar y escribir sobre diferentes
	/// ficheros XML, asi como para extraer datos.
	/// </summary>
	public class ManagerXML : Singleton<ManagerXML> {
		/// <summary>Contiene los personajes existentes y si estan bloqueados o no</summary>




		/// <summary>
		/// Funcion generica para crear un XML, dado que nunca tendremos que crearlo una vez
		/// todos esten creados (algo que pasara en el producto final) este metodo se puede
		/// reutiizar para crear los xmls a traves del editor
		/// </summary>
		public void CreateXMLInfoPersonajes(InfoPersonajeDatabase database){
			//el serializador tiene el tipo de lo que queremos guardar
			XmlSerializer serializer = new XmlSerializer(typeof(InfoPersonajeDatabase));
			FileStream stream = new FileStream(Application.dataPath
								+"/StreamingAssets/XML/InfoPersonaje.xml", FileMode.Create);
			//realizamos la escritura en ese fichero
			serializer.Serialize(stream, database);	
			stream.Close ();
		}

	
		/// <summary>
		/// Loads the XML info personajes. Este metodo solo se utiliza para el reseteo asi que es privado
		/// </summary>
		private InfoPersonajeDatabase LoadXMLInfoPersonajes(){
			XmlSerializer serializer = new XmlSerializer (typeof(InfoPersonajeDatabase));
			FileStream stream = new FileStream(Application.dataPath
				+"/StreamingAssets/XML/InfoPersonaje.xml",FileMode.Open);
			InfoPersonajeDatabase ipdb = serializer.Deserialize (stream) as InfoPersonajeDatabase;
			stream.Close ();
			return ipdb;
		}

		/// <summary>
		/// Loads the XML info personaje. Este metodo se puede utilizar desde el game manager
		/// para obtener la informacion de un personaje
		/// </summary>
		/// <returns>La informacion de un personaje en concreto.</returns>
		/// <param name="knight">Knight.</param>
		public InfoPersonaje LoadXMLInfoPersonajes(TypeCharacter knight){
			XmlSerializer serializer = new XmlSerializer (typeof(InfoPersonajeDatabase));
			FileStream stream = new FileStream(Application.dataPath
				+"/StreamingAssets/XML/InfoPersonaje.xml",FileMode.Open);
			InfoPersonajeDatabase ipdb = serializer.Deserialize (stream) as InfoPersonajeDatabase;
			stream.Close ();
			InfoPersonaje i = new InfoPersonaje();
			foreach (InfoPersonaje ip in ipdb.DataBase) {
				if (ip.TypeKnight == knight) {
					i = ip;
					break;
				}
			}
			return i;
		}


		/// <summary>
		/// Resetea el XML de INFOPersonaje.xml poniendo la etiqueta 'unlocked' a false para todos
		/// los caballeros salvo el de BaseKnight
		/// </summary>
		public void ResetXMLInfoPersonajes(){
			InfoPersonajeDatabase ipdb = LoadXMLInfoPersonajes ();
			for (int i = 1; i < ipdb.DataBase.Count; i++)
				ipdb.DataBase [i].Unlocked = "False";
			CreateXMLInfoPersonajes (ipdb);				//si existe lo sobrescribe
		}


/*FUNCIONES PARA ATRIBUTOPERSONAJES*/
		public void CreateXMLAtributosPersonajes(AtributosPersonajeDatabase database){
			//el serializador tiene el tipo de lo que queremos guardar
			XmlSerializer serializer = new XmlSerializer(typeof(AtributosPersonajeDatabase));
			FileStream stream = new FileStream(Application.dataPath
				+"/StreamingAssets/XML/AtributosPersonaje.xml", FileMode.Create);
			//realizamos la escritura en ese fichero
			serializer.Serialize(stream, database);	
			stream.Close ();
		}
		private AtributosPersonajeDatabase LoadXMLAtributosPersonajes(){
			XmlSerializer serializer = new XmlSerializer (typeof(AtributosPersonajeDatabase));
			FileStream stream = new FileStream(Application.dataPath
				+"/StreamingAssets/XML/AtributosPersonaje.xml",FileMode.Open);
			AtributosPersonajeDatabase apdb = serializer.Deserialize (stream) as AtributosPersonajeDatabase;
			stream.Close ();
			return apdb;
		}

		public AtributosPersonaje LoadXMLAtributosPersonajes(TypeCharacter knight){
			XmlSerializer serializer = new XmlSerializer (typeof(AtributosPersonajeDatabase));
			FileStream stream = new FileStream(Application.dataPath
				+"/StreamingAssets/XML/AtributosPersonaje.xml",FileMode.Open);
			AtributosPersonajeDatabase apdb = serializer.Deserialize (stream) as AtributosPersonajeDatabase;
			stream.Close ();
			AtributosPersonaje ap = new AtributosPersonaje();
			foreach (AtributosPersonaje apd in apdb.DataBase) {
				if (apd.TypeKnight == knight) {
					ap = apd;
					break;
				}
			}
			return ap;
		}

		public void ResetXMLAtributosPersonajes(){
			AtributosPersonajeDatabase apdb = LoadXMLAtributosPersonajes ();
			for (int i = 1; i < apdb.DataBase.Count; i++) {
				apdb.DataBase [i].Experience = 0;		// poner la exp al 0
				apdb.DataBase [i].ActualLevel = 0;		// poner de nivel el 0
			}
			CreateXMLAtributosPersonajes (apdb);				//si existe lo sobrescribe
		}

/**FUNCIONES PARA LevelEXP: No hacen falta métodos para resetear dado que es la misma que crear
		para eso utilizamos actualmente esta funcion:  
		/*int expRequired = 250;
			for (int i = 1; i <= 100; i++) {
				LevelExp le = new LevelExp ();
				le.Level = i;
				le.ExpRequired = expRequired;
				//7f was good enough
				//expRequired = expRequired + Mathf.RoundToInt(Mathf.Sqrt(expRequired*i/7f*1.85f));
				database.DataBase.Add (le);
			}*/
		
		private void CreateXMLLevelExp(LevelExpDatabase database){
			//el serializador tiene el tipo de lo que queremos guardar
			XmlSerializer serializer = new XmlSerializer(typeof(LevelExpDatabase));
			FileStream stream = new FileStream(Application.dataPath
				+"/StreamingAssets/XML/LevelExpDatabase.xml", FileMode.Create);
			//realizamos la escritura en ese fichero
			serializer.Serialize(stream, database);	
			stream.Close ();
		}

		public LevelExpDatabase LoadXMLLevelExp(){
			XmlSerializer serializer = new XmlSerializer (typeof(LevelExpDatabase));
			FileStream stream = new FileStream(Application.dataPath
				+"/StreamingAssets/XML/LevelExpDatabase.xml",FileMode.Open);
			LevelExpDatabase led = serializer.Deserialize (stream) as LevelExpDatabase;
			stream.Close ();
			return led;
		}

		/**FUNCIONES PARA ATRIBUTOSENEMIGO: No hace falta una para resetear, una vez esta creado
		sus valor es constante**/

		public void CreateXMLAtributosEnemigos(AtributosEnemigoDatabase database){
			//el serializador tiene el tipo de lo que queremos guardar
			XmlSerializer serializer = new XmlSerializer(typeof(AtributosEnemigoDatabase));
			FileStream stream = new FileStream(Application.dataPath
				+"/StreamingAssets/XML/AtributosEnemigoDatabase.xml", FileMode.Create);
			//realizamos la escritura en ese fichero
			serializer.Serialize(stream, database);	
			stream.Close ();
		}

		public AtributosEnemigo LoadXMLAtributosEnemigo(TypeEnemy typeEnemy){
			XmlSerializer serializer = new XmlSerializer (typeof(AtributosEnemigoDatabase));
			FileStream stream = new FileStream(Application.dataPath
				+"/StreamingAssets/XML/AtributosEnemigoDatabase.xml",FileMode.Open);
			AtributosEnemigoDatabase aed = serializer.Deserialize (stream) as AtributosEnemigoDatabase;
			stream.Close ();
			AtributosEnemigo ae = new AtributosEnemigo();
			foreach (AtributosEnemigo ae1 in aed.DataBase) {
				if (ae.EnemyType == typeEnemy) {
					ae = ae1;
					break;
				}
			}
			return ae;
		}



	}
}

