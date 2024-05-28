using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; 
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    private int scenesLoadedCount = 0;

    public Image successImage;
    public Image failImage;
    public TextMeshProUGUI successScoreText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI timeText;
    public Button gameButton;

    private int successScore = 0;
    private int failScore = 0;
    private float timeRemaining = 10;
    private bool isGameActive = false;
    private int totalRounds = 5;
    public int TotalRounds{
        get{ return totalRounds; }
    }
    private int currentRound = 0;

    [SerializeField]
    private List<VideoClip> videoClips = new List<VideoClip>();
    private List<string> videoMessages = new List<string>(){"보자기", "주먹", "가위", "최고"};
    [SerializeField]
    private TMP_Text guideText;
    [SerializeField]
    private VideoPlayer gameCharacter;

    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scenesLoadedCount++; 
    }

    public int GetScenesLoadedCount()
    {
        return scenesLoadedCount; 
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
            SceneManager.sceneLoaded += OnSceneLoaded; 
            // SceneManager.sceneLoaded -= OnSceneLoaded; 추가가 필요함
        }
        else
        {
            Destroy(gameObject); 
        }
        successImage.gameObject.SetActive(false);
        failImage.gameObject.SetActive(false);
        gameButton.onClick.AddListener(HandleButtonClick);
        totalRounds = GamePlayData.actionCount;
        UpdateScoreText();
        // 약점 보완 모드 true라면 보완 모드 실행 함수 호출
        timeText.text = "10"; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            if(Input.GetKey(KeyCode.Z))
            {
                HandleButtonClick();
            }
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimeText(); 
            }
            else
            {
                EndRound(false);
            }
        }
    }


    private bool OnKeyboardInput(KeyCode z)
    {
        throw new System.NotImplementedException();
    }

    void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = Mathf.Ceil(timeRemaining).ToString(); 
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

    public void StartRound()
    {
        isGameActive = true;
        timeRemaining = 10;
        int ranNum = Random.RandomRange(0, 3);
        gameCharacter.clip = videoClips[ranNum];
        guideText.text = "동작을 따라해보세요(" + videoMessages[ranNum] + ")";
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
        timeRemaining = 10; 
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
        yield return new WaitForSeconds(2); 
        successImage.gameObject.SetActive(false);
        failImage.gameObject.SetActive(false);

        CheckGameEnd(); 
    }

    void UpdateScoreText()
    {
        successScoreText.text = "성공 : " + successScore;
        roundText.text = currentRound + " / " + totalRounds;
    }

    void CheckGameEnd()
    {
        if (currentRound >= totalRounds)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            StartRound(); 
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

