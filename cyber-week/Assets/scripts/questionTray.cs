using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class questionTray : MonoBehaviour
{

    [SerializeField] GameObject tab;
    [SerializeField] GameObject body;

    [SerializeField] Text questionBox;
    [SerializeField] Text[] optionBoxes;

    [SerializeField] GameObject[] platforms;

    Question question;
    int[] answerOrder;

    string tagRight = "right_answer";
    string tagWrong = "wrong_answer";

    int currentQuestionNumber;

    // Start is called before the first frame update
    void Start()
    {
        loadQuestionsToObject();
        randomiseOrder();
        applyQuestionText();
        applyPlatformTags();
    }

    void loadQuestionsToObject()
    {
        string text = System.IO.File.ReadAllText("list.json");
        QuestionList questionList = QuestionList.CreateFromJSON(text);
        
        currentQuestionNumber = SceneManager.GetActiveScene().buildIndex;
        int index = currentQuestionNumber * 5;

        string questionText = questionList.allQuestions[index];
        string rightAnswer = questionList.allQuestions[index + 1];
        string[] allAnswers = new string[4];
        Array.Copy(questionList.allQuestions, index + 1, allAnswers, 0, 4);

        question = new Question(questionText, rightAnswer, allAnswers);

        foreach (var item in question.allAnswers)
        {
            print(item);
        }
    }

    void applyQuestionText()
    {
        questionBox.text = question.questionText;
        for (int i = 0; i < 4; i++)
        {
            optionBoxes[i].text = question.allAnswers[answerOrder[i]];
        }

    }

    void randomiseOrder()
    {
        List<int> MyArray = new List<int> { 0, 1, 2, 3 };
        System.Random rnd = new System.Random();
        int[] MyRandomArray = MyArray.OrderBy(x => rnd.Next()).ToArray();
        answerOrder = MyRandomArray;
    }

    void applyPlatformTags()
    {
        print("boop");
        foreach (GameObject platform in platforms)      // tag all platforms as wrong
        {
            platform.tag = tagWrong;
        }
        int rightAnswerIndex = Array.Find<int>(answerOrder, i => answerOrder[i] == 0);  // find the platform that the first answer (originally indexed 0) was mapped to
        platforms[rightAnswerIndex].tag = tagRight;                                     // and tag it as right
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            toggleOpen();
        }
    }

    public void toggleOpen()
    {

        toggleActive(tab);
        toggleActive(body);
    }

    void toggleActive(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    [System.Serializable]
    private class Question
    {
        public string questionText;
        public string rightAnswer;
        public string[] allAnswers;

        public Question(string questionText, string rightAnswer, string[] allAnswers)
        {
            this.questionText = questionText;
            this.rightAnswer = rightAnswer;
            this.allAnswers = allAnswers;
        }

        public static Question CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<Question>(jsonString);
        }
    }

    [System.Serializable]
    private class QuestionList
    {
        public string[] allQuestions;

        public QuestionList(string[] allQuestions)
        {
            this.allQuestions = allQuestions;
        }
        
        public static QuestionList CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<QuestionList>(jsonString);
        }
    }

}