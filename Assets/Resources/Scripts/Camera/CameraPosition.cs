using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LK{

	public class CameraPosition : MonoBehaviour {

		private GameObject _player;
		private Camera _camera;
		private Vector3 _position;
		[SerializeField] private float _velocityTransition = 0.05f;
		void Start(){
			_camera = GetComponent<Camera> ();
		}

		public void SetPlayer(){
			_player = GameObject.FindGameObjectWithTag("Player");
		}

		void LateUpdate(){	//los movimientos de camara mejor hacerlos en el late update cuando todo se ha movido
			if (_player) {
				_position = Vector3.Lerp(transform.position, _player.transform.position, _velocityTransition);
				_position.z = -10f;																//anchor it at the -10z index so the map and the player is visible
				_camera.transform.position = _position;
			}
		}
	}
}

