using UnityEngine;

namespace Geekbrains
{
    [RequireComponent(typeof(Animator))]
    public class IKController : MonoBehaviour
    {
        [SerializeField] private float _rayLength = 1;
        [SerializeField] private LayerMask _rayLayer;

        private Animator _animator;
        private Transform _footRight;
        private Transform _footLeft;
        private Vector3 _rFposition;
        private Vector3 _lFposition;
        private Quaternion _rFrotation;
        private Quaternion _lFrotation;
        private float _weightFootR;
        private float _weightFootL;
        private float _smoothness = 0.5f;
        private float _offsetY = 0.1f;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _footRight = _animator.GetBoneTransform(HumanBodyBones.RightFoot);
            _footLeft = _animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        }

        private void Update()
        {
            if (Time.frameCount % 2 == 0)
            {
                var rPos = _footRight.TransformPoint(Vector3.zero);
                if (Physics.Raycast(rPos, Vector3.down,
                    out var rightHit, _rayLength, _rayLayer))
                {
                    _rFposition = Vector3.Lerp(_footRight.position, rightHit.point, _smoothness);
                    _rFrotation = Quaternion.FromToRotation(transform.up, rightHit.normal) *
                             transform.rotation;
                }
            }
            else if (Time.frameCount % 2 != 0)
            {
                var lPos = _footLeft.TransformPoint(Vector3.zero);
                if (Physics.Raycast(lPos, Vector3.down, out var leftHit, _rayLength, _rayLayer))
                {
                    _lFposition = Vector3.Lerp(_footLeft.position, leftHit.point, _smoothness);
                    _lFrotation = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
                }
            }
        }

        private void OnAnimatorIK()
        {
            _weightFootR = _animator.GetFloat("Right_Foot");
            _weightFootL = _animator.GetFloat("Left_Foot");

            _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, _weightFootR);
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, _weightFootL);

            _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, _weightFootR);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, _weightFootL);

            _animator.SetIKPosition(AvatarIKGoal.RightFoot,
                _rFposition + new Vector3(0, _offsetY, 0));
            _animator.SetIKPosition(AvatarIKGoal.LeftFoot,
                _lFposition + new Vector3(0, _offsetY, 0));

            _animator.SetIKRotation(AvatarIKGoal.RightFoot, _rFrotation);
            _animator.SetIKRotation(AvatarIKGoal.LeftFoot, _lFrotation);
        }
    }
}
