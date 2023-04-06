

public class Round{

    private Match[] matches {get; set;}

    public string printRound(Match[] matches){

        string roundS = "Home-Team___Goals(H)___Versus___Goals(V)___Visitor-Team___Winner\n";

        for (int i = 0; i < matches.Length; i++)
        {
            roundS = roundS + matches[i].printMatch;
        }

        return "";
    }


}