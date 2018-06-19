using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text), typeof(LayoutElement)), ExecuteInEditMode]
public class SudokuText : MonoBehaviour
{
	private Text m_Text;
	public Text Text
	{
		get { return m_Text; }
	}

	public Material Material
	{
		set
		{
			if (m_Text.material == value)
			{
				return;
			}

			m_Text.material = value;
		}
	}

	public Color Color
	{
		set
		{
			if (m_Text.color == value)
			{
				return;
			}

			m_Text.color = value;
		}
	}

	public Font Font
	{
		set
		{
			if (value == null || value == m_Text.font)
			{
				return;
			}

			m_Text.font = value;
		}
	}

	private void Awake()
	{
		m_Text = GetComponent<Text>();
	}
}
