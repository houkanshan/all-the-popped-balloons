using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Lean.Localization
{
	[CustomEditor(typeof(LeanLocalization))]
	public class LeanLocalization_Editor : Editor
	{
		class PresetLanguage
		{
			public string Name;

			public string[] CultureName;
		}

		// Languages
		private static List<PresetLanguage> presetLanguages = new List<PresetLanguage>();

		// Currently expanded language
		private int languageIndex = -1;

		// Currently expanded language phrase
		private int reverseIndex = -1;

		// Currently expanded culture
		private int cultureIndex = -1;

		// Currently expanded phrase
		private int phraseIndex = -1;

		// Currently expanded translation
		private int translationIndex = -1;

		private bool dirty;

		private List<string> existingLanguages = new List<string>();

		private List<string> existingPhrases = new List<string>();

		private GUIStyle labelStyle;

		private GUIStyle LabelStyle
		{
			get
			{
				if (labelStyle == null)
				{
					labelStyle = new GUIStyle(EditorStyles.label);
					labelStyle.clipping = TextClipping.Overflow;
				}

				return labelStyle;
			}
		}

		static LeanLocalization_Editor()
		{
			AddPresetLanguage("Chinese", "ChineseSimplified", "ChineseTraditional", "zh", "zh-TW", "zh-CN", "zh-HK", "zh-SG", "zh-MO");
			AddPresetLanguage("English", "en", "en-GB", "en-US", "en-AU", "en-CA", "en-NZ", "en-IE", "en-ZA", "en-JM", "en-en029", "en-BZ", "en-BZ", "en-TT", "en-ZW", "en-PH");
			AddPresetLanguage("Spanish", "es", "es-ES", "es-MX", "es-GT", "es-CR", "es-PA", "es-DO", "es-VE", "es-CO", "es-PE", "es-AR", "es-EC", "es-CL", "es-UY", "es-PY", "es-BO", "es-SV", "es-SV", "es-HN", "es-NI", "es-PR");
			AddPresetLanguage("Arabic", "ar", "ar-SA", "ar-IQ", "ar-EG", "ar-LY", "ar-DZ", "ar-MA", "ar-TN", "ar-OM", "ar-YE", "ar-SY", "ar-JO", "ar-LB", "ar-KW", "ar-AE", "ar-BH", "ar-QA");
			AddPresetLanguage("German", "de", "de-DE", "de-CH", "de-AT", "de-LU", "de-LI");
			AddPresetLanguage("Korean", "ko", "ko-KR");
			AddPresetLanguage("French", "fr", "fr-FR", "fr-BE", "fr-CA", "fr-CH", "fr-LU", "fr-MC");
			AddPresetLanguage("Russian", "ru", "ru-RU");
			AddPresetLanguage("Japanese", "ja", "ja-JP");
			AddPresetLanguage("Italian", "it", "it-IT", "it-CH");
			AddPresetLanguage("Other...");
		}

		[MenuItem("GameObject/Lean/Localization", false, 1)]
		public static void CreateLocalization()
		{
			var gameObject = new GameObject(typeof(LeanLocalization).Name);

			UnityEditor.Undo.RegisterCreatedObjectUndo(gameObject, "Create Localization");

			gameObject.AddComponent<LeanLocalization>();

			Selection.activeGameObject = gameObject;
		}

		// Draw the whole inspector
		public override void OnInspectorGUI()
		{
			var localization = (LeanLocalization)target;

			Validate(localization);

			EditorGUILayout.Separator();

			DrawCurrentLanguage();

			EditorGUILayout.Separator();

			EditorGUILayout.PropertyField(serializedObject.FindProperty("DefaultLanguage"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("DetectLanguage"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("SaveLanguage"));

			EditorGUILayout.Separator();

			DrawLanguages(localization);

			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			DrawCultures(localization);

			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			DrawPhrases(localization);

			EditorGUILayout.Separator();

			// Update if dirty?
			if (serializedObject.ApplyModifiedProperties() == true || dirty == true)
			{
				dirty = false;

				LeanLocalization.UpdateTranslations();

				SetDirty(localization);
			}
		}

		private void DrawCurrentLanguage()
		{
			var labelA = Reserve();
			var valueA = Reserve(ref labelA, 38.0f);

			LeanLocalization.CurrentLanguage = EditorGUI.TextField(labelA, "Current Language", LeanLocalization.CurrentLanguage);

			if (GUI.Button(valueA, "List") == true)
			{
				var menu = new GenericMenu();

				for (var i = 0; i < LeanLocalization.CurrentLanguages.Count; i++)
				{
					var language = LeanLocalization.CurrentLanguages[i];
					
					menu.AddItem(new GUIContent(language), LeanLocalization.CurrentLanguage == language, () => { LeanLocalization.CurrentLanguage = language; MarkAsModified(); });
				}

				if (menu.GetItemCount() > 0)
				{
					menu.DropDown(valueA);
				}
				else
				{
					Debug.LogWarning("Your scene doesn't contain any languages, so the language name list couldn't be created.");
				}
			}
		}

		private void DrawLanguages(LeanLocalization localization)
		{
			var labelA = Reserve();
			var valueA = Reserve(ref labelA, 35.0f);

			EditorGUI.LabelField(labelA, "Languages", EditorStyles.boldLabel);

			// Add a new language?
			if (GUI.Button(valueA, "Add") == true)
			{
				MarkAsModified();

				var menu = new GenericMenu();

				for (var i = 0; i < presetLanguages.Count; i++)
				{
					var presetLanguage = presetLanguages[i];

					menu.AddItem(new GUIContent(presetLanguage.Name), false, () => localization.AddLanguage(presetLanguage.Name, presetLanguage.CultureName));
				}

				menu.DropDown(valueA);
			}

			existingLanguages.Clear();

			// Draw all added languages
			for (var i = 0; i < localization.Languages.Count; i++)
			{
				var language = localization.Languages[i];
				var labelB   = Reserve();
				var valueB   = Reserve(ref labelB, 20.0f);

				// Edit language name or remove
				if (languageIndex == i)
				{
					BeginModifications();
					{
						localization.Languages[i] = EditorGUI.TextField(labelB, language);
					}
					EndModifications();

					if (GUI.Button(valueB, "X") == true)
					{
						MarkAsModified();

						localization.Languages.RemoveAt(i); languageIndex = -1;
					}
				}

				// Expand language?
				if (EditorGUI.Foldout(labelB, languageIndex == i, languageIndex == i ? "" : language) == true)
				{
					if (languageIndex != i)
					{
						languageIndex = i;
						reverseIndex  = -1;
					}

					EditorGUI.indentLevel += 1;
					{
						DrawReverse(localization, language);
					}
					EditorGUI.indentLevel -= 1;

					EditorGUILayout.Separator();
				}
				else if (languageIndex == i)
				{
					languageIndex = -1;
					reverseIndex  = -1;
				}

				// Already added?
				if (existingLanguages.Contains(language) == true)
				{
					EditorGUILayout.HelpBox("This language already exists in the list!", MessageType.Warning);
				}
				else
				{
					existingLanguages.Add(language);
				}
			}
		}

		// Reverse lookup the phrases for this language
		private void DrawReverse(LeanLocalization localization, string language)
		{
			for (var i = 0; i < localization.Phrases.Count; i++)
			{
				var phrase      = localization.Phrases[i];
				var labelA      = Reserve();
				var translation = phrase.Translations.Find(t => t.Language == language);

				BeginModifications();
				{
					EditorGUI.LabelField(labelA, phrase.Name);
				}
				EndModifications();

				if (translation != null)
				{
					if (reverseIndex == i)
					{
						BeginModifications();
						{
							phrase.Name = EditorGUI.TextField(labelA, "", phrase.Name);
						}
						EndModifications();
					}

					if (EditorGUI.Foldout(labelA, reverseIndex == i, reverseIndex == i ? "" : phrase.Name) == true)
					{
						reverseIndex = i;

						EditorGUI.indentLevel += 1;
						{
							DrawTranslation(translation);
						}
						EditorGUI.indentLevel -= 1;

						EditorGUILayout.Separator();
					}
					else if (reverseIndex == i)
					{
						reverseIndex = -1;
					}
				}
				else
				{
					var valueA = Reserve(ref labelA, 120.0f);

					if (GUI.Button(valueA, "Create Translation") == true)
					{
						MarkAsModified();

						var newTranslation = new LeanTranslation();

						newTranslation.Language = language;
						newTranslation.Text     = phrase.Name;

						phrase.Translations.Add(newTranslation);
					}
				}
			}
		}

		private void DrawCultures(LeanLocalization localization)
		{
			var labelA = Reserve();
			var valueA = Reserve(ref labelA, 35.0f);

			EditorGUI.LabelField(labelA, "Cultures", EditorStyles.boldLabel);

			// Add a new culture?
			if (localization.Languages.Count > 0)
			{
				if (GUI.Button(valueA, "Add") == true)
				{
					MarkAsModified();

					var menu = new GenericMenu();

					for (var i = 0; i < localization.Languages.Count; i++)
					{
						var language = localization.Languages[i];

						menu.AddItem(new GUIContent(language), false, () => { var culture = localization.AddCulture(language, ""); cultureIndex = localization.Cultures.IndexOf(culture); } );
					}

					menu.DropDown(valueA);
				}
			}

			// Draw all cultures
			for (var i = 0; i < localization.Languages.Count; i++)
			{
				var language = localization.Languages[i];

				if (localization.Cultures.Exists(c => c.Language == language) == true)
				{
					var labelB = Reserve();

					if (EditorGUI.Foldout(labelB, cultureIndex == i, language) == true)
					{
						if (cultureIndex != i)
						{
							cultureIndex = i;
						}
						EditorGUI.indentLevel += 1;
						{
							for (var j = 0; j < localization.Cultures.Count; j++)
							{
								var culture = localization.Cultures[j];

								if (culture.Language == language)
								{
									DrawCulture(localization, culture, false);
								}
							}
						}
						EditorGUI.indentLevel -= 1;
					}
					else if (cultureIndex == i)
					{
						cultureIndex = -1;
					}
				}
			}

			for (var i = 0; i < localization.Cultures.Count; i++)
			{
				var culture         = localization.Cultures[i];
				var cultureLanguage = culture.Language;

				if (localization.Languages.Contains(cultureLanguage) == false)
				{
					DrawCulture(localization, culture, true);
				}
			}
		}

		private void DrawCulture(LeanLocalization localization, LeanCulture culture, bool full)
		{
			var labelA = Reserve();
			var valueA = Reserve(ref labelA, 20.0f);

			BeginModifications();
			{
				//culture.Language = EditorGUI.TextField(labelA, "", culture.Name);
				culture.Alias = EditorGUI.TextField(labelA, "", culture.Alias);

				if (GUI.Button(valueA, "X") == true)
				{
					MarkAsModified();

					localization.Cultures.Remove(culture);

					if (localization.Cultures.Exists(c => c.Language == culture.Language) == false)
					{
						cultureIndex = 0;
					}
				}
			}
			EndModifications();
		}

		private void DrawPhrases(LeanLocalization localization)
		{
			var labelA = Reserve();
			var valueA = Reserve(ref labelA, 35.0f);

			EditorGUI.LabelField(labelA, "Phrases", EditorStyles.boldLabel);

			if (GUI.Button(valueA, "Add") == true)
			{
				MarkAsModified();

				var newPhrase = localization.AddPhrase("New Phrase");

				phraseIndex = localization.Phrases.IndexOf(newPhrase);
			}

			existingPhrases.Clear();

			for (var i = 0; i < localization.Phrases.Count; i++)
			{
				var phrase = localization.Phrases[i];
				var labelB = Reserve();
				var valueB = Reserve(ref labelB, 20.0f);

				if (phraseIndex == i)
				{
					BeginModifications();
					{
						phrase.Name = EditorGUI.TextField(labelB, "", phrase.Name);
					}
					EndModifications();

					if (GUI.Button(valueB, "X") == true)
					{
						MarkAsModified();

						localization.Phrases.RemoveAt(i); phraseIndex = -1;
					}
				}

				if (EditorGUI.Foldout(labelB, phraseIndex == i, phraseIndex == i ? "" : phrase.Name) == true)
				{
					if (phraseIndex != i)
					{
						phraseIndex      = i;
						translationIndex = -1;
					}

					EditorGUI.indentLevel += 1;
					{
						DrawTranslations(localization, phrase);
					}
					EditorGUI.indentLevel -= 1;

					EditorGUILayout.Separator();
				}
				else if (phraseIndex == i)
				{
					phraseIndex      = -1;
					translationIndex = -1;
				}

				if (existingPhrases.Contains(phrase.Name) == true)
				{
					EditorGUILayout.HelpBox("This phrase already exists in the list!", MessageType.Warning);
				}
				else
				{
					existingPhrases.Add(phrase.Name);
				}
			}
		}

		private void DrawTranslations(LeanLocalization localization, LeanPhrase phrase)
		{
			existingLanguages.Clear();

			for (var i = 0; i < phrase.Translations.Count; i++)
			{
				var labelA      = Reserve();
				var valueA      = Reserve(ref labelA, 20.0f);
				var translation = phrase.Translations[i];

				if (translationIndex == i)
				{
					BeginModifications();
					{
						translation.Language = EditorGUI.TextField(labelA, "", translation.Language);
					}
					EndModifications();

					if (GUI.Button(valueA, "X") == true)
					{
						MarkAsModified();

						phrase.Translations.RemoveAt(i); translationIndex = -1;
					}
				}

				if (EditorGUI.Foldout(labelA, translationIndex == i, translationIndex == i ? "" : translation.Language) == true)
				{
					translationIndex = i;

					EditorGUI.indentLevel += 1;
					{
						DrawTranslation(translation);
					}
					EditorGUI.indentLevel -= 1;

					EditorGUILayout.Separator();
				}
				else if (translationIndex == i)
				{
					translationIndex = -1;
				}

				if (existingLanguages.Contains(translation.Language) == true)
				{
					EditorGUILayout.HelpBox("This phrase has already been translated to this language!", MessageType.Warning);
				}
				else
				{
					existingLanguages.Add(translation.Language);
				}

				if (localization.Languages.Contains(translation.Language) == false)
				{
					EditorGUILayout.HelpBox("This translation uses a language that hasn't been set in the localization!", MessageType.Warning);
				}
			}

			for (var i = 0; i < localization.Languages.Count; i++)
			{
				var language = localization.Languages[i];

				if (phrase.Translations.Exists(t => t.Language == language) == false)
				{
					var labelA = Reserve();
					var valueA = Reserve(ref labelA, 120.0f);

					EditorGUI.LabelField(labelA, language);

					if (GUI.Button(valueA, "Create Translation") == true)
					{
						MarkAsModified();

						var newTranslation = new LeanTranslation();

						newTranslation.Language = language;
						newTranslation.Text     = phrase.Name;

						translationIndex = phrase.Translations.Count;

						phrase.Translations.Add(newTranslation);
					}
				}
			}
		}

		private void DrawTranslation(LeanTranslation translation)
		{
			BeginModifications();
			{
				EditorGUILayout.BeginHorizontal();
				{
					EditorGUILayout.LabelField("Text", LabelStyle, GUILayout.Width(50.0f));

					translation.Text = EditorGUILayout.TextArea(translation.Text);
				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				{
					EditorGUILayout.LabelField("Object", LabelStyle, GUILayout.Width(50.0f));

					translation.Object = EditorGUILayout.ObjectField(translation.Object, typeof(Object), true);
				}
				EditorGUILayout.EndHorizontal();
			}
			EndModifications();
		}

		private static Rect Reserve(ref Rect rect, float rightWidth = 0.0f, float padding = 2.0f)
		{
			if (rightWidth == 0.0f)
			{
				rightWidth = rect.width - EditorGUIUtility.labelWidth;
			}

			var left  = rect; left.xMax -= rightWidth;
			var right = rect; right.xMin = left.xMax;

			left.xMax -= padding;

			rect = left;

			return right;
		}

		private static Rect Reserve(float height = 16.0f)
		{
			var rect = EditorGUILayout.BeginVertical();
			{
				EditorGUILayout.LabelField("", GUILayout.Height(height));
			}
			EditorGUILayout.EndVertical();

			return rect;
		}

		private static void SetDirty(Object target)
		{
			EditorUtility.SetDirty(target);

#if UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
			EditorApplication.MarkSceneDirty();
#else
			UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
#endif
		}

		private void MarkAsModified()
		{
			dirty = true;
		}

		private void BeginModifications()
		{
			EditorGUI.BeginChangeCheck();
		}

		private void EndModifications()
		{
			if (EditorGUI.EndChangeCheck() == true)
			{
				dirty = true;
			}
		}

		private static void AddPresetLanguage(string name, params string[] cultureNames)
		{
			var presetLanguage = new PresetLanguage();

			presetLanguage.Name        = name;
			presetLanguage.CultureName = cultureNames;

			presetLanguages.Add(presetLanguage);
		}

		private static void Validate(LeanLocalization localization)
		{
			if (localization.Languages == null) localization.Languages = new List<string>();
			if (localization.Cultures  == null) localization.Cultures  = new List<LeanCulture>();
			if (localization.Phrases   == null) localization.Phrases   = new List<LeanPhrase>();

			for (var i = localization.Cultures.Count - 1; i >= 0; i--)
			{
				var culture = localization.Cultures[i];

				if (culture == null)
				{
					culture = new LeanCulture();

					culture.Language = "New Language";
					culture.Alias    = "new-Alias";

					localization.Cultures[i] = culture;
				}
			}

			for (var i = localization.Phrases.Count - 1; i >= 0; i--)
			{
				var phrase = localization.Phrases[i];
				
				if (phrase.Translations == null)
				{
					phrase.Translations = new List<LeanTranslation>();
				}
			}
		}
	}
}