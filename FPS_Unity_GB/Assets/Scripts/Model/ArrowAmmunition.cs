using UnityEngine;

namespace Geekbrains
{
    public class ArrowAmmunition : Ammunition
    {
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.transform.parent = collision.gameObject.transform;

            var tempObj = collision.gameObject.GetComponent<ISetDamage>();

            if (tempObj != null)
            {
                tempObj.SetDamage(new InfoCollision(_curDamage, collision.contacts[0], collision.transform,
                    Rigidbody.velocity));
            }
        }
    }
}
