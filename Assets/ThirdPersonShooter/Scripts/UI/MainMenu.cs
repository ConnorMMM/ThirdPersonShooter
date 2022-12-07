using UnityEngine;

namespace ThirdPersonShooter.UI
{
	public class MainMenu : MenuBase
	{
		public override string ID => "Main";

		public override void OnOpenMenu(UIManager _manager) => _manager.SetAudioListenerState(true);

		public override void OnCloseMenu(UIManager _manager) => _manager.SetAudioListenerState(false);

		public void OnClickPlay() => GameManager.Instance.levelManager.LoadGame(() => UIManager.ShowMenu("HUD", false));

		public void OnClickOptions() => UIManager.ShowMenu("Options");
		
		public void OnClickQuit() => GameManager.Instance.QuitGame();

	}
}