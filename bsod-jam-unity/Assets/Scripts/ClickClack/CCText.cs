using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CCText : MonoBehaviour
{
    [SerializeField]
    private RectTransform textRect;

    [SerializeField]
    private TextMeshProUGUI text;

    List<string> letters = new List<string>();
    string actualWord;
    int letterCount = 0;
    float fallingSpeed = 0;

    public void Initialize(Vector3 startingPos, string word, float speed)
    {
        textRect.anchoredPosition = startingPos;
        transform.SetAsFirstSibling();

        actualWord = word;
        text.text = word;

        for (int i = 0; i < word.Length; i++)
        {
            letters.Add(word.Substring(i, 1));
        }

        fallingSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        textRect.anchoredPosition = new Vector3(textRect.anchoredPosition.x, textRect.anchoredPosition.y - Time.deltaTime * fallingSpeed, 0f);

        if (textRect.anchoredPosition.y < -230f)
        {
            // game over
            Destroy(gameObject);
            CCManager.Instance.GameOver();
        }

        KeyCode currentLetter = (KeyCode) System.Enum.Parse(typeof(KeyCode), letters[letterCount].ToUpper());

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(currentLetter))
            {
                text.text = string.Format("<color=\"green\">{0}</color>{1}", actualWord.Substring(0, letterCount + 1), actualWord.Substring(letterCount + 1, actualWord.Length - letterCount - 1));
                letterCount++;

                if (letterCount == letters.Count)
                {
                    // Word has been typed!
                    Destroy(gameObject);
                    CCManager.Instance.AddToScore();
                }
            }
            else
            {
                // some other letter, refresh
                letterCount = 0;
                text.text = actualWord;
            }
        }
    }
}
