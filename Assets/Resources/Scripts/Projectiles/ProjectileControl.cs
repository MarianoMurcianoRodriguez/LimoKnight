using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LK{


	public class ProjectileControl : MonoBehaviour {
		private TypeProjectile _typeProjectile;
		private AtributeCharacter _ac;
		private Rigidbody2D _rb;
		private float _velocity = 3f;

		public void SetType(TypeProjectile type){
			_typeProjectile = type;
		}
		public void SetAtributeCharacter(AtributeCharacter ac){
			_ac = ac;
		}

		void Start(){
			_rb = GetComponent<Rigidbody2D> ();

		}
		void Update(){
			_rb.velocity = transform.up * _velocity;

		}
	
		void OnTriggerEnter2D(Collider2D coll){
			if (coll.gameObject.tag == "Wall") {
				PoolManager.Instance.ReturnToProjectilePool (gameObject, _typeProjectile);
			} else if (coll.gameObject.tag == "Enemy") {
				PoolManager.Instance.ReturnToProjectilePool (gameObject, _typeProjectile);
				float damage = _ac.Damage;
				bool isCritic = false;
				if (Random.Range (0f, 1f) <= _ac.ChanceCritic) isCritic = true;
				if (isCritic) damage = damage + damage * _ac.ChanceCritic;
				coll.gameObject.GetComponent<AtributeEnemy> ().ApplyDamage (damage, isCritic);
			}
		}
	
	
	}
}

