using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public AudioClip fireSfx;

    private new AudioSource audio;
    private MeshRenderer muzzleFlash;

    private RaycastHit hit;

   void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;
    }

    void Update()
    {
        Debug.DrawRay(firePos.position, firePos.forward * 10.0f, Color.green);

        // 마우스 왼쪽 버튼을 클릭했을 때 Fire 함수 호출
        if (Input.GetMouseButtonDown(0))
        {
            Fire();

            // Ray를 발사
            if (Physics.Raycast(firePos.position, firePos.forward, out hit, 10.0f, 1<<6))
            {
                Debug.Log($"Hit={hit.transform.name}");
                hit.transform.GetComponent<MonsterCtrl>()?.OnDamage(hit.point, hit.normal);
            }
        }
    }

    void Fire()
    {
        // Bullet 프리팹을 동적으로 생성(생성할 객체, 위치, 회전)
        Instantiate(bullet, firePos.position, firePos.rotation);
        
        // 총소리 발생
        audio.PlayOneShot(fireSfx, 1.0f);
        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator ShowMuzzleFlash()
    {
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.mainTextureOffset = offset;

        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);

        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.2f);
        muzzleFlash.enabled = false;
    }
}
