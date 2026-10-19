using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI questText;
    private Quest currentQuest;

    private void UpdateQuestText()
    {
        if(currentQuest.state == QuestState.inProgress)
        {
            questText.text =
                currentQuest.questName + "\n" +
                currentQuest.targetItemName + " : " +
                currentQuest.currentAmount + " / " +
                currentQuest.targetAmount;
        }
        else if(currentQuest.state == QuestState.ReadyToComplete)
        {
            questText.text = "수집 완료! \nNPC에게 돌아가세요.";
        }
        else if (currentQuest.state == QuestState.completed)
        {
          questText.text = currentQuest.questName + "\n퀘스트 완료!";
        }
    }

    void StartQuest(Quest newQuset)
    {
        currentQuest = newQuset;
        currentQuest.currentAmount = 0;
        currentQuest.state = QuestState.inProgress;

        Debug.Log("퀘스트 시작! " + currentQuest.questName);
        UpdateQuestText();
    }

    public void AddQuestItem(string itemName , int amount)
    {
        if (currentQuest == null) return;
        if (currentQuest.state != QuestState.inProgress) return;
        if (currentQuest.targetItemName != itemName) return;

        currentQuest.currentAmount += amount;

        if(currentQuest.currentAmount >= currentQuest.targetAmount)
        {
            currentQuest.currentAmount = currentQuest.targetAmount;
            currentQuest.state = QuestState.ReadyToComplete;
        }

        UpdateQuestText();
    }

    public void CompleteQuest()
    {
        currentQuest.state = QuestState.completed;
        Debug.Log("퀘스트 완료! " + currentQuest.questName);
        UpdateQuestText();
    }


    public void TalkToNPC(Quest npcQuest)
    {
        if (npcQuest.state == QuestState.completed)
        {
            Debug.Log("이미 완료한 퀘스트입니다.");
            return;
        }
        if (currentQuest == null || currentQuest.state == QuestState.ReadyToComplete)
        {
            StartQuest(npcQuest);
        }

        if (currentQuest != npcQuest)
        {
            Debug.Log("이미 다른 퀘스트를 진행 중입니다.");
            return;
        }

        if (currentQuest.state == QuestState.inProgress)
        {
            Debug.Log("아직 아이템이 부족합니다. " + currentQuest.targetItemName + "을(를) 더 모아야 합니다.");
        }
        else if (currentQuest.state == QuestState.ReadyToComplete)
        {
            CompleteQuest();
        }
    }



        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            questText.text = "진행중인 퀘스트가 없습니다.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
