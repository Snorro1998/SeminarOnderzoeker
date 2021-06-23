using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealRatingButtons : MonoBehaviour
{
    public GameObject HidingSprite;
    public void EnableRatingButtons(string rating)
    {
        HidingSprite.SetActive(false);
    }

    public void DisableRatingButtons(string rating)
    {
        HidingSprite.SetActive(true);
    }
}
