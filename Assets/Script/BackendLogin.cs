using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڳ� SDK namespace �߰�
using BackEnd;

// 1. �α���/ȸ������ �����ϱ�
//public class BackendLogin : MonoBehaviour
public class BackendLogin
{
    private static BackendLogin _instance = null;

    // Step 1. �����غ�
    public static BackendLogin Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendLogin();
            }

            return _instance;
        }
    }

    // Step 2. ȸ������ �����ϱ� ����
    public void CustomSignUp(string id, string pw)
    {
        Debug.Log("ȸ�������� ��û�մϴ�.");

        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("ȸ�����Կ� �����߽��ϴ�. : " + bro);
        }
    }

    // Step 3. �α��� �����ϱ� ����
    public void CustomLogin(string id, string pw)
    {
        Debug.Log("�α����� ��û�մϴ�.");

        var bro = Backend.BMember.CustomLogin(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("�α����� �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("�α����� �����߽��ϴ�. : " + bro);
        }
    }

    // Step 4. �г��� ���� �����ϱ� ����
    public void UpdateNickname(string nickname)
    {
        Debug.Log("�г��� ������ ��û�մϴ�.");

        var bro = Backend.BMember.UpdateNickname(nickname);

        if (bro.IsSuccess())
        {
            Debug.Log("�г��� ���濡 �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("�г��� ���濡 �����߽��ϴ�. : " + bro);
        }
    }
    
}
