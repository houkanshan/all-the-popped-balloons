using UnityEngine;

namespace Lean.Localization
{
	/// <summary>This contains information about an alias for a language. An alias is an alternative name for a language based on region or system settings. For example, English might use one of these aliases: en, en-GB, en-US</summary>
	[System.Serializable]
	public class LeanCulture
	{
		/// <summary>The LeanLocalization language name for this culture (e.g. English, Japanese).</summary>
		public string Language;

		/// <summary>The alias for this culture (e.g. en, en-GB, en-US)</summary>
		public string Alias;
	}
}