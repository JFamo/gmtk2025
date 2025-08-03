using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class QuestCoordinator : MonoBehaviour
    {
        private Dictionary<QuestKeys, IQuestStateController> _questStateControllers;
        private static QuestCoordinator _instance;

        public static QuestCoordinator GetInstance()
        {
            if (_instance == null)
            {
                Debug.LogError("QuestCoordinator singleton is null!");
            }
            return _instance;
        }

        void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError("Multiple instances of QuestCoordinator detected. Using the existing instance.");
                Destroy(gameObject);
            }
            _instance = this;
        }
        
        void Start()
        {
            _questStateControllers = new Dictionary<QuestKeys, IQuestStateController>
            {
                { QuestKeys.WATER_GUN_KID_QUEST, new WaterGunKidQuestStateController() },
                { QuestKeys.MONK_QUEST, new MonkQuestStateController() },
                { QuestKeys.COWGIRL_QUEST, new MonkQuestStateController() },
                { QuestKeys.ALIEN_QUEST, new MonkQuestStateController() }
            };
        }

        public float GetCompletionPercentage()
        {
            // Iterate QuestKeys values
            float totalQuests = _questStateControllers.Count;
            float completedQuests = 0.0f;
            foreach (var controller in _questStateControllers.Values)
            {
                if (controller.GetQuestState() != QuestStates.NOT_STARTED)
                {
                    completedQuests += 1.0f;
                }
            }

            return completedQuests / totalQuests;
        }
        
        public IQuestStateController GetQuestStateController(QuestKeys key)
        {
            if (_questStateControllers.TryGetValue(key, out var controller))
            {
                return controller;
            }
            Debug.LogWarning($"Quest state controller for {key} not found.");
            return null;
        }
    }
}