using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ratings
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

    public Ratings(int _nRating0, int _nRating1, int _nRating2, int _nRating3, int _nRating4, int _nRating5)
    {

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

    public Button reveal1;
    public Button reveal2;
    public Button reveal3;

    public Question currentQuestion;
    public int questionindex = -1;

    public GameObject disclaimerWindow;
    public GameObject thxWindow;

    [TextArea]
    public List<string> answers = new List<string>();

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisableButtons()
    {
        reveal1.interactable = false;
        reveal2.interactable = false;
        reveal3.interactable = false;
        //nextButton.interactable = true;
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
        questionindex++;
        if (questionindex == askQuestions.Count) End();
        else
        {
            currentQuestion = askQuestions[questionindex];
            SetCharacterVisuals(currentQuestion); 

            reveal1.GetComponentInChildren<Text>().text = "";
            reveal2.GetComponentInChildren<Text>().text = "";
            reveal3.GetComponentInChildren<Text>().text = "";
            reveal1.interactable = true;
            reveal2.interactable = true;
            reveal3.interactable = true;
            //nextButton.interactable = false;
        }
    }

    public void End()
    {
        /*
        TxtName.gameObject.SetActive(false);
        TxtDesc.gameObject.SetActive(false);
        reveal1.gameObject.SetActive(false);
        reveal2.gameObject.SetActive(false);
        reveal3.gameObject.SetActive(false);
        */
        StartCoroutine(EndRoutine());
        thxWindow.SetActive(true);
        //nextButton.gameObject.SetActive(false);
    }

    public void ClickReveal1()
    {
        DisableButtons();
        //reveal1.GetComponentInChildren<Text>().text = currentQuestion.reveal1;
    }

    public void ClickReveal2()
    {
        DisableButtons();
        //reveal2.GetComponentInChildren<Text>().text = currentQuestion.reveal2;
    }

    public void ClickReveal3()
    {
        DisableButtons();
        //reveal3.GetComponentInChildren<Text>().text = currentQuestion.reveal3;
    }

    public void StartGame()
    {
        StartCoroutine(StartRoutine());
    }

    private void FilterNames(string[] names)
    {
        foreach (var n in names)
        {
            if (n != "")
            {
                //Debug.Log(n);
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
        answers.Add(currentQuestion.firstName + "," + rating);
        NextQuestion();
    }


    private IEnumerator StartRoutine()
    {
        disclaimerWindow.SetActive(false);
        yield return StartCoroutine(DBManager.OpenPHPURL("seminar_get_characters"));
        if (DBManager.response != null)
        {
            Debug.Log(DBManager.response);
            FilterNames(DBManager.response.Split(','));
        }
        NextQuestion();
        yield return 0;
    }

    private IEnumerator EndRoutine()
    {
        var q1 = answers[0].Split(',');
        var q2 = answers[1].Split(',');
        var q3 = answers[2].Split(',');

        yield return StartCoroutine(DBManager.OpenPHPURL("seminar_push_results", "name1=" + q1[0], "choice1=" + q1[1], "name2=" + q2[0], "choice2=" + q2[1], "name3=" + q3[0], "choice3=" + q3[1]));
        if (DBManager.response != null)
        {
            Debug.Log("Klaar");
        }
        yield return 0;
    }

    private IEnumerator ResultsRoutine()
    {
        yield return StartCoroutine(DBManager.OpenPHPURL("seminar_get_results"));
        if (DBManager.response != null)
        {
            var strs = DBManager.response.Split(',');
            for (int i = 0; i < strs.Length; i++)
            {
                Debug.Log(strs[i]);
            }
            //Debug.Log("Klaar");
        }
        yield return 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
}
