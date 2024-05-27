using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    private static InfoManager instance;
    protected bool isFirstTime = true;
    public TMP_InputField inputField_id;
    public TMP_InputField inputField_pw;

    [SerializeField]
    private GameObject MembershipSet_id;
    [SerializeField]
    private TMP_Text matterText_id;
    [SerializeField]
    private GameObject MembershipSet_pw;
    [SerializeField]
    private TMP_Text matterText_pw;

    private UserInformation userInformation;


    void Awake()
    {
        if(isFirstTime == true){
            userInformation = new UserInformation();
            matterText_pw.gameObject.SetActive(false);
            matterText_id.gameObject.SetActive(false);
            MembershipSet_id.SetActive(true);
            MembershipSet_pw.SetActive(false);
        }
        else{
            MembershipSet_id.SetActive(false);
            MembershipSet_pw.SetActive(false);
        }
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static InfoManager GetInstance()
    {
        if(!instance)
        {
            instance = GameObject.FindObjectOfType<InfoManager>();
            if(!instance)
            {
                Debug.LogError("There needs to be one active InfoManager script on a GameObject in your scene");
            }
        }

        return instance;
    }

    public void RegisterButton_ID(){
        // 이미 데이터베이스에 존재하는 id라면, 이미 존재하는 id입니다. 문구 출력
        if(inputField_id.text.Length < 5)
        {
            matterText_id.text = "id 길이가 너무 짧습니다. 5자 이상으로 설정해주세요.";
            matterText_id.gameObject.SetActive(true);
        }
        else if(inputField_id.text == "") // 이미 존재하는 아이디라면
        {
            matterText_id.text = "이미 존재하는 id입니다.";
            matterText_id.gameObject.SetActive(true);
        }
        else{
            userInformation.userID = matterText_id.text;
            matterText_id.gameObject.SetActive(false);
            MembershipSet_id.SetActive(false);
            MembershipSet_pw.SetActive(true);
        }
    }

    public void RegisterButton_PW(){
        if(inputField_pw.text.Length < 8)
        {
            matterText_pw.text = "비밀 번호 길이가 너무 짧습니다.";
            matterText_pw.gameObject.SetActive(true);
        }
        else if(Regex.IsMatch(inputField_pw.text, @"[^\w\d*!?]"))
        {
            matterText_pw.text = "비밀 번호에 *!? 외의 특수 문자를 사용할 수 없습니다.";
            matterText_pw.gameObject.SetActive(true);
        }
        else
        {
            userInformation.userPW = matterText_pw.text;
            matterText_pw.gameObject.SetActive(false);
            MembershipSet_pw.SetActive(false);
        }
    }

}

class UserInformation
{
    public string userName;
    public string userID;
    public string userPW;

}
