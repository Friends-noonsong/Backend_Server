using System.Collections;
// Tuple<string, string>���� ������ �߻��� ��� �Ʒ� �ڵ� �߰�
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;

// �ڳ� SDK namespace �߰�
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

    // Step 2. ģ�� ��û ������ ���� �߰�
    public void SendFriendRequest(string nickName)
    {
        var inDateBro = Backend.Social.GetUserInfoByNickName(nickName);

        if (inDateBro.IsSuccess() == false)
        {
            Debug.LogError("���� �̸� �˻� ���� ������ �߻��߽��ϴ�. : " + inDateBro);
            return;
        }

        string inDate = inDateBro.GetReturnValuetoJSON()["row"]["inDate"].ToString();

        Debug.Log($"{nickName}�� inDate���� {inDate} �Դϴ�.");

        var friendBro = Backend.Friend.RequestFriend(inDate);

        if(friendBro.IsSuccess() == false)
        {
            Debug.LogError($"{inDate} ģ�� ��û ���� ������ �߻��߽��ϴ�. : " + friendBro);
            return;
        }

        Debug.Log("ģ�� ��û�� �����߽��ϴ�." + friendBro);
    }

    // Step 3. ģ�� ��û �ҷ����� �� �����ϱ� ���� �߰�(�ҷ����� �κ�)
    public void GetReceivedRequestFriend()
    {
        var bro = Backend.Friend.GetReceivedRequestList();

        if(bro.IsSuccess() == false)
        {
            Debug.Log("ģ�� ��û ���� ����Ʈ�� �ҷ����� �� ������ �߻��߽��ϴ�. : " + bro);
            return;
        }

        if(bro.FlattenRows().Count <= 0)
        {
            Debug.LogError("ģ�� ��û�� �� ������ �������� �ʽ��ϴ�.");
            return;
        }

        Debug.Log("ģ�� ��û ���� ����Ʈ �ҷ����⿡ �����߽��ϴ�. : " + bro);

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

    // Step 3. ģ�� ��û �ҷ����� �� �����ϱ� ���� �߰�(�����ϱ� �κ�)
    public void ApplyFriend(int index)
    {
        if(_requestFriendList.Count <= 0)
        {
            Debug.LogError("��û�� �� ģ���� �������� �ʽ��ϴ�.");
            return;
        }

        if(index >= _requestFriendList.Count)
        {
            Debug.LogError("��û�� �� ģ���� �������� �ʽ��ϴ�.");
            return;
        }

        var bro = Backend.Friend.AcceptFriend(_requestFriendList[index].Item2);

        if(bro.IsSuccess() == false)
        {
            Debug.LogError("ģ�� ���� �� ������ �߻��߽��ϴ�. : " + bro);
            return;
        }

        Debug.Log($"{_requestFriendList[index].Item1}��(��) ģ���� �Ǿ����ϴ�. : " + bro);
    }

    // Step 4. ģ�� ����Ʈ �ҷ�����
    public void GetFriendList()
    {
        var bro = Backend.Friend.GetFriendList();

        if(bro.IsSuccess() == false)
        {
            Debug.LogError("ģ�� ��� �ҷ����� �� ������ �߻��߽��ϴ�. : " + bro);
            return;
        }

        Debug.Log("ģ�� ��� �ҷ����⿡ �����߽��ϴ�. : " + bro);

        if(bro.FlattenRows().Count <= 0)
        {
            Debug.Log("ģ���� �������� �ʽ��ϴ�.");
            return;
        }

        int index = 0;

        string friendListString = "ģ�� ���\n";

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
