/****************************************************
    文件：ExitGameUtil.cs
    功能：Nothing
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class ExitGameUtil : MonoBehaviour
{
    public Text txtShow;
    float fadingSpeed = 1f;
    bool fading;
    float startFadingTime;
    Color originalColor;
    Color transparentColor;
    const string showStr = "再次按下返回键退出游戏";
    
 
    void Start () {
        originalColor = txtShow.color;
        transparentColor = originalColor;
        transparentColor.a = 0;
        txtShow.color = transparentColor;
        gameObject.SetActive(false);
	}
    
    public void Init(){
        gameObject.SetActive(true);
        txtShow.text = showStr;
    }	
 
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (startFadingTime==0)
            {
                //点击一次的
                txtShow.color = originalColor;
                startFadingTime = Time.time;
                fading = true;
            }
            else
            {
                //双击退出
                Application.Quit();
            }
        }
        if (fading)
        {
            txtShow.color = Color.Lerp(originalColor, transparentColor, (Time.time - startFadingTime) * fadingSpeed);
            if (txtShow.color.a<2.0/255)
            {
                txtShow.color = transparentColor;
                startFadingTime = 0;
                fading = false;
            }
        }
	}
}
