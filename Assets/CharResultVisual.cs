using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharResultVisual : MonoBehaviour
{
    public int nTotal = 0;
    public int nRating0 = 0;
    public int nRating1 = 0;
    public int nRating2 = 0;
    public int nRating3 = 0;
    public int nRating4 = 0;
    public int nRating5 = 0;

    public Slider ratObj0;
    public Slider ratObj1;
    public Slider ratObj2;
    public Slider ratObj3;
    public Slider ratObj4;
    public Slider ratObj5;

    public void SetValues(int _nAsked, int _nRating0, int _nRating1, int _nRating2, int _nRating3, int _nRating4, int _nRating5)
    {
        UpdateRatings();
    }

    public void UpdateRatings()
    {
        ratObj0.value = Mathf.InverseLerp(0, nTotal, nRating0);
        ratObj1.value = Mathf.InverseLerp(0, nTotal, nRating1);
        ratObj2.value = Mathf.InverseLerp(0, nTotal, nRating2);
        ratObj3.value = Mathf.InverseLerp(0, nTotal, nRating3);
        ratObj4.value = Mathf.InverseLerp(0, nTotal, nRating4);
        ratObj5.value = Mathf.InverseLerp(0, nTotal, nRating5);
    }
}
