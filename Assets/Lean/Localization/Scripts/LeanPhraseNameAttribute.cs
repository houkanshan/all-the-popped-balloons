using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace Lean.Localization
{
	[CustomPropertyDrawer(typeof(LeanPhraseNameAttribute))]
	public class LeanPhraseNameDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var left  = position; left.xMax -= 40;
			var right = position; right.xMin = left.xMax + 2;
			
			EditorGUI.PropertyField(left, property);
			
			if (GUI.Button(right, "List") == true)
			{
				var menu = new GenericMenu();
				
				for (var i = 0; i < LeanLocalization.CurrentPhrases.Count; i++)
				{
					var phraseName = LeanLocalization.CurrentPhrases[i];
					
					menu.AddItem(new GUIContent(phraseName), property.stringValue == phraseName, () => { property.stringValue = phraseName; property.serializedObject.ApplyModifiedProperties(); });
				}
				
				if (menu.GetItemCount() > 0)
				{
					menu.DropDown(right);
				}
				else
				{
					Debug.LogWarning("Your scene doesn't contain any phrases, so the phrase name list couldn't be created.");
				}
			}
		}
	}
}
#endif

namespace Lean.Localization
{
	/// <summary>This attribute allows you to select a phrase from all the localizations in the scene.</summary>
	public class LeanPhraseNameAttribute : PropertyAttribute
	{
	}
}