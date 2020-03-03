using UnityEngine;

namespace Geekbrains
{
	public sealed class Inventory : IInitialization
	{
		private Weapon[] _weapons = new Weapon[3];
        private static int _weaponIndex = 0;
        private static int _maxWeaponIndex = 2;

		public Weapon[] Weapons => _weapons;

		//public FlashLightModel FlashLight { get; private set; }

		public void Initialization()
		{
			_weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>().
				GetComponentsInChildren<Weapon>();

			foreach (var weapon in Weapons)
			{
				weapon.IsVisible = false;
			}

			//FlashLight = Object.FindObjectOfType<FlashLightModel>();
			//FlashLight.Switch(FlashLightActiveType.Off);
		}

        /// <summary>
        /// Выбор оружия по номеру
        /// </summary>
        /// <param name="i">Номер оружия</param>
        public Weapon SelectWeapon(int i)
        {
            if (i < 0) i = 0;
            if (i >= Weapons.Length) i = Weapons.Length - 1;

            Weapon tempWeapon = ServiceLocator.Resolve<Inventory>().Weapons[i];

            if (tempWeapon != null)
            {
                _weaponIndex = i;
                return tempWeapon;
            }
            else return ServiceLocator.Resolve<Inventory>().Weapons[0];
        }

        public Weapon SelectNextWeapon()
        {
            _weaponIndex++;
            Weapon tempWeapon = SelectWeapon(_weaponIndex);
            return tempWeapon;
        }

        public Weapon SelectPreviousWeapon()
        {
            _weaponIndex--;
            Weapon tempWeapon = SelectWeapon(_weaponIndex);
            return tempWeapon;
        }
	}
}