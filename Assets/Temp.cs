using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Temp : MonoBehaviour
{
    Rigidbody2D rb;
    bool isWorking;
    public float moveSpeed;
    public float jumpForce;
    public GameObject clone;
    private int frameCounter = 0;
    private List<TransformData> transformHistory = new List<TransformData>();
    private class TransformData
    {
        public Vector3 position;
        public Quaternion rotation;

        public TransformData(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isWorking = false;
        clone = Instantiate(clone);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        rb.velocity = movement * moveSpeed;
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReplayCoroutine(0.12f));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        frameCounter++;

        // 5프레임마다 위치와 회전 정보 저장
        if (frameCounter >= 5 && !isWorking)
        {
            RecordTransform();
            frameCounter = 0; // 카운터 초기화
        }
    }
    void RecordTransform()
    {
        // 현재 위치와 회전 정보를 기록
        TransformData data = new TransformData(transform.position, transform.rotation);
        transformHistory.Add(data);
        Debug.Log("Saved Position: " + transform.position + ", Rotation: " + transform.rotation);
    }
    public void ReplayPositions()
    {
        foreach (var data in transformHistory)
        {
            // 위치와 회전을 설정하는 예시 (시간 간격을 두고 설정해야 함)
            transform.position = data.position;
            transform.rotation = data.rotation;

            // 딜레이를 줄 수 있도록 코루틴 사용 가능
            // WaitForSeconds 등을 활용해 프레임 간격을 조절할 수 있습니다.
        }
    }
    IEnumerator ReplayCoroutine(float interval)
    {
        isWorking = true;
        foreach (var data in transformHistory)
        {
            clone.transform.position = data.position;
            clone.transform.rotation = data.rotation;
            yield return new WaitForSeconds(interval); // 지정된 시간 간격 대기
        }
    }
}
