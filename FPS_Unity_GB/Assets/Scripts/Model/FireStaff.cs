using UnityEngine;

namespace Geekbrains
{
    public sealed class FireStaff : Weapon
    {
        public override void Fire()
        {
            if (CountAmmunition <= 0) return;

            var Ammo = Instantiate(Ammunition, _barrel.position, _barrel.rotation);
            Ammo.AddForce(_barrel.forward * _force);
            CountAmmunition--;
        }
    }
}
