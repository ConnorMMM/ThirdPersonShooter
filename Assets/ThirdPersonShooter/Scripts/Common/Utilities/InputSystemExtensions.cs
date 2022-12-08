using UnityEngine;
using UnityEngine.InputSystem;

namespace ThirdPersonShooter.Utilities
{
	public static class InputSystemExtensions
	{
		public static bool IsDown(this InputAction _action)
		{
			return Mathf.Approximately(_action.ReadValue<float>(), 1);
		}
	}
}