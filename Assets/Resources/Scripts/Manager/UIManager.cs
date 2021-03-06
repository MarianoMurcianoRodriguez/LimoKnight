using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LK {

	public enum TypeUI {
		None = -1, 
		MainMenu = 0,
		SelectCharacter = 1,
		SelectInfo = 2,
		DiceMenu = 3,
		DificultMenu = 4,
		OptionPanel = 5, 
	}

	/// <summary>
	/// User interface manager.
	/// </summary>
	public class UIManager : Singleton<UIManager> {
		[SerializeField] private GameObject[] _uiPanels;
		private Text _infoCharacter;
		private TypeCharacter lastInfoShowed = TypeCharacter.None;

		/// <summary>
		/// Este metodo carga uno a uno todos los paneles que hay en la escena y estan ocultos
		/// deben de ser introducidos en el mismo orden que en el que se declaran en el typeUI
		/// </summary>
		void Start(){
			if (_uiPanels.Length <= 0)
				Debug.LogWarning ("Load UIpanels in UI.Manager and for each TypeUI enum in the SAME order");
			_infoCharacter = _uiPanels [(int)TypeUI.SelectInfo].GetComponentInChildren<Text> ();	
		}



		/// <summary>
		/// Ocultamos un panel y activamos otro
		/// </summary>
		/// <param name="toHide">El panel a ocultar</param>
		/// <param name="toEnable">El panel a habilitar</param>
		public void DisableAndEnable(TypeUI toHide, TypeUI toEnable){
			if (toHide != TypeUI.None)
				_uiPanels [(int)toHide].gameObject.SetActive (false);
			if (toEnable != TypeUI.None)
				_uiPanels [(int)toEnable].gameObject.SetActive (true);
		}

		/// <summary>
		/// Pone la información del caballero en cuestion sí esta desbloqueado y sí no ha sido 
		/// el mismo que se seleccionó previamente.
		/// </summary>
		/// <param name="character">Caballero a mostrar info.</param>
		public void PlaceInfo(InfoPersonaje ip, TypeCharacter character){
			if (character != lastInfoShowed) {
				lastInfoShowed = character;
				if (ip.Unlocked.Equals ("True")) {
					_infoCharacter.text = "\n\n"+ip.NameKnight+": "+ip.History;
				} else {
					_infoCharacter.text = "This Limo was not discovered...yet, you may play for a" +
						" little more";
				}
			}
		}

	}
}

