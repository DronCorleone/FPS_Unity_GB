using UnityEngine;

namespace Geekbrains
{
    public sealed class InputController : BaseController, IExecute
    {
        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private int _mouseButton = (int)MouseButton.LeftButton;

        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
		
        public void Execute()
        {
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch(ServiceLocator.Resolve<Inventory>().FlashLight);
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                Weapon weapon = ServiceLocator.Resolve<Inventory>().SelectPreviousWeapon();
                ServiceLocator.Resolve<WeaponController>().On(weapon);
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                Weapon weapon = ServiceLocator.Resolve<Inventory>().SelectNextWeapon();
                ServiceLocator.Resolve<WeaponController>().On(weapon);
            }

            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectionOfWeapon(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectionOfWeapon(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SelectionOfWeapon(2);
            }

            if (Input.GetMouseButtonDown(_mouseButton))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().Fire();
                }
            }

            if (Input.GetKeyDown(_cancel))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<FlashLightController>().Off();
            }
        }

        private void SelectionOfWeapon(int i)
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            Weapon weapon = ServiceLocator.Resolve<Inventory>().SelectWeapon(i);
            ServiceLocator.Resolve<WeaponController>().On(weapon);
        }
    }
}
