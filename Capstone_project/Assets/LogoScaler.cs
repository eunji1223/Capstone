using UnityEngine;

public class LogoScaler : MonoBehaviour
{
    public float scaleAmount = 0.5f; // 현재 크기에서 추가로 커질 최대 크기 변화량
    public float speed = 0.5f; // 변화 속도 (값을 줄여서 더 천천히 움직이게 함)

    private Vector3 originalScale; // 시작 스케일

    void Start()
    {
        originalScale = transform.localScale; // 시작 시의 로컬 스케일 저장
    }

    void Update()
    {
        // Mathf.PingPong을 사용하여 시간에 따라 값을 0과 scaleAmount 사이에서 왕복하게 합니다.
        // 이 때, originalScale.x에 scaleAmount를 더해 최대 크기를 결정합니다.
        float scale = Mathf.PingPong(Time.time * speed, scaleAmount) + originalScale.x;
        transform.localScale = new Vector3(scale, scale, scale); // 새로운 스케일 적용
    }
}
