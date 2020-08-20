using System;
using System.Collections.Generic;

namespace Git {
    public class Git {
        public int commitNumber;
        public int filesCount;
        public List<int> file;

        public Git(int filesCount)
        {
            this.filesCount = filesCount;
            this.file = new List<int>();
            for (int i = 0; i < filesCount; i++)
                this.file.Add(0);
            this.commitNumber = -1;
        }

        public void Update(int fileNumber, int value)
        {
            try
            {
                int way = fileNumber;
                while(Math.Abs(this.file[way]) % 10000 > 1)
                    way = Math.Abs(this.file[way]) % 10000;
                if (Math.Abs(this.file[way]) % 10000 == 1)
                {
                    this.file.Add(value * 10000);
                    if (this.file[way] >= 0)
                        this.file[way] += this.file.Count-2;
                    else
                        this.file[way] -= this.file.Count - 2;
                } else if (Math.Abs(this.file[way]) % 10000 == 0)
                    this.file[way] = value * 10000;
            }
            catch(ArgumentException)
            {
                Console.WriteLine("Ошибка обновления!");
            }
        }

        public int Commit()
        {
            this.commitNumber++;
            for (int i = 0; i < this.filesCount; i++)
            {
                int way = i;
                while (Math.Abs(this.file[way]) % 10000 > 1)
                {
                    way = Math.Abs(this.file[way]) % 10000;
                }
                    
                if (Math.Abs(this.file[way]) % 10000 != 1)
                {
                    if(this.file[way]>=0)
                        this.file[way] += 1;
                    else
                        this.file[way] -= 1;
                }
                    
            }
            return this.commitNumber;
        }

        public int Checkout(int commitNumber, int fileNumber)
        {
            if (commitNumber > this.commitNumber)
                throw new ArgumentException();
            else
            {
                int way = fileNumber;
                for(int i = commitNumber; i > 0; i--)
                {
                    if(Math.Abs(this.file[way]) % 10000 > 1)
                    way = Math.Abs(this.file[way]) % 10000;
                }
                return this.file[way] / 10000; 
            }
        }

        public static void Main()
        {
            Git git1 = new Git(3);
            git1.Update(0, 5);
            git1.Update(1, 7);
            git1.Update(2, -8);
            Console.WriteLine("Коммит №"+git1.Commit());
            git1.Update(0, 4);
            git1.Update(1, 9);
            git1.Update(0, 356);
            Console.WriteLine("Коммит №"+git1.Commit());
            Console.WriteLine("Файл №0 по коммиту №0: "+git1.Checkout(0, 0));
            Console.WriteLine("Файл №1 по коммиту №0: "+git1.Checkout(0, 1));
            Console.WriteLine("Файл №0 по коммиту №1: " + git1.Checkout(1, 0));
            Console.WriteLine("Файл №1 по коммиту №1: " + git1.Checkout(1, 1));
            Console.WriteLine("Файл №2 по коммиту №1: " + git1.Checkout(1, 2));
            for (int i=0;i<git1.file.Count;i++)
                Console.WriteLine(i+": "+ git1.file[i]);
            Console.ReadKey();
        }
    }
}