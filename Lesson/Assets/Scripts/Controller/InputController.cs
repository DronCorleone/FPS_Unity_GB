using UnityEngine;

namespace Geekbrains
{
    public sealed class InputController : BaseController, IExecute
    {
        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
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

            //todo реализовать выбор оружия по колесику мыши - done

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Inventory.SelectNextWeapon();
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                Inventory.SelectPreviousWeapon();
            }

            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                Inventory.RemoveWeapon();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Inventory.SelectWeapon(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Inventory.SelectWeapon(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Inventory.SelectWeapon(2);
            }

            if (Input.GetMouseButton(_mouseButton))
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

            if (Input.GetKeyDown(_reloadClip))
            {
                ServiceLocator.Resolve<WeaponController>().ReloadClip();
            }
        }
    }
}
