using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowUIOnProximity : MonoBehaviour
{
    public Transform playerTransform; // Transform ของผู้เล่น
    public Transform objectToShowNear; // Transform ของวัตถุที่จะแสดง UI เมื่อเข้าใกล้
    public float activationDistance = 1f; // ระยะที่ใช้เป็นเงื่อนไขในการเปิด/ปิด UI
    public GameObject uiElement; // UI Element ที่จะแสดง เช่น Text, Image, หรือ Canvas
    public string sceneNameToLoad; // ชื่อซีนที่ต้องการโหลด

    private bool isNearObject = false; // เก็บค่าว่าผู้เล่นอยู่ใกล้วัตถุหรือไม่

    void Update()
    {
        // ตรวจสอบระยะทางระหว่างผู้เล่นกับวัตถุ
        float distance = Vector3.Distance(playerTransform.position, objectToShowNear.position);

        // ถ้าระยะทางน้อยกว่าระยะที่กำหนด แสดง UI
        if (distance <= activationDistance && !isNearObject)
        {
            ShowUI();
            isNearObject = true;
        }
        // ถ้าระยะทางมากกว่าระยะที่กำหนด ซ่อน UI
        else if (distance > activationDistance && isNearObject)
        {
            HideUI();
            isNearObject = false;
        }

        // ตรวจสอบการกดปุ่ม E เพื่อโหลดซีนใหม่
        if (Input.GetKeyDown(KeyCode.E) && isNearObject && objectToShowNear.CompareTag("Door"))
        {
            LoadScene(sceneNameToLoad); // โหลดซีนโดยใช้ชื่อที่ระบุไว้ใน sceneNameToLoad
        }
    }

    // เมื่อผู้เล่นอยู่ใกล้วัตถุ แสดง UI
    void ShowUI()
    {
        uiElement.SetActive(true);
    }

    // เมื่อผู้เล่นไม่ได้อยู่ใกล้วัตถุ ซ่อน UI
    void HideUI()
    {
        uiElement.SetActive(false);
    }

    // ฟังก์ชันโหลดซีนใหม่
    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}