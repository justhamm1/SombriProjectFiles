using UnityEngine;

[ExecuteInEditMode]
public class SudokuUI : MonoBehaviour
{

	[SerializeField] protected SudokuUIData UIData;
	
	protected  virtual void Awake()
	{
		//UIData = ScriptableObject.CreateInstance<SudokuUIData>();
		
		if (!OnUI() && Application.isPlaying)
		{
			Debug.LogWarning("Can't update UI for " + name + " because no UI Data was assigned. Please assign UI Data");
		}
	}

	protected virtual bool OnUI()
	{
		return UIData != null;
	}
	
#if UNITY_EDITOR
	protected virtual void Update()
	{
		OnUI();
	}
#endif
	
}
