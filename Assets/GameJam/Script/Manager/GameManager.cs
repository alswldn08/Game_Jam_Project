using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menuSet;
    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    public bool isGameover = false;
    public GameObject gameoverUI;
    public Text scoreText;
    public Button TitleBtn;
    public string sceneName;


    private int score = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }

    }

    void Start()
    {
        if (TitleBtn != null)
            TitleBtn.onClick.AddListener(OnClickTitleBtn); 
        
    }

    void Update()
    {
        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // esc로 메뉴 끄고 켜기
        if (Input.GetButtonDown("Cancel"))
        {
            menuSet.SetActive(!menuSet.activeSelf);

            if (menuSet.activeSelf)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }

    public void OnClickTitleBtn()
    {
        // 씬으로 전환
        SoundManager.Instance.PlayEffect(1);
        SceneManager.LoadScene(sceneName);
    }

    // 점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        if (!isGameover) // 게임 오버 상태가 아닌 경우에만 점수 증가
        {
            score += newScore;
            scoreText.text = "Score: " + score;
        }
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    }

    public void GameExit() // 게임 종료
    {
        Application.Quit();
    }
}
