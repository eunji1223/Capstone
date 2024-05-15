using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // TextMeshPro ���ӽ����̽� ���
using UnityEngine.SceneManagement; // �� ������ ���� ���ӽ����̽� �߰�

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱��� �ν��Ͻ�

    // �߰��� �κ�: �� �ε� Ƚ���� ����
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
        scenesLoadedCount++; // ���� �ε�� ������ ī��Ʈ ����
        
    }

    public int GetScenesLoadedCount()
    {
        return scenesLoadedCount; // �ε�� ���� �� ���� ��ȯ
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� �ı����� ����
            SceneManager.sceneLoaded += OnSceneLoaded; // ���� �ε�� ������ OnSceneLoaded ȣ��
            // SceneManager.sceneLoaded -= OnSceneLoaded; 추가가 필요함
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
        }
        successImage.gameObject.SetActive(false);
        failImage.gameObject.SetActive(false);
        gameButton.onClick.AddListener(HandleButtonClick);
        UpdateScoreText();
        timeText.text = "10"; // �ʱ� ���� �ð� �ؽ�Ʈ ����

        StartRound(); // ���� ������ ���� ȣ��
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimeText(); // ���� �ð� �ؽ�Ʈ ������Ʈ
            }
            else
            {
                EndRound(false);
            }
        }
    }

    void UpdateTimeText()
    {
        // timeText�� null�� �ƴ� ���� ����
        if (timeText != null)
        {
            timeText.text = Mathf.Ceil(timeRemaining).ToString(); // ���� �ð��� �ݿø��Ͽ� ǥ��
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
        timeRemaining = 10; // 10�� ���ѽð� �缳��

        // successImage�� failImage�� null�� �ƴ� ���� ����
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
        timeRemaining = 10; // ���� �ð� �ʱ�ȭ
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
        yield return new WaitForSeconds(2); // 2�� ���� ��� ǥ��
        successImage.gameObject.SetActive(false);
        failImage.gameObject.SetActive(false);

        CheckGameEnd(); // ���� ���� üũ
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
            // ��� ���尡 �Ϸ�Ǹ� ���� ������ ��ȯ
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            StartRound(); // ���� ���� ����
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

