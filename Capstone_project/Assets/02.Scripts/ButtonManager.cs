using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    [SerializeField]
    private GameObject userSet;
    [SerializeField]
    private Button lobbyUserCharacter;
    [SerializeField]
    private Button userCharacter;
    [SerializeField]
    private GameObject modeSelectObject;
    [SerializeField]
    private List<Sprite> numberSprites = new List<Sprite>();
    [SerializeField]
    private Image number1;
    [SerializeField]
    private Image number2;
    [SerializeField]
    private GameObject nameInputField;

    public TMP_Text lobbyUserName;
    public TMP_Text userSetName;
    public TMP_InputField inputField;
    public Button startButton;

    private bool supplementationMode = false;
    private int defaultActionNUmber = 5;
    private int maxActionNumber = 10;
    private int actionNumber;
    private Sprite recentUserCharacter;

    

    void Start(){
        nameInputField.SetActive(false);
        modeSelectObject.SetActive(false);
        option.SetActive(false);
        level.SetActive(false);
        userSet.SetActive(false);
        supplementationMode = false;
        startButton.interactable = false;
        actionNumber = defaultActionNUmber;
        SetNumber();
        foreach(Button button in buttons){
            button.onClick.AddListener(OnChooseGameModeClick);
        }
    }

    void OnChooseGameModeClick(){
        Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        startButton.interactable = true;
        RectTransform buttonRectTransform = clickedButton.GetComponent<RectTransform>();
        RectTransform levelRectTransform = level.GetComponent<RectTransform>();
        levelRectTransform.anchoredPosition = new Vector3 (buttonRectTransform.anchoredPosition.x, buttonRectTransform.anchoredPosition.y - 120);
        level.SetActive(true);
        
    }

    private void SetNumber(){
        ChangeNumber(number1, actionNumber/10);
        ChangeNumber(number2, actionNumber%10);
    }

    private void ChangeNumber(Image number, int index){
        if(index >= 0 && index <= 9){
            Debug.Log(index);
            number.sprite = numberSprites[index];
        }
        else{
            Debug.Log("lobby scene : number sprite error");
        }
    }


    public void OnClickMinusActionButton(){
        actionNumber -= 1;
        if(actionNumber > 0){
            SetNumber();
        }
        else{
            actionNumber = 0;
        }
    }

    public void OnClickPlusActionButton(){
        actionNumber += 1;
        if(actionNumber <= maxActionNumber){
            SetNumber();
        }
        else{
            actionNumber = maxActionNumber;
        }
    }

    public void OnChooseSupplementationMode(){
        supplementationMode = !supplementationMode;
        Debug.Log("supplementationMode = " + supplementationMode);
        modeSelectObject.SetActive(supplementationMode);
        // 보완모드 ==> 약한 동작
    }

    public void OnClickUserButton(){
        userSet.SetActive(true);
    }

    public void OnClickCheckButton(){
        if(recentUserCharacter != null){
            lobbyUserCharacter.image.sprite = recentUserCharacter;
        }
        userSet.SetActive(false);
    }

    public void OnClickOptionButton(){
        option.SetActive(true);
    }

    public void OnClickCloseButton(){
        option.SetActive(false);
    }

    public void OnChangeCharacter(Button button){
        
        Sprite newSprite = button.GetComponent<Image>().sprite;

        userCharacter.image.sprite = newSprite;
        recentUserCharacter = newSprite;
    }

    public void SetNewName(){
        nameInputField.SetActive(true);

        TMP_InputField inputField = nameInputField.GetComponentInChildren<TMP_InputField>();
        if(inputField != null){
            inputField.text = "";
        }
    }

    public void SubmitName()
    {
        string newName = nameInputField.GetComponentInChildren<TMP_InputField>().text;
        Debug.Log("새로운 이름: " + newName);
        userSetName.text = newName;
        lobbyUserName.text = newName;
        nameInputField.SetActive(false);
    }
}
