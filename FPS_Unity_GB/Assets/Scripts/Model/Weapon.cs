using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
	public abstract class Weapon : BaseObjectScene
	{
		public Ammunition Ammunition;
        public int CountAmmunition = 999;

		[SerializeField] protected Transform _barrel;
		[SerializeField] protected float _force = 999;

		public abstract void Fire();
	}
}