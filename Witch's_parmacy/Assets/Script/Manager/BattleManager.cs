using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public enum BattleState { Start, PlayerTurn, EnemyTurn, Action, Win, Lose }
    public BattleState State;

    public List<Character> playerParty;
    public List<Character> enemyParty;

    private Queue<Character> actionQueue = new Queue<Character>();

    void Start()
    {
        State = BattleState.Start;
        SetupBattle();
    }

    void SetupBattle()
    {
        // 캐릭터, 위치, UI 등 초기화
        EnqueueAllCharacters();
        State = BattleState.Action;
        NextAction();
    }

    void EnqueueAllCharacters()
    {
        actionQueue.Clear();
        List<Character> allCharacters = new List<Character>();
        allCharacters.AddRange(playerParty);
        allCharacters.AddRange(enemyParty);

        // 이니셔티브/속도 순으로 정렬
        allCharacters.Sort((a, b) => b.Speed.CompareTo(a.Speed));
        foreach (var character in allCharacters)
            actionQueue.Enqueue(character);
    }

    void NextAction()
    {
        if (CheckBattleEnd()) return;

        if (actionQueue.Count == 0)
            EnqueueAllCharacters();

        Character current = actionQueue.Dequeue();
        if (playerParty.Contains(current))
        {
            State = BattleState.PlayerTurn;
            // 명령 UI 표시, 플레이어 입력 대기
        }
        else
        {
            State = BattleState.EnemyTurn;
            // AI가 행동 선택
            EnemyAction(current);
        }
    }

    public void OnPlayerAction(Character player, Skill skill, Character target)
    {
        // 플레이어 행동 실행
        skill.Use(player, target);
        NextAction();
    }

    void EnemyAction(Character enemy)
    {
        // 간단한 AI: 랜덤 플레이어 공격
        Character target = playerParty[Random.Range(0, playerParty.Count)];
        Skill skill = enemy.GetDefaultSkill();
        skill.Use(enemy, target);
        NextAction();
    }

    bool CheckBattleEnd()
    {
        if (playerParty.TrueForAll(c => c.IsDead))
        {
            State = BattleState.Lose;
            // 패배 UI 표시
            return true;
        }
        if (enemyParty.TrueForAll(c => c.IsDead))
        {
            State = BattleState.Win;
            // 승리 UI 표시
            return true;
        }
        return false;
    }
}

// 예시 Character와 Skill 클래스 (간략화)
public class Character
{
    public string Name;
    public int Speed;
    public bool IsDead;
    public Skill GetDefaultSkill() { return new Skill(); }
}

public class Skill
{
    public void Use(Character user, Character target)
    {
        // 스킬 로직 구현
    }
}