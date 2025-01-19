using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;
public static class Data
    {
    public static int ending = 2;
    public static int stage = 1;
    public static int score = 0;
    public static int normal = 0;
    public static int dream = 1;
    public static int combo = 1;
    public static List<GameObject> students = new List<GameObject>();
    public static List<StudentScript> studentScripts = new List<StudentScript>();
    public static List<BubbleScript> bubbleScripts = new List<BubbleScript>();
    }
public class ProcessManager : MonoBehaviour
    {
    bool craze = false;
    float time = 0;
    [SerializeField] bool randomGenerate;
    [SerializeField] float interval;
    [SerializeField] float crazeTime = 0.5f;
    bool a = false;
    float aimFill;
    float length = 20;
    int count;
    public List<GameObject> students = new List<GameObject>();
    public List<StudentScript> studentScripts = new List<StudentScript>();
    public List<BubbleScript> bubbleScripts = new List<BubbleScript>();
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image processBar;
    [SerializeField] GameObject processBarGam;
    [SerializeField] SpriteRenderer back;
    [SerializeField] List<Sprite> backGrounds;
    [SerializeField] GameObject PausePanel;
    //[SerializeField] List<Sprite> BubbleSprites = new List<Sprite>();
    [SerializeField] GameObject BubblePrefab;
    public static ProcessManager instance;
    private Coroutine scoreAnimationCoroutine;
    private float maxScale = 1.5f; // ���Ŵ���
    private float rotationAngle = 3f; // ��������Ƕ�
    private float animationDuration = 0.13f; // ��������ʱ��
    public List<GameObject> hand = new List<GameObject>();
    private void Awake()
        {
        instance = this;
        Data.students = students;
        Data.studentScripts = studentScripts;
        Data.bubbleScripts = bubbleScripts;
        for (int i = 0; i < students.Count; i++)
            {
            students[i].GetComponent<Student>().Init(studentScripts[UnityEngine.Random.Range(0, studentScripts.Count)]);
            if (UnityEngine.Random.value < 0.5f) // 50%�ĸ���
            {
                GameObject handInstance = Instantiate(hand[i]);
                handInstance.transform.position = transform.position + new Vector3(students[i].transform.position.x + 4.145f - 4.01f, students[i].transform.position.y + 2.958f - 3f, 0); // ����hand��λ��
                handInstance.transform.parent = transform;
            }
        }
        }
    public void Initialize()
        {
        count = 0;
        Data.score = 0;
        Data.combo = 0;
        back.sprite = backGrounds[0];
        StartGame();
        TextAsset textAsset = Resources.Load<TextAsset>("bubbleGenerate" + Data.stage.ToString());//���ﲻҪ���ļ���չ��
        if (textAsset != null)
            {
            string text = textAsset.text;
            string[] lines = textAsset.text.Split('\n');
            length = lines.Length;
            StartCoroutine(Generatebu(lines));
            }
        else
            {
            Debug.LogError("Text file not found in Resources!");
            }
        
        }
    public void StartGame()
        {
        processBarGam.SetActive(true);
        AudioManager.instance.PlayBGM("�ؿ�" + Data.stage);
        if ((interval < crazeTime) && !craze && randomGenerate) { ReflectionManager.Instance.Reflect("�����ģʽ������", Vector3.zero, Color.red, 80); craze = true; Skill._1 = true; }
        else if ((interval < crazeTime) && craze && randomGenerate) { craze = false; Skill._1 = false; }

    }
    public void EndGame()
        {
        Data.stage++;
        }
    public void GenerateBubble(BubbleScript bubbleScript, int studentId)
        {
        Bubble bubble = Instantiate(BubblePrefab).GetComponent<Bubble>();
        bubble.Init(bubbleScript, bubbleScript.offect, studentId);
        //Data.students[bubbleScript.id];
        }
    public float GetProcess()
        {
        if (count == length)
            {
            AudioManager.instance.PlayEffect("�¿���");
            Tool.instance.DelayTime(() =>
        {
            if (!a)
                {
                a = true;
                if (Data.stage != 8)
                    {
                    if (Data.score >= 30000 + 45000 * (Data.stage - 1)) { DialogueManager.Instance.BeginEnd1Dialogue(); Time.timeScale = 0; }
                    else DialogueManager.Instance.BeginEnd2Dialogue(); Time.timeScale = 0;
                    }
                else
                    {
                    if ((float)Data.dream / (Data.dream + Data.normal) >= 0.3) Data.ending = 1;
                    else Data.ending = 2;
                    SceneManager.LoadScene(2);
                    }
                }

        }, 3);
            }
        //Debug.Log(count+" "+length);
        return count / length;
        }
    public BubbleScript FindBubble(int id)
        {
        for (int i = 0; i < Data.bubbleScripts.Count; i++)
            {
            if (id == Data.bubbleScripts[i].id)
                {
                return Data.bubbleScripts[i];
                }

            }
        return null;

        }
    private void Update()
        {
        time += Time.deltaTime;
        processBar.fillAmount = math.lerp(processBar.fillAmount, GetProcess(), Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Escape))
            {
            PausePanel.SetActive(true);
            Time.timeScale = 0f;
            }
        if (randomGenerate && time>=interval) { time = 0;
            GenerateBubble(FindBubble(UnityEngine.Random.Range(0,10)), UnityEngine.Random.Range(0,24));
        }
        }


    public void AddScore(int score)
        {
        Data.score += score;
        scoreText.text =  "��Ч��" + Data.score;//Data.stage +
        if (Data.combo >= 1)
            {
            ReflectionManager.Instance.Reflect("����X" + Data.combo, new Vector3(-8, 3, 0), Color.red);

            }
        // ����Ѿ��ж��������У���ֹͣ��
        if (scoreAnimationCoroutine != null)
            {
            StopCoroutine(scoreAnimationCoroutine);
            }

        // �����µĶ���
        scoreAnimationCoroutine = StartCoroutine(AnimateScoreText());
        }
    private IEnumerator AnimateScoreText()
        {
        float elapsedTime = 0f;

        // ȷ��ÿ�ζ����ӳ�ʼ״̬��ʼ
        Vector3 originalScale = Vector3.one; // �����ʼ������ (1, 1, 1)
        Quaternion originalRotation = Quaternion.identity; // �����ʼ��ת������ת

        int ran = UnityEngine.Random.Range(0, 2) * 2 - 1;
        // �Ŵ�������׶�
        while (elapsedTime < animationDuration)
            {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / animationDuration;

            // ƽ���Ŵ����ת
            float scale = Mathf.Lerp(1f, maxScale, progress);
            float angle = Mathf.Lerp(0f, rotationAngle, progress);

            scoreText.transform.localScale = new Vector3(scale, scale, 1f);
            scoreText.transform.rotation = Quaternion.Euler(0, 0, ran * angle);

            yield return null;
            }

        // ��λ�׶�
        elapsedTime = 0f;
        while (elapsedTime < animationDuration)
            {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / animationDuration;

            // ƽ���ָ�����ʼ״̬
            scoreText.transform.localScale = Vector3.Lerp(scoreText.transform.localScale, originalScale, progress);
            scoreText.transform.rotation = Quaternion.Lerp(scoreText.transform.rotation, originalRotation, progress);

            yield return null;
            }

        // ȷ����ȫ��λ
        scoreText.transform.localScale = originalScale;
        scoreText.transform.rotation = originalRotation;
        scoreAnimationCoroutine = null; // ������ɣ����Э������
        }
    private IEnumerator Generatebu(string[] lines)
        {
        //if (count == 1) { AudioManager.instance.PlayEffect(""); }   
        string line = lines[count];
        //Debug.Log(line);
        count++;
        if (line[0] == 'f') { EndGame(); }
        else
            {
            int id = (line[0] - 48) * 10 + line[1] - 48;
            int studentId = (line[3] - 48) * 10 + line[4] - 48;
            string type = line[6].ToString() + line[7].ToString();
            float nextTime = (line[9] - 48) * 100 + (line[10] - 48) * 10 + (line[11] - 48) * 1f + (line[12] - 48) * 0.1f + (line[13] - 48) * 0.01f + (line[14] - 48) * 0.0011f;//(line[9] - 48) * 10 + line[10] - 48 + (line[11] - 48) * 0.1f;
            Debug.Log(id + " " + studentId + " " + type + " " + nextTime);
            if ((nextTime < crazeTime || interval < crazeTime) && !craze) { ReflectionManager.Instance.Reflect("�����ģʽ������", Vector3.zero, Color.red, 80);craze = true;Skill._1 = true; }
            else if ((nextTime >= crazeTime || interval >= crazeTime) && craze) { craze = false; Skill._1 = false; }
            switch (type)
                {
                case "01": GenerateBubble(FindBubble(id), studentId); break;
                }

            yield return new WaitForSeconds(nextTime);
            }
        if (count < lines.Length) StartCoroutine(Generatebu(lines));
        }
    public void TimeNormal()
        {
        Time.timeScale = 1.0f;

        }
    public void nextStage()
        {
        SceneManager.LoadScene(1);

        }
    public void quit()
        {

        Application.Quit();
        }
    public void ChangeSence(int id) {
        SceneManager.LoadScene(id);
    
    }
        }
    