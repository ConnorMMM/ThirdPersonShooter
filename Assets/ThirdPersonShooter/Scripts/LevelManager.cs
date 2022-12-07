using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ThirdPersonShooter
{
	public class LevelManager : MonoBehaviour
	{
		[SerializeField] private string ui;
		[SerializeField] private string game;

		public void LoadUI(Action _callback = null)
		{
			StartCoroutine(LoadScene_CR(ui, LoadSceneMode.Additive, _callback));
		}

		public void LoadGame(Action _callback = null)
		{
			StartCoroutine(LoadScene_CR(game, LoadSceneMode.Additive, _callback, true));
		}
		
		public void UnloadGame(Action _callback = null)
		{
			StartCoroutine(UnloadScene_CR(game, _callback));
		}

		private IEnumerator LoadScene_CR(string _scene, LoadSceneMode _loadSceneMode = LoadSceneMode.Single, Action _callback = null, bool _makeAction = false)
		{
			yield return SceneManager.LoadSceneAsync(_scene, _loadSceneMode);

			if(_makeAction)
				SceneManager.SetActiveScene(SceneManager.GetSceneByName(_scene));
			
			_callback?.Invoke();
		}
		
		private IEnumerator UnloadScene_CR(string _scene, Action _callback = null)
		{
			yield return SceneManager.UnloadSceneAsync(_scene);

			_callback?.Invoke();
		}
	}
}