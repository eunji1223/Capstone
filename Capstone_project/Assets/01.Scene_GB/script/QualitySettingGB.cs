using UnityEngine;

public class QualitySetting : MonoBehaviour
{
    // 인스턴스의 타입도 QualitySetting으로 바꿔줍니다.
    public static QualitySetting Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 현재 씬의 변경과 관계없이 이 객체는 파괴되지 않습니다.
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 중복 인스턴스가 생성될 경우 이를 제거합니다.
        }
    }

    public void SetLowQuality()
    {
        QualitySettings.SetQualityLevel(0, true); // '하' 퀄리티 설정
    }

    public void SetMediumQuality()
    {
        QualitySettings.SetQualityLevel(1, true); // '중' 퀄리티 설정
    }

    public void SetHighQuality()
    {
        QualitySettings.SetQualityLevel(2, true); // '상' 퀄리티 설정
    }
}
