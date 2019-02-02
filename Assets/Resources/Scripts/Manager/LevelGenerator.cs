using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LK{
	enum gridSpace {empty, floor, wall};
	public class LevelGenerator : MonoBehaviour {
		gridSpace[,] grid;
		int roomHeight, roomWidth;
		float worldUnitsInOneGridCell = 1;
		struct walker{
			public Vector2 dir;
			public Vector2 pos;
		}
		List<walker> walkers;

		//<summary>Variables utilizadas para determinar la creacion de un nivel</summary>
		[SerializeField] private Vector2 _roomSizeWorldUnits = new Vector2(22,52);
		[SerializeField] private float _chanceWalkerChangeDir = 0.12f, _chanceWalkerSpawn = 0.05f;
		[SerializeField] private float _chanceWalkerDestroy = 0.05f;
		[SerializeField] private int _maxWalkers = 12;
		[SerializeField] private float _percentToFill = 0.4f; 
		[SerializeField] private GameObject _wallObj, _floorObj;
		[SerializeField] private GameObject _levelHolder;


		// <summary>
		// Este metodo se utiliza de forma publica para proceder a la inicializacion del mapa
		//</summary>
		public void StartLevel (Vector2 roomSizeWorldUnits, float chanceWalkerChangeDir,  float chanceWalkerSpawn,
			float chanceWalkerDestroy, int maxWalkers, float percentToFill, TypeCharacter character) {
			StablishValueLevel (roomSizeWorldUnits, chanceWalkerChangeDir, chanceWalkerSpawn, chanceWalkerDestroy, maxWalkers, percentToFill);
			Setup();
			CreateFloors();
			CreateExtraFloors ();
			CreateWalls ();
			//RemoveSingleWalls();
			SpawnLevel();
			SpawnPlayer (character);
		}
			
		// <summary>
		// Este metodo inicializa los valores con los que se procederá a crear el grid
		//</summary>
		void StablishValueLevel(Vector2 roomSizeWorldUnits, float chanceWalkerChangeDir,  float chanceWalkerSpawn,
			float chanceWalkerDestroy, int maxWalkers, float percentToFill){
			_roomSizeWorldUnits = roomSizeWorldUnits;
			_chanceWalkerSpawn = chanceWalkerSpawn;
			_chanceWalkerChangeDir = chanceWalkerChangeDir;
			_chanceWalkerDestroy = chanceWalkerDestroy;
			_maxWalkers = maxWalkers;
			_percentToFill = percentToFill;
		}

		// <summary>
		// Este metodo prepara el grid y crea el primer walker.
		//</summary>
		void Setup(){
			//find grid size
			roomHeight = Mathf.RoundToInt(_roomSizeWorldUnits.x / worldUnitsInOneGridCell);
			roomWidth = Mathf.RoundToInt(_roomSizeWorldUnits.y / worldUnitsInOneGridCell);
			//create grid
			grid = new gridSpace[roomWidth,roomHeight];
			//set grid's default state
			for (int x = 0; x < roomWidth-1; x++){
				for (int y = 0; y < roomHeight-1; y++){
					//make every cell "empty"
					grid[x,y] = gridSpace.empty;
				}
			}
			//set first walker
			//init list
			walkers = new List<walker>();
			//create a walker 
			walker newWalker = new walker();
			newWalker.dir = RandomDirection();
			//find center of grid
			Vector2 spawnPos = new Vector2(Mathf.RoundToInt(roomWidth/ 2.0f),
				Mathf.RoundToInt(roomHeight/ 2.0f));
			newWalker.pos = spawnPos;
			//add walker to list
			walkers.Add(newWalker);
		}

		// <summary>
		// Este bucle se encarga de la logica principal, cada walker pondra un suelo donde esté, a continuacion se mira si hay que destruir uno, sino
		// para cada walker se establece si debe de cambiar su direccion o no, o si debe de spawnear a otro o no
		//</summary>
		void CreateFloors(){
			int iterations = 0;//loop will not run forever
			do{
				//create floor at position of every walker
				foreach (walker myWalker in walkers){
					grid[(int)myWalker.pos.x,(int)myWalker.pos.y] = gridSpace.floor;
				}
				//chance: destroy walker
				int numberChecks = walkers.Count; //might modify count while in this loop
				for (int i = 0; i < numberChecks; i++){
					//only if its not the only one, and at a low chance
					if (Random.value < _chanceWalkerDestroy && walkers.Count > 1){
						walkers.RemoveAt(i);
						break; //only destroy one per iteration
					}
				}
				//chance: walker pick new direction
				for (int i = 0; i < walkers.Count; i++){
					if (Random.value < _chanceWalkerChangeDir){
						walker thisWalker = walkers[i];
						thisWalker.dir = RandomDirection();
						walkers[i] = thisWalker;
					}
				}
				//chance: spawn new walker
				numberChecks = walkers.Count; //might modify count while in this loop
				for (int i = 0; i < numberChecks; i++){
					//only if # of walkers < max, and at a low chance
					if (Random.value < _chanceWalkerSpawn && walkers.Count < _maxWalkers){
						//create a walker 
						walker newWalker = new walker();
						newWalker.dir = RandomDirection();
						newWalker.pos = walkers[i].pos;
						walkers.Add(newWalker);
					}
				}
				//move walkers
				for (int i = 0; i < walkers.Count; i++){
					walker thisWalker = walkers[i];
					thisWalker.pos += thisWalker.dir;
					walkers[i] = thisWalker;				
				}
				//avoid boarder of grid
				for (int i =0; i < walkers.Count; i++){
					walker thisWalker = walkers[i];
					//clamp x,y to leave a 1 space boarder: leave room for walls
					thisWalker.pos.x = Mathf.Clamp(thisWalker.pos.x, 2, roomWidth-3);
					thisWalker.pos.y = Mathf.Clamp(thisWalker.pos.y, 2, roomHeight-3);
					walkers[i] = thisWalker;
				}
				//check to exit loop
				if ((float)NumberOfFloors() / (float)grid.Length > _percentToFill){
					break;
				}
				iterations++;
			}while(iterations < 100000);
		}


		// <summary>
		// Este metodo se utiliza para, una vez creado el mapa aumentar el grosor del terreno jugable  si una celda esta rodeada por 4 o 5 espacios
		//</summary>
		void CreateExtraFloors(){
			gridSpace[,] grid2 = grid;
			int count;
			for (int x = 1; x < roomWidth-1; x++){
				for (int y = 1; y < roomHeight-1; y++){
					//if theres a floor, check the spaces around it
					count = CountFloorAround(x,y,gridSpace.floor);
					if (grid[x,y] == gridSpace.empty && count>=4 && count<=5){
						grid2[x,y]=gridSpace.floor;
					}
				}
			}
			grid = grid2;
		}

		// <summary>
		// Este metodo recorre todo el grid para determinar si junto a un suelo hay un espacio y entonces lo pone como muro.
		//</summary>
	void CreateWalls(){
			//loop though every grid space
			for (int x = 0; x < roomWidth-1; x++){
				for (int y = 0; y < roomHeight-1; y++){
					//if theres a floor, check the spaces around it
					if (grid[x,y] == gridSpace.floor){
						//if any surrounding spaces are empty, place a wall
						if (grid[x,y+1] == gridSpace.empty){
							grid[x,y+1] = gridSpace.wall;
						}
						if (grid[x,y-1] == gridSpace.empty){
							grid[x,y-1] = gridSpace.wall;
						}
						if (grid[x+1,y] == gridSpace.empty){
							grid[x+1,y] = gridSpace.wall;
						}
						if (grid[x-1,y] == gridSpace.empty){
							grid[x-1,y] = gridSpace.wall;
						}
					}
				}
			}
		}

		// <summary>
		// Este metodo elimina la mayoria de los pasillos de un solo bloque de longitud.
		//</summary>
		void RemoveSingleWalls(){
			//loop though every grid space
			for (int x = 0; x < roomWidth-1; x++){
				for (int y = 0; y < roomHeight-1; y++){
					//if theres a wall, check the spaces around it
					if (grid[x,y] == gridSpace.wall){
						//assume all space around wall are floors
						bool allFloors = true;
						//check each side to see if they are all floors
						for (int checkX = -1; checkX <= 1 ; checkX++){
							for (int checkY = -1; checkY <= 1; checkY++){
								if (x + checkX < 0 || x + checkX > roomWidth - 1 || 
									y + checkY < 0 || y + checkY > roomHeight - 1){
									//skip checks that are out of range
									continue;
								}
								if ((checkX != 0 && checkY != 0) || (checkX == 0 && checkY == 0)){
									//skip corners and center
									continue;
								}
								if (grid[x + checkX,y+checkY] != gridSpace.floor){
									allFloors = false;
								}
							}
						}
						if (allFloors){
							grid[x,y] = gridSpace.floor;
						}
					}
				}
			}
		}

		// <summary>
		// Este método transforma el grid a instancias en la escena, construyendo los objetos.
		//</summary>
		void SpawnLevel(){
			for (int x = 0; x < roomWidth; x++){
				for (int y = 0; y < roomHeight; y++){
					switch(grid[x,y]){
					case gridSpace.empty:
						break;
					case gridSpace.floor:
						Spawn(x,y,_floorObj);
						break;
					case gridSpace.wall:

						Spawn(x,y,_wallObj);
						break;
					}
				}
			}
		}
		// <summary>
		// Este metodo permite devolver una de las cuatro posibles direcciones escogiendo un numero entre [0-3]
		//</summary>
		private Vector2 RandomDirection(){
			//pick random int between 0 and 3
			int choice = Mathf.FloorToInt(Random.value * 3.99f);
			//use that int to chose a direction
			switch (choice){
			case 0:
				return Vector2.down;
			case 1:
				return Vector2.left;
			case 2:
				return Vector2.up;
			default:
				return Vector2.right;
			}
		}
		// <summary>
		// Este metodo devuelve el numero de espacios en el mapa que son suelo.
		//</summary>
		private int NumberOfFloors(){
			int count = 0;
			foreach (gridSpace space in grid){
				if (space == gridSpace.floor){
					count++;
				}
			}
			return count;
		}
		// <summary>
		//Esta función coloca un objeto en una posición determinada en el mapa. A traves del calculo de un offset (para el centro del mismo).
		//</summary>
		void Spawn(float x, float y, GameObject toSpawn){
			//find the position to spawn
			Vector2 offset = _roomSizeWorldUnits / 2.0f;
			Vector2 spawnPos = new Vector2(x,y) * worldUnitsInOneGridCell - offset;
			//spawn object
			GameObject go = (GameObject) Instantiate(toSpawn, spawnPos, Quaternion.identity);
			go.transform.SetParent (_levelHolder.transform);
			go.name = toSpawn.name +" (" + x.ToString() + "-" + y.ToString() + ")";
		}

		private int CountFloorAround(int i, int j, gridSpace patronBuscar){
			int number = 0;
				if (grid [i - 1, j - 1] == patronBuscar)
					number++;
				if (grid [i - 1, j] == patronBuscar)
					number++;
				if (grid [i, j - 1] == patronBuscar)
					number++;
				if (grid [i, j + 1] == patronBuscar)
					number++;
				if (grid [i + 1, j] == patronBuscar)
					number++;
				if (grid [i, j + 1] == patronBuscar)
					number++;
				if (grid [i + 1, j + 1] == patronBuscar)
					number++;
			return number;
		}

		void SpawnPlayer(TypeCharacter character){
			bool instantiated = false;
			for (int i = 0; i < roomWidth; i++) {
				for (int j = 0; j < roomHeight; j++) {
					if (grid [i, j] == gridSpace.floor) {
						Vector2 offset = _roomSizeWorldUnits / 2.0f;
						Vector2 spawnPos = new Vector2 (i, j) * worldUnitsInOneGridCell - offset;
						GameObject.Instantiate (CharacterManager.Instance.GetCharacterToPlay(character), spawnPos, Quaternion.identity);
						instantiated = true;
						break;
					}
				}
				if (instantiated)
					break;
			}
		}

	}
}
