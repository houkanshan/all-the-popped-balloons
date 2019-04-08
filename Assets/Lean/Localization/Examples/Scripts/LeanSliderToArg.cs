using UnityEngine;
using UnityEngine.UI;

namespace Lean.Localization
{
	/// <summary>This component reads the current slider value, and sends it to LeanLocalizedText as an argument.</summary>
	[HelpURL(LeanLocalization.HelpUrlPrefix + "LeanSliderToArg")]
	public class LeanSliderToArg : MonoBehaviour
	{
		[Tooltip("The slider we're getting values from.")]
		public UnityEngine.UI.Slider Slider;

		[Tooltip("The LeanLocalizedText we will send arguments to.")]
		public LeanLocalizedText Text;

		[Tooltip("The argument index we want to replace.")]
		public int Index;

		// Called from the Slider.OnValueChanged event
		public void OnValueChanged(float value)
		{
			if (Text != null)
			{
				Text.SetArg(value.ToString(), 0);
			}
		}

		protected virtual void Awake()
		{
			// Slider doesn't call OnValueChanged on Awake, so do it manually
			if (Slider != null)
			{
				OnValueChanged(Slider.value);
			}
		}
	}
}