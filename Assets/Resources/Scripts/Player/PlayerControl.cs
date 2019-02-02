using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LK {
	public class PlayerControl : MonoBehaviour {


		void Start () {
			CameraPosition cp = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraPosition> ();
			cp.SetPlayer ();
		}

	}
}

