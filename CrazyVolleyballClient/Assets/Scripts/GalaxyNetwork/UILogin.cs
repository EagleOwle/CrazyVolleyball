using GalaxyCoreLib;
using GalaxyCoreLib.Api;
using CrasyVolleyballCommon.Messages;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    [SerializeField] private InputField login;
    [SerializeField] private InputField password;
    [SerializeField] private Text status;
    [SerializeField] private Button loginBtn, registrationBtn;
    [SerializeField] private GameObject processConnect;
    [SerializeField] private GameObject registrationPanel;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private bool autoConnect;

    private void Start()
    {
        loginBtn.onClick.AddListener(Authorization);
        registrationBtn.onClick.AddListener(OnButtonRegistration);

        if (autoConnect)
        {
            Authorization();
        }
    }

    private void OnEnable()
    {
        processConnect.SetActive(false);
        GalaxyEvents.OnGalaxyApprovalResponse += OnGalaxyApprovalResponse; //подписка на событие об ошибках авторизации
        GalaxyEvents.OnGalaxyConnect += OnGalaxyConnect; // событие успешного коннекта 
    }

    private void OnGalaxyConnect(byte[] message)
    {
        MessageApproval messageApproval = MessageApproval.Deserialize<MessageApproval>(message); // распаковываем первое ответное сообщение   
        Debug.Log("Наше имя " + messageApproval.Name);
        lobbyPanel.SetActive(true);
        gameObject.SetActive(false);
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

    private void OnGalaxyApprovalResponse(byte code, string message)
    {
        //Debug.Log("OnGalaxyApprovalResponse " + message);
        status.text = message;
        processConnect.SetActive(false);
    }

    /// <summary>
    /// Метод авторизации вызываемый из UI
    /// </summary>
    private void Authorization()
    {
        //Создаем новое сообщение аунтефикации, которое мы положили в GalaxyTemplateCommon
        MessageAuth messageAuth = new MessageAuth();
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

        status.text = "";
        messageAuth.login = login.text;
        messageAuth.password = password.text;
        GalaxyApi.connection.Connect(messageAuth); // Отправляем запрос на сервер    
        processConnect.SetActive(true);

    }

    private void OnButtonRegistration()
    {
        registrationPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
