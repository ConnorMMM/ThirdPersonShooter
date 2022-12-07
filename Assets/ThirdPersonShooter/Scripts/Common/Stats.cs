using System;

using UnityEngine;

namespace ThirdPersonShooter
{
	[Serializable]
	public struct Stats
	{
		public float MaxHealth => maxHealth;
		public float Speed => speed * SpeedModifier;
		public float Range => range;
		public float Damage => damage;
		public float AttackRate => attackRate;
		
		public float Health { get; private set; }
		public float SpeedModifier { get; set; }

		public Action<float> onHealthChanged;
		public Action onDeath;
		
		[SerializeField, Min(1.0f)] private float maxHealth;
		[SerializeField, Range(.1f, 10f)] private float speed;
		[SerializeField] private float range;
		[SerializeField] private float damage;
		[SerializeField] private float attackRate;

		public void Start()
		{
			Health = maxHealth;
			SpeedModifier = 1.0f;
		}
	}
}