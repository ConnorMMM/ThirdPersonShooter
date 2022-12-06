using UnityEngine;

namespace ThirdPersonShooter
{
	public class DestroyOnLoad : MonoBehaviour
	{
		private void Awake()
		{
			if(!Application.isEditor)
				Destroy(this);
		}
	}
}