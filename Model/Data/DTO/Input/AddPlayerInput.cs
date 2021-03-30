using System.ComponentModel.DataAnnotations;

namespace Model.Data.DTO.Input
{
    public class AddPlayerInput
    {
        [Required]
        public string PlayerID { get; set; }
        public string PlayerName { get; set; }
    }
}
