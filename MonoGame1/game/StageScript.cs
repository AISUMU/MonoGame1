using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame1
{
    public class StageScript
    {
        private int progress;
        private bool isEndProgress;

        public void Reset ()
        {
            progress = 0;
            isEndProgress = false;
        }

        public void UpdateScript (int stage)
        {
            switch (stage)
            {
                case 1:
                    _UpdateScriptStage1();
                    break;
            }
        }

        public bool IsEndProgress()
        {
            return isEndProgress;
        }

        private void _UpdateScriptStage1()
        {
            if (progress < 1000) progress++;
            else isEndProgress = true;

            switch(progress)
            {
                case 100:
                    {
                        Monster2 e1 = new Monster2();
                        e1.SetPosition(100, -100);
                        Monster2 e2 = new Monster2();
                        e2.SetPosition(100, -130);
                        Monster2 e3 = new Monster2();
                        e3.SetPosition(100, -160);
                        Game1.data.AddEnemy(e1);
                        Game1.data.AddEnemy(e2);
                        Game1.data.AddEnemy(e3);
                    }
                    break;
                case 200:
                    {
                        Monster2 e1 = new Monster2();
                        e1.SetPosition(700, -100);
                        Monster2 e2 = new Monster2();
                        e2.SetPosition(700, -130);
                        Monster2 e3 = new Monster2();
                        e3.SetPosition(700, -160);
                        Game1.data.AddEnemy(e1);
                        Game1.data.AddEnemy(e2);
                        Game1.data.AddEnemy(e3);
                    }
                    break;
                case 300:
                    {
                        Monster3 e1 = new Monster3();
                        e1.SetPosition(400, -100);
                        Monster3 e2 = new Monster3();
                        e2.SetPosition(400, -130);
                        Monster3 e3 = new Monster3();
                        e3.SetPosition(400, -160);
                        Game1.data.AddEnemy(e1);
                        Game1.data.AddEnemy(e2);
                        Game1.data.AddEnemy(e3);
                    }
                    break;
                case 500:
                    {
                        Monster1 e = new Monster1();
                        e.SetPosition(100, -200);
                        Game1.data.AddEnemy(e);
                    }
                    break;
                case 800:
                    {
                        Monster1 e = new Monster1();
                        e.SetPosition(400, -200);
                        Game1.data.AddEnemy(e);
                    }
                    break;
            }
        }
    }
}
