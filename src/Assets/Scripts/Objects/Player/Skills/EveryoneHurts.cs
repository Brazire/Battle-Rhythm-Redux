using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryoneHurts : PlayerSkill
{

    public EveryoneHurts()
    {
        icon = Resources.Load<Sprite>("SkillSprites/EveryoneHurts");
    }

    public override void DoAction()
    {
        Player player = BattleManagerRedux.bManager.GetPlayer();
        if (player.HasEnoughATB(3))
        {
            player.UseATB(3);
            List<Entity> enemies = BattleManagerRedux.bManager.GetAllEnemies();
            for (int x = enemies.Count -1; x >= 0; x--)
            {
                enemies[x].TakeDamage(10f + (player.GetMana() * 4f));
            }
        }
    }
}
