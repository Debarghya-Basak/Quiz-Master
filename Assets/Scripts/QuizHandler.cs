using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizHandler : MonoBehaviour
{

    bool GAMESTATE = true;

    // [Header("Questions")] 
    [SerializeField] GameObject questionField;
    [SerializeField] GameObject[] mcqButtons = new GameObject[4];
    [SerializeField] List<QuestionMaker> questions = new List<QuestionMaker>();

    TextMeshProUGUI questionText;
    TextMeshProUGUI[] mcqText = new TextMeshProUGUI[4];

    bool answerClicked = false;

    [SerializeField] float defaultQuestionTimer = 5f;
    [SerializeField] float defaultAnswerTimer = 2f;
    float showQuestionTimer;
    float showAnswerTimer; 
    bool questionState = true;
    int questionNumberIndex = 0;
    int globalClickedIndex = -1;

    [SerializeField] AudioClip[] sounds = new AudioClip[4];

    [SerializeField] Sprite[] buttonColors = new Sprite[4];

    void Start()
    {
        //Initialize timer
        showQuestionTimer = defaultQuestionTimer;
        showAnswerTimer = defaultAnswerTimer;   
        GetComponent<AudioSource>().PlayOneShot(sounds[0]);

        displayQuestionAtIndex(0);
       
    }

    void Update()
    {
        timer();
    }

    private void displayQuestionAtIndex(int index){

        questionText = questionField.GetComponent<TextMeshProUGUI>();
        questionText.text = questions[index].getQuestion();

        for(int i=0;i<4;i++){
            mcqText[i] = mcqButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            mcqText[i].text = questions[index].getMCQAtIndex(i);
        }

    }

    private void timer(){

        if(GAMESTATE){
            if(questionState){
            showQuestionTimer -= Time.deltaTime;
                Debug.Log("Question Timer : " + showQuestionTimer);
                if(showQuestionTimer <= 0){
                    //TODO: Show answer
                    GetComponent<AudioSource>().Stop();
                    //Correct answer
                    if(questions[questionNumberIndex].getCorrectAnswerIndex() == globalClickedIndex){
                        mcqButtons[questions[questionNumberIndex].getCorrectAnswerIndex()].GetComponent<Image>().sprite = buttonColors[2];
                        GetComponent<AudioSource>().PlayOneShot(sounds[2]);
                    }//Wrong answer
                    else if(questions[questionNumberIndex].getCorrectAnswerIndex() != globalClickedIndex && globalClickedIndex > -1){
                        mcqButtons[questions[questionNumberIndex].getCorrectAnswerIndex()].GetComponent<Image>().sprite = buttonColors[2];
                        mcqButtons[globalClickedIndex].GetComponent<Image>().sprite = buttonColors[3];
                        GetComponent<AudioSource>().PlayOneShot(sounds[3]);
                    }
                    else{
                        Debug.Log("Not clicked");
                        mcqButtons[questions[questionNumberIndex].getCorrectAnswerIndex()].GetComponent<Image>().sprite = buttonColors[2];
                        GetComponent<AudioSource>().PlayOneShot(sounds[3]);
                    }

                    showQuestionTimer = defaultQuestionTimer;
                    questionState = false;
                }    
            }
            else{
                showAnswerTimer -= Time.deltaTime;
                Debug.Log("Answer Timer : " + showAnswerTimer);
                if(showAnswerTimer <= 0){
                    //TODO: Show next question
                    GetComponent<AudioSource>().PlayOneShot(sounds[0]);
                    answerClicked = false;
                    enableOrDisableButtons(!answerClicked);

                    if(questionNumberIndex >= questions.Count){
                        GAMESTATE = false;
                        //SceneManager.LoadScene(Actu)
                        enableOrDisableButtons(false);
                        GetComponent<AudioSource>().Stop();
                    }
                    else
                        displayQuestionAtIndex(++questionNumberIndex);
                    globalClickedIndex = -1;
                    for(int j=0;j<4;j++){
                         mcqButtons[j].GetComponent<Image>().sprite = buttonColors[0];
                    }

                    showAnswerTimer = defaultAnswerTimer;
                    questionState = true;        
                }            
            }    

        }
    }

    public void markAnswerGivenByUser(int index){

        Debug.Log(index + " button is clicked.");
        globalClickedIndex = index;
        mcqButtons[index].GetComponent<Image>().sprite = buttonColors[1];
        GetComponent<AudioSource>().PlayOneShot(sounds[1]);
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
