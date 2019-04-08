using UnityEngine;

namespace Lean.Localization
{
	/// <summary>This component allows you to open a URL using Unity events (e.g. a button).</summary>
	[HelpURL(LeanLocalization.HelpUrlPrefix + "LeanOpenUrl")]
	public class LeanOpenUrl : MonoBehaviour
	{
		public void Open(string url)
		{
			Application.OpenURL(url);
		}
	}
}