using UnityEngine;

namespace ThirdPersonShooter.UI
{
	public class PauseMenu : MenuBase
	{
		public override string ID => "Pause";

		public override void OnOpenMenu(UIManager _manager)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}

		public override void OnCloseMenu(UIManager _manager)
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}

		public void OnClickResume() => GameManager.Instance.TogglePaused();

		public void OnClickOptions() => UIManager.ShowMenu("Options");

		public void OnClickMainMenu() => GameManager.Instance.levelManager.UnloadGame(() => UIManager.ShowMenu("Main", false));
	}
}