using UnityEngine;

namespace ThirdPersonShooter.UI
{
	public abstract class MenuBase : MonoBehaviour
	{
		public bool IsDefault => isDefault;
		public bool IsVisible => gameObject.activeSelf;
		
		public abstract string ID { get; }

		[SerializeField] private bool isDefault;

		public void SetVisible(bool _visible) => gameObject.SetActive(_visible);
		
		public virtual void OnOpenMenu(UIManager _manager) { }
		public virtual void OnCloseMenu(UIManager _manager) { }
	}
}