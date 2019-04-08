using UnityEngine;
using UnityEngine.UI;

namespace Lean.Localization
{
	/// <summary>This component will update a UI.Text component with localized text, or use a fallback if none is found.</summary>
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Text))]
	[HelpURL(LeanLocalization.HelpUrlPrefix + "LeanLocalizedText")]
	public class LeanLocalizedText : LeanLocalizedBehaviour
	{
		[Tooltip("If PhraseName couldn't be found, this text will be used")]
		public string FallbackText;

		[Tooltip("This allows you to set the words or numbers to insert into your formatted text.\ne.g. My Name is {0}, nice to meet you!")]
		public string[] Args;

		/// <summary>This allows you to set the arg at the specified index.</summary>
		public void SetArg(string arg, int index)
		{
			if (index >= 0)
			{
				// Create args?
				if (Args == null)
				{
					Args = new string[index + 1];
				}

				// Expand args?
				if (index >= Args.Length)
				{
					System.Array.Resize(ref Args, index + 1);
				}

				// Arg modified?
				if (Args[index] != arg)
				{
					Args[index] = arg;

					UpdateLocalization();
				}
			}
		}

		// This gets called every time the translation needs updating
		public override void UpdateTranslation(LeanTranslation translation)
		{
			// Get the Text component attached to this GameObject
			var text = GetComponent<Text>();

			// Use translation?
			if (translation != null)
			{
				if (Args != null && Args.Length > 0)
				{
					text.text = string.Format(translation.Text, Args);
				}
				else
				{
					text.text = translation.Text;
				}
			}
			// Use fallback?
			else
			{
				if (Args != null && Args.Length > 0)
				{
					text.text = string.Format(FallbackText, Args);
				}
				else
				{
					text.text = FallbackText;
				}
			}
		}

		protected virtual void Awake()
		{
			// Should we set FallbackText?
			if (string.IsNullOrEmpty(FallbackText) == true)
			{
				// Get the Text component attached to this GameObject
				var text = GetComponent<Text>();

				// Copy current text to fallback
				FallbackText = text.text;
			}
		}
	}
}