using UnityEngine;

namespace ThirdPersonShooter.Entities.Player
{
	public class PlayerEntity : MonoBehaviour, IEntity
	{
		public ref Stats Stats => ref stats;
		public Vector3 Position => transform.position;

		[SerializeField] private Stats stats;

		private void Awake()
		{
			stats.Start();
		}
	}
}