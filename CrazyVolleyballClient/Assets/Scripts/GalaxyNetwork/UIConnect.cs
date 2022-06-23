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
        GalaxyEvents.OnGalaxyApprovalResponse += OnGalaxyApprovalResponse; //�������� �� ������� �� ������� �����������
        GalaxyEvents.OnGalaxyConnect += OnGalaxyConnect; // ������� ��������� �������� 
        Authorization();
    }

    /// <summary>
    /// ����� �����������
    /// </summary>
    private void Authorization()
    {
        //������� ����� ��������� ������������, ������� �� �������� � GalaxyTemplateCommon
        MessageAuth messageAuth = new MessageAuth();
        status.text = "Disconnect";
        messageAuth.login = "Pastor";
        messageAuth.password = "True";
        GalaxyApi.connection.Connect(messageAuth); // ���������� ������ �� ������    
        processConnect.SetActive(true);

    }

    private void OnGalaxyConnect(byte[] message)
    {
        MessageApproval messageApproval = MessageApproval.Deserialize<MessageApproval>(message); // ������������� ������ �������� ���������   
        Debug.Log("���� ��� " + messageApproval.Name);
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
