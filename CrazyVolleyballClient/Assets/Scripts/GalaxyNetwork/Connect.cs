using GalaxyCoreLib.Api;
using CrasyVolleyballCommon.Messages;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Connect : MonoBehaviour
{
    [SerializeField] private Text statusText;
    [SerializeField] private Text connectBtnText;
    [SerializeField] private Button cancelBtn;
    [SerializeField] Transform processImage;
    private bool processConnect = false;

    private void Awake()
    {
        cancelBtn.onClick.AddListener(OnButtonCancel);
    }

    private void Start()
    { 
        GalaxyEvents.OnGalaxyConnect += OnGalaxyConnect;
        OnButtonConnect();
    }

    private void OnGalaxyConnect(byte[] message)
    {
        MessageApproval messageApproval = MessageApproval.Deserialize<MessageApproval>(message); // распаковываем первое ответное сообщение   
    }

    private void OnButtonCancel()
    {
        processConnect = false;
        statusText.text = "Отключен";
        connectBtnText.text = "Подключится";
        cancelBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.AddListener(OnButtonConnect);
    }

    private void OnButtonConnect()
    {
        StartCoroutine(ProcessConnect());
        statusText.text = "Подключаюсь к серверу";
        connectBtnText.text = "Отмена";
        cancelBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.AddListener(OnButtonCancel);
    }

    IEnumerator ProcessConnect()
    {
        processConnect = true;

        while (processConnect)
        {
            processImage.eulerAngles += Vector3.forward * 500 * Time.deltaTime;
            yield return null;
        }
    }
}
