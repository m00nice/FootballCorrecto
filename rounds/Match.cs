

public class Match{
    
    public Team HomeTeam{get; set;}

    

    public int HomeGoals{get; set;}

    public Team VisitTeam{get; set;}

    

    public int VisitGoals{get; set;}

    public string Winner{get; set;}

    Random rnd = new Random();

    public Match(Team HomeTeam, Team VisitTeam){

        this.HomeTeam = HomeTeam;
        
        this.VisitTeam = VisitTeam;
        
    }

    public void PlayMatch(){
        this.HomeGoals = rnd.Next(0,3) + HomeTeam.power - VisitTeam.power;
        if(this.HomeGoals < 0){
            this.HomeGoals = 0;
        }

        this.VisitGoals = rnd.Next(0,3) + VisitTeam.power - HomeTeam.power;
        if(this.VisitGoals < 0){
            this.VisitGoals = 0;
        }

        if(this.HomeGoals > this.VisitGoals){
        this.Winner = HomeTeam.Abbreviation;
        this.VisitTeam.lost++;
        this.HomeTeam.wins++;
        this.HomeTeam.points = this.HomeTeam.points +2;
        this.HomeTeam.streak = this.HomeTeam.streak +1;
        this.VisitTeam.streak = 0;
        }else if(this.HomeGoals < this.VisitGoals){
        this.Winner = VisitTeam.Abbreviation;
        this.VisitTeam.wins++;
        this.VisitTeam.points = this.VisitTeam.points +2;
        this.HomeTeam.lost++;
        this.HomeTeam.streak = 0;
        this.VisitTeam.streak = this.VisitTeam.streak +1;
        }else{this.Winner = "Draw";
        this.VisitTeam.draws++;
        this.VisitTeam.points = this.VisitTeam.points +1;
        this.HomeTeam.draws++;
        this.HomeTeam.points = this.HomeTeam.points +1;
        this.HomeTeam.streak = 0;
        this.HomeTeam.streak = 0;
        }


        this.HomeTeam.goalsAgainst = this.HomeTeam.goalsAgainst + VisitGoals;
        this.HomeTeam.goalsFor = this.HomeTeam.goalsFor + HomeGoals;

        this.VisitTeam.goalsAgainst = this.VisitTeam.goalsAgainst + HomeGoals;
        this.VisitTeam.goalsFor = this.VisitTeam.goalsFor + VisitGoals;

        this.HomeTeam.played++;
        this.VisitTeam.played++;

    }

    


}