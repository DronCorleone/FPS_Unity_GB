using UnityEngine;

namespace Geekbrains
{ 
    public class RagdollController : MonoBehaviour
    {
        public void Die()
        {
            SetKinematic(false);
            GetComponent<Animator>().enabled = false;
        }

        private void Start()
        {
            SetKinematic(true);
        }

        private void SetKinematic(bool newValue)
        {
            var bodies = GetComponentsInChildren<Rigidbody>();
            foreach (var body in bodies)
            {
                body.isKinematic = newValue;
            }
        }
    }
}