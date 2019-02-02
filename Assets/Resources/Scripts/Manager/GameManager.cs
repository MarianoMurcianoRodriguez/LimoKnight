using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LK{
	public class GameManager : Singleton<GameManager> {
		private TypeCharacter _characterSelected;
		private bool _isCharacterUnlocked;
		private float _penalization = 0f;
		private float _bonification = 0f;
		private float _multiplierByUser;

		public float MultiplierByUser() { return _multiplierByUser; }



		public void Start(){
			UIManager.Instance.DisableAndEnable (TypeUI.None, TypeUI.MainMenu);
			//LevelManager.Instance.LoadLevel(TypeLevel.Floor, TypeCharacter.BaseKnight);
		}

		///<summary>Este método se encarga de la transición del menu principal a la seleccion
		///de caracteres.</summary>
		public void SelectCharacter(){
			UIManager.Instance.DisableAndEnable(TypeUI.MainMenu, TypeUI.SelectCharacter);
		}

		///<summary>Este método es unico de cada boton para la seleccion de personajes, si no es el 
		/// BaseKnight se intentará ver si el XML del jugador de CharactersUnlocked ha sido acertado
		/// de lo contrario no se carga nada. El metodo recibe un string para que desde los botones
		/// se llame al mismo metodo, reutilizandolo.
		/// </summary>
		public void ShowInfoCharacterBaseKnight(string character){
			if (character.Equals ("BaseKnight"))
				_characterSelected = TypeCharacter.BaseKnight;
			else if (character.Equals ("SpeedKnight"))
				_characterSelected = TypeCharacter.SpeedKnight;

			InfoPersonaje ip = ManagerXML.Instance.LoadXMLInfoPersonajes (_characterSelected);
			_isCharacterUnlocked = ip.Unlocked.Equals ("True");
			UIManager.Instance.DisableAndEnable (TypeUI.None, TypeUI.SelectInfo);
			UIManager.Instance.PlaceInfo (ip, _characterSelected);
		}
			

		/// <summary>
		/// loads the dice menu. Deshabilitando antes los dos de selecion del personaje solo 
		/// si este personaje esta desbloqueado.
		/// </summary>
		public void LoadDiceMenu(){
			if (_isCharacterUnlocked) {
				UIManager.Instance.DisableAndEnable(TypeUI.SelectCharacter, TypeUI.None);
				UIManager.Instance.DisableAndEnable (TypeUI.SelectInfo, TypeUI.DiceMenu);
			}
		}
			
		public AtributosPersonaje GetAtributosPersonajeActual(){
			return ManagerXML.Instance.LoadXMLAtributosPersonajes (_characterSelected);
		}

		public void SetDicePenalization(){
			_penalization = 1.20f;
		}

		public void RecieveAtributes(int[] i, TypeMode mode){
			//Pasarle los atributos al CharacterManager, penalizaciones, modo par calcular al final la exp
			CharacterManager.Instance.InitializeAtributesOnStart (i);
			//Hacer cambio hacia la pantalla de dificultad
			UIManager.Instance.DisableAndEnable(TypeUI.DiceMenu, TypeUI.DificultMenu);
		}

		///<summary>Este método se encarga de la transición del menu principal a la seleccion
		///de personajes, deshabilitando el panel de dados.</summary>
		public void LoadGame(){
			Debug.Log ("Game Loading");
			UIManager.Instance.DisableAndEnable(TypeUI.DificultMenu, TypeUI.None);
			LevelManager.Instance.LoadLevel (TypeLevel.Floor, _characterSelected);
		}

		public float ObtainGlobalMultipliers(){
			return 1f + _bonification + _penalization;
		}

		/// <summary>
		/// Este metodo permite establecer la dificultad global del juego, las int deberan de ser
		/// type.Adventage y type.Disadventage para hacer algo cuando se instancien los jugadores
		/// o los personajes, no obstante lo importante es que segun el numero permiten saber 
		/// como deben de escalar los personajes
		/// </summary>
		public void PassInfo(List<int> adventages, List<int> disadventages){
			int number = adventages.Count + disadventages.Count;
			if (number == 2)
				_multiplierByUser = 0f;
			else if (number == 3)
				_multiplierByUser = 0.35f;
			else
				_multiplierByUser = 0.75f;
		}


		public void QuitGame(){
			Application.Quit ();
			Debug.Log ("En modo aplicacion no me eliminare!");
		}


	
	}
}
