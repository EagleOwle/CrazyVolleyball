using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIPanel
{
    [SerializeField] private Button _startGameBtn;
    [SerializeField] private Button _preferenceBtn;
    [SerializeField] private Button _connectBtn;
    [SerializeField] private Button _quitBtn;

    public override void Init()
    {
        base.Init();
        _startGameBtn.onClick.AddListener(OnButtonStart);
        _preferenceBtn.onClick.AddListener(OnButtonPreference);
        _connectBtn.onClick.AddListener(OnButtonConnect);
        _quitBtn.onClick.AddListener(OnButtonQuit);
    }

    private void OnButtonStart()
    {
        UIHud.Singletone.OnChangePanel(UIPanelName.ScenePreference);
    }

    private void OnButtonPreference()
    {
        UIHud.Singletone.OnChangePanel(UIPanelName.Preference);
    }

    private void OnButtonConnect()
    {
        UIHud.Singletone.OnChangePanel(UIPanelName.Connect);
    }

    private void OnButtonQuit()
    {
        QuitApplication();
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
        Application.Quit();
#elif UNITY_IOS
         Application.Quit();
#else
         Application.Quit();
#endif
    }

}
