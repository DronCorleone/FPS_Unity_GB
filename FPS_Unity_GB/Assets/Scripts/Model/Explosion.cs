using UnityEngine;


namespace Geekbrains
{
    public class Explosion : BaseObjectScene
    {
        public void Explode(Vector3 position, Quaternion rotation)
        {
            Instantiate(gameObject, position, rotation);
            Destroy(gameObject, gameObject.GetComponent<ParticleSystem>().main.duration);
            // Почему-то не работает уничтожение по данному таймеру.
            // Есть подозрение, что он выполняется до создания объекта.
        }
    }
}