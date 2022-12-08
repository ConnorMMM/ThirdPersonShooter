using ThirdPersonShooter.Entities.Player;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace ThirdPersonShooter.UI
{
	public class HUD : MenuBase
	{
		public override string ID => "HUD";

		private PlayerEntity player;

		[SerializeField] private Slider healthBar;
		[SerializeField] private TextMeshProUGUI scoreText;

		public override void OnOpenMenu(UIManager _manager)
		{
			player = GameManager.IsValid() ? GameManager.Instance.Player : FindObjectOfType<PlayerEntity>();

			player.Stats.onHealthChanged += OnHealthChanged;
			player.Stats.onDeath += OnPlayerDied;
			player.onScoreUpdated += OnScoreUpdated;

			healthBar.maxValue = player.Stats.MaxHealth;
			healthBar.value = player.Stats.Health;

			scoreText.text = "Score: 0";
		}

		public override void OnCloseMenu(UIManager _manager)
		{
			player.Stats.onHealthChanged -= OnHealthChanged;
			player.Stats.onDeath -= OnPlayerDied;
			player.onScoreUpdated -= OnScoreUpdated;
		}

		private void OnHealthChanged(float _health) => healthBar.value = _health;

		private void OnPlayerDied() => UIManager.ShowMenu("Game Over");

		private void OnScoreUpdated(int _score) => scoreText.text = $"Score: {_score}";
	}
}