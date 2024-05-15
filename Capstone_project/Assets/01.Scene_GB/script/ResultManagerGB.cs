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

    void Start()
    {
        // GameManager���� ���� ��������
        int successScore = GameManager.instance.GetSuccessScore();
        int failScore = GameManager.instance.GetFailScore();
        int totalRounds = 5; // ���� ���� 5�� ����
        // GameManager���� �ε�� ���� �� ���� ��������
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
        roundsText.text = "동작 갯수: " + loadedScenesCount.ToString();
        successCountText.text = "성공 횟수: " + successScore.ToString() +" / 5";

        float successRate = ((float)successScore / totalRounds) * 100; // ������ ���
        successRateText.text = "성공률: " + successRate.ToString("F2") + "%"; // �Ҽ��� ��° �ڸ����� ǥ��

        // �̹��� ǥ�� ����
        image1.SetActive(successScore == 3);
        image2.SetActive(successScore == 4);
        image3.SetActive(successScore == 5);

    }
}
