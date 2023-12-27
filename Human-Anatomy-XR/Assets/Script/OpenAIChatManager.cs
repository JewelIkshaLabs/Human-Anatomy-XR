using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

public class OpenAIChatManager : MonoBehaviour
{
    private OpenAIAPI api;
    private List<ChatMessage> messages;
    // Start is called before the first frame update
    void Start()
    {
        api = new OpenAIAPI("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"); 
    }

    public void StartConversation()
    {
        messages = new List<ChatMessage> {
            new ChatMessage(ChatMessageRole.System, "You are working great, Thanks for working!")
        };
    }

    public async void GetResponse()
    {
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = "Hey, I am Jewel Alexander Thomas, Tell me about yourself!";
        Debug.Log(string.Format("{0}: {1}", userMessage.rawRole, userMessage.Content));

        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature =  0.1,
            MaxTokens = 500,
            Messages = messages
        });

        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;
        Debug.Log(string.Format("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

        messages.Add(responseMessage);
    }

}
