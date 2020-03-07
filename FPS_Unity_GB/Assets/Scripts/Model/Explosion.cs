using UnityEngine;


namespace Geekbrains
{
    public class Explosion : BaseObjectScene
    {
        private void Start()
        {
            float mainDuration = gameObject.GetComponent<ParticleSystem>().main.duration;

            Destroy(gameObject, mainDuration);
        }
    }
}