using System;

using ThirdPersonShooter.UI;

using UnityEngine;
using UnityEngine.InputSystem;

namespace ThirdPersonShooter.Entities.Player
{
	public class PlayerEntity : MonoBehaviour, IEntity
	{
		public ref Stats Stats => ref stats;
		public Vector3 Position => transform.position;

		[SerializeField] private Stats stats;
		[SerializeField] private InputActionReference pauseAction;

		[Header("Components")]
		[SerializeField] private PlayerInput input;

		[SerializeField] private AudioSource hurtSource;
		[SerializeField] private AudioSource deathSource;
		
		private void Awake()
		{
			stats.Start();

			if(GameManager.IsValid())
				GameManager.Instance.Player = this;

			if(UIManager.IsValid())
				input.uiInputModule = UIManager.Instance.inputModule;
		}

		private void OnEnable() => pauseAction.action.performed += OnPausePerformed;

		private void OnDisable() => pauseAction.action.performed -= OnPausePerformed;

		private void OnPausePerformed(InputAction.CallbackContext _context)
		{
			if(GameManager.IsValid())
				GameManager.Instance.TogglePaused();
		}
	}
}