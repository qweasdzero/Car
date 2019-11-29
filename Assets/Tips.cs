using UnityEngine;
using UnityEngine.EventSystems;

namespace EndlessCarChase
{
    public class Tips : MonoBehaviour
    {
        internal float instanceTime = 0;
        private float m_TipTime;

        [SerializeField] private EventSystem m_EventSystem;

        [SerializeField] private GameObject m_StartGame;
        [SerializeField] private GameObject m_Tips;

        // Start is called before the first frame update
        private void Awake()
        {
            GameObject[] tipsObjects = GameObject.FindGameObjectsWithTag("Tips");
            //Keep only the music object which has been in the game for more than 0 seconds
            if (tipsObjects.Length > 1)
            {
                foreach (var musicObject in tipsObjects)
                {
                    if (musicObject.GetComponent<Tips>().instanceTime <= 0) Destroy(gameObject);
                }
            }
        }

        void Start()
        {
            DontDestroyOnLoad(transform.gameObject);
            m_TipTime = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_TipTime < 3)
            {
                m_TipTime += Time.deltaTime;
                if (m_TipTime > 3)
                {
                    m_EventSystem.SetSelectedGameObject(m_StartGame);
                    m_Tips.SetActive(false);
                }
            }
        }
    }
}