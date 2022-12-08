using System.Collections;

using UnityEngine;

namespace ThirdPersonShooter.VFX
{
	public class SinkOnDeath : MonoBehaviour
	{
		[SerializeField] private float sinkTime = 1f;
		[SerializeField] private float sinkAmount = 1f;
		[SerializeField] private Transform parent;

		public void StartSinking() => StartCoroutine(Sink_CR());

		private IEnumerator Sink_CR()
		{
			float time = 0;
			float initialY = parent.position.y;

			while(time < sinkTime)
			{
				parent.position = new Vector3(parent.position.x, Mathf.Lerp(initialY, initialY - sinkAmount, time / sinkTime), parent.position.z);

				yield return null;
				
				time += Time.deltaTime;
			}
			
			Destroy(parent.gameObject);
		}
	}
}