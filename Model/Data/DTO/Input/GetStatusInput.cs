using System.ComponentModel.DataAnnotations;

namespace Model.Data.DTO.Input
{
    public class GetStatusInput
    {
        [Required]
        public string PlayerID { get; set; }
    }
}
