using System;

using ThirdPersonShooter.Utilities;

using TMPro;

using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace ThirdPersonShooter.UI
{
	[RequireComponent(typeof(Slider))]
	public class VolumeSlider : MonoBehaviour
	{
		private const float MIN_VALUE = 0.0001f;
		private const float MAX_VALUE = 1f;

		[SerializeField] private TextMeshProUGUI volumeText;

		private Slider slider;
		private string sliderName;
		private string parameter;

		private void Awake()
		{
			slider = gameObject.GetComponent<Slider>();
		}

		public void Activate()
		{
			parameter = GameManager.Instance.settings[transform.GetSiblingIndex()];

			slider.minValue = MIN_VALUE;
			slider.maxValue = MAX_VALUE;
			sliderName = parameter.Replace("Volume", " ");

			slider.value = PlayerPrefs.GetFloat(parameter, MAX_VALUE);
			GameManager.Instance.settings.SetVolume(parameter, slider.value);
			UpdateText();
			
			slider.onValueChanged.AddListener(OnSliderValueChanged);
		}

		private void OnSliderValueChanged(float _value)
		{
			GameManager.Instance.settings.SetVolume(parameter, slider.value);
			UpdateText();
		}

		private void UpdateText()
		{
			volumeText.text = $"{sliderName}: {Mathf.RoundToInt(slider.value.Remap(MIN_VALUE, MAX_VALUE, 0, 100)):000}%";
		}
	}
}