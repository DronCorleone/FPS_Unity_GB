using UnityEngine;

namespace Geekbrains
{
    public sealed class Bullet : Ammunition
    {
        public Explosion Explosion;

        private void OnCollisionEnter(Collision collision)
        {
            Instantiate(Explosion, collision.contacts[0].point, Explosion.transform.rotation);

            var tempObj = collision.gameObject.GetComponent<ISetDamage>();

            if (tempObj != null)
            {
                tempObj.SetDamage(new InfoCollision(_curDamage, collision.contacts[0], collision.transform,
                    Rigidbody.velocity));
            }

            DestroyAmmunition();
        }
    }
}
