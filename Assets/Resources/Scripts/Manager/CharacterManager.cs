using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LK {

	public enum TypeCharacter{
		None = -1,
		BaseKnight = 0, 
		SpeedKnight = 1,
		MisteriousKnight = 2, 
		KingKnight = 3,
	}

	public class CharacterManager : Singleton<CharacterManager> {
		/// <summary>
		/// Los sprites deben estar en modo individual y 2d-ui
		/// </summary>
		private Sprite[] _sprites = new Sprite[5-1];
		private GameObject _knight;

		private int _constitution; /**/
		private int _dexterity;
		private int _power;
		private int _agility;
		private int _luck;


		//Setters and getter when the player gets some item, this will update their values when
		//a character picks up a new item whose function is increase or reduce these atributtes
		public int Constitution{
			get { return _constitution; }
			set { _constitution = value; }
		}
		public int Dexterity{
			get { return _dexterity; }
			set { _dexterity = value; }
		}
		public int Power{
			get { return _power; }
			set { _power = value; }
		}
		public int Agility{
			get { return _agility; }
			set { _agility = value; }
		}
		public int Luck{
			get { return _luck; }
			set { _luck = value; }
		}



		/// <summary>
		/// Inicializa la instancia para eso se le deben de adjuntar todos los prefabs
		/// por tanto deben de hacerse todos en ese orden. Ademas los sprites deben de ser
		/// 2d -ui ; multiple .
		/// </summary>
		void Start () {  
			Sprite[] sprite = Resources.LoadAll<Sprite>("Sprites/BaseKnight") as Sprite[];
			_sprites [(int)TypeCharacter.BaseKnight] = sprite [0];
			_knight = Resources.Load ("Prefabs/Character") as GameObject;
		}

		/// <summary>
		/// Sets the atributes on the starts of the game.
		/// </summary>
		/// <param name="parametres">Parametres in the same ORDER</param>
		public void InitializeAtributesOnStart(int[] parametres){
			_constitution = parametres[0];
			_dexterity = parametres[1];
			_power = parametres[2];
			_agility = parametres[3];
			_luck = parametres[4];
		}
	
		/// <summary>
		/// Establece el personaje para jugar, este metodo lo llama LevelManager.
		/// No obstante hay que añadirle los datos a este script. 
		/// </summary>
		/// <returns>The character to play.</returns>
		/// <param name="character">Caballero para el modo de juego.</param>
		public GameObject GetCharacterToPlay(TypeCharacter character){
			_knight.GetComponent<SpriteRenderer> ().sprite = _sprites [(int)character];
			return _knight;
		}




	}
}
