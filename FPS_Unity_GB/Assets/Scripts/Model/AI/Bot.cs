using System;
using UnityEngine;
using UnityEngine.AI;

namespace Geekbrains
{
    public sealed class Bot : BaseObjectScene
	{
		public float Hp = 100;
		public Vision Vision;
        public Transform Target { get; set; }
		public NavMeshAgent Agent { get; private set; }

        private StateBot _stateBot;
        private Vector3 _point;
        private Animator _animator;
        private float _waitTime = 3;
		private float _stoppingDistance = 1.25f;
        private float _attackDistance = 1.5f;
        private float _maxVisionDistance = 10;
        private string _isWalk = "IsWalk";
        private string _isAttack = "IsAttack";

        public event Action<Bot> OnDieChange;

        private StateBot StateBot
		{
			get => _stateBot;
			set
			{
				_stateBot = value;
				switch (value)
				{
					case StateBot.None:
						//Color = Color.white;
                        break;
					case StateBot.Patrol:
                        //Color = Color.green;
                        break;
					case StateBot.Inspection:
                        //Color = Color.yellow;
                        break;
					case StateBot.Detected:
                        //Color = Color.red;
                        break;
                    case StateBot.UnderDamage:
                        //Color = Color.blue;
                        break;
					case StateBot.Died:
                        //Color = Color.gray;
                        break;
					default:
                        //Color = Color.white;
                        break;
				}

			}
		}

		protected override void Awake()
		{
			base.Awake();
			Agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
		}

		private void OnEnable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange += SetDamage;

            //var headBot = GetComponentInChildren<HeadBot>();
            //if (headBot != null) headBot.OnApplyDamageChange += SetDamage;
        }

        private void OnDisable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange -= SetDamage;

            //var headBot = GetComponentInChildren<HeadBot>();
            //if (headBot != null) headBot.OnApplyDamageChange -= SetDamage;
        }

        public void Tick()
        {
	        if (StateBot == StateBot.Died) return;

			if (StateBot != StateBot.Detected)
			{
				if (!Agent.hasPath)
				{
					if (StateBot != StateBot.Inspection)
					{
						if (StateBot != StateBot.Patrol)
						{
							StateBot = StateBot.Patrol;
							_point = Patrol.GenericPoint(transform);
                            MovePoint(_point);
							Agent.stoppingDistance = 0;
						}
						else
						{
							if (Vector3.Distance(_point, transform.position) <= 1)
							{
								StateBot = StateBot.Inspection;
                                _animator.SetBool(_isWalk, false);
                                Invoke(nameof(ResetStateBot), _waitTime);
							}
						}
					}
				}

				if (Vision.VisionM(transform, Target))
				{
					StateBot = StateBot.Detected;
				}
			}
			else
			{
				if (Agent.stoppingDistance != _stoppingDistance)
				{
					Agent.stoppingDistance = _stoppingDistance;
				}
				if (Vision.VisionM(transform, Target) && Vector3.Distance(transform.position, Target.position) <= (_stoppingDistance + 1f))
				{
                    _animator.SetBool(_isWalk, false);
				}
				else
				{
					MovePoint(Target.position);
				}

                if (StateBot == StateBot.Detected && Vector3.Distance(transform.position, Target.position) <= _attackDistance)
                {
                    Attack();
                }

                if (StateBot == StateBot.Detected && Vector3.Distance(transform.position, Target.position) >= _maxVisionDistance)
                {
                    StateBot = StateBot.Patrol;
                    _point = Patrol.GenericPoint(transform);
                    MovePoint(_point);
                    Agent.stoppingDistance = 0;
                }
            }
        }

        private void ResetStateBot()
        {
	        StateBot = StateBot.None;
        }

		private void SetDamage(InfoCollision info)
		{
			if (Hp > 0)
			{
                StateBot = StateBot.UnderDamage;
				Hp -= info.Damage;
				return;
                // Данная реализация состояния получения урона работает только пока бот не видит игрока
                // TODO доработать
			}

			if (Hp <= 0)
			{
				StateBot = StateBot.Died;
				Agent.enabled = false;
				foreach (var child in GetComponentsInChildren<Transform>())
				{
					child.parent = null;

					var tempRbChild = child.GetComponent<Rigidbody>();
					if (!tempRbChild)
					{
						tempRbChild = child.gameObject.AddComponent<Rigidbody>();
					}
					//tempRbChild.AddForce(info.Dir * Random.Range(10, 300));
					
					Destroy(child.gameObject, 10);
				}

                OnDieChange?.Invoke(this);
            }
		}

		public void MovePoint(Vector3 point)
		{
			Agent.SetDestination(point);
            _animator.SetBool(_isWalk, true);
            _animator.SetBool(_isAttack, false);
        }

        public void Attack()
        {
            _animator.SetBool(_isWalk, false);
            _animator.SetBool(_isAttack, true);
        }
	}
}