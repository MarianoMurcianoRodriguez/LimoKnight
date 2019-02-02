using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LK {
	public class ThrowDices : MonoBehaviour {
		[SerializeField] private Text[] _atributes;
		[SerializeField] private Text _advise;
		private int _timesPressed = 0;
		private int _minDiceValue = 50;	
		private int _maxDiceValue = 100;
		
		public void Start(){
			//GameManager.Instance.GetGlobalUserLevel ();
		}

		public void ThrowDicesRandomly(){
			if (_timesPressed == 1) {
				_advise.text = "Desafiar a la suerte = -10% exp en la partida";
				_timesPressed++;
			} else {
				Debug.Log ("Lanzado dado");
				if (_timesPressed==2)	//desafié a la suerte
					GameManager.Instance.SetDicePenalization();
				_timesPressed++;
				float time = 0.0f;
				float wait = 10f;
				int randomNumber;
				for (int i = 0; i < _atributes.Length; i++) {
						randomNumber = Mathf.RoundToInt ((Random.value*_minDiceValue) + (_maxDiceValue-_minDiceValue));
						_atributes [i].text = randomNumber.ToString ();
					}
				}
			}


		}
}

