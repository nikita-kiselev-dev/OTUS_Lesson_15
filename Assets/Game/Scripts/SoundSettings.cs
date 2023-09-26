using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public static SoundSettings Instance;
    public PlaySound PlaySound;
    
    public Slider MusicSlider;
    public Slider SoundSlider;
    
    public AudioMixer audioMixer;
    
    public string musicGroupName;
    public string soundGroupName;
    
    public Button BackButton;
    
    private MenuManager menuManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += CheckNextScene;

        menuManager = FindObjectOfType<MenuManager>();
        BackButton.onClick.AddListener(menuManager.CloseLevels);
        audioMixer.SetFloat(musicGroupName, MusicSlider.value);
        audioMixer.SetFloat(soundGroupName, SoundSlider.value);
        
        PlaySound.PlaySoundEffect("MenuMusic");
    }

    private void CheckNextScene(Scene current, Scene next)
    {
        if (next.name == "Menu")
        {
            PlaySound.PlaySoundEffect("MenuMusic");
            menuManager = FindObjectOfType<MenuManager>();
            BackButton.onClick.AddListener(menuManager.CloseLevels);
            audioMixer.SetFloat(musicGroupName, MusicSlider.value);
            audioMixer.SetFloat(soundGroupName, SoundSlider.value);
        }
        else
        {
            PlaySound.PlaySoundEffect("LevelMusic");
        }
    }

    private void OnEnable()
    {
        MusicSlider.onValueChanged.AddListener(MusicValueChanged);
        SoundSlider.onValueChanged.AddListener(SoundValueChanged);
    }

    private void OnDisable()
    {
        MusicSlider.onValueChanged.RemoveListener(MusicValueChanged);
        SoundSlider.onValueChanged.RemoveListener(SoundValueChanged);
    }

    private void MusicValueChanged(float value)
    {
        audioMixer.SetFloat(musicGroupName, value);
    }

    private void SoundValueChanged(float value)
    {
        audioMixer.SetFloat(soundGroupName, value);
    }
    
    private void OnDestroy()
    {
        BackButton.onClick.RemoveAllListeners();
    }
}
