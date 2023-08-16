using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // ����ũ ��ƼŬ �������� ������ ����
    public GameObject sparkEffect;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            // ù ��° �浹 ������ ���� ����
            ContactPoint cp = collision.GetContact(0);
            // �浹�� �Ѿ��� ���� ���͸� ���ʹϾ� Ÿ������ ��ȯ
            Quaternion rot = Quaternion.LookRotation(-cp.normal);

            // ����Ŭ ��ƼŬ�� �������� ����
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);

            Destroy(spark, 0.5f);

            Destroy(collision.gameObject);
        }
    }
}