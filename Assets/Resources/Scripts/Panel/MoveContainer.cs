using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LK {
	
	public class MoveContainer : MonoBehaviour {
		[SerializeField] private float _speed = 0.05f;
		private Vector3 _toBePlaced;

		void Start(){
			_toBePlaced = new Vector3 (0f, transform.position.y, transform.position.z);
		}
		void Update () {
			transform.localPosition = Vector3.Lerp (transform.localPosition,_toBePlaced,_speed);
		}
	}
}

