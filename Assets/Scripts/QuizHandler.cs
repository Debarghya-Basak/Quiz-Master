using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizHandler : MonoBehaviour
{
    [SerializeField] GameObject questionField;
    [SerializeField] GameObject[] mcqButtons = new GameObject[4];
    [SerializeField] QuestionMaker[] questions = new QuestionMaker[100];

    TextMeshProUGUI questionText;
    TextMeshProUGUI[] mcqText = new TextMeshProUGUI[4];

    bool answerClicked = false;

    float defaultQuestionTimer = 5f;
    float defaultAnswerTimer = 2f;
    float showQuestionTimer;
    float showAnswerTimer; 
    bool questionState = true;

    void Start()
    {
        //Initialize timer
        showQuestionTimer = defaultQuestionTimer;
        showAnswerTimer = defaultAnswerTimer;    


        questionText = questionField.GetComponent<TextMeshProUGUI>();
        questionText.text = questions[0].getQuestion();

        for(int i=0;i<4;i++){
            mcqText[i] = mcqButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            mcqText[i].text = questions[0].getMCQAtIndex(i);
        }
        
    }

    void Update()
    {
        if(questionState){
            showQuestionTimer -= Time.deltaTime;
            Debug.Log("Question Timer : " + showQuestionTimer);
            if(showQuestionTimer <= 0){
                //TODO: Show answer

                showQuestionTimer = defaultQuestionTimer;
                questionState = false;
            }
        }
        else{
            showAnswerTimer -= Time.deltaTime;
            Debug.Log("Answer Timer : " + showAnswerTimer);
            if(showAnswerTimer <= 0){
                //TODO: Show next question
                answerClicked = false;
                enableOrDisableButtons(!answerClicked);

                showAnswerTimer = defaultAnswerTimer;
                questionState = true;
            }
        }
        
    }

    public void markAnswerGivenByUser(int index){

        Debug.Log(index + " button is clicked.");
        answerClicked = true;
        enableOrDisableButtons(!answerClicked);

    }

    public void enableOrDisableButtons(bool buttonState){
        if(buttonState){
            for(int i=0;i<4;i++){
                mcqButtons[i].GetComponent<Button>().interactable = true;
            }
        }
        else{
            for(int i=0;i<4;i++){
                mcqButtons[i].GetComponent<Button>().interactable = false;
            }
        }
        
        
    }
}
