using System;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    // Abstraction: Abstract base class
    public abstract class SystemUser
    {
        // Encapsulation: Protected setters, public getters
        public Guid Id { get; protected set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // In a real app, this should be hashed
        public bool IsActive { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime DateAdded { get; set; }

        protected SystemUser(string username, string email, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            Password = password;
            IsActive = true;
            LastActive = DateTime.Now;
            DateAdded = DateTime.Now;
        }

        // Polymorphism: Abstract method implementation required
        public abstract string GetRoleDisplay();
        
        // Polymorphism: Virtual method can be overridden
        public virtual bool CanEditUsers()
        {
            return false;
        }
    }

    // Inheritance: AdminUser inherits from SystemUser
    public class AdminUser : SystemUser
    {
        public AdminUser(string username, string email, string password) 
            : base(username, email, password)
        {
        }

        public override string GetRoleDisplay()
        {
            return "Admin";
        }

        public override bool CanEditUsers()
        {
            return true;
        }
    }

    // Inheritance: GeneralUser inherits from SystemUser
    public class GeneralUser : SystemUser
    {
        public GeneralUser(string username, string email, string password) 
            : base(username, email, password)
        {
        }

        public override string GetRoleDisplay()
        {
            return "User";
        }
    }
}
