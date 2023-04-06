

    public class Setup{

        private string LeagueName{get;set;}

        private int PosPromoChampion{get;set;}

        private int PosPromoEuro{get;set;}

        private int PosPromoConference{get;set;}

        private int PosPromoUpper{get;set;}

        private int PosDemoLower{get;set;}


        public Setup(string LeagueName, int PosPromoChampion, int PosPromoEuro, int PosPromoConference, int PosPromoUpper, int PosDemoLower){
            this.LeagueName = LeagueName;
            this.PosPromoChampion = PosPromoChampion;
            this.PosPromoEuro = PosPromoEuro;
            this.PosPromoConference = PosPromoConference;
            this.PosPromoUpper = PosPromoUpper;
            this.PosDemoLower = PosDemoLower;
        }

        
    }