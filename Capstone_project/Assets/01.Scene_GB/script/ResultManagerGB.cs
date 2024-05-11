using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 사용
using UnityEngine.UI;

public class ResultManagerGB : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI roundsText;
    public TextMeshProUGUI successCountText;
    public TextMeshProUGUI successRateText;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;

    void Start()
    {
        // GameManager에서 점수 가져오기
        int successScore = GameManager.instance.GetSuccessScore();
        int failScore = GameManager.instance.GetFailScore();
        int totalRounds = 5; // 라운드 수를 5로 고정
        // GameManager에서 로드된 씬의 총 개수 가져오기
        int loadedScenesCount = GameManager.instance.GetScenesLoadedCount();

        // 성공 여부에 따른 텍스트 업데이트
        if (successScore > failScore)
        {
            resultText.text = "성공!";
        }
        else if (failScore > successScore)
        {
            resultText.text = "실패!";
        }


        // 게임 씬의 개수 업데이트
        roundsText.text = "동작 개수: " + loadedScenesCount.ToString();
        successCountText.text = "성공 횟수: " + successScore.ToString() +" / 5";

        float successRate = ((float)successScore / totalRounds) * 100; // 성공률 계산
        successRateText.text = "성공률: " + successRate.ToString("F2") + "%"; // 소수점 둘째 자리까지 표시

        // 이미지 표시 제어
        image1.SetActive(successScore == 3);
        image2.SetActive(successScore == 4);
        image3.SetActive(successScore == 5);

    }
}
