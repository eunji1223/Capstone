using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
