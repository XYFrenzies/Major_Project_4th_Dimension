using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DrawFPS : MonoBehaviour
{
	private float m_deltaTime = 0.0f;
	[SerializeField] private Toggle m_check;
	private void Update()
	{
		if (CheckValue())
			m_deltaTime += (Time.unscaledDeltaTime - m_deltaTime) * 0.1f;
	}

	private void OnGUI()
	{
		if (CheckValue())
		{
			int w = Screen.width, h = Screen.height;

			GUIStyle style = new GUIStyle();

			Rect rect = new Rect(0, 0, w, h * 2 / 100);
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = h * 5 / 100;
			style.normal.textColor = new Color(1.0f, 1.0f, 0.5f, 1.0f);
			float msec = m_deltaTime * 1000.0f;
			float fps = 1.0f / m_deltaTime;
			string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
			GUI.Label(rect, text, style);
		}
	}

	private bool CheckValue()
	{
		if (m_check.isOn)
			return true;
		else
			return false;
	}
}
