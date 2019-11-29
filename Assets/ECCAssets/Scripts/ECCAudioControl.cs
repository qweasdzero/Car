using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EndlessCarChase
{
    /// <summary>
    /// Toggles a sound source when clicked on. It also records the sound state (on/off) in a PlayerPrefs. 
    /// In order to detect clicks you need to attach this script to a UI Button and set the proper OnClick() event.
    /// </summary>
    public class ECCAudioControl : MonoBehaviour
    {
        // Holds the volume of all sound effects in the game, excluding the music
        static float currentSoundVolume = 1;

        // Holds the volume of the music in the game
        static float currentMusicVolume = 1;

        private AudioSource m_AudioSource;
        private Text m_Text;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// Awake is used to initialize any variables or game state before the game starts. Awake is called only once during the 
        /// lifetime of the script instance. Awake is called after all objects are initialized so you can safely speak to other 
        /// objects or query them using eg. GameObject.FindWithTag. Each GameObject's Awake is called in a random order between objects. 
        /// Because of this, you should use Awake to set up references between scripts, and use Start to pass any information back and forth. 
        /// Awake is always called before any Start functions. This allows you to order initialization of scripts. Awake can not act as a coroutine.
        /// </summary>
        void Awake()
        {
            m_AudioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
            // Separate the music volume from the volume of other sound effects
            m_AudioSource.ignoreListenerVolume = true;

            if (transform.Find("Text"))
            {
                m_Text = transform.Find("Text").GetComponent<Text>();
            }
            // Get the the value of the music volume recorded in PlayerPrefs
            currentMusicVolume = PlayerPrefs.GetFloat("MusicVolume", currentMusicVolume);

            // Set the volume of the music based on the record we got from PlayerPrefs
            m_AudioSource.volume = currentMusicVolume;

            // Get the current sound volume from the PlayerPrefs record
            currentSoundVolume = PlayerPrefs.GetFloat("SoundVolume", currentSoundVolume);

            // Set the audio listener volume based on the sound volume. This controls all the sounds in the game, except the music
            AudioListener.volume = currentSoundVolume;

            // Check if this button is a music or sound button
            if (m_Text!=null)
            {
                // If this is a music button, update it based on the music volume
                if (m_Text.text.Contains("MUSIC"))
                {
                    if (currentMusicVolume > 0) m_Text.text = "MUSIC:ON";
                    else if (currentMusicVolume <= 0) m_Text.text = "MUSIC:OFF";
                } // Otherwise, update it based on the sound volume
                else if (m_Text.text.Contains("SOUND"))
                {
                    if (currentSoundVolume > 0) m_Text.text = "SOUND:ON";
                    else if (currentSoundVolume <= 0) m_Text.text = "SOUND:OFF";
                }
            }
        }

        /// <summary>
        /// Toggles the volume of the music between 1 and 0
        /// </summary>
        public void ToggleMusic()
        {
            // If the volume is full, set it to 0, and update the text
            if (currentMusicVolume == 1)
            {
                // Mute the music volume
                currentMusicVolume = 0;
                
                Color newColor = GetComponent<Image>().color;
                newColor = Color.gray;
                GetComponent<Image>().color = newColor;

                // Set the relevant text
                if (m_Text!=null) m_Text.text = "MUSIC:OFF";
            }
            else // If the volume is 0, set it to full, and update the text
            {
                // Set the music volume to full
                currentMusicVolume = 1;

                Color newColor = GetComponent<Image>().color;
                newColor = Color.white;
                GetComponent<Image>().color = newColor;

                // Set the relevant text
                if (m_Text!=null) m_Text.text = "MUSIC:ON";
            }

            // Set the volume for the music object
            m_AudioSource.volume = currentMusicVolume;

            // Save the current volume of the music in a PlayerPrefs record
            PlayerPrefs.SetFloat("MusicVolume", currentMusicVolume);
        }

        /// <summary>
        /// Toggles the volume of the sound between 1 and 0
        /// </summary>
        public void ToggleSound()
        {
            // If the volume is full, set it to 0, and update the text
            if (currentSoundVolume == 1)
            {
                // Mute the sound volume
                currentSoundVolume = 0;

                Color newColor = GetComponent<Image>().color;
                newColor = Color.gray;
                GetComponent<Image>().color = newColor;

                // Set the relevant text
                if (m_Text!=null) m_Text.text = "SOUND:OFF";
            }
            else // If the volume is 0, set it to full, and update the text
            {
                // Set the sound volume to full
                currentSoundVolume = 1;

                Color newColor = GetComponent<Image>().color;
                newColor = Color.white;
                GetComponent<Image>().color = newColor;

                // Set the relevant text
                if (m_Text!=null) m_Text.text = "SOUND:ON";
            }

            // Set the volume for the audiolistener object, which controls all sounds, except music
            AudioListener.volume = currentSoundVolume;

            // Save the current volume of the sound in a PlayerPrefs record
            PlayerPrefs.SetFloat("SoundVolume", currentSoundVolume);
        }
    }
}