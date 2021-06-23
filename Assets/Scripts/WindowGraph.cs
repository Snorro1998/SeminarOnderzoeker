using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        CreateCircle(new Vector2(200, 200));
        List<int> valueList = new List<int>()
        {
            5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 40
        };
        ShowGraph(valueList);
    }

    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gm = new GameObject("circle", typeof(Image));
        gm.transform.SetParent(graphContainer, false);
        gm.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gm.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
    }

    private void ShowGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xSize = 50f;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            CreateCircle(new Vector2(xPosition, yPosition));
        }
    }
}
