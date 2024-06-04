using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// [����] async ����� �̿��ϱ� ���ؼ��� �ش� namespace�� �ʿ��մϴ�.
using System.Threading.Tasks;

// �ڳ� SDK namespace �߰�
using BackEnd;

// ���� ���� �� �ڵ����� BackendManager ȣ��
public class BackendManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // �ڳ� �ʱ�ȭ
        var bro = Backend.Initialize(true);

        // �ڳ� �ʱ�ȭ�� ���� ���䰪
        if (bro.IsSuccess())
        {
            // ������ ��� statusCode 204 Success
            Debug.Log("�ʱ�ȭ ���� : " + bro);
        }
        else
        {
            // ������ ��� statusCode 400�� ���� �߻�
            Debug.LogError("�ʱ�ȭ ���� : " + bro);
        }

        Test();
    }

    // [�߰�] ���� �Լ��� �񵿱⿡�� ȣ���ϰ� ���ִ� �Լ�(����Ƽ UI ���� �Ұ�)
    async void Test()
    {
        await Task.Run(() =>
        {
            // 1. ���� �׽�Ʈ ���̽� �߰�
            // [�߰�] �ڳ� ȸ������ �Լ� ȣ��
            // �ڳ� �ʱ�ȭ �� �ش� �Լ� ȣ��
            //BackendLogin.Instance.CustomSignUp("user1", "1234");
            // [�߰�] �ڳ� �α��� �Լ� ȣ��
            //BackendLogin.Instance.CustomLogin("user1", "1234");
            // [�߰�] �г��� ����
            //BackendLogin.Instance.UpdateNickname("���ϴ� �̸�");



            // �ڳ� �α��� �Լ�
            //BackendLogin.Instance.CustomLogin("user1", "1234");

            // 2. ���� ���� ��� ���� ���� �߰�
            // [�߰�] ������ ���� �Լ�
            //BackendGameData.Instance.GameDataInsert();
            // [�߰�] ������ �ҷ����� �Լ�
            // ������ ���� �Լ�
            //BackendGameData.Instance.GameDataGet();
            // [�߰�] ������ �ҷ��� �����Ͱ� �������� ���� ���, �����͸� ���� �����Ͽ� ����
            /*
            if(BackendGameData.userData == null)
            {
                BackendGameData.Instance.GameDataInsert();
            }
            */

            // [�߰�] ���ÿ� ����� �����͸� ����
            //BackendGameData.Instance.LevelUp();
            // [�߰�] ������ ����� �����͸� �����(����� �κи�)
            //BackendGameData.Instance.GameDataUpdate();



            // 3. ��ŷ ��� ���� ���� �߰�
            // [�߰�] ��ŷ ����ϱ� �Լ�
            //BackendRank.Instance.RankInsert(100);
            // [�߰�] ��ŷ �ҷ����� �Լ�
            //BackendRank.Instance.RankGet();



            // 4. ��Ʈ ���� �ҷ����� ���� �߰�
            // [�߰�] chartId�� ��Ʈ ���� �ҷ�����
            // [���� �ʿ�] <���� ID>�� �ڳ� �ܼ� > ��Ʈ ���� > ��������Ʈ���� ����� ��Ʈ�� ���� ID������ �������ּ���.
            //BackendChart.Instance.ChartGet("<���� ID>");
            //BackendChart.Instance.ChartGet("117210");



            // 5. ���� ���� �߰�
            // ���ӵ����͸� �ҷ��� ���ÿ� �����մϴ�. (ĳ��)
            //BackendGameData.Instance.GameDataGet();

            // [�߰�] ���� ����Ʈ �ҷ�����
            // ���� ����Ʈ�� �ҷ��� ������ ������ inDate������ ���ÿ� �����մϴ�.
            //BackendPost.Instance.PostListGet(PostType.Admin);

            // ����� ������ ��ġ�� �о� ������ �����մϴ�.
            // ���⼭ index�� ������ ����.
            // 0�̸� ���� �� ����, 1�̸� �� ���� ����
            //BackendPost.Instance.PostReceive(PostType.Admin, 0);

            // ��ȸ�� ��� ������ �����մϴ�.
            //BackendPost.Instance.PostReceiveAll(PostType.Admin);



            // 6. ���� ��� �����ϱ�

            // ���� ��� ���� �߰�
            // [���� �ʿ�] ���� �ڵ带 �ڳ� �ܼ� > ���� ���� > �׽�Ʈ �������� ������ �����ڵ� ������ �������ּ���.
            //BackendCoupon.Instance.CouponUse("���� �ڵ�");
            //BackendCoupon.Instance.CouponUse("8cd54dcce1e3072024");



            // 7. ���� �α� ���� ���� �߰�
            // [�߰�] ���ӷα� ���� ���
            //BackendGameLog.Instance.GameLogInsert();



            // 8. ģ�� ��� ���� �߰�
            /*
            // user1���� ���� ���̹Ƿ� user2�� ȸ������
            string user2Id = "user2";

            // user2Id�� ȸ������(409 ���� �߻� ��, �̹� user2Id�� ���̵� ���������Ƿ� CustomSignUp�� CustomLogin���� ����_
            BackendLogin.Instance.CustomSignUp(user2Id, "1234");
            // ���̵�� �����ϰ� �г��� ����
            BackendLogin.Instance.UpdateNickname(user2Id);

            // ����1�� �г���(������ ���� �ٸ� �� �ֽ��ϴ�.)
            string user1Nickname = "���ϴ� �̸�";
            // ģ�� ��û ������ �Լ�
            BackendFriend.Instance.SendFriendRequest(user1Nickname);
            */

            /*
            // Step2���� user2�� ģ����û�� ���� ������ �α����ؾ��մϴ�.
            BackendLogin.Instance.CustomLogin("user1", "1234");

            // ģ�� ��û ����Ʈ �ҷ�����
            BackendFriend.Instance.GetReceivedRequestFriend();
            // ģ�� ��û ����Ʈ �� �ֽ� ��û �����ϱ�
            BackendFriend.Instance.ApplyFriend(0);
            */

            // Step 2���� ȸ������ �� ����(ģ�� ��û �߼���) Ȥ��
            // Step 3���� �α����� ����(ģ�� ��û ������) �� �ϳ��� �α����ؾ��մϴ�.
            BackendLogin.Instance.CustomLogin("user1", "1234");

            // ģ�� ����Ʈ �ҷ�����
            BackendFriend.Instance.GetFriendList();



            Debug.Log("�׽�Ʈ�� �����մϴ�.");
        });
    }



}
