using UnityEngine;
using UnityEngine.Serialization;

namespace Lean.Localization
{
	/// <summary>This component simplifies the updating process, extend it if you want to cause a specific object to get localized</summary>
	public abstract class LeanLocalizedBehaviour : MonoBehaviour
	{
		[Tooltip("The name of the phrase we want to use for this localized component")]
		[LeanPhraseName]
		[SerializeField]
		[FormerlySerializedAs("PhraseName")]
		private string phraseName;

		// This property is used to set the phraseName from code
		public string PhraseName
		{
			set
			{
				// phraseName changed?
				if (value != phraseName)
				{
					// Update localization with new phrase
					phraseName = value;

					UpdateLocalization();
				}
			}

			get
			{
				return phraseName;
			}
		}

		// This gets called every time the translation needs updating
		// NOTE: translation may be null if it can't be found
		public abstract void UpdateTranslation(LeanTranslation translation);

		// Call this to force the behaviour to get updated
		public void UpdateLocalization()
		{
			UpdateTranslation(LeanLocalization.GetTranslation(phraseName));
		}

		protected virtual void OnEnable()
		{
			LeanLocalization.OnLocalizationChanged += UpdateLocalization;
			
			UpdateLocalization();
		}

		protected virtual void OnDisable()
		{
			LeanLocalization.OnLocalizationChanged -= UpdateLocalization;
		}

#if UNITY_EDITOR
		// This gets called from the inspector when changing 'phraseName'
		protected virtual void OnValidate()
		{
			if (isActiveAndEnabled == true)
			{
				UpdateLocalization();
			}
		}
#endif
	}
}