using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName; // สร้างตัวแปรสำหรับเก็บชื่อซีน

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadScene(sceneName); // เรียกใช้ฟังก์ชัน LoadScene() โดยส่งชื่อซีนที่ต้องการไปด้วย
        }
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // โหลดซีนตามชื่อที่รับมา
    }
}