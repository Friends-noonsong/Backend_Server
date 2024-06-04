using System.Collections;
// Tuple<string, string>에서 에러가 발생할 경우 아래 코드 추가
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;

// 뒤끝 SDK namespace 추가
using BackEnd;

public class BackendFriend
{
    private static BackendFriend _instance = null;

    public static BackendFriend Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new BackendFriend();
            }

            return _instance;
        }
    }

    private List<Tuple<string, string>> _requestFriendList = new List<Tuple<string, string>>();

    // Step 2. 친구 요청 보내기 로직 추가
    public void SendFriendRequest(string nickName)
    {
        var inDateBro = Backend.Social.GetUserInfoByNickName(nickName);

        if (inDateBro.IsSuccess() == false)
        {
            Debug.LogError("유저 이름 검색 도중 에러가 발생했습니다. : " + inDateBro);
            return;
        }

        string inDate = inDateBro.GetReturnValuetoJSON()["row"]["inDate"].ToString();

        Debug.Log($"{nickName}의 inDate값은 {inDate} 입니다.");

        var friendBro = Backend.Friend.RequestFriend(inDate);

        if(friendBro.IsSuccess() == false)
        {
            Debug.LogError($"{inDate} 친구 요청 도중 에러가 발생했습니다. : " + friendBro);
            return;
        }

        Debug.Log("친구 요청에 성공했습니다." + friendBro);
    }

    // Step 3. 친구 요청 불러오기 및 수락하기 로직 추가(불러오기 부분)
    public void GetReceivedRequestFriend()
    {
        var bro = Backend.Friend.GetReceivedRequestList();

        if(bro.IsSuccess() == false)
        {
            Debug.Log("친구 요청 받은 리스트를 불러오는 중 에러가 발생했습니다. : " + bro);
            return;
        }

        if(bro.FlattenRows().Count <= 0)
        {
            Debug.LogError("친구 요청이 온 내역이 존재하지 않습니다.");
            return;
        }

        Debug.Log("친구 요청 받은 리스트 불러오기에 성공했습니다. : " + bro);

        int index = 0;
        foreach(LitJson.JsonData friendJson in bro.FlattenRows())
        {
            string nickName = friendJson["nickname"]?.ToString();
            string inDate = friendJson["inDate"].ToString();

            _requestFriendList.Add(new Tuple<string, string>(nickName, inDate));

            Debug.Log($"{index}. {nickName} - {inDate}");
            index++;
        }
    }

    // Step 3. 친구 요청 불러오기 및 수락하기 로직 추가(수락하기 부분)
    public void ApplyFriend(int index)
    {
        if(_requestFriendList.Count <= 0)
        {
            Debug.LogError("요청이 온 친구가 존재하지 않습니다.");
            return;
        }

        if(index >= _requestFriendList.Count)
        {
            Debug.LogError("요청이 온 친구가 존재하지 않습니다.");
            return;
        }

        var bro = Backend.Friend.AcceptFriend(_requestFriendList[index].Item2);

        if(bro.IsSuccess() == false)
        {
            Debug.LogError("친구 수락 중 에러가 발생했습니다. : " + bro);
            return;
        }

        Debug.Log($"{_requestFriendList[index].Item1}이(가) 친구가 되었습니다. : " + bro);
    }

    // Step 4. 친구 리스트 불러오기
    public void GetFriendList()
    {
        var bro = Backend.Friend.GetFriendList();

        if(bro.IsSuccess() == false)
        {
            Debug.LogError("친구 목록 불러오기 중 에러가 발생했습니다. : " + bro);
            return;
        }

        Debug.Log("친구 목록 불러오기에 성공했습니다. : " + bro);

        if(bro.FlattenRows().Count <= 0)
        {
            Debug.Log("친구가 존재하지 않습니다.");
            return;
        }

        int index = 0;

        string friendListString = "친구 목록\n";

        foreach(LitJson.JsonData friendJson in bro.FlattenRows())
        {
            string nickName = friendJson["nickname"]?.ToString();
            string inDate = friendJson["inDate"].ToString();

            friendListString += $"{index}. {nickName} - {inDate}\n";
            index++;
        }

        Debug.Log(friendListString);
    }
}
