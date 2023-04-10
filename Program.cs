
namespace Football

{
    class Program
    {
        static void Main(string[] args)
        {
            

            //string setupPath = "setup.csv";

            string teamsPath = "teams/Teams.csv";


            bool isPlaying = false;

            string? userInput;

            Console.WriteLine("You game?(write yes or y if you're ready)");

            userInput = Console.ReadLine();

            string choice = userInput.ToLower();

            if(choice.Equals("yes") | choice.Equals("y")){

            isPlaying = true;

             }
            /*
            /

            using (var reader = new StreamReader(setupPath)){

                while(!reader.EndOfStream){
                    string line = reader.ReadLine();
                    string[] values = line.Split(",");


                    if(values[0].Equals("Super Liga")){
                        Setup superLiga = new Setup(values[0],int.Parse(values[1]) ,int.Parse(values[2]),int.Parse(values[3]),int.Parse(values[4]),int.Parse(values[5]));
                    }

                }

            }*/



             while(isPlaying){

            Console.WriteLine(
            """
            Start by select your setup for your tournament,
            you can choose between(type the number of the tournament)

            1. Superligaen

            2. NordicBetLigaen

            3. Exit

            """);

            userInput = Console.ReadLine();
            switch(userInput){

            case "1":

            List<Team> teamsSL = new List<Team>();
            Random random = new Random();
            teamsSL = teamsSL.OrderBy(x => random.Next()).ToList();
            
            using (var reader = new StreamReader(teamsPath)){
                int i = 0;
                
                while(!reader.EndOfStream){
                    
                    string line = reader.ReadLine();
                    string[] values = line.Split(",");
                     

                    if(values[3].Equals("SuperLiga")){
                        
                        Team team = new Team(values[0], values[1], values[2]);
                        teamsSL.Add(team);
                        i++;
                    }
                }
                
            }
            
            string standingS = """
            You have chosen Superligaen

            The current standings are

            Position:     Team:    Played:    Wins:    Draws:   Lost:   GoalsFor:   GoalsAgainst:   Points:     Streak:

            """;
            
            for (int i = 0; i < teamsSL.Count; i++)
            {
                standingS = standingS + "\n"+(i+1)+"\t\t"+teamsSL[i].Abbreviation+"\t\t"+teamsSL[i].played+"\t"+teamsSL[i].wins+"\t"+teamsSL[i].draws+"\t"+teamsSL[i].lost+"\t\t"+teamsSL[i].goalsFor+"\t"+teamsSL[i].goalsAgainst+"\t\t"+teamsSL[i].points+"\t"+teamsSL[i].streak+"";
            }

            Console.WriteLine(standingS);
            
            List<Match> listOfMatches = new List<Match>();            
                foreach(Team homeTeam in teamsSL){
                    foreach(Team visitorTeam in teamsSL){
                        if(homeTeam != visitorTeam){
                            bool isDuplicate = false;
                                foreach(Match otherMatch in listOfMatches){
                                        if((otherMatch.HomeTeam == homeTeam && otherMatch.VisitTeam == visitorTeam) || (otherMatch.VisitTeam == visitorTeam && otherMatch.HomeTeam == homeTeam)){
                                            isDuplicate = true;
                                            break;
                                        }
                                    }
                                if(isDuplicate){
                                    break;
                                }

                        if(!isDuplicate){
                                Match match = new Match(homeTeam, visitorTeam);
                                listOfMatches.Add(match);
                            }
                        }
                }
            }
            
            List<Round> rounds = new List<Round>();
            for (int l = 0; l < 22; l++)
            {
            rounds.Add(new Round(l+1));
            List<Team> usedTeams = new List<Team>();
            for (int i = 0; i < 6; i++)
                {
                    bool noGo;
                    noGo = true;
                    while(noGo){
                        int index = random.Next(0, 132);
                    if(!usedTeams.Contains(listOfMatches[index].HomeTeam) && !usedTeams.Contains(listOfMatches[index].VisitTeam)){
                    rounds[l].matches.Add(listOfMatches[index]);
                    usedTeams.Add(listOfMatches[index].HomeTeam);
                    usedTeams.Add(listOfMatches[index].VisitTeam);
                    noGo = false;
                    }}
                }
            }
            


            
            int R = 0;
            bool isPlaying2 = true;
            while(isPlaying2){

            Console.WriteLine($"""

            Choose your next move

            1. Play round {R+1}

            2. See standings

            3. Exit

            """);




                userInput = Console.ReadLine();
            switch (userInput)
            {
                
                case "1":
                
                if(R <= 22){
                for (int i = 0; i < 6; i++)
                {
                    rounds[R].matches[i].PlayMatch();
                     Console.WriteLine("In the match between "+rounds[R].matches[i].HomeTeam.Abbreviation+"(H) and "+rounds[R].matches[i].VisitTeam.Abbreviation+"(V)... the winner is  "+rounds[R].matches[i].Winner+" the score was ("+rounds[R].matches[i].HomeGoals+"-"+rounds[R].matches[i].VisitGoals+")");
                    
                    string roundPath = $"rounds/round{R+1}.csv";
                    using(StreamWriter writer = new StreamWriter(roundPath)){
                        writer.WriteLine("HomeTeam,GoalsHome,GoalsVisit,VisitTeam,Winner");
                        foreach (Match match in rounds[R].matches)
                        {
                            writer.WriteLine(""+match.HomeTeam.Abbreviation+","+match.HomeGoals+","+match.VisitGoals+","+match.VisitTeam.Abbreviation+","+match.Winner+"");
                        }
                }
                
                }
                R++;
                if(R == 22){

                    teamsSL.Sort(new PointsComparer());
                    Console.WriteLine("""
                    This tables final standings are

                    Position:     Team:    Played:    Wins:    Draws:   Lost:   GoalsFor:   GoalsAgainst:   Points:     Streak:
                    """);
                    string standingStr = "";
                    for (int i = 0; i < teamsSL.Count; i++)
                    {
                    standingStr = standingStr + "\n"+(i+1)+"\t   \t"+teamsSL[i].Abbreviation+""+teamsSL[i].SpecialRanking+"\t\t"+teamsSL[i].played+"\t"+teamsSL[i].wins+"\t"+teamsSL[i].draws+"\t"+teamsSL[i].lost+"\t"+teamsSL[i].goalsFor+"\t\t"+teamsSL[i].goalsAgainst+"\t"+teamsSL[i].points+"\t\t"+teamsSL[i].streak+"";
                    }

                    Console.WriteLine(standingStr);
                    
                    
                    Console.WriteLine("\nOkay now that the first 22 rounds have been played the teams will be split into two tables each with six teams\nIf teams are tied in points they will be playing eachother bellow");


                    string roundPath = $"rounds/roundRematch.csv";
                    using(StreamWriter writer = new StreamWriter(roundPath)){
                    writer.WriteLine("HomeTeam,GoalsHome,GoalsVisit,VisitTeam,Winner");
                    foreach(Team team in teamsSL){
                        foreach (Team otherTeam in teamsSL)
                        {
                            if(team != otherTeam){
                            if(team.points == otherTeam.points){
                                Console.WriteLine("Due to "+team.Abbreviation+" and "+otherTeam.Abbreviation+" having equals points, a rematch must be played");
                                Match rematch = new Match(team, otherTeam);
                                rematch.PlayMatch();
                                if(team.points == otherTeam.points){
                                    if(team.power < otherTeam.power){otherTeam.points = otherTeam.points + 1;}else if(team.power > otherTeam.power){team.points = team.points + 1;}
                                    else{team.points = team.points + 1;}
                                }
                                Console.WriteLine("In the match between "+team.Abbreviation+"(H) and "+otherTeam.Abbreviation+"(V)... the winner is  "+rematch.Winner+" the score was ("+rematch.HomeGoals+"-"+rematch.VisitGoals+")");
                                
                                
                                writer.WriteLine(""+rematch.HomeTeam.Abbreviation+","+rematch.HomeGoals+","+rematch.VisitGoals+","+rematch.VisitTeam.Abbreviation+","+rematch.Winner+"");
                                
                            }}
                        }
                    }
                    }

                    
                    List<Team> upperTable = new List<Team>();
                    List<Team> lowerTable = new List<Team>();
                    for (int i = 0; i < teamsSL.Count - 6; i++)
                    {
                        upperTable.Add(teamsSL[i]);
                    }

                    for (int i = teamsSL.Count/2; i < teamsSL.Count; i++)
                    {
                        lowerTable.Add(teamsSL[i]);
                    }

                    for (int i = 0; i < upperTable.Count; i++)
                    {
                        upperTable[i].draws = 0;
                        upperTable[i].lost = 0;
                        upperTable[i].wins = 0;
                        upperTable[i].points = 0;
                        upperTable[i].played = 0;
                        upperTable[i].goalsAgainst = 0;
                        upperTable[i].goalsFor = 0;
                        upperTable[i].streak = 0;
                    }

                    for (int i = 0; i < upperTable.Count; i++)
                    {
                        lowerTable[i].draws = 0;
                        lowerTable[i].lost = 0;
                        lowerTable[i].wins = 0;
                        lowerTable[i].points = 0;
                        lowerTable[i].played = 0;
                        lowerTable[i].goalsAgainst = 0;
                        lowerTable[i].goalsFor = 0;
                        lowerTable[i].streak = 0;
                    }

                    
                    string standingStri = """
                    The two tables have now been put together, this is the current standings
                    """;

                    string table1 = "\ntable 1\nPosition:     Team:    Played:    Wins:    Draws:   Lost:   GoalsFor:   GoalsAgainst:   Points:     Streak:";

                    for (int i = 0; i < upperTable.Count; i++)
                    {
                    table1 = table1 + "\n"+(i+1)+"\t   \t"+upperTable[i].Abbreviation+""+upperTable[i].SpecialRanking+"\t\t"+upperTable[i].played+"\t"+upperTable[i].wins+"\t"+upperTable[i].draws+"\t"+upperTable[i].lost+"\t"+upperTable[i].goalsFor+"\t\t"+upperTable[i].goalsAgainst+"\t"+upperTable[i].points+"\t\t"+upperTable[i].streak+"";
                    }  

                    string table2 = "\ntable 2\nPosition:     Team:    Played:    Wins:    Draws:   Lost:   GoalsFor:   GoalsAgainst:   Points:     Streak:";

                    for (int i = 0; i < lowerTable.Count; i++)
                    {
                    table2 = table2 + "\n"+(i+1)+"\t   \t"+lowerTable[i].Abbreviation+""+lowerTable[i].SpecialRanking+"\t\t"+lowerTable[i].played+"\t"+lowerTable[i].wins+"\t"+lowerTable[i].draws+"\t"+lowerTable[i].lost+"\t"+lowerTable[i].goalsFor+"\t\t"+lowerTable[i].goalsAgainst+"\t"+lowerTable[i].points+"\t\t"+lowerTable[i].streak+"";
                    }       
                    
                    Console.WriteLine(standingStri+table1+table2);

                    List<Match> table1Matches = new List<Match>();
                foreach(Team homeTeam in upperTable){
                    foreach(Team visitorTeam in upperTable){
                        if(homeTeam != visitorTeam){
                            bool isDuplicate1 = false;
                                foreach(Match otherMatch in table1Matches){
                                        if((otherMatch.HomeTeam == homeTeam && otherMatch.VisitTeam == visitorTeam) || (otherMatch.VisitTeam == visitorTeam && otherMatch.HomeTeam == homeTeam)){
                                            isDuplicate1 = true;
                                            break;
                                        }
                                    }
                                if(isDuplicate1){
                                    break;
                                }

                        if(!isDuplicate1){
                                Match match = new Match(homeTeam, visitorTeam);
                                table1Matches.Add(match);
                            }
                        }
                }
            }
       

            List<Round> roundsTable1 = new List<Round>();
            for (int l = 0; l < 10; l++)
            {
            roundsTable1.Add(new Round(l+1));
            List<Team> usedTeams1 = new List<Team>();
            for (int i = 0; i < 3; i++)
                {
                    
                    bool noGo1;
                    noGo1 = true;
                    while(noGo1){
                        int index1 = random.Next(0, table1Matches.Count);
                    if(!usedTeams1.Contains(table1Matches[index1].HomeTeam) && !usedTeams1.Contains(table1Matches[index1].VisitTeam)){
                    roundsTable1[l].matches.Add(table1Matches[index1]);
                    usedTeams1.Add(table1Matches[index1].HomeTeam);
                    usedTeams1.Add(table1Matches[index1].VisitTeam);
                    noGo1 = false;
                    }}
                }
            }




            List<Match> table2Matches = new List<Match>();
                foreach(Team homeTeam in lowerTable){
                    foreach(Team visitorTeam in lowerTable){
                        if(homeTeam != visitorTeam){
                            bool isDuplicate2 = false;
                                foreach(Match otherMatch in table2Matches){
                                        if((otherMatch.HomeTeam == homeTeam && otherMatch.VisitTeam == visitorTeam) || (otherMatch.VisitTeam == visitorTeam && otherMatch.HomeTeam == homeTeam)){
                                            isDuplicate2 = true;
                                            break;
                                        }
                                    }
                                if(isDuplicate2){
                                    break;
                                }

                        if(!isDuplicate2){
                                Match match = new Match(homeTeam, visitorTeam);
                                table2Matches.Add(match);
                            }
                        }
                }
            }
            

            List<Round> roundsTable2 = new List<Round>();
            for (int l = 0; l < 10; l++)
            {
            roundsTable2.Add(new Round(l+1));
            List<Team> usedTeams2 = new List<Team>();
            for (int i = 0; i < 3; i++)
                {
                    bool noGo2;
                    noGo2 = true;
                    while(noGo2){
                        int index2 = random.Next(0, table2Matches.Count);
                    if(!usedTeams2.Contains(table2Matches[index2].HomeTeam) && !usedTeams2.Contains(table2Matches[index2].VisitTeam)){
                    roundsTable2[l].matches.Add(table2Matches[index2]);
                    usedTeams2.Add(table2Matches[index2].HomeTeam);
                    usedTeams2.Add(table2Matches[index2].VisitTeam);
                    noGo2 = false;
                    }}
                }
            }



                    bool isPlaying3 = true;
                    R = 0;
                    while(isPlaying3){

                    Console.WriteLine($"""

                    Choose your next move

                    1. Play round {R+1}

                    2. See standings

                    3. Exit

                    """);
                    
                    userInput = Console.ReadLine();
                    
                    switch (userInput)
                    {
                        case "1":

                        if(R <= 10){
                        for (int i = 0; i < 3; i++)
                    {
                    roundsTable1[R].matches[i].PlayMatch();
                     Console.WriteLine("On Table 1 the match between "+roundsTable1[R].matches[i].HomeTeam.Abbreviation+"(H) and "+roundsTable1[R].matches[i].VisitTeam.Abbreviation+"(V)... the winner is  "+roundsTable1[R].matches[i].Winner+" the score was ("+roundsTable1[R].matches[i].HomeGoals+"-"+roundsTable1[R].matches[i].VisitGoals+")");
                    
                    string roundT1Path = $"rounds/Table1R{R+1}.csv";
                    using(StreamWriter writer = new StreamWriter(roundPath)){
                        writer.WriteLine("HomeTeam,GoalsHome,GoalsVisit,VisitTeam,Winner");
                        foreach (Match match in roundsTable1[R].matches)
                        {
                            writer.WriteLine(""+match.HomeTeam.Abbreviation+","+match.HomeGoals+","+match.VisitGoals+","+match.VisitTeam.Abbreviation+","+match.Winner+"");
                        }
                }
                
                }
                for (int j = 0; j < 3; j++)
                {
                    roundsTable2[R].matches[j].PlayMatch();
                     Console.WriteLine("On Table 2 the match between "+roundsTable2[R].matches[j].HomeTeam.Abbreviation+"(H) and "+roundsTable2[R].matches[j].VisitTeam.Abbreviation+"(V)... the winner is  "+roundsTable2[R].matches[j].Winner+" the score was ("+roundsTable2[R].matches[j].HomeGoals+"-"+roundsTable2[R].matches[j].VisitGoals+")");
                    
                    string roundT2Path = $"rounds/Table2R{R+1}.csv";
                    using(StreamWriter writer = new StreamWriter(roundPath)){
                        writer.WriteLine("HomeTeam,GoalsHome,GoalsVisit,VisitTeam,Winner");
                        foreach (Match match in roundsTable2[R].matches)
                        {
                            writer.WriteLine(""+match.HomeTeam.Abbreviation+","+match.HomeGoals+","+match.VisitGoals+","+match.VisitTeam.Abbreviation+","+match.Winner+"");
                        }
                }
                
                }
                R++;
                if(R == 10){

                    Console.WriteLine("\ntime for rematches, if there are any");


                    string roundR1Path = $"rounds/roundT1Rematch.csv";
                    using(StreamWriter writer = new StreamWriter(roundR1Path)){
                    writer.WriteLine("HomeTeam,GoalsHome,GoalsVisit,VisitTeam,Winner");
                    foreach(Team team in upperTable){
                        foreach (Team otherTeam in upperTable)
                        {
                            if(team != otherTeam){
                            if(team.points == otherTeam.points){
                                Console.WriteLine("Due to "+team.Abbreviation+" and "+otherTeam.Abbreviation+" having equals points, a rematch must be played");
                                Match rematch = new Match(team, otherTeam);
                                rematch.PlayMatch();
                                if(team.points == otherTeam.points){
                                    if(team.power < otherTeam.power){otherTeam.points = otherTeam.points + 1;}else if(team.power > otherTeam.power){team.points = team.points + 1;}
                                    else{team.points = team.points + 1;}
                                }
                                Console.WriteLine("In the match between "+team.Abbreviation+"(H) and "+otherTeam.Abbreviation+"(V)... the winner is  "+rematch.Winner+" the score was ("+rematch.HomeGoals+"-"+rematch.VisitGoals+")");
                                
                                
                                writer.WriteLine(""+rematch.HomeTeam.Abbreviation+","+rematch.HomeGoals+","+rematch.VisitGoals+","+rematch.VisitTeam.Abbreviation+","+rematch.Winner+"");
                                
                            }}
                        }
                    }
                    }


                    string roundR2Path = $"rounds/roundRematch1.csv";
                    using(StreamWriter writer = new StreamWriter(roundR2Path)){
                    writer.WriteLine("HomeTeam,GoalsHome,GoalsVisit,VisitTeam,Winner");
                    foreach(Team team in lowerTable){
                        foreach (Team otherTeam in lowerTable)
                        {
                            if(team != otherTeam){
                            if(team.points == otherTeam.points){
                                Console.WriteLine("Due to "+team.Abbreviation+" and "+otherTeam.Abbreviation+" having equals points, a rematch must be played");
                                Match rematch = new Match(team, otherTeam);
                                rematch.PlayMatch();
                                if(team.points == otherTeam.points){
                                    if(team.power < otherTeam.power){otherTeam.points = otherTeam.points + 1;}else if(team.power > otherTeam.power){team.points = team.points + 1;}
                                    else{team.points = team.points + 1;}
                                }
                                Console.WriteLine("In the match between "+team.Abbreviation+"(H) and "+otherTeam.Abbreviation+"(V)... the winner is  "+rematch.Winner+" the score was ("+rematch.HomeGoals+"-"+rematch.VisitGoals+")");
                                
                                
                                writer.WriteLine(""+rematch.HomeTeam.Abbreviation+","+rematch.HomeGoals+","+rematch.VisitGoals+","+rematch.VisitTeam.Abbreviation+","+rematch.Winner+"");
                                
                            }}
                        }
                    }
                    }

                    upperTable.Sort(new PointsComparer());
                    lowerTable.Sort(new PointsComparer());
                    Console.WriteLine($"""
                    
                    
                    The matches and rematches in each table have now been played and a champion has been decided
                    This years super liga champion is
                    *********************************
                    {upperTable[0].FullClubName}
                    *********************************
                    The champion has earned a positions in champions league
                    
                    This years cup winner is {lowerTable[1].FullClubName}

                    {lowerTable[4].FullClubName} will demote to the nordicbetliga

                    {lowerTable[5].FullClubName} will demote to the nordicbetliga

                    
                    """);

                    Console.WriteLine("""

                    The final standings were
                    """);
                    table1 = "\n\ntable 1\nPosition:     Team:    Played:    Wins:    Draws:   Lost:   GoalsFor:   GoalsAgainst:   Points:     Streak:";

                    for (int i = 0; i < upperTable.Count; i++)
                    {
                    table1 = table1 + "\n"+(i+1)+"\t   \t"+upperTable[i].Abbreviation+""+upperTable[i].SpecialRanking+"\t\t"+upperTable[i].played+"\t"+upperTable[i].wins+"\t"+upperTable[i].draws+"\t"+upperTable[i].lost+"\t"+upperTable[i].goalsFor+"\t\t"+upperTable[i].goalsAgainst+"\t"+upperTable[i].points+"\t\t"+upperTable[i].streak+"";
                    }  

                    table2 = "\n\ntable 2\nPosition:     Team:    Played:    Wins:    Draws:   Lost:   GoalsFor:   GoalsAgainst:   Points:     Streak:";

                    for (int i = 0; i < lowerTable.Count; i++)
                    {
                    table2 = table2 + "\n"+(i+1)+"\t   \t"+lowerTable[i].Abbreviation+""+lowerTable[i].SpecialRanking+"\t\t"+lowerTable[i].played+"\t"+lowerTable[i].wins+"\t"+lowerTable[i].draws+"\t"+lowerTable[i].lost+"\t"+lowerTable[i].goalsFor+"\t\t"+lowerTable[i].goalsAgainst+"\t"+lowerTable[i].points+"\t\t"+lowerTable[i].streak+"";
                    }       
                    
                    Console.WriteLine(table1+table2);
                    isPlaying = false;
                    isPlaying2 = false;
                    isPlaying3 = false;

                }
                }

                        break;

                        case "2":

                        
                    upperTable.Sort(new PointsComparer());
                    lowerTable.Sort(new PointsComparer());

            
                    table1 = "\n\ntable 1\nPosition:     Team:    Played:    Wins:    Draws:   Lost:   GoalsFor:   GoalsAgainst:   Points:     Streak:";

                    for (int i = 0; i < upperTable.Count; i++)
                    {
                    table1 = table1 + "\n"+(i+1)+"\t   \t"+upperTable[i].Abbreviation+""+upperTable[i].SpecialRanking+"\t\t"+upperTable[i].played+"\t"+upperTable[i].wins+"\t"+upperTable[i].draws+"\t"+upperTable[i].lost+"\t"+upperTable[i].goalsFor+"\t\t"+upperTable[i].goalsAgainst+"\t"+upperTable[i].points+"\t\t"+upperTable[i].streak+"";
                    }  

                    table2 = "\n\ntable 2\nPosition:     Team:    Played:    Wins:    Draws:   Lost:   GoalsFor:   GoalsAgainst:   Points:     Streak:";

                    for (int i = 0; i < lowerTable.Count; i++)
                    {
                    table2 = table2 + "\n"+(i+1)+"\t   \t"+lowerTable[i].Abbreviation+""+lowerTable[i].SpecialRanking+"\t\t"+lowerTable[i].played+"\t"+lowerTable[i].wins+"\t"+lowerTable[i].draws+"\t"+lowerTable[i].lost+"\t"+lowerTable[i].goalsFor+"\t\t"+lowerTable[i].goalsAgainst+"\t"+lowerTable[i].points+"\t\t"+lowerTable[i].streak+"";
                    }       
                    
                    Console.WriteLine(standingStri+table1+table2);
                
                    

                        break;

                        case "3":
                        isPlaying3 = false;
                        break;
                        
                        default:
                        break;
                    }

                }
                }
                }

                


                
                
                break;

                case "2":

                string standingSL = """

                The current standings are

                Position:     Team:    Played:    Wins:    Draws:   Lost:   GoalsFor:   GoalsAgainst:   Points:     Streak:

                """;

                teamsSL.Sort(new PointsComparer());

                for (int i = 0; i < teamsSL.Count; i++)
                {
                    standingSL = standingSL + "\n"+(i+1)+"\t   \t"+teamsSL[i].Abbreviation+""+teamsSL[i].SpecialRanking+"\t\t"+teamsSL[i].played+"\t"+teamsSL[i].wins+"\t"+teamsSL[i].draws+"\t"+teamsSL[i].lost+"\t"+teamsSL[i].goalsFor+"\t\t"+teamsSL[i].goalsAgainst+"\t"+teamsSL[i].points+"\t\t"+teamsSL[i].streak+"";
                }
                
                Console.WriteLine(standingSL);
                break;


                case "3":
                isPlaying2 = false;
                break;

                default:

                break;
            }}
            


            break;
            
            case "2":

            Console.WriteLine("sorry league not avaiable");

            break;

            case "3":
            isPlaying = false;
            break;

            default:
            
            break;
        }
        
    }


    


}
}
    
}
