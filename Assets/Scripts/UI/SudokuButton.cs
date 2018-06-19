using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Button), typeof(ContentSizeFitter), typeof(Image))]
public class SudokuButton : SudokuUI
{
	private Button m_Button;
	private ContentSizeFitter m_ContentSizeFitter;
	private HorizontalOrVerticalLayoutGroup m_LayoutGroup;
	
	[SerializeField] private SudokuText m_ButtonText;
	[SerializeField] private bool m_UseVerticalLayoutGroup;

	public Material Material
	{
		set
		{
			if (value == null)
			{
				return;
			}

			if (m_Button.image.material != value)
			{
				m_Button.image.material = value;
			}
		}
	}
	
	protected override void Awake()
	{
		m_Button = GetComponent<Button>();
		m_ContentSizeFitter = GetComponent<ContentSizeFitter>();
		
		if (transform.childCount > 0)
		{
			Text text = GetComponentInChildren<Text>();
			
			if (text == null)
			{
				m_ButtonText = text.GetComponent<SudokuText>();
				if (m_ButtonText == null)
				{
					m_ButtonText = transform.GetChild(0).gameObject.AddComponent<SudokuText>();
				}
			}
			else
			{
				m_ButtonText = text.gameObject.AddComponent<SudokuText>();
			}
		}
		else
		{
			GameObject textGo = new GameObject();
			textGo.transform.parent = transform;
			m_ButtonText = textGo.AddComponent<SudokuText>();
		}
				
		base.Awake();
	}

	/// <summary>
	/// Updates the element's UI, only if UI Data has been assigned
	/// </summary>
	/// <returns>Whether or not UI Data has been assigned</returns>
	protected override bool OnUI()
	{
		if (!base.OnUI())
		{
			return false;
		}

		m_LayoutGroup = SwitchLayoutGroupTypes(m_UseVerticalLayoutGroup);
		
		m_Button.colors = UIData.ColorBlock;
		
		m_ButtonText.Material = UIData.TextMaterial;
		m_ButtonText.Color = UIData.TextColor;
		m_Button.image.sprite = UIData.Sprite;
		
		m_Button.image.type = UIData.ImageType;
		
		m_ButtonText.Text.fontSize = UIData.FontSize;
		m_ButtonText.Font = UIData.Font;
		m_LayoutGroup.childAlignment = UIData.TextAnchor;
		m_LayoutGroup.spacing = UIData.Spacing;
		m_LayoutGroup.childControlHeight = UIData.ChildControlHeight;
		m_LayoutGroup.childControlWidth = UIData.ChildControlWidth;
		m_LayoutGroup.childForceExpandHeight = UIData.ChildForceExpandHeight;
		m_LayoutGroup.childForceExpandWidth = UIData.ChildForceExpandWidth;
		UIData.UpdateLayoutGroup(m_LayoutGroup);


		m_Button.image.material = UIData.Material;
		
		m_ContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		m_ContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		StartCoroutine(UpdateLayoutGroup());
		
		return true;
	}

	private IEnumerator UpdateLayoutGroup()
	{
		yield return new WaitForEndOfFrame();
		UIData.UpdateLayoutGroup(m_LayoutGroup);
	}

	private HorizontalOrVerticalLayoutGroup SwitchLayoutGroupTypes(bool useVerticalLayoutGroup)
	{
		HorizontalOrVerticalLayoutGroup layoutGroup = GetComponent<HorizontalOrVerticalLayoutGroup>();
		if ((layoutGroup is HorizontalLayoutGroup && useVerticalLayoutGroup) || (layoutGroup is VerticalLayoutGroup && !useVerticalLayoutGroup))
		{
			if (Application.isPlaying)
			{
				Destroy(layoutGroup);

			} else
			{
				DestroyImmediate(layoutGroup);
			}
		}
		else if (layoutGroup == null){} 
		else if ((layoutGroup is HorizontalLayoutGroup && !useVerticalLayoutGroup) || (layoutGroup is VerticalLayoutGroup && useVerticalLayoutGroup))
		{
			return layoutGroup;
		}
		else
		{
			Debug.LogError("I'm not even sure how I got here");
			return layoutGroup;
		}
		
		
		if (useVerticalLayoutGroup)
		{
			return gameObject.AddComponent<VerticalLayoutGroup>();
		}
		else
		{
			return gameObject.AddComponent<HorizontalLayoutGroup>();
		}
	}
	
	#if UNITY_EDITOR
	[MenuItem("GameObject/UI/Sudoku Button", false, 1)]
	private static void CreateInEditor(MenuCommand menuCommand)
	{
		GameObject go = new GameObject("Button");
		GameObject textGo = new GameObject("Text");
		Text text = textGo.AddComponent<Text>();
		text.text = "Button";
		text.color = Color.black;
		text.alignment = TextAnchor.MiddleCenter;
		
		GameObjectUtility.SetParentAndAlign(textGo, go);
		Button button = go.AddComponent<SudokuButton>().GetComponent<Button>();
		button.image = go.GetComponent<Image>();
		
		GameObject context = menuCommand.context as GameObject;
		Canvas canvas;
		if (context != null && context.GetComponentInParent<Canvas>() != null)
		{
			GameObjectUtility.SetParentAndAlign(go, context);
		}
		else
		{
			canvas = GameObject.FindObjectOfType(typeof(Canvas)) as Canvas;
			/*if (canvas == null)
			{
				GameObject canvasGo = new GameObject("Canvas");
				canvas = canvasGo.AddComponent<Canvas>();
				canvasGo.AddComponent<CanvasScaler>();
				canvasGo.AddComponent<GraphicRaycaster>();
			}*/
			
			/*EventSystem eventSystem = GameObject.FindObjectOfType(typeof(EventSystem)) as EventSystem;
			if (eventSystem == null)
			{
				GameObject eventSystemGo = new GameObject("EventSystem");
				eventSystem = eventSystemGo.AddComponent<EventSystem>();
				eventSystemGo.AddComponent<StandaloneInputModule>();
			}*/

			if (canvas != null)
			{
				GameObjectUtility.SetParentAndAlign(go, canvas.gameObject);

			}
		}
		
		Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
		Selection.activeObject = go;
	}
	#endif
}
