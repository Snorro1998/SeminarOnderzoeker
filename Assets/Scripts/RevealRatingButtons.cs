using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealRatingButtons : Singleton<RevealRatingButtons>
{
    public GameObject HidingSprite;
    public void EnableRatingButtons(string rating)
    {
        HidingSprite.SetActive(false);
        QuizManager.Instance.readItButton.gameObject.SetActive(false);
    }

    public void DisableRatingButtons(string rating)
    {
        HidingSprite.SetActive(true);
        QuizManager.Instance.readItButton.gameObject.SetActive(true);
    }
}
