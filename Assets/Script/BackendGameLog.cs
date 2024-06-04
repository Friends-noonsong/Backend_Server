using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

// 뒤끝 SDK namespace 추가
using BackEnd;

// 7. 게임로그 저장기능 구현하기
public class BackendGameLog
{
    private static BackendGameLog _instance = null;

    public static BackendGameLog Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendGameLog();
            }

            return _instance;
        }
    }

    // Step 2. 게임로그 저장하기 로직 추가
    public void GameLogInsert()
    {
        Param param = new Param();

        param.Add("clearStage", 1);
        param.Add("currentMoney", 100000);

        Debug.Log("게임로그 삽입을 시도합니다.");

        var bro = Backend.GameLog.InsertLog("ClearStage", param);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("게임로그 삽입 중 에러가 발생했습니다. : " + bro);
            return;
        }

        Debug.Log("게임로그 삽입에 성공했습니다. : " + bro);
    }
}
