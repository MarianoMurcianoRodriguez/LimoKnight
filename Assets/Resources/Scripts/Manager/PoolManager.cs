using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LK {


	public enum TypeProjectile{
		Normal, 
	}
	/// <summary>
	/// Pool manager. 
	/// En total contiene 2 diccionarios donde son grupos de cosas que se pueden instanciar:
	/// Objetos - Enemigos - Proyectiles
	/// 
	/// 
	/// El objetivo es tener el primero el prefab generico obtenido del resource.load
	/// 
	/// En los segundos, la lista de gameobjects que vienen tras el instantiate, lo creamos oculto
	/// y lo devolvemos para que el que lo tenga que gestionar lo tenga, al mismo tiempo cuando 
	/// llegue destruirlo lo que se hará es devolverlo a la lista, desactivado, para poder reutilizarlo
	/// 
	/// SI hay 1000 objetos se han tenido que hacer 1000 instanciaciones pero posteriormente ¡se pueden 
	/// reutilizar! Esto se hace porque la operacion de destroy e instiantate reutilizada al extremo
	/// es muy costosa para la cpu
	/// 
	/// Probado: crear 10, requiere 10 instanciaciones. Borrar 10 y pedir otros 10, ¡NINGUNA!
	/// </summary>

	public class PoolManager : Singleton<PoolManager> {
		private Dictionary<TypeEnemy, GameObject> enemyDictionary;
		private Dictionary<TypeEnemy, GameObject> objectDictionary;
		private Dictionary<TypeProjectile, GameObject> projectileDictionary;

		private Dictionary<TypeEnemy, List<GameObject>> enemyPoolObject;
		private Dictionary<TypeEnemy, List<GameObject>> objectPoolObject;
		private Dictionary<TypeProjectile, List<GameObject>> projectilePoolObject;

		private int numProjectiles=0;

		protected override void Awake(){
			base.Awake ();
			enemyDictionary = new Dictionary<TypeEnemy, GameObject> ();
			objectDictionary = new Dictionary<TypeEnemy, GameObject> ();
			projectileDictionary = new Dictionary<TypeProjectile, GameObject> ();

			enemyPoolObject = new Dictionary<TypeEnemy, List<GameObject>> ();
			objectPoolObject = new Dictionary<TypeEnemy, List<GameObject>> ();
			projectilePoolObject = new Dictionary<TypeProjectile, List<GameObject>> ();
		}


		/// <summary>
		/// Devuelve un enemigo, si no existe su clave en el diccionario la crea y otro metodo lo creara
		/// </summary>
		/// <param name="typeEnemy">Type enemy.</param>
		public GameObject RequestEnemy(TypeEnemy typeEnemy) {
			if (!enemyDictionary.ContainsKey(typeEnemy)) {
				CreateDictionaryEnemy (typeEnemy);
				CreateEnemy (typeEnemy);
			} else if (enemyPoolObject [typeEnemy].Count == 0) {
				CreateEnemy (typeEnemy);
			}
			GameObject go = enemyPoolObject[typeEnemy][0];
			enemyPoolObject [typeEnemy].Remove (go);
			return go;
		}

		private void CreateDictionaryEnemy(TypeEnemy typeEnemy){
			GameObject enemyPrefab = null;
			switch (typeEnemy) {
			case TypeEnemy.None:
				enemyPrefab = Resources.Load ("Prefabs/Character") as GameObject;
				break;
			}
			enemyDictionary.Add (typeEnemy, enemyPrefab);
			enemyPoolObject.Add (typeEnemy, new List<GameObject> ());
		}

		/// <summary>
		/// Creates the enemy.
		/// </summary>
		/// <param name="typeEnemy">Type enemy.</param>
		private void CreateEnemy(TypeEnemy typeEnemy){
			GameObject enemy = null;
			enemy = Instantiate(enemyDictionary[typeEnemy]);
			enemy.gameObject.SetActive (false);
			enemyPoolObject [typeEnemy].Add (enemy);
		}

		/// <summary>
		/// Devuelve un objeto, see 'RequestEnemy'
		/// </summary>
		/// <param name="typeObject">Type object.</param>
		public GameObject RequestObject(TypeEnemy typeEnemy) {
			if (!objectDictionary.ContainsKey(typeEnemy)) {
				CreateDictionaryObject (typeEnemy);
				CreateObject (typeEnemy);
			} else if (objectPoolObject [typeEnemy].Count == 0) {
				CreateEnemy (typeEnemy);
			}
			GameObject go = objectPoolObject [typeEnemy] [0];
			objectPoolObject [typeEnemy].Remove (go);
			return go;
		}

		//TODO TODO
		private void CreateDictionaryObject(TypeEnemy typeEnemy){
			GameObject objectPrefab = null;
			switch (typeEnemy) {
			case TypeEnemy.None:
				objectPrefab = Resources.Load ("Prefabs/Character") as GameObject;
				break;
			}
			objectDictionary.Add (typeEnemy, objectPrefab);
			objectPoolObject.Add (typeEnemy, new List<GameObject> ());
		}


		/// <summary>
		/// Creates the object. 
		/// </summary>
		/// <param name="typeObject">Type object.</param>
		private void CreateObject(TypeEnemy typeEnemy){
			GameObject objeto = null;
			objeto = Instantiate(enemyDictionary[typeEnemy]);
			objeto.gameObject.SetActive (false);
			enemyPoolObject [typeEnemy].Add (objeto);
		}

		public void ReturnToEnemyPool(GameObject enemy, TypeEnemy typeEnemy){
			enemy.SetActive (false);
			enemyPoolObject [typeEnemy].Add (enemy);
		}

		//TODO CAMBIAR ESTO TODO

		/// <summary>
		/// Devuelve un projectil, see 'RequestEnemy'
		/// </summary>
		/// <param name="typeProjectile">Type projectile.</param>
		public GameObject RequestProjectile(TypeProjectile typeProjectile, AtributeCharacter _ac, Vector3 position, Quaternion rotation) {
			if (!projectileDictionary.ContainsKey(typeProjectile)) {
				CreateDictionaryProjectile (typeProjectile);
				CreateProjectile (typeProjectile);
			} else if (projectilePoolObject [typeProjectile].Count == 0) {
				CreateProjectile (typeProjectile);
			}
			string s = "Hay " + projectilePoolObject.Count+" projectiles, se extraera el "
						+projectilePoolObject[typeProjectile][0].name;
			Debug.Log (s);
			GameObject go = projectilePoolObject[typeProjectile][0];
			//Sin importar el tipo de bala le pasamos el unico dato variable actualizado:
			go.GetComponent<ProjectileControl>().SetAtributeCharacter(_ac);
			go.transform.position = position;
			go.transform.rotation = rotation;
			go.SetActive (true);
			projectilePoolObject [typeProjectile].Remove (go);
			return go;
		}

		//TODO CAMBIAR ESTO TODO


		private void CreateDictionaryProjectile(TypeProjectile typeProjectile){
			GameObject projectilePrefab = null;
			switch (typeProjectile) {
			case TypeProjectile.Normal:
				projectilePrefab = Resources.Load ("Prefabs/Shoots/Normal") as GameObject;
				break;
			}
			//Quizás los disparos deben de ser siempre el 'normal' pero añadiendo algun script
			projectileDictionary.Add (typeProjectile, projectilePrefab);
			projectilePoolObject.Add (typeProjectile, new List<GameObject> ());
		}


		/// <summary>
		/// Creates the projectile. 
		/// </summary>
		/// <param name="typeProjectile">Type projectile.</param>
		private void CreateProjectile(TypeProjectile typeProjectile){
			GameObject projectile = null;
			numProjectiles++;
			projectile = Instantiate(projectileDictionary[typeProjectile]);
			projectile.name = "Proyectil: " + numProjectiles.ToString ();
			projectile.GetComponent<ProjectileControl> ().SetType (typeProjectile);
			projectilePoolObject [typeProjectile].Add (projectile);
		}

		public void ReturnToProjectilePool(GameObject projectile, TypeProjectile typeProjectile){
			projectile.SetActive (false);
			projectilePoolObject [typeProjectile].Add (projectile);
		}


	}
}
