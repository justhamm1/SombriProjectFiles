using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UI/UI Data", fileName = "SudokuUIData")]
public class SudokuUIData : ScriptableObject
{
	[Header("Setup")] 
	public Sprite Sprite;
	public Image.Type ImageType;
	public Material Material;


	[Header("Formatting")]
	public RectOffset Padding;
	public float Spacing;
	public TextAnchor TextAnchor;
	public bool ChildControlHeight;
	public bool ChildControlWidth;
	public bool ChildForceExpandHeight;
	public bool ChildForceExpandWidth;
	
	[Header("Color Block")]
	public ColorBlock ColorBlock = ColorBlock.defaultColorBlock;

	[Header("Text Appearance")] 
	public Font Font;
	public int FontSize = 14;
	public Color TextColor = Color.black;
	public Material TextMaterial;

	public void UpdateLayoutGroup(HorizontalOrVerticalLayoutGroup layoutGroup)
	{
		if (Padding.top != layoutGroup.padding.top || Padding.left != layoutGroup.padding.left ||Padding.right != layoutGroup.padding.right ||Padding.bottom != layoutGroup.padding.bottom)
		{
			layoutGroup.padding = Padding;
			layoutGroup.CalculateLayoutInputHorizontal();
			layoutGroup.CalculateLayoutInputVertical();
		}
	}
}
