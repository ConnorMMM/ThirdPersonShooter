using UnityEngine;

namespace ThirdPersonShooter.Entities
{
	public interface IEntity
	{
		public ref Stats Stats { get; }
		public Vector3 Position { get; }
	}
}