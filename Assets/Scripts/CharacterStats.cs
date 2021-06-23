using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    public string charName;
    public int nAsked;
    public int nRating0;
    public int nRating1;
    public int nRating2;
    public int nRating3;
    public int nRating4;
    public int nRating5;

    public CharacterStats(string _charName, int _nAsked, int _nRating0, int _nRating1, int _nRating2, int _nRating3, int _nRating4, int _nRating5)
    {
        charName = _charName;
        nAsked = _nAsked;
        nRating0 = _nRating0;
        nRating1 = _nRating1;
        nRating2 = _nRating2;
        nRating3 = _nRating3;
        nRating4 = _nRating4;
        nRating5 = _nRating5;
    }
}
