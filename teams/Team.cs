
public class Team{

    private string Abbreviation{get; set;}
    
    private string FullClubName{get; set;}

    private string SpecialRanking{get; set;}

    public Team(string Abbreviation, string FullClubName, string SpecialRanking){
        this.Abbreviation = Abbreviation;
        this.FullClubName = FullClubName;
        this.SpecialRanking = SpecialRanking;
        
    }



}