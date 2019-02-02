using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LK{

	public enum TypeLevel{
		Floor = 1,
		Middle = 2, 
		Upper = 3, 
		Roof = 4,
	}

	public class LevelManager : Singleton<LevelManager>{

		private int _levelToLoad;
		private int _actualLevel;
		private LevelGenerator _levelGenerator;
		private LevelController _levelController;
		private float _expEarned;

		private Vector2 _roomSizeWorldUnits = new Vector2(22,52);
		private float _chanceWalkerChangeDir = 0.12f, _chanceWalkerSpawn = 0.05f;
		private float _chanceWalkerDestroy = 0.05f;
		private int _maxWalkers = 12;
		private float _percentToFill = 0.4f;

		void Start(){
			_levelGenerator = GetComponent<LevelGenerator> ();
			_levelController = GetComponent<LevelController> ();
			_expEarned = 0f;
		}

		/// <summary>
		/// Carga el nivel para el personaje especificado.
		/// </summary>
		/// <param name="level">Level.</param>
		/// <param name="character">Character.</param>
		public void LoadLevel(TypeLevel level, TypeCharacter character){
			switch (level) {
				case TypeLevel.Floor:
					_levelGenerator.StartLevel (_roomSizeWorldUnits, _chanceWalkerChangeDir, _chanceWalkerSpawn, 
												_chanceWalkerDestroy, _maxWalkers, _percentToFill, character);
					break;
				default:
					break;
			}
		}


	}


}

