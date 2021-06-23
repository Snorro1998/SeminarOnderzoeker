using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : Singleton<QuizManager>
{
    public List<Question> allQuestions;
    public List<Question> askQuestions;

    public TextMeshProUGUI TxtName;
    public TextMeshProUGUI TxtAge;
    public TextMeshProUGUI TxtGender;
    public TextMeshProUGUI TxtOccupation;
    public TextMeshProUGUI TxtStatus;

    public TextMeshProUGUI TxtDesc;
    public TextMeshProUGUI TxtQuote;

    public Question currentQuestion;
    public int questionindex = -1;

    public Button readItButton;
    public Button nextQuestionButton;
    public Button gotoResultsButton;

    public GameObject prefabButton;
    public RectTransform resultsButtonGroup;

    private string currentAnswer = "";

    [TextArea]
    public List<string> answers = new List<string>();

    public List<CharacterStats> chars = new List<CharacterStats>();

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetCharacterVisuals(Question character)
    {
        TxtName.text = character.firstName + " " + character.lastName;
        TxtAge.text = "<color=#FFFFFFFF><b>Leeftijd: </b></color>" + character.age.ToString();
        TxtGender.text = "<color=#FFFFFFFF><b>Geslacht: </b></color>" + character.gender;
        TxtOccupation.text = "<color=#FFFFFFFF><b>Beroep: </b></color>" + character.occupation;
        TxtStatus.text = "<color=#FFFFFFFF><b>Status: </b></color>" + character.status;

        TxtDesc.text = character.description;
        TxtQuote.text = "\"" + character.quote + "\"";
    }

    public void NextQuestion()
    {
        //readItButton.interactable = false;
        nextQuestionButton.interactable = false;

        RevealRatingButtons.Instance.DisableRatingButtons("wtf");
        if (currentAnswer != "") answers.Add(currentAnswer);
        questionindex++;
        // t was de laatste vraag
        if (questionindex == askQuestions.Count)
        {
            ScreenSwitcher.Instance.SwitchScreen(ActiveScreen.THANKS);
        }
        else
        {
            currentQuestion = askQuestions[questionindex];
            SetCharacterVisuals(currentQuestion);
        }
    }

    private void FilterNames(string[] names)
    {
        foreach (var n in names)
        {
            if (n != "")
            {
                foreach (var qq in allQuestions)
                {
                    if (n == qq.firstName)
                    {
                        askQuestions.Add(qq);
                    }
                }
            }
            
        }
    }

    public void RateCharacter(string rating)
    {
        currentAnswer = currentQuestion.firstName + "," + rating;
        nextQuestionButton.interactable = true;
    }


    public IEnumerator StartRoutine()
    {
        Debug.Log("getcharacters");
        yield return StartCoroutine(DBManager.OpenPHPURL("seminar_get_characters"));
        if (DBManager.response != null)
        {
            Debug.Log(DBManager.response);
            FilterNames(DBManager.response.Split(','));
        }
        NextQuestion();
        yield return 0;
    }

    public IEnumerator EndRoutine()
    {
        var q1 = answers[0].Split(',');
        var q2 = answers[1].Split(',');
        var q3 = answers[2].Split(',');

        yield return StartCoroutine(DBManager.OpenPHPURL("seminar_push_results", "name1=" + q1[0], "choice1=" + q1[1], "name2=" + q2[0], "choice2=" + q2[1], "name3=" + q3[0], "choice3=" + q3[1]));
        if (DBManager.response != null)
        {
            Debug.Log("Resultaten geupload!");
            gotoResultsButton.gameObject.SetActive(true);
        }
        yield return 0;
    }

    public IEnumerator ResultsRoutine()
    {
        yield return StartCoroutine(DBManager.OpenPHPURL("seminar_get_results"));
        if (DBManager.response != null)
        {
            var strs = DBManager.response.Split(',');
            for (int i = 0; i < strs.Length; i++)
            {
                Debug.Log(strs[i]);
            }
            for (int j = 0; j < allQuestions.Count; j++)
            {
                string cname = strs[j * allQuestions.Count];
                int nAsked = int.Parse(strs[j * allQuestions.Count + 1]);
                int nRating0 = int.Parse(strs[j * allQuestions.Count + 2]);
                int nRating1 = int.Parse(strs[j * allQuestions.Count + 3]);
                int nRating2 = int.Parse(strs[j * allQuestions.Count + 4]);
                int nRating3 = int.Parse(strs[j * allQuestions.Count + 5]);
                int nRating4 = int.Parse(strs[j * allQuestions.Count + 6]);
                int nRating5 = int.Parse(strs[j * allQuestions.Count + 7]);
                chars.Add(new CharacterStats(cname, nAsked, nRating0, nRating1, nRating2, nRating3, nRating4, nRating5));
            }
            //Debug.Log("Klaar");
        }
        foreach (var i in chars)
        {
            var j = Instantiate(prefabButton, resultsButtonGroup);
            Text t = j.GetComponentInChildren<Text>();
            t.text = i.charName;
            Button b = j.GetComponent<Button>();
            List<int> ints = new List<int> { i.nRating0, i.nRating1, i.nRating2, i.nRating3, i.nRating4, i.nRating5 };
            b.onClick.AddListener(delegate {Window_Graph.Instance.ShowGraph(ints, -1, (int _i) => "" + (_i + 1), (float _f) => "" + Mathf.RoundToInt(_f));});
            //b.onClick.AddListener(Window_Graph.Instance.ShowGraph(ints, -1, (int _i) => "Day " + (_i + 1), (float _f) => "$" + Mathf.RoundToInt(_f)));
        }
        yield return 0;
    }

    private void Start()
    {
        ScreenSwitcher.Instance.SwitchScreen(ActiveScreen.DISCLAIMER);
        gotoResultsButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
}
