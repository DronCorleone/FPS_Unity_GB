using System;
using UnityEngine;

namespace Geekbrains
{
    public sealed class Aim : MonoBehaviour, ISetDamage, ISelectObj
    {
        public event Action OnPointChange;
		
        public float Hp = 100;
        public float Armor = 0;
        public float Shield = 1;

        private bool _isDead;
        //todo дописать поглащение урона - done
        public void SetDamage(InfoCollision info)
        {
            if (_isDead) return;

            float damage = info.Damage / Shield;

            if (Armor > 0)
            {
                Armor -= damage;
            }

            if (Armor <=0 && Hp > 0)
            {
                Hp -= damage;
            }

            if (Hp <= 0)
            {
                if (!TryGetComponent<Rigidbody>(out _))
                {
                    gameObject.AddComponent<Rigidbody>();
                }
                Destroy(gameObject, 10);

                OnPointChange?.Invoke();
                _isDead = true;
            }
        }

        public string GetMessage()
        {
            return gameObject.name;
        }
    }
}
