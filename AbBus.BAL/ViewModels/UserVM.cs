using System;

namespace AbBus.BAL.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
