using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
// using System.Web.Script.Serialization;
using UnityEngine;
using UnityEngine.UI;

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
        string text = System.IO.File.ReadAllText("questions.json");
        print(text);
        // var serializer = new JavaScriptSerializer();
        string questionText = "Second question";
        string rightAnswer = "Apple";
        string[] allAnswers = new string[] { "Apple", "Pineapple", "Orange", "Papaya" };
        question = new Question(questionText, rightAnswer, allAnswers);
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
        foreach (GameObject platform in platforms)
        {
            platform.tag = tagWrong;
        }
        int rightAnswerIndex = Array.Find<int>(answerOrder, i => answerOrder[i] == 0);
        platforms[rightAnswerIndex].tag = tagRight;
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
    }

}