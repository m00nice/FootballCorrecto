
namespace Football

{
    class Program
    {
        static void Main(string[] args)
        {
            string setupPath = "setup.csv";

            string teamsPath = "teams/SuperLigaTeams.csv";


            bool isPlaying = false;

            string userInput;

            Console.WriteLine("You game?(write yes or y if you're ready)");

            userInput = Console.ReadLine();

            string choice = userInput.ToLower();

            if(choice.Equals("yes") | choice.Equals("y")){

            isPlaying = true;

             }




             while(isPlaying){

            Console.WriteLine(
            """
            Start by select your setup for your tournament,
            you can choose between(type the number of the tournament)

            1. Superligaen

            2. NordicBetLigaen
            """);

            userInput = Console.ReadLine();
            switch(userInput){

            case "1":
            //scan csv files and write rules and teams in console

            using (var reader = new StreamReader(setupPath)){

                while(!reader.EndOfStream){
                    string line = reader.ReadLine();
                    string[] values = line.Split(",");

                    if(values[0].Equals("Super Liga")){
                        Setup superLiga = new Setup(values[0],int.Parse(values[1]) ,int.Parse(values[2]),int.Parse(values[3]),int.Parse(values[4]),int.Parse(values[5]));
                    }

                }

            }
            
            


























            Console.WriteLine("""
            You have chosen Superligaen

            The current standing are

            Position:     Team:    Played:    Wins:    Draws:   Lost:   GoalsFor:   GoalsAgainst:   Points:     Streak:

            """+userInput+"""



            """);

            break;

            case "2":

            break;

            default:
            isPlaying = false;
            break;
        }
    }
}

}
    
}
