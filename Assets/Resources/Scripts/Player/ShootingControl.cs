using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LK {
	public class ShootingControl : MonoBehaviour {
		private AtributeCharacter _ac;
		private float _currentTime;
		private float _delayShoots;
		private Transform _pointSpawn;


		public void Initialize() {
			_pointSpawn = transform.GetChild (1).gameObject.transform; 
			//obtenemos el puntero del spawn
			_ac = gameObject.GetComponent<AtributeCharacter> ();
			_currentTime = 0f;
			_delayShoots = _ac.DelayProjectiles;
		}

		// Update is called once per frame
		void LateUpdate () {
			if (_currentTime <= _delayShoots)
				_currentTime = _currentTime + Time.deltaTime;
			if (_currentTime > _delayShoots && Input.GetKey(KeyCode.Space)) {
				_currentTime = 0f;
				Shoot ();

			}
		}

		void Shoot(){
			Vector3 position = _pointSpawn.position;
			Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, _pointSpawn.localRotation.eulerAngles.z-90));
			GameObject go = PoolManager.Instance.RequestProjectile (TypeProjectile.Normal, _ac, position, rotation);
		}
	}
}

