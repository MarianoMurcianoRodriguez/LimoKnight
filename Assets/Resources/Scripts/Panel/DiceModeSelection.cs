using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LK{
	public enum TypeMode{
		Normal = 0,
		Random = 1,
	}
	public class DiceModeSelection : MonoBehaviour {

		[SerializeField] private Text[] _diceValues;
		[SerializeField] private Text[] _finalValues;
		private int[] _atribBases;
		private AtributosPersonaje _ap;
		private bool _doneNormally = false;
		private bool _doneRandomly = false;
		private TypeMode _mode;
		private bool _initialized;
	
		void OnEnable(){
			_initialized = false;
		}

		void OnDisable(){
			_initialized = false;
		}

		public void Initialize(){
			_initialized = true;
			_ap = GameManager.Instance.GetAtributosPersonajeActual ();
			_atribBases = new int[] {_ap.Constitution, _ap.Dexterity, _ap.Power, _ap.Agility, _ap.Luck};
		}

		public void AsignValuesNormally(){
			if (!_doneNormally && _initialized) {
				for (int i = 0; i < _finalValues.Length; i++)
					_finalValues [i].text = Mathf.FloorToInt ((int.Parse (_diceValues [i].text)/100f *_atribBases [i] + _atribBases[i])).ToString ();					
				_doneNormally = true;
			} 
		}

		public void AsignValuesRandomly(){
			if (!_doneRandomly && _initialized) {
				List<int> list = new List<int> ();
				int random;
				for (int i = 0; i < _atribBases.Length; i++)
					list.Add (_atribBases [i]);
				for (int i = 0; i < _finalValues.Length; i++) {
					random = Random.Range (0, list.Count);
					_finalValues [i].text = Mathf.FloorToInt ((int.Parse (_diceValues [i].text)/100f * list [random] + list[random])).ToString ();
					list.RemoveAt (random);
				}
				_mode = TypeMode.Random;
				_doneRandomly = true;
			}
		}

		public void AtributesPhaseEnded(){
			if (_doneNormally || _doneRandomly) {
				int[] atributes = new int[_finalValues.Length];
				for (int i = 0; i < _finalValues.Length; i++) {
					atributes [i] = int.Parse(_finalValues [i].text);
				}
				GameManager.Instance.RecieveAtributes (atributes, _mode);
			}
		}


	}
}


