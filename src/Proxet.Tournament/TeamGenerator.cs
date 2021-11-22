using System;
using System.Collections.Generic;
using System.IO;
namespace Proxet.Tournament
{
    class Player : IComparable
    {
        public string Name { get; set; }
        public int WaitingTime { get; set; }
        public int VehicleType { get; set; }
        public Player(string Name, int WaitingTime, int VehicleType)
        {
            this.Name = Name;
            this.WaitingTime = WaitingTime;
            this.VehicleType = VehicleType;
        }
        //Method for sorting
        public int CompareTo(object obj)
        {
            Player p = obj as Player;
            if (p != null)
            {
                return p.WaitingTime.CompareTo(this.WaitingTime);
            }
            else
            {
                throw new Exception("Cannot to compare");
            }
        }
    }
    public class TeamGenerator
    {
        public (string[] team1, string[] team2) GenerateTeams(string filePath)
        {
            string[] team1 = new string[9];
            string[] team2 = new string[9];
            List<Player> players = new List<Player>();
            //Read file data
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] player = line.Split('\t');
                    players.Add(new Player(player[0].ToString(), Int32.Parse(player[1]), Int32.Parse(player[2])));
                }
            }
            players.Sort();

            AddPlayerToTeam(team1, players);
            AddPlayerToTeam(team2, players);

            return (team1, team2);
        }

        static void AddPlayerToTeam(string[] team, List<Player> players)
        {
            int b1 = 0, b2 = 0, b3 = 0, k = 0;
            int j;
            //take 3 vehicles of each of 3 possible vehicle types
            for (int i = 0; i < team.Length; i++)
            {
                j = 0;
                j += k;
                for (; j < players.Count; j++)
                {
                    if (players[j].VehicleType == 1 && b1 != 3)
                    {
                        team[i] = players[j].Name;
                        players.Remove(players[j]);
                        b1++;
                        break;
                    }
                    else if (players[j].VehicleType == 2 && b2 != 3)
                    {
                        team[i] = players[j].Name;
                        players.Remove(players[j]);
                        b2++;
                        break;
                    }
                    else if (players[j].VehicleType == 3 && b3 != 3)
                    {
                        team[i] = players[j].Name;
                        players.Remove(players[j]);
                        b3++;
                        break;
                    }
                    k++;
                }
            }
        }
    }
}