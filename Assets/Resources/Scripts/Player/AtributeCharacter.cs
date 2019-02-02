using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LK{
	/// <summary>
	/// Atribute character. Esta clase existe para todo caballero.
	/// </summary>
	public class AtributeCharacter : MonoBehaviour {
		private float _finalConstitution = -1f;
		private float _finalAgility = -1f;
		private float _finalDexterity = -1f;
		private float _finalAtack = -1f;
		private float _finalLuck = -1f;

		private float _exp;
		public float EXP{ get { return _exp;} }

		public int HP{ get { return (10 + Mathf.RoundToInt(_finalConstitution));} }
		public int Damage{ get { return Mathf.RoundToInt(_finalAtack); } }
		public int Energy{ get { return Mathf.RoundToInt((float)(_finalConstitution / 2.5f)); } }
		public float DelayProjectiles{ get { return 1 / (2.5f + _finalAgility / 200f); } }
		public float ChanceCritic{ get { return Mathf.Min(1f, 0.05f + _finalDexterity/300f); } }
		public float DamageCritic{ get { return 1.5f + _finalDexterity/250f; } }
		public float CharacterMultiplierExp{ get { return 1f + _finalLuck/250f; } }
		public float UpgradePool{ get { return Mathf.Min (0.8f, _finalLuck / 420f); } }


		public void Start(){
			_finalLuck = CharacterManager.Instance.Luck;
			_finalAtack = CharacterManager.Instance.Power;
			_finalAgility = CharacterManager.Instance.Agility;
			_finalDexterity = CharacterManager.Instance.Dexterity;
			_finalConstitution = CharacterManager.Instance.Constitution;
			_exp = 0f;
			//cuando todo está asignado se inicializa al shootingControl para que pueda tomar los datos
			gameObject.GetComponent<ShootingControl>().Initialize();
		}

		public void RefreshAtack(float totalAtack, float totalObjectAtack, float diceMultAtack, float finalMultAtack){
			_finalAtack = (totalAtack + totalObjectAtack) * (diceMultAtack + finalMultAtack);
		}

		public void RefreshConstitution(float totalConstitution, float totalObjectConstitution, float diceMultConstitution, float finalMultConstitution){
			_finalConstitution = (totalConstitution + totalObjectConstitution) * (diceMultConstitution + finalMultConstitution);
		}

		public void RefreshDexterity(float totalDexterity, float totalObjectDexterity, float diceMultDexterity, float finalMultDexterity){
			_finalDexterity = (totalDexterity + totalObjectDexterity) * (diceMultDexterity + finalMultDexterity);
		}

		public void RefreshAgility(float totalAgility, float totalObjectAgility, float diceMultAgility, float finalMultAgility){
			_finalAgility = (totalAgility + totalObjectAgility) * (diceMultAgility + finalMultAgility);
		}

		public void RefreshLuck(float totalLuck, float totalObjectLuck, float diceMultLuck, float finalMultLuck){
			_finalLuck = (totalLuck + totalObjectLuck) * (diceMultLuck + finalMultLuck);
		}

		public void RefreshExp(float exp){
			_exp = _exp + exp;
		}

	}
}
