using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LK {
	public class PlayerMovement : MonoBehaviour {

		private Rigidbody2D _rb2d;
		private BoxCollider2D _bc2d;
		private float _velocity = 0.075f;
		private Vector2 _movement;

		void Start () {
			_rb2d = GetComponent<Rigidbody2D> ();
			_bc2d = GetComponent<BoxCollider2D> ();
		}

		void FixedUpdate () {									//function frame-independent
			_movement.x = Input.GetAxisRaw("Horizontal");
			_movement.y = Input.GetAxisRaw("Vertical");
			_rb2d.MovePosition(Vector2.Lerp(_rb2d.position, _rb2d.position+_movement, _velocity));
		}

	}
}

