using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
	public sealed class WeaponUiText : MonoBehaviour
	{
		private Text _text;

		private void Awake()
		{
			_text = GetComponent<Text>();
		}

		public void ShowData(int countAmmunition)
		{
			_text.text = $"{countAmmunition}";
		}

		public void SetActive(bool value)
		{
			_text.gameObject.SetActive(value);
		}
	}
}