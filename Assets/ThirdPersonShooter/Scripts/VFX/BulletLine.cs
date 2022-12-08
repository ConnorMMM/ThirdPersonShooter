using System.Collections;

using UnityEngine;

namespace ThirdPersonShooter.VFX
{
	[RequireComponent(typeof(LineRenderer))]
	public class BulletLine : MonoBehaviour
	{
		[SerializeField] private GameObject shootParticles;
		[SerializeField] private GameObject hitParticles;
		[SerializeField] private float lineVisibilityLength = .1f;
		[SerializeField] private float effectLife = .1f;

		private new LineRenderer renderer;

		public void Play(Vector3 _start, Vector3 _end, bool _didHit)
		{
			renderer = gameObject.GetComponent<LineRenderer>();

			renderer.positionCount = 2;
			renderer.SetPosition(0, _start);
			renderer.SetPosition(1, _end);

			Instantiate(shootParticles, _start, Quaternion.identity, transform);

			if(_didHit)
				Instantiate(hitParticles, _end, Quaternion.identity, transform);

			StartCoroutine(DisableLine_CR());
			Destroy(gameObject, effectLife);
		}

		private IEnumerator DisableLine_CR()
		{
			yield return new WaitForSeconds(lineVisibilityLength);

			renderer.positionCount = 0;
		}
	}
}