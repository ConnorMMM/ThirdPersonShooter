using System;

using UnityEngine;
using UnityEngine.Audio;

namespace ThirdPersonShooter
{
	public class SettingsManager : MonoBehaviour
	{
		[SerializeField] private AudioMixer mixer;
		[SerializeField] private string[] parameters;

		public string this[int _paramIndex] => parameters[_paramIndex];

		private void Start()
		{
			foreach(string parameter in parameters)
			{
				if(parameter.Contains("Volume"))
					SetVolume(parameter, PlayerPrefs.GetFloat(parameter, 100));
			}
		}

		private void OnApplicationQuit()
		{
			PlayerPrefs.Save();
		}

		public void SetVolume(string _id, float _value)
		{
			// 0.0001 - 1
			mixer.SetFloat(_id, Mathf.Log10(_value) * 20);
			PlayerPrefs.SetFloat(_id, _value);
		}
	}
}