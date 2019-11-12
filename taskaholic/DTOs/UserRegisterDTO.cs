using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using taskaholic.Models;

namespace taskaholic.DTOs
{
    public class UserRegisterDTO
    {
        public User User { get; set; }
        [Required(ErrorMessage = "Uma senha é necessário para o cadastro")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha precisa ter pelo menos 8 caracteres")]
        public string Password { get; set; }
    }
}
