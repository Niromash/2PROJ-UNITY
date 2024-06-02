using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemyAI
{
    private GameManager gameManager;
    private int difficultyLevel;
    private SpawnTurret spawnTurret;
    private SpawnEntity spawnEntity;
    private int currentEntityIndex = 0;
    private EvolveAge evolveAge;
    private SpawnSpell spawnSpell;
    private Team enemyTeam;


    public EnemyAI(GameManager gameManager, int difficultyLevel)
    {
        this.gameManager = gameManager;
        this.difficultyLevel = difficultyLevel;
        spawnTurret = new SpawnTurret();
        evolveAge = new EvolveAge();
        evolveAge.gameManager = gameManager;
        this.enemyTeam = gameManager.GetTeams().Find(team => team.GetSide() == Side.Enemy);
        spawnEntity = new SpawnEntity();
        spawnEntity.gameManager = gameManager;
        spawnEntity.enemySpawnPosition = new Vector2(35, 0);
        spawnEntity.Start();
        spawnSpell = new SpawnSpell();
        spawnSpell.gameManager = gameManager;
    }

    public IEnumerator ManageEnemies()
    {
        while (GameManager.GetGameState() == GameState.Playing)
        {
            switch (difficultyLevel)
            {
                case 1:
                    yield return new WaitForSeconds(2);
                    yield return Level1AI();
                    break;
                case 2:
                    yield return new WaitForSeconds(2);
                    yield return Level2AI();
                    break;
                case 3:
                    yield return new WaitForSeconds(2);
                    yield return Level3AI();
                    break;
                case 4:
                    yield return new WaitForSeconds(2);
                    yield return Level4AI();
                    break;
                default:
                    yield return Level1AI();
                    break;
            }
        }
    }


    private IEnumerator Level1AI()
    {
        if (enemyTeam == null)
        {
            yield break; // No enemy team found, exit the coroutine
        }

        while (GameManager.GetGameState() == GameState.Playing)
        {
            if (currentEntityIndex >= 5)
            {
                currentEntityIndex = 0;
            }

            if (currentEntityIndex == 0)
            {
                spawnEntity.TankEnemySpawn();
            }
            else if (currentEntityIndex == 1)
            {
                spawnEntity.InfantryEnemySpawn();
            }
            else if (currentEntityIndex == 2)
            {
                spawnEntity.AntiArmorEnemySpawn();
            }
            else if (currentEntityIndex == 3)
            {
                spawnEntity.SupportEnemySpawn();
            }
            else
            {
                spawnTurret.ShowNextTurret(enemyTeam);
            }

            currentEntityIndex++;

            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    private IEnumerator Level2AI()
    {
        if (enemyTeam == null)
        {
            yield break; // No enemy team found, exit the coroutine
        }

        enemyTeam.SetTeamGoldMultiplier(1.2f);

        List<int> entities = new List<int> { 0, 1, 2, 3, 4 };
        entities = entities.OrderBy(x => Random.value).ToList();

        foreach (var entity in entities)
        {
            if (entity == 0)
            {
                spawnEntity.TankEnemySpawn();
            }
            else if (entity == 1)
            {
                spawnEntity.InfantryEnemySpawn();
            }
            else if (entity == 2)
            {
                spawnEntity.AntiArmorEnemySpawn();
            }
            else if (entity == 3)
            {
                spawnEntity.SupportEnemySpawn();
            }
            else
            {
                spawnTurret.ShowNextTurret(enemyTeam);
            }
        }
    }


    private IEnumerator Level3AI()
    {
        if (enemyTeam == null)
        {
            yield break;
        }

        enemyTeam.SetTeamGoldMultiplier(1.4f);

        List<int> entities = new List<int> { 0, 1, 2, 3 };
        entities = entities.OrderBy(x => Random.value).ToList();

        var playerEntities = gameManager.GetTeams().Find(team => team.GetSide() == Side.Player).GetEntities();
        if (playerEntities.Count == 0)
        {
            foreach (var entity in entities)
            {
                if (entity == 0)
                {
                    spawnEntity.TankEnemySpawn();
                }
                else if (entity == 1)
                {
                    spawnEntity.InfantryEnemySpawn();
                }
                else if (entity == 2)
                {
                    spawnEntity.AntiArmorEnemySpawn();
                }
                else if (entity == 3)
                {
                    spawnEntity.SupportEnemySpawn();
                }
                else
                {
                    spawnTurret.ShowNextTurret(enemyTeam);
                }
            }

            yield break;
        }

        var playerFirstEntity = playerEntities[0];
        var strongestEntityTypesAgainstPlayer =
            gameManager.GetEntityStrengthWeakness().GetStrongest(playerFirstEntity.GetEntityType());

        switch (strongestEntityTypesAgainstPlayer)
        {
            case EntityTypes.Tank:
                spawnEntity.TankEnemySpawn();
                break;
            case EntityTypes.Infantry:
                spawnEntity.InfantryEnemySpawn();
                break;
            case EntityTypes.Support:
                spawnEntity.SupportEnemySpawn();
                break;
            case EntityTypes.AntiArmor:
                spawnEntity.AntiArmorEnemySpawn();
                break;
            case EntityTypes.Extra:
                spawnEntity.ExtraEntityEnemySpawn();
                break;
        }


        List<int> spellOrNot = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        spellOrNot = spellOrNot.OrderBy(x => Random.value).ToList();
        if (spellOrNot[0] == 0)
        {
            spawnSpell.SpawnEnemyAge();
        }

        TryEvolve();
    }
    
    private IEnumerator Level4AI()
    {
        if (enemyTeam == null)
        {
            yield break;
        }

        enemyTeam.SetTeamGoldMultiplier(1.6f);

        List<int> entities = new List<int> { 0, 1, 2, 3 };
        entities = entities.OrderBy(x => Random.value).ToList();

        var playerEntities = gameManager.GetTeams().Find(team => team.GetSide() == Side.Player).GetEntities();
        if (playerEntities.Count == 0)
        {
            foreach (var entity in entities)
            {
                if (entity == 0)
                {
                    spawnEntity.TankEnemySpawn();
                }
                else if (entity == 1)
                {
                    spawnEntity.InfantryEnemySpawn();
                }
                else if (entity == 2)
                {
                    spawnEntity.AntiArmorEnemySpawn();
                }
                else if (entity == 3)
                {
                    spawnEntity.SupportEnemySpawn();
                }
                else
                {
                    spawnTurret.ShowNextTurret(enemyTeam);
                }
            }

            yield break;
        }

        var playerFirstEntity = playerEntities[0];
        var strongestEntityTypesAgainstPlayer =
            gameManager.GetEntityStrengthWeakness().GetStrongest(playerFirstEntity.GetEntityType());

        switch (strongestEntityTypesAgainstPlayer)
        {
            case EntityTypes.Tank:
                spawnEntity.TankEnemySpawn();
                break;
            case EntityTypes.Infantry:
                spawnEntity.InfantryEnemySpawn();
                break;
            case EntityTypes.Support:
                spawnEntity.SupportEnemySpawn();
                break;
            case EntityTypes.AntiArmor:
                spawnEntity.AntiArmorEnemySpawn();
                break;
            case EntityTypes.Extra:
                spawnEntity.ExtraEntityEnemySpawn();
                break;
        }


        List<int> spellOrNot = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        spellOrNot = spellOrNot.OrderBy(x => Random.value).ToList();
        if (spellOrNot[0] == 0)
        {
            spawnSpell.SpawnEnemyAge();
        }

        TryEvolve();
    }


    public void TryEvolve()
    {
        if (enemyTeam.GetExperience() >= enemyTeam.GetCurrentAge().GetAgeEvolvingCost())
        {
            evolveAge.EvolveEnemyAge();
        }
    }
}