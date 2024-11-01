using System.Collections;
using TMPro;
using UnityEngine;

namespace BBX.Dialogue.GUI
{
    public class TypingTextAnimator : MonoBehaviour
    {
        public TMP_Text TMProText;       // TextMeshPro �ؽ�Ʈ ������Ʈ
        public float TypingSpeed = 0.05f; // ���� ���� ���� (�� ����, �������� ����)

        private void Start()
        {
            TMProText.ForceMeshUpdate(); // �ؽ�Ʈ �޽� ���� ������Ʈ
            StartCoroutine(TypingEffect()); // Ÿ���� ȿ�� ����
        }

        /// <summary>
        /// �� ���ھ� ���������� ��Ÿ���� �ϴ� �ڷ�ƾ
        /// </summary>
        private IEnumerator TypingEffect()
        {
            TMProText.maxVisibleCharacters = 0; // �ʱ� ����: ��� ���� ����
            int totalCharacters = TMProText.textInfo.characterCount; // ��ü ���� ��

            for (int i = 0; i <= totalCharacters; i++)
            {
                TMProText.maxVisibleCharacters = i; // ���� ���� ������ ����ȭ
                yield return new WaitForSeconds(TypingSpeed); // ������ �ӵ��� ���
            }
        }
    }
}
