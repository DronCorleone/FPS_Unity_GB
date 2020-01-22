using UnityEngine;

namespace Geekbrains
{
	public sealed class Inventory : IInitialization
	{
		private Weapon[] _weapons = new Weapon[5];
        private static int _weaponIndex = 0;
        private static int _minWeaponIndex = 0;
        private static int _maxWeaponIndex = 2;

		public Weapon[] Weapons => _weapons;

		public FlashLightModel FlashLight { get; private set; }

		public void Initialization()
		{
			_weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>().
				GetComponentsInChildren<Weapon>();

			foreach (var weapon in Weapons)
			{
				weapon.IsVisible = false;
			}

			FlashLight = Object.FindObjectOfType<FlashLightModel>();
			FlashLight.Switch(FlashLightActiveType.Off);
		}

        /// <summary>
        /// Выбор оружия по номеру
        /// </summary>
        /// <param name="i">Номер оружия</param>
        public static void SelectWeapon(int i)
        {
            if (i < _minWeaponIndex) i = _minWeaponIndex;
            if (i > _maxWeaponIndex) i = _maxWeaponIndex;
            ServiceLocator.Resolve<WeaponController>().Off();
            var tempWeapon = ServiceLocator.Resolve<Inventory>().Weapons[i];
            if (tempWeapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
                _weaponIndex = i;
            }
        }

        public static void SelectNextWeapon()
        {
            _weaponIndex++;
            SelectWeapon(_weaponIndex);
        }

        public static void SelectPreviousWeapon()
        {
            _weaponIndex--;
            SelectWeapon(_weaponIndex);
        }

        public static void RemoveWeapon()
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            _weaponIndex = 0;
        }
	}
}