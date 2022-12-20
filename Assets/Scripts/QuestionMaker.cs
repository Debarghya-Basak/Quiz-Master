using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question 1", menuName = "Quiz Master/New Question", order = 0)]
public class QuestionMaker : ScriptableObject {

    [TextArea(1,5)]
    [SerializeField] string question =  "Enter your question here";
    [SerializeField] string[] mcq = new string[4];
    [SerializeField] int correctAnswerIndex;

    public string getQuestion(){
        return question;
    }

    public string getMCQAtIndex(int index){
        return mcq[index];
    }

    public int getCorrectAnswerIndex(){
        return correctAnswerIndex;
    }
        
}