﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Girl : MonoBehaviour
{

    private Hashtable questionResponses;
    private Hashtable talkResponses;

    public struct QuestionResponse
    {
        public string response;
        public string knowledge;

        public QuestionResponse(string r, string k)
        {
            response = r;
            knowledge = k;
        }
    };

    public void Init(GoodQuestion[] questions)
    {
        questionResponses = new Hashtable();
        talkResponses = new Hashtable();
        foreach (var q in questions)
        {
            foreach (var a in q.Answer)
            {
                AddQuestionResponse(q.Sentence, a.Sentence, a.Knowlegde.Correct);
                AddTalkResponse(a.Knowlegde.Correct, a.Knowlegde.TalkResponse);
            }
        }
    }

    private void AddQuestionResponse(string question, string response, string knowledge)
    {
        QuestionResponse qr = new QuestionResponse(response, knowledge);

        if (!questionResponses.ContainsKey(question))
        {
            questionResponses.Add(question, new List<QuestionResponse> { qr });
        }
        else
        {
            ((List<QuestionResponse>)questionResponses[question]).Add(qr);
        }
    }

    private void AddTalkResponse(string knowledge, string response)
    {
        talkResponses.Add(knowledge, response);
    }

    public List<string> GetQuestionResponse(string inquery)
    {
        List<QuestionResponse> responses = (List<QuestionResponse>)questionResponses[inquery];

        QuestionResponse response = responses[Random.Range(0, responses.Count)];

        return new List<string> { response.response, response.knowledge };
    }

    public string GetTalkResponse(string inquery)
    {
        if (!talkResponses.Contains(inquery))
            return "Uh oh, looks like Acerola forgot to code a response to this, or he made a typo. It's quite hard to test all of this by yourself you know.";

        return (string)talkResponses[inquery];
    }
}


