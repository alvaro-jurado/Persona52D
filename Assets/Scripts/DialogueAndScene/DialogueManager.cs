using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI characterNameText;
    public Image characterImage;
    public GameObject dialoguePanel;
    public float typingSpeed = 0.05f;
    [SerializeField] SceneController sceneController;

    [System.Serializable]
    public class DialogueLine
    {
        public string characterName;
        [TextArea(3, 10)]
        public string dialogue;
        public Sprite characterSprite;
    }

    public DialogueLine[] dialogues;

    private int currentDialogueIndex;
    private bool isTyping;
    private bool textFullyDisplayed;

    public VideoPlayer videoPlayer;
    public string nextSceneName;

    void Start()
    {
        currentDialogueIndex = 0;
        isTyping = false;
        textFullyDisplayed = false;

        dialoguePanel.SetActive(false);

        if (videoPlayer != null)
        {
            videoPlayer.enabled = false;
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        }

        StartDialogue();
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        currentDialogueIndex = 0;
        ShowNextDialogue();
    }

    void Update()
    {
        if (videoPlayer != null && videoPlayer.isPlaying && Input.GetMouseButtonDown(0))
        {
            SkipCinematic();
        }
        else if (dialoguePanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogues[currentDialogueIndex].dialogue;
                isTyping = false;
                textFullyDisplayed = true;
            }
            else if (textFullyDisplayed)
            {
                currentDialogueIndex++;
                ShowNextDialogue();
            }
        }
    }

    void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            characterNameText.text = dialogues[currentDialogueIndex].characterName;
            characterImage.sprite = dialogues[currentDialogueIndex].characterSprite;
            characterImage.enabled = dialogues[currentDialogueIndex].characterSprite != null;

            StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex].dialogue));
            textFullyDisplayed = false;
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueText.text = "";
        characterNameText.text = "";
        characterImage.sprite = null;
        characterImage.enabled = false;
        dialoguePanel.SetActive(false);

        if (videoPlayer != null)
        {
            videoPlayer.enabled = true;
            videoPlayer.Play();
        }
        else
        {
            ChangeScene();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        ChangeScene();
    }

    void ChangeScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            if(sceneController != null)
            {
                sceneController.LoadScene(nextSceneName);
            } else
            {
                Debug.LogWarning("El controlador de escena no está configurado.");
            }
            
        }
        else
        {
            Debug.LogWarning("El nombre de la siguiente escena no está configurado.");
        }
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";
        isTyping = true;

        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        textFullyDisplayed = true;
    }

    void SkipCinematic()
    {
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
            ChangeScene();
        }
    }
}
