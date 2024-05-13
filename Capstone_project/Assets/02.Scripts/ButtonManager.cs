using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*lobby scene에서 서 있는 모드, 앉아 있는 모드 등 모드 선택 시 사용되는 button 기능 구현*/
public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private GameObject level;
    [SerializeField]
    private GameObject option;
    

    void Start(){
        option.SetActive(false);
        level.SetActive(false);
        foreach(Button button in buttons){
            button.onClick.AddListener(OnChooseModeClick);
        }
    }

    void OnChooseModeClick(){
        Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        RectTransform buttonRectTransform = clickedButton.GetComponent<RectTransform>();
        RectTransform levelRectTransform = level.GetComponent<RectTransform>();
        levelRectTransform.anchoredPosition = new Vector3 (buttonRectTransform.anchoredPosition.x, buttonRectTransform.anchoredPosition.y - 120);
        level.SetActive(true);
        
    }

    public void ClickOptionButton(){
        option.SetActive(true);
    }

    public void ClickCloseButton(){
        option.SetActive(false);
    }
}
