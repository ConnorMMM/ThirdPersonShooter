using UnityEngine;

namespace ThirdPersonShooter.UI
{
	public class OptionsMenu : MenuBase
	{
		public override string ID => "Options";

		[SerializeField] private VolumeSlider[] sliders;

		public override void OnOpenMenu(UIManager _manager)
		{
			foreach(VolumeSlider slider in sliders)
				slider.Activate();
		}

		public override void OnCloseMenu(UIManager _manager) => PlayerPrefs.Save();

		public void OnClickBack() => UIManager.HideMenu(ID);
	}
}