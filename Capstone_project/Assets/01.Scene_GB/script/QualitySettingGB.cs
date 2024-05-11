using UnityEngine;

public class QualitySetting : MonoBehaviour
{
    // �ν��Ͻ��� Ÿ�Ե� QualitySetting���� �ٲ��ݴϴ�.
    public static QualitySetting Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ���� ����� ������� �� ��ü�� �ı����� �ʽ��ϴ�.
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ��� ������ ��� �̸� �����մϴ�.
        }
    }

    public void SetLowQuality()
    {
        QualitySettings.SetQualityLevel(0, true); // '��' ����Ƽ ����
    }

    public void SetMediumQuality()
    {
        QualitySettings.SetQualityLevel(1, true); // '��' ����Ƽ ����
    }

    public void SetHighQuality()
    {
        QualitySettings.SetQualityLevel(2, true); // '��' ����Ƽ ����
    }
}
