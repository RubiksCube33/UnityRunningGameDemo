using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject characterPanel;

    public void CharacterMenuOpen()
    {
        characterPanel.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        characterPanel.SetActive(true);

        StartCoroutine(OpenAnim());
    }

    public void CharacterMenuClose()
    {
        characterPanel.SetActive(false);
    }

    IEnumerator OpenAnim()
    {
        WaitForSeconds delay = new WaitForSeconds(0.016f);

        while (characterPanel.transform.localScale.x < 0.98f)
        {
            characterPanel.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            yield return delay;
        }
        characterPanel.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // 버튼 눌르고 있는 듯한 효과 구현
    public void OnPointerDown(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
    }

    // 크기 복원
    public void OnPointerUp(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
