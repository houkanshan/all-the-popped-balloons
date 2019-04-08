using UnityEngine;

namespace Lean.Localization
{
	/// <summary>This component will update a Renderer component's sharedMaterial with a localized material, or use a fallback if none is found.</summary>
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Renderer))]
	[HelpURL(LeanLocalization.HelpUrlPrefix + "LeanLocalizedRenderer")]
	public class LeanLocalizedRenderer : LeanLocalizedBehaviour
	{
		[Tooltip("If PhraseName couldn't be found, this material will be used")]
		public Material FallbackMaterial;

		[Tooltip("The material index you want to replace.")]
		public int Index;

		// This gets called every time the translation needs updating
		public override void UpdateTranslation(LeanTranslation translation)
		{
			// Get the Renderer component attached to this GameObject
			var renderer = GetComponent<Renderer>();

			// Get the shared materials of this component
			var sharedMaterials = renderer.sharedMaterials;

			// Use translation?
			if (translation != null && translation.Object is Material)
			{
				sharedMaterials[Index] = (Material)translation.Object;
			}
			// Use fallback?
			else
			{
				sharedMaterials[Index] = FallbackMaterial;
			}

			renderer.sharedMaterials = sharedMaterials;
		}

		protected virtual void Awake()
		{
			// Should we set FallbackFont?
			if (FallbackMaterial == null)
			{
				// Get the Renderer component attached to this GameObject
				var renderer = GetComponent<Renderer>();

				// Copy current material to fallback
				FallbackMaterial = renderer.sharedMaterials[Index];
			}
		}
	}
}