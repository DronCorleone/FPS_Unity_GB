﻿using UnityEngine;


namespace Geekbrains
{
    public sealed class Controllers : IInitialization
    {
        private readonly IExecute[] _executeControllers;

        public int Length => _executeControllers.Length;

        public Controllers()
        {
            IMotor motor = new UnitMotor(ServiceLocatorMonoBehaviour.GetService<CharacterController>());
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new InputController());
            ServiceLocator.SetService(new SelectionController());
            ServiceLocator.SetService(new WeaponController());
            ServiceLocator.SetService(new Inventory());
            ServiceLocator.SetService(new BotController());
            
            _executeControllers = new IExecute[4];

            _executeControllers[0] = ServiceLocator.Resolve<FlashLightController>();

            _executeControllers[1] = ServiceLocator.Resolve<InputController>();
            
            _executeControllers[2] = ServiceLocator.Resolve<SelectionController>();
            
            _executeControllers[3] = ServiceLocator.Resolve<BotController>();
        }
        
        public IExecute this[int index] => _executeControllers[index];

        public void Initialization()
        {
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }
            
            ServiceLocator.Resolve<Inventory>().Initialization();
            ServiceLocator.Resolve<InputController>().On();
            ServiceLocator.Resolve<SelectionController>().On();
            ServiceLocator.Resolve<BotController>().On();
        }
    }
}
