

public class Match{
    
    private string HomeTeam;

    private int HomeGoals;

    private string VisitTeam;

    private int VisitGoals;

    private string Winner;

    public Match(string HomeTeam, int HomeGoals, string VisitTeam, int VisitGoals, string Winner){

        this.HomeTeam = HomeTeam;
        this.HomeGoals = HomeGoals;
        this.VisitTeam = VisitTeam;
        this.VisitGoals = VisitGoals;
        this.Winner = Winner;

    }

    public string printMatch(){
        return ""+HomeTeam+"___"+HomeGoals+"___Versus___"+VisitGoals+"___"+VisitTeam+"___"+Winner+"\n \n";
    }


}