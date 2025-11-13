namespace Banking_Payments.Models.DTO
{
   
        public class ClientStatsDTO
        {
            public int TotalClients { get; set; }
            public int PendingOnboard { get; set; }
            public int VerifiedClients { get; set; }
            public int RejectedClients { get; set; }
        }
    
}
