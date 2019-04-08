using UnityEngine;

namespace Lean.Localization
{
	/// <summary>This component will load localizations from a file. By default they should be in the format:
	/// Phrase Name Here : Language Name Here = Translation Here // Optional Comment Here
	/// </summary>
	[HelpURL(LeanLocalization.HelpUrlPrefix + "LeanLocalizationMultiLoader")]
	public class LeanLocalizationMultiLoader : MonoBehaviour
	{
		[Tooltip("The LeanLocalization target the translations will be loaded into. If this is empty, the first in the scene will automatically be used. If there are none in the scene, one will be created")]
		public LeanLocalization Target;

		[Tooltip("The file with all the translations in")]
		public TextAsset Source;

		[Tooltip("The string separating the phrase name from the language name")]
		public string Language = " : ";

		[Tooltip("The string separating the language name from the translation")]
		public string Separator = " = ";

		[Tooltip("The string denoting a new line within a translation")]
		public string NewLine = "\\n";

		[Tooltip("The string separating the translation from the comment (empty = no comments)")]
		public string Comment = " // ";

		// The characters used to detect a newline
		private static readonly char[] newlineCharacters = new char[] { '\r', '\n' };

		protected virtual void Start()
		{
			LoadFromSource();
		}

		[ContextMenu("Load From Source")]
		public void LoadFromSource()
		{
			if (Source != null)
			{
				var target = Target;

				// Automatically get first LeanLocalization?
				if (target == null && LeanLocalization.AllLocalizations.Count > 0)
				{
					target = LeanLocalization.AllLocalizations[0];
				}

				// Create a new LeanLocalization?
				if (target == null)
				{
					target = new GameObject("LeanLocalization").AddComponent<LeanLocalization>();
				}

				// Split file by line and loop through them all
				var lines          = Source.text.Split(newlineCharacters, System.StringSplitOptions.RemoveEmptyEntries);
				var updateLanguage = false;

				for (var i = 0; i < lines.Length; i++)
				{
					var line        = lines[i];
					var equalsIndex = line.IndexOf(Separator);

					if (equalsIndex != -1)
					{
						var lineLeft   = line.Substring(0, equalsIndex).Trim();
						var colonIndex = lineLeft.IndexOf(Language);

						if (colonIndex != -1)
						{
							var phraseName        = lineLeft.Substring(0, colonIndex).Trim();
							var languageName      = lineLeft.Substring(colonIndex + Language.Length).Trim();
							var phraseTranslation = line.Substring(equalsIndex + Separator.Length).Trim();

							// Replace newline markers with actual newlines
							if (string.IsNullOrEmpty(NewLine) == false)
							{
								phraseTranslation = phraseTranslation.Replace(NewLine, System.Environment.NewLine);
							}

							// Does this entry have a comment?
							if (string.IsNullOrEmpty(Comment) == false)
							{
								var commentIndex = phraseTranslation.LastIndexOf(Comment);

								if (commentIndex != -1)
								{
									phraseTranslation = phraseTranslation.Substring(0, commentIndex).Trim();
								}
							}

							// Find or add the translation for this phrase
							var translation = target.AddTranslation(languageName, phraseName);

							// Set the translation text for this phrase
							translation.Text = phraseTranslation;

							// Update translations later?
							if (LeanLocalization.CurrentLanguage == languageName)
							{
								updateLanguage = true;
							}
						}
					}
				}

				// Update translations?
				if (updateLanguage == true)
				{
					LeanLocalization.UpdateTranslations();
				}
			}
		}

		// NOTE: Saving text files like this doesn't work with webplayer build set
		// If this doesn't work for other platforms then add them to the list
#if UNITY_EDITOR && !UNITY_WEBPLAYER
		[ContextMenu("Export Text Asset")]
		public void ExportTextAsset()
		{
			var target = Target;

			if (target == null && LeanLocalization.AllLocalizations.Count > 0)
			{
				target = LeanLocalization.AllLocalizations[0];
			}

			if (target != null)
			{
				// Find where we want to save the file
				var path = UnityEditor.EditorUtility.SaveFilePanelInProject("Export Text Asset", "MultiLanguage", "txt", "");

				// Make sure we didn't cancel the panel
				if (string.IsNullOrEmpty(path) == false)
				{
					var data = "";
					var gaps = 0;

					// Add all phrase names and existing translations to lines
					for (var i = target.Phrases.Count - 1; i >= 0; i--)
					{
						var phrase = target.Phrases[i];

						if (gaps > 0)
						{
							gaps = 2;
						}

						for (var j = 0; j < target.Languages.Count; j++)
						{
							var languageName = target.Languages[j];
							var translation  = phrase.FindTranslation(languageName);

							for (var k = 0; k < gaps; k++)
							{
								data += System.Environment.NewLine;
							}

							data += phrase.Name + Language + languageName + Separator;
							gaps  = 1;

							if (translation != null)
							{
								var text = translation.Text;

								// Replace all new line permutations with the new line token
								text = text.Replace("\r\n", NewLine);
								text = text.Replace("\n\r", NewLine);
								text = text.Replace("\n", NewLine);
								text = text.Replace("\r", NewLine);

								data += text;
							}
						}
					}

					// Write text to file
					using (var file = System.IO.File.OpenWrite(path))
					{
						var encoding = new System.Text.UTF8Encoding();
						var bytes    = encoding.GetBytes(data);

						file.Write(bytes, 0, bytes.Length);
					}

					// Import asset into project
					UnityEditor.AssetDatabase.ImportAsset(path);

					// Replace Soure with new Text Asset?
					var textAsset = (TextAsset)UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));

					if (textAsset != null)
					{
						Source = textAsset;

						UnityEditor.EditorGUIUtility.PingObject(textAsset);

						UnityEditor.EditorUtility.SetDirty(this);
					}
				}
			}
		}
#endif
	}
}