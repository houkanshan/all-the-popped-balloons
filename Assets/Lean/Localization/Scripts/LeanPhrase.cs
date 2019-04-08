using System.Collections.Generic;

namespace Lean.Localization
{
	/// <summary>This contains data about each phrase, which is then translated into different languages.</summary>
	[System.Serializable]
	public class LeanPhrase
	{
		/// <summary>The name of this phrase (e.g. when picking it in the editor).</summary>
		public string Name;

		/// <summary>This list stores all translations of this phrase in each language.</summary>
		public List<LeanTranslation> Translations = new List<LeanTranslation>();

		/// <summary>Find the translation using this language, or return null.</summary>
		public LeanTranslation FindTranslation(string language)
		{
			return Translations.Find(t => t.Language == language);
		}

		/// <summary>Add a new translation to this phrase, or return the current one.</summary>
		public LeanTranslation AddTranslation(string language)
		{
			var translation = FindTranslation(language);

			// Add it?
			if (translation == null)
			{
				translation = new LeanTranslation();

				translation.Language = language;

				Translations.Add(translation);
			}

			return translation;
		}
	}
}