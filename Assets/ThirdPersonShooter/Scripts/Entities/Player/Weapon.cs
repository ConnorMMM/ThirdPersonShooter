using JetBrains.Annotations;

using System;
using System.Collections;

using ThirdPersonShooter.Entities.AI;
using ThirdPersonShooter.Utilities;
using ThirdPersonShooter.VFX;

using UnityEngine;
using UnityEngine.InputSystem;

namespace ThirdPersonShooter.Entities.Player
{
	public class Weapon : MonoBehaviour
	{
		public Action<int> onBulletCountUpdate;
		
		[SerializeField] private InputActionReference shootAction;
		[SerializeField] private InputActionReference reloadAction;
		[SerializeField] private Transform shootPoint;
		[SerializeField] private BulletLine bulletLine;

		[CanBeNull] private IEntity player;

		private int bulletCount = 12;
		private bool canShoot = true;

		public void SetPlayer(IEntity _player) => player = _player;

		private void Update()
		{
			if(player != null && canShoot && shootAction.action.IsDown())
			{
				if(bulletCount > 0)
				{
					bulletCount--;
				
					bool didHit = Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hit, player.Stats.Range);

					if(didHit && hit.collider.TryGetComponent(out EnemyEntity entity))
					{
						entity.Stats.TakeDamage(player.Stats.Damage);
					}

					BulletLine newLine = Instantiate(bulletLine);
					newLine.Play(shootPoint.position, didHit ? hit.point : shootPoint.position + shootPoint.forward * player.Stats.Range, didHit);
					onBulletCountUpdate?.Invoke(bulletCount);
				}

				StartCoroutine(ShootCooldown_CR());
			}
		}

		private void OnEnable() => reloadAction.action.performed += OnReloadPerformed;

		private void OnDisable() => reloadAction.action.performed -= OnReloadPerformed;

		private IEnumerator ShootCooldown_CR()
		{
			canShoot = false;
			
			if(player != null)
				yield return new WaitForSeconds(player.Stats.AttackRate);

			canShoot = true;
		}

		private void OnReloadPerformed(InputAction.CallbackContext _context)
		{
			bulletCount = 12;
			onBulletCountUpdate?.Invoke(bulletCount);
		}
	}
}