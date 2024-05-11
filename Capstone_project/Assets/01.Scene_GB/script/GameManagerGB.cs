using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // TextMeshPro 네임스페이스 사용
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스 추가

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 인스턴스

    // 추가된 부분: 씬 로드 횟수를 추적
    private int scenesLoadedCount = 0;

    public Image successImage;
    public Image failImage;
    public TextMeshProUGUI successScoreText;
    public TextMeshProUGUI failScoreText;
    public TextMeshProUGUI timeText;
    public Button gameButton;

    private int successScore = 0;
    private int failScore = 0;
    private float timeRemaining = 10;
    private bool isGameActive = false;
    private int totalRounds = 5;
    private int currentRound = 0;

    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scenesLoadedCount++; // 씬이 로드될 때마다 카운트 증가
        
    }

    public int GetScenesLoadedCount()
    {
        return scenesLoadedCount; // 로드된 씬의 총 개수 반환
    }
   

   

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않음
            SceneManager.sceneLoaded += OnSceneLoaded; // 씬이 로드될 때마다 OnSceneLoaded 호출
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }

        successImage.gameObject.SetActive(false);
        failImage.gameObject.SetActive(false);
        gameButton.onClick.AddListener(HandleButtonClick);
        UpdateScoreText();
        timeText.text = "10"; // 초기 제한 시간 텍스트 설정

        StartRound(); // 게임 시작을 위한 호출
    }


    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimeText(); // 제한 시간 텍스트 업데이트
            }
            else
            {
                EndRound(false);
            }
        }
    }

    void UpdateTimeText()
    {
        // timeText가 null이 아닐 때만 실행
        if (timeText != null)
        {
            timeText.text = Mathf.Ceil(timeRemaining).ToString(); // 제한 시간을 반올림하여 표시
        }
        else
        {
            Debug.LogWarning("timeText is not assigned!");
        }
    }

    void HandleButtonClick()
    {
        if (!isGameActive && currentRound < totalRounds)
        {
            StartRound();
        }
        else if (isGameActive)
        {
            EndRound(true);
        }
    }

    void StartRound()
    {
        isGameActive = true;
        timeRemaining = 10; // 10초 제한시간 재설정

        // successImage와 failImage가 null이 아닐 때만 실행
        if (successImage != null && failImage != null)
        {
            successImage.gameObject.SetActive(false);
            failImage.gameObject.SetActive(false);
        }
        else
        {
            if (successImage == null)
            {
                Debug.LogWarning("successImage is not assigned!");
            }
            if (failImage == null)
            {
                Debug.LogWarning("failImage is not assigned!");
            }
        }
        UpdateTimeText();
    }

    void EndRound(bool isSuccess)
    {
        isGameActive = false;
        timeRemaining = 10; // 제한 시간 초기화
        currentRound++;

        if (isSuccess)
        {
            successScore++;
            successImage.gameObject.SetActive(true);
        }
        else
        {
            failScore++;
            failImage.gameObject.SetActive(true);
        }

        UpdateScoreText();
        StartCoroutine(ShowResultAndContinue());
    }

    IEnumerator ShowResultAndContinue()
    {
        yield return new WaitForSeconds(2); // 2초 동안 결과 표시
        successImage.gameObject.SetActive(false);
        failImage.gameObject.SetActive(false);

        CheckGameEnd(); // 게임 종료 체크
    }

    void UpdateScoreText()
    {
        successScoreText.text = "성공 : " + successScore;
        failScoreText.text = "실패 : " + failScore;
    }

    void CheckGameEnd()
    {
        if (currentRound >= totalRounds)
        {
            // 모든 라운드가 완료되면 다음 씬으로 전환
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            StartRound(); // 다음 라운드 시작
        }
    }
    public int GetSuccessScore()
    {
        return successScore;
    }

    public int GetFailScore()
    {
        return failScore;
    }
}

