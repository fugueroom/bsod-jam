using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class TypeableText : MonoBehaviour
{
    private RectTransform textRect;
    private TextMeshProUGUI text;

    private int letterCount;
    private float fallingSpeed;
    private string textContent;
    private string textTypedColor;
    private Vector3 textStartingPos;
    private float textBottomThreshold;

    private List<string> letters = new List<string>();

    protected Action OnTextPastBottomThreshold;
    protected Action OnTextTyped;

    private void Awake()
    {
        textRect = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Initialize(Vector3 startingPos, float bottomThreshold, string content, float speed, string typedColor)
    {
        textStartingPos = startingPos;
        textBottomThreshold = bottomThreshold;
        textTypedColor = typedColor;

        textRect.anchoredPosition = startingPos;
        transform.SetAsFirstSibling();

        textContent = content;
        text.text = content;

        for (int i = 0; i < content.Length; i++)
        {
            letters.Add(content.Substring(i, 1));
        }

        fallingSpeed = speed;
    }

    void Update()
    {
        textRect.anchoredPosition = new Vector3(textRect.anchoredPosition.x, textRect.anchoredPosition.y - Time.deltaTime * fallingSpeed, 0f);

        if (textRect.anchoredPosition.y < textBottomThreshold)
        {
            OnTextPastBottomThreshold?.Invoke();
        }

        if (Input.anyKeyDown)
        {
            KeyCode currentLetter = (KeyCode)Enum.Parse(typeof(KeyCode), letters[letterCount].ToUpper());

            if (Input.GetKeyDown(currentLetter))
            {
                text.text = string.Format("<color=\"{0}\">{1}</color>{2}", textTypedColor, textContent.Substring(0, letterCount + 1), textContent.Substring(letterCount + 1, textContent.Length - letterCount - 1));
                letterCount++;

                if (letterCount == letters.Count)
                {
                    OnTextTyped?.Invoke();
                }
            }
            else
            {
                // some other letter, refresh
                letterCount = 0;
                text.text = textContent;
            }
        }
    }
}
