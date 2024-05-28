using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽� ���
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

    void Awake(){
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
    }

    void Start()
    {
        // GameManager���� ���� ��������
        int successScore = GameManager.instance.GetSuccessScore();
        int failScore = GameManager.instance.GetFailScore();
        int totalRounds = GameManager.instance.TotalRounds; 
        int loadedScenesCount = GameManager.instance.GetScenesLoadedCount();

        // ���� ���ο� ���� �ؽ�Ʈ ������Ʈ
        if (successScore > failScore)
        {
            resultText.text = "성공!";
        }
        else if (failScore > successScore)
        {
            resultText.text = "실패!";
        }


        // ���� ���� ���� ������Ʈ
        roundsText.text = "동작 갯수: " + totalRounds;
        successCountText.text = "성공 횟수: " + successScore.ToString() +" / " + totalRounds;

        float successRate = ((float)successScore / totalRounds) * 100; 
        successRateText.text = "성공률: " + successRate.ToString("F2") + "%"; 

        Debug.Log(successRate);
        if(successRate > 85.0f){
            Debug.Log("실행1");
            image3.SetActive(true);
        }
        else if(successRate > 70.0f){
            Debug.Log("실행2");
            image2.SetActive(true);
        }
        else if(successRate > 50.0f){
            Debug.Log("실행3");
            image1.SetActive(true);
        }
        else{
            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(false);
        }

    }
}
