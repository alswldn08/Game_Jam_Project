using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    public Button StartBtn;
    public Button SettingBtn; // 설정 버튼 추가
    public Button SettingEsc; //설정창 나가기 버튼
    public Button HelpBtn; //도움말 버튼 추가
    public Button HelpEsc; //도움말 나가기 버튼

    public string sceneName;
    public Image settingpage;
    public Image helppage;

    // Start is called before the first frame update
    void Start()
    {
        StartBtn.onClick.AddListener(OnClickStartBtn);
        SettingBtn.onClick.AddListener(OnClickSettingBtn);
        SettingEsc.onClick.AddListener(OnClickSettingEsc);
        HelpBtn.onClick.AddListener(OnClickHelpBtn);
        HelpEsc.onClick.AddListener(OnClickHelpEsc);

        SoundManager.Instance.PlaySound(0);


        // 처음에 이미지를 비활성화
        settingpage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClickStartBtn() // 게임 시작
    {
        // 씬으로 전환
        SceneManager.LoadScene(sceneName);
    }

    void OnClickSettingBtn() // 설정 버튼 클릭 시
    {
        // 이미지를 활성화
        settingpage.gameObject.SetActive(true);
    }
    void OnClickHelpBtn() // 설정 버튼 클릭 시
    {
        // 이미지를 활성화
        helppage.gameObject.SetActive(true);
    }

    void OnClickSettingEsc()
    {
        settingpage.gameObject.SetActive(false);
    }
    void OnClickHelpEsc()
    {
        helppage.gameObject.SetActive(false);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}