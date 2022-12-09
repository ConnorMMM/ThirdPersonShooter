using System;

using ThirdPersonShooter.UI;

using UnityEngine;
using UnityEngine.InputSystem;

namespace ThirdPersonShooter.Entities.Player
{
	public class PlayerEntity : MonoBehaviour, IEntity
	{
		public ref Stats Stats => ref stats;
		
		public ref Weapon Weapon => ref weapon;
		
		public Vector3 Position => transform.position;

		public Action<int> onScoreUpdated;

		[SerializeField] private Stats stats;
		[SerializeField] private InputActionReference pauseAction;

		[Header("Components")]
		[SerializeField] private PlayerInput input;
		[SerializeField] private Weapon weapon;
		[SerializeField] private AudioSource hurtSource;
		[SerializeField] private AudioSource deathSource;
		
		private int score;

		public void AddScore(int _value)
		{
			score += _value;
			onScoreUpdated?.Invoke(score);
		}

		private void Awake()
		{
			stats.Start();
			stats.onDeath += OnDeath;
			stats.onHealthChanged += OnHealthChanged;
			
			weapon.SetPlayer(this);

			if(GameManager.IsValid())
				GameManager.Instance.Player = this;

			if(UIManager.IsValid())
				input.uiInputModule = UIManager.Instance.inputModule;
		}

		private void OnDestroy()
		{
			stats.onDeath -= OnDeath;
			stats.onHealthChanged -= OnHealthChanged;
		}

		private void OnEnable() => pauseAction.action.performed += OnPausePerformed;

		private void OnDisable() => pauseAction.action.performed -= OnPausePerformed;

		private void OnPausePerformed(InputAction.CallbackContext _context)
		{
			if(GameManager.IsValid())
				GameManager.Instance.TogglePaused();
		}

		private void OnDeath() => deathSource.Play();

		private void OnHealthChanged(float _health) => hurtSource.Play();
	}
}