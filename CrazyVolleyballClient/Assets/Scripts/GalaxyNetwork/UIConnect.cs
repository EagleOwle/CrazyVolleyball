using CrasyVolleyballCommon.Messages;
using GalaxyCoreLib;
using GalaxyCoreLib.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConnect : MonoBehaviour
{
    [SerializeField] private GameObject processConnect;
    [SerializeField] private Text status;

    private void OnEnable()
    {
        processConnect.SetActive(false);
        GalaxyEvents.OnGalaxyApprovalResponse += OnGalaxyApprovalResponse; //подписка на событие об ошибках авторизации
        GalaxyEvents.OnGalaxyConnect += OnGalaxyConnect; // событие успешного коннекта 
        Authorization();
    }

    /// <summary>
    /// Метод авторизации
    /// </summary>
    private void Authorization()
    {
        //Создаем новое сообщение аунтефикации, которое мы положили в GalaxyTemplateCommon
        MessageAuth messageAuth = new MessageAuth();
        status.text = "Disconnect";
        messageAuth.login = "Pastor";
        messageAuth.password = "True";
        GalaxyApi.connection.Connect(messageAuth); // Отправляем запрос на сервер    
        processConnect.SetActive(true);

    }

    private void OnGalaxyConnect(byte[] message)
    {
        MessageApproval messageApproval = MessageApproval.Deserialize<MessageApproval>(message); // распаковываем первое ответное сообщение   
        Debug.Log("Наше имя " + messageApproval.Name);
        processConnect.SetActive(false);
    }

    private void OnGalaxyApprovalResponse(byte code, string message)
    {
        //Debug.Log("OnGalaxyApprovalResponse " + message);
        status.text = message;
        processConnect.SetActive(false);
    }

    private void OnDisable()
    {
        GalaxyEvents.OnGalaxyApprovalResponse -= OnGalaxyApprovalResponse;
        GalaxyEvents.OnGalaxyConnect -= OnGalaxyConnect;
    }


    private void OnDestroy()
    {
        GalaxyEvents.OnGalaxyApprovalResponse -= OnGalaxyApprovalResponse;
        GalaxyEvents.OnGalaxyConnect -= OnGalaxyConnect;
    }


}
