using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LK{

	public class LevelController : MonoBehaviour {

		private bool _onProcess;
		private float _timePassed;
		private AtributeCharacter ac;

		public void Start(){
			ac = GetComponent<AtributeCharacter> ();
		}

		public void StartLevelControll(){
			_onProcess = true;
			_timePassed = 0f;
		}

		public void Update(){
			if (_onProcess)
				_timePassed = _timePassed + Time.deltaTime;
		}

		public void EndedGame(){
			_onProcess = false;
			float multiplier = GameManager.Instance.ObtainGlobalMultipliers ();
			float exp = ac.EXP;
			//GameManager.Instance.AddCharacterExp(ac.Exp*ac.CharacterMultiplierExp));

		}




	}
}

