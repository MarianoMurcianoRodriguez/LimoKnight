using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LK {

	public class DificultModeSelection : MonoBehaviour {

		[SerializeField] private Text _adventages;
		[SerializeField] private Text _disadventages;
		private List<int> _indexAdventages;
		private List<int> _indexDisadventages;
		private bool _selected;

		public void OnEnable(){
			_adventages.text = "";
			_disadventages.text = "";
			_selected = false;
			_indexAdventages = new List<int> ();
			_indexDisadventages = new List<int> ();
		}

		public void NormalMode(){
			if (!_selected) {
				_selected = true;
				//XMLGens
				_adventages.text = _adventages.text+"Soy una ventaja jojó\n";
				_disadventages.text = _disadventages.text+"Soy una desventaja jeje\n";
				//Volcar los identificadores de cada uno en los arrays, el gamemanager se los pasa
				//al personajeManager que ya sabra el bonus que debe de aplicar si recibe 2, 3 o 4
				GameManager.Instance.PassInfo (_indexAdventages, _indexDisadventages);
				Invoke("StartGame", 7f);
			}

		}

		public void HardMode(){
			if (!_selected) {
				_selected = true;
				//XMLGens
				_adventages.text = _adventages.text + "Soy una ventaja jojó\n";
				_disadventages.text = _disadventages.text + "Soy una desventaja jeje\n";
				_disadventages.text = _disadventages.text + "Soy una desventaja 2 jeje\n";
				//Volcar los identificadores de cada uno en los arrays, el gamemanager se los pasa
				//al personajeManager que ya sabra el bonus que debe de aplicar si recibe 2, 3 o 4
				GameManager.Instance.PassInfo (_indexAdventages, _indexDisadventages);
				Invoke ("StartGame", 7f);
			}
		}

		public void ExtremeMode(){
			if (!_selected) {
				_selected = true;
				//XMLGens
				_adventages.text =_adventages.text + "Soy una ventaja jojó\n";
				_adventages.text = _adventages.text + "Soy una ventaja 2 jojó\n";
				_disadventages.text =  _disadventages.text +"Soy una desventaja jeje\n";
				_disadventages.text = _disadventages.text + "Soy una desventaja 2 jeje\n";
				//Volcar los identificadores de cada uno en los arrays, el gamemanager se los pasa
				//al personajeManager que ya sabra el bonus que debe de aplicar si recibe 2, 3 o 4
				GameManager.Instance.PassInfo (_indexAdventages, _indexDisadventages);
				Invoke ("StartGame", 7f);
			}
		}

		public void StartGame(){
			GameManager.Instance.LoadGame ();
		}

	}
}


