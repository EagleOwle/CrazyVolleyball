using GalaxyCoreLib;
using GalaxyCoreLib.Api;
using CrasyVolleyballCommon.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRegistration : MonoBehaviour
{
    [SerializeField] private InputField login;
    [SerializeField] private InputField password;
    [SerializeField] private InputField confirmPassword;
    [SerializeField] private Text status;
    [SerializeField] private Button registrationBtn;
    [SerializeField] private Button returnBtn;
    [SerializeField] private GameObject processConnect;
    [SerializeField] private GameObject loginPanel;

    private void Start()
    {
        registrationBtn.onClick.AddListener(Registration);
        returnBtn.onClick.AddListener(OnButtonReturn);
    }

    private void OnEnable()
    {
        processConnect.SetActive(false);
        GalaxyEvents.OnGalaxyApprovalResponse += GalaxyEvents_OnGalaxyApprovalResponse;
    }

    private void GalaxyEvents_OnGalaxyApprovalResponse(byte code, string message)
    {
        //Debug.Log("Пришел ответ с сервера: " + message);

        switch ((ServerApproval)code)
        {
            case ServerApproval.accept:
                status.text = message;
                gameObject.SetActive(false);
                break;

            case ServerApproval.denial:
                status.text = message;
                processConnect.SetActive(false);
                break;
        }
    }

    private void OnDisable()
    {
        GalaxyEvents.OnGalaxyApprovalResponse -= GalaxyEvents_OnGalaxyApprovalResponse;
    }


    private void OnDestroy()
    {
        GalaxyEvents.OnGalaxyApprovalResponse -= GalaxyEvents_OnGalaxyApprovalResponse;
    }

    private void Registration()
    {
        //Создаем новое сообщение аунтефикации, которое мы положили в GalaxyTemplateCommon
        MessageRegistration message = new MessageRegistration();

        if (login.text.Length < 4)
        {
            status.text = "Слишком короткий короткий логин";
            return;
        }

        if (password.text.Length < 1)
        {
            status.text = "Введите пароль";
            return;
        }

        if (password.text.Length < 4)
        {
            status.text = "Слишком короткий пароль";
            return;
        }

        if (confirmPassword.text.Length < 1)
        {
            status.text = "Повторите пароль";
            return;
        }

        if (password.text != confirmPassword.text)
        {
            status.text = "Пароли не совпадают.";
            return;
        }


        status.text = "";
        message.login = login.text;
        message.password = password.text;
        GalaxyApi.connection.Registration(message); // Отправляем запрос на сервер 
        processConnect.SetActive(true);

    }

    private void OnButtonRegistration()
    {
        Registration();
    }

    private void OnButtonReturn()
    {
        gameObject.SetActive(false);
        loginPanel.SetActive(true);
    }

}
