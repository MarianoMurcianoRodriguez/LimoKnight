using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LK {
	/// <summary>
	/// Esta clase se encarga de cargar ls atributos del enemigo en base a cuando se instancia
	/// para esto obtiene la informacion de la base de datos de que atributos por nivel  antes
	/// de asignarle un valor en base al del personaje * nivel actual + usuario * dificultad
	/// 
	/// Ademas, al igual que AtributeCharacter se encarga de saber exactamente cuando debe destruirse
	/// porque ha llegado su inevitable muerte. 
	/// </summary>
	public class AtributeEnemy : MonoBehaviour {
		private TypeEnemy _type;
		private int _levelEnemy;
		private float _multiplierByUserLevel;
		private float _multiplierByCharacterLevel;
		private float _hp;
		private int _power;
		private int _agility;
		private int _defense;
		private int _exp;

		private Text text;


		void InitializeValues(TypeEnemy type){
			_type = type;
			AtributosEnemigo ae = ManagerXML.Instance.LoadXMLAtributosEnemigo(type);
			//_levelEnemy = ManagerXML.Instance.LoadXMLInfoUser().Level;
			_hp = ae.Constitution;
			_power = ae.Power;
			_agility = ae.Agility;
			_defense = ae.Defense;
			_exp = ae.Experience;
			_multiplierByUserLevel = GameManager.Instance.MultiplierByUser ();
		}

		public void ApplyDamage(float damage, bool isCritic){
			float realDamage = Mathf.Max ((float)damage * 0.2f, (float)damage - _defense);
	
			Debug.Log("He recibido: "+damage+ " [CRITICO: "+isCritic+" ]");
			_hp = _hp - realDamage;
			if (_hp <= 0)
				return;
				//PoolManager.Instance.ReturnToEnemyPool (this, _type);
		}


	}
}

