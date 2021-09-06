using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame1
{
    public class Data
    {
        public List<Bullet> playerBullets;
        public List<Bullet> enemyBullets;
        public List<Sprite> enemies;

        public Data ()
        {
            playerBullets = new List<Bullet>();
            enemyBullets = new List<Bullet>();
            enemies = new List<Sprite>();
        }

        public void ClearBullets ()
        {
            playerBullets.Clear();
            enemyBullets.Clear();
        }

        public void ClearAll ()
        {
            playerBullets.Clear();
            enemyBullets.Clear();
            enemies.Clear();
        }

        public void AddPlayerBullet (Bullet b)
        {
            playerBullets.Add(b);
        }

        public List<Bullet> GetPlayerBullets ()
        {
            return playerBullets;
        }

        public void RemovePlayerBuller (Bullet b)
        {
            playerBullets.Remove(b);
        }

        public void AddEnemyBullet(Bullet b)
        {
            enemyBullets.Add(b);
        }

        public List<Bullet> GetEnemyBullets()
        {
            return enemyBullets;
        }

        public void RemoveEnemyBuller(Bullet b)
        {
            enemyBullets.Remove(b);
        }

        public void AddEnemy (Sprite enemy)
        {
            enemies.Add(enemy);
        }

        public List<Sprite> GetEnemies ()
        {
            return enemies;
        }

        public void RemoveEnemy (Sprite enemy)
        {
            enemies.Remove(enemy);
        }
    }
}
