using System.ComponentModel.DataAnnotations;

namespace SkolSystem.Models
{
    public class ElevDetails
    {
        public int El_Id { get; set; }
        [Required]
        public string El_Fornamn { get; set; }
        [Required]
        public string El_Efternamn { get; set; }

        /*public int Ku_Id { get; set; } */

    }

}
