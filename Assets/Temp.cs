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

        // 5�����Ӹ��� ��ġ�� ȸ�� ���� ����
        if (frameCounter >= 5 && !isWorking)
        {
            RecordTransform();
            frameCounter = 0; // ī���� �ʱ�ȭ
        }
    }
    void RecordTransform()
    {
        // ���� ��ġ�� ȸ�� ������ ���
        TransformData data = new TransformData(transform.position, transform.rotation);
        transformHistory.Add(data);
        Debug.Log("Saved Position: " + transform.position + ", Rotation: " + transform.rotation);
    }
    public void ReplayPositions()
    {
        foreach (var data in transformHistory)
        {
            // ��ġ�� ȸ���� �����ϴ� ���� (�ð� ������ �ΰ� �����ؾ� ��)
            transform.position = data.position;
            transform.rotation = data.rotation;

            // �����̸� �� �� �ֵ��� �ڷ�ƾ ��� ����
            // WaitForSeconds ���� Ȱ���� ������ ������ ������ �� �ֽ��ϴ�.
        }
    }
    IEnumerator ReplayCoroutine(float interval)
    {
        isWorking = true;
        foreach (var data in transformHistory)
        {
            clone.transform.position = data.position;
            clone.transform.rotation = data.rotation;
            yield return new WaitForSeconds(interval); // ������ �ð� ���� ���
        }
    }
}
