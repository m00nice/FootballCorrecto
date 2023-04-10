
public class Team{

    public string Abbreviation{get; set;}
    
    public string FullClubName{get; set;}

    public string SpecialRanking{get; set;}

    public int power{get; set;}

    public int played{get; set;}

    public int wins{get; set;} = 0;

    public int draws{get; set;} = 0;

    public int lost{get; set;} = 0;

    public int goalsFor{get; set;} = 0;

    public int goalsAgainst{get; set;} = 0;

    public int points{get; set;} = 0;

    public int streak{get; set;} = 0;
    
    Random rnd = new Random();

    public Team(string Abbreviation, string FullClubName, string SpecialRanking){
        this.Abbreviation = Abbreviation;
        this.FullClubName = FullClubName;
        this.SpecialRanking = SpecialRanking;
        this.power = rnd.Next(0, 5);
        
    }

}