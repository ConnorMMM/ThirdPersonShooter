using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace ThirdPersonShooter.UI
{
	public class UIManager : Singleton<UIManager>
	{
		public static void ShowMenu(string _id, bool _additive = true) => Instance.ActiveMenu(_id, _additive);

		public static void HideMenu(string _id) => Instance.DeactivateMenu(_id);
		
		public InputSystemUIInputModule inputModule;

		[SerializeField] private AudioListener uiListener;
		[SerializeField] private AudioSource uiSource;
		[SerializeField] private MenuBase[] menus;

		private readonly Dictionary<string, MenuBase> menuDictionary = new Dictionary<string, MenuBase>();
		private readonly List<string> activeMenus = new List<string>();

		private void Start()
		{
			foreach(MenuBase menu in menus)
			{
				if(!menuDictionary.ContainsKey(menu.ID))
				{
					menuDictionary.Add(menu.ID, menu);
					if(menu.IsDefault)
					{
						activeMenus.Add(menu.ID);
						menu.SetVisible(true);
						menu.OnOpenMenu(this);
					}
					else
					{
						menu.SetVisible(false);
					}
				}
				else
				{
					Debug.Log($"Duplicate menu ID detected! ID: {menu.ID}");
				}
			}
		}

		public void SetAudioListenerState(bool _active) => uiListener.enabled = _active;

		public void PlaySound(AudioClip _clip) => uiSource.PlayOneShot(_clip);

		private void ActiveMenu(string _id, bool _additive = true)
		{
			if(!_additive)
			{
				while(activeMenus.Count > 0)
				{
					string id = activeMenus[0];
					menuDictionary[id].OnCloseMenu(this);
					menuDictionary[id].SetVisible(false);
					activeMenus.RemoveAt(0);
				}
			}
			
			menuDictionary[_id].SetVisible(true);
			menuDictionary[_id].OnOpenMenu(this);
			activeMenus.Add(_id);
		}

		private void DeactivateMenu(string _id)
		{
			if(activeMenus.Contains(_id))
			{
				activeMenus.Remove(_id);
				menuDictionary[_id].OnCloseMenu(this);
				menuDictionary[_id].SetVisible(false);
			}
		}
	}
}