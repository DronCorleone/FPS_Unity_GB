using UnityEngine;

namespace Geekbrains
{
    public abstract class MajicWeapon : BaseObjectScene
    {
        public Ammunition Ammunition;
        public int CountAmmunition = 999;

        [SerializeField] protected Transform _barrel;
        [SerializeField] protected float _force = 1000;


        public abstract void Fire();
    }
}
