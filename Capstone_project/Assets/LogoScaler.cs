using UnityEngine;

public class LogoScaler : MonoBehaviour
{
    public float scaleAmount = 0.5f; // ���� ũ�⿡�� �߰��� Ŀ�� �ִ� ũ�� ��ȭ��
    public float speed = 0.5f; // ��ȭ �ӵ� (���� �ٿ��� �� õõ�� �����̰� ��)

    private Vector3 originalScale; // ���� ������

    void Start()
    {
        originalScale = transform.localScale; // ���� ���� ���� ������ ����
    }

    void Update()
    {
        // Mathf.PingPong�� ����Ͽ� �ð��� ���� ���� 0�� scaleAmount ���̿��� �պ��ϰ� �մϴ�.
        // �� ��, originalScale.x�� scaleAmount�� ���� �ִ� ũ�⸦ �����մϴ�.
        float scale = Mathf.PingPong(Time.time * speed, scaleAmount) + originalScale.x;
        transform.localScale = new Vector3(scale, scale, scale); // ���ο� ������ ����
    }
}
