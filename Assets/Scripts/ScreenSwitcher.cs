using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ActiveScreen
{
    DISCLAIMER,
    INTRO,
    QUESTIONS,
    THANKS,
    RESULTS,
}

public class ScreenSwitcher : Singleton<ScreenSwitcher>
{
    public GameObject windowDisc;
    public GameObject windowIntro;
    public GameObject windowQuestions;
    public GameObject windowThx;
    public GameObject windowResults;

    private Dictionary<ActiveScreen, GameObject> screens = new Dictionary<ActiveScreen, GameObject>();

    private void InitDict()
    {
        screens.Add(ActiveScreen.DISCLAIMER, windowDisc);
        screens.Add(ActiveScreen.INTRO, windowIntro);
        screens.Add(ActiveScreen.QUESTIONS, windowQuestions);
        screens.Add(ActiveScreen.THANKS, windowThx);
        screens.Add(ActiveScreen.RESULTS, windowResults);
    }

    public void SwitchScreen(ActiveScreen screen)
    {
        if (screens.ContainsKey(screen))
        {
            foreach (var i in screens)
            {
                if (i.Key == screen) i.Value.SetActive(true);
                else i.Value.SetActive(false);
            }
            OnScreenSwitched(screen);
        }
    }

    public void OnScreenSwitched(ActiveScreen screen)
    {
        switch (screen)
        {
            case ActiveScreen.QUESTIONS:
                StartCoroutine(QuizManager.Instance.StartRoutine());
                break;
            case ActiveScreen.THANKS:
                StartCoroutine(QuizManager.Instance.EndRoutine());
                break;
            case ActiveScreen.RESULTS:
                StartCoroutine(QuizManager.Instance.ResultsRoutine());
                break;
        }
    }

    public void SwitchToQuestions()
    {
        SwitchScreen(ActiveScreen.QUESTIONS);
    }

    public void SwitchToResults()
    {
        SwitchScreen(ActiveScreen.RESULTS);
    }

    public void SwitchToIntro()
    {
        SwitchScreen(ActiveScreen.INTRO);
    }

    protected override void Awake()
    {
        base.Awake();
        InitDict();
    }
}
