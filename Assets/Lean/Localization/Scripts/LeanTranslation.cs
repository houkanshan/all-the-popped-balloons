using UnityEngine;

namespace Lean.Localization
{
	/// <summary>This contains data about the phrases after it's been translated to a specific language.</summary>
	[System.Serializable]
	public class LeanTranslation
	{
		/// <summary>The language of this translation.</summary>
		public string Language;

		/// <summary>The translated text.</summary>
		public string Text;

		/// <summary>The translated object (e.g. language specific texture).</summary>
		public Object Object;
	}
}