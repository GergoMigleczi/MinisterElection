using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MinisterElection
{
    struct candidate
    {
        public int district;
        public int numberOfVotes;
        public string surname;
        public string firstname;
        public string party;
        public candidate(int district, int numberOfVotes, string surname, string firstname, string party)
        {
            this.district = district;
            this.numberOfVotes = numberOfVotes;
            this.surname = surname;
            this.firstname = firstname;
            this.party = party;
        }

    }
    class MinisterElection
    {
        static List<candidate> candidatelist = new List<candidate>();
        static void Task1()
        {
            StreamReader sr = new StreamReader("szavazatok.txt");
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split();
                int district = int.Parse(line[0]);
                int numberOfVotes = int.Parse(line[1]);
                string surname = line[2];
                string firstname = line[3];
                string party = line[4];

                candidate item = new candidate(district, numberOfVotes, surname, firstname, party);
                candidatelist.Add(item);
            }
            sr.Close();
        }

        //Calculate the number of candidates
        static void Task2()
        {
            Console.WriteLine("Task 2");
            Console.WriteLine($" Number of candidates: {candidatelist.Count}.");
        }

        //Check if a given candidate is in the list of candidates
        static void Task3()
        {
            Console.WriteLine("Task 3");
            Console.WriteLine("Candidate's surname");
            string givenSurname = Console.ReadLine();
            Console.WriteLine("Candidate's firstname");
            string givenFirstname = Console.ReadLine();
            bool NotInTheList = false;

            foreach (candidate item in candidatelist)
            {
                if (givenSurname == item.surname && givenFirstname == item.firstname)
                {
                    Console.WriteLine($"{item.surname} {item.firstname} candidate has {item.numberOfVotes} votes");
                    NotInTheList = false;
                    break;
                }
                else
                {
                    NotInTheList = true;
                }
            }
            if (NotInTheList)
            {
                Console.WriteLine($"{givenSurname} {givenFirstname} candidate is not in the list of candidates");
            }
        }

        //Calculate how many people has voted and the percentage of the people who has voted
        //The tasksheet stated there were 12345 people who had the right to vote
        static int peopleWhoVoted = 0;
        static void Task4()
        {
            Console.WriteLine("Task 4");
            peopleWhoVoted = 0;
            foreach (candidate item in candidatelist)
            {
                peopleWhoVoted += item.numberOfVotes;
            }
            int peopleWhoCouldVote = 12345;
            double percentage = Math.Round((double)peopleWhoVoted / peopleWhoCouldVote * 100, 2);
            Console.WriteLine($"{peopleWhoVoted} people votes, {percentage}% of all");
        }

        //Calculate the percentage of votes each party has recieved
        static void Task5()
        {
            Console.WriteLine("Task 5");
            List<string> parties = new List<string>();
            foreach (candidate item in candidatelist)
            {
                if (!parties.Contains(item.party))
                {
                    parties.Add(item.party);
                }
            }
            foreach (string i in parties)
            {
                int votes = 0;
                foreach (candidate item in candidatelist)
                {
                    if (item.party == i)
                    {
                        votes += item.numberOfVotes;
                    }
                }
                double percentage = Math.Round((double)votes / peopleWhoVoted * 100, 2);
                if (i == "-")
                {
                    Console.WriteLine($"Independent candidates= {percentage}%");
                }
                else
                {
                    Console.WriteLine($"{i}= {percentage}%");
                }

            }
        }

        //Who got the most votes?
        static void Task6()
        {
            Console.WriteLine("Task 6");
            int max = 0;
            foreach (candidate item in candidatelist)
            {
                if (item.numberOfVotes > max)
                {
                    max = item.numberOfVotes;
                }
            }
            foreach (candidate item in candidatelist)
            {
                if (item.numberOfVotes == max)
                {
                    if (item.party != "-")
                    {
                        Console.WriteLine($" {item.surname} {item.firstname} {item.party}");
                    }
                    else
                    {
                        Console.WriteLine($" {item.surname} {item.firstname} independent");
                    }

                }
            }
        }

        //Calculate who von in each district. List them in kepviselok.txt (kepviselok=candidates)
        static void Task7()
        {
            Console.WriteLine("Task 7 - kepviselok.txt");
            StreamWriter sw = new StreamWriter("kepviselok.txt");

            List<int> district = new List<int>();
            foreach (candidate item in candidatelist)
            {
                if (!district.Contains(item.district))
                {
                    district.Add(item.district);
                }
            }
            for (int i = 1; i < district.Count; i++)
            {
                int max = 0;
                foreach (candidate item in candidatelist)
                {
                    if (item.district == i)
                    {
                        if (item.numberOfVotes > max)
                            max = item.numberOfVotes;
                    }
                }
                foreach (candidate item in candidatelist)
                {
                    if (item.numberOfVotes == max && item.district == i)
                    {
                        if (item.party != "-")
                        {
                            sw.WriteLine($"{i} {item.surname} {item.firstname} {item.party}");
                        }
                        else
                        {
                            sw.WriteLine($"{i} {item.surname} {item.firstname} independent");
                        }
                    }
                }
            }

            sw.Flush();
            sw.Close();
        }
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
            Task5();
            Task6();
            Task7();
            Console.ReadKey();
        }
    }
}
