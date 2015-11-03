using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Gravity))]
public class StarEnabler : MonoBehaviour 
{
	[SerializeField] MeshRenderer starRender;
	[SerializeField] Color disableColor;
	Gravity gravity;
	Color startColor;
	string colorKey = "_EmissionColor";

//	void Awake()
//	{
//		gravity = GetComponent<Gravity>();
//		startColor = starRender.material.GetColor(colorKey);
//	}
//
//	void OnMouseUp()
//	{
//		Debug.Log("Click");
//		StopCoroutine("ChangeGravityEnabled");
//		StartCoroutine("ChangeGravityEnabled", !gravity.enabled);
//	}
//
//	IEnumerator ChangeGravityEnabled(bool enable)
//	{
//		float elapsedTime = 0;
//
//		do
//		{
//			yield return null;
//			elapsedTime += Time.deltaTime;
//
//			if (enable)
//			{
//				starRender.material.SetColor(colorKey, Color.Lerp(disableColor, startColor, elapsedTime));
//			}
//			else
//			{
//				starRender.material.SetColor(colorKey, Color.Lerp(startColor, disableColor, elapsedTime));
//			}
//
//		} while (elapsedTime < 1.0f);
//
//		gravity.enabled = enable;
//	}

}
