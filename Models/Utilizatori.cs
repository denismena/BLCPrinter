using System.ComponentModel.DataAnnotations;


public class Utilizatori
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [Display(Name = "Numele si prenumele")]
    public string NumePrenume { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    public string Parola { get; set; }

    [Display(Name = "Admin")]
    public bool IsAdmin { get; set; }

    [Display(Name = "Activ")]
    public bool IsActive { get; set; }
}
