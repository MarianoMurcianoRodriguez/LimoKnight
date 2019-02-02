using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LK {
	public class Crosshair : MonoBehaviour {
		private float _angle;
		private Vector3 _mousePosition;
		private int _screenHeightFactor;
		private int _screenWidthFactor;

		void Start(){
			_screenWidthFactor = Screen.width/2;
			_screenHeightFactor = Screen.height/2;
		}

		void Update(){
			_mousePosition = Input.mousePosition;	
			//Esta posicion va de (0,0) a (anchura, altura) ergo hay que cambiarlo para que el hayan
			//distancias relativas o sino calcular el angulo es imposible
			_mousePosition.x = _mousePosition.x - _screenWidthFactor;
			_mousePosition.y = _mousePosition.y - _screenHeightFactor;
			//Calculamos el angulo desde ese punto al 0-0 (el jugador) por eso no hay que restar nada
			_angle = Mathf.Atan2 (_mousePosition.y, _mousePosition.x) * Mathf.Rad2Deg;
			//Si es 0 = 360, si es negativo es 360 - angulo
			if (_angle <= 0)
				_angle = Mathf.Abs (360+_angle);
			transform.rotation = Quaternion.Euler (0f, 0f, _angle);
			//mover al padre al Z implica que el circulito hijo gire.
		}


	}
}
