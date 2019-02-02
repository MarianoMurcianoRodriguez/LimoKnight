using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LK{

	public class AtributeLoader : MonoBehaviour {
		[SerializeField] private Text _levelValue;
		[SerializeField] private Text _constValue;
		[SerializeField] private Text _dextValue;
		[SerializeField] private Text _powerValue;
		[SerializeField] private Text _agilityValue;
		[SerializeField] private Text _luckValue;		

		public void Start(){
			AtributosPersonaje ap = GameManager.Instance.GetAtributosPersonajeActual ();
				_levelValue.text = ap.ActualLevel.ToString ();
				_constValue.text = (ap.Constitution + (ap.ActualLevel * ap.IncreaseByLevel [0])).ToString ();
				_dextValue.text = (ap.Dexterity + (ap.ActualLevel * ap.IncreaseByLevel [1])).ToString ();
				_powerValue.text = (ap.Power + (ap.ActualLevel * ap.IncreaseByLevel [2])).ToString ();
				_agilityValue.text = (ap.Agility + (ap.ActualLevel * ap.IncreaseByLevel [3])).ToString ();
				_luckValue.text = (ap.Luck + (ap.ActualLevel * ap.IncreaseByLevel [4])).ToString ();
		}

	}
}

