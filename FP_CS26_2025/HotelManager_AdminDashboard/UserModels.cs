using System;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    // Abstraction: Abstract base class
    // Abstraction: Abstract base class
    public abstract class SystemUser
    {
        // Encapsulation: Protected setters, public getters
        public Guid Id { get; protected set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string EmployeeId { get; set; } // Sequential ID e.g. "001"
        public string Email { get; set; }
        public string Password { get; set; } // In a real app, this should be hashed
        public DateTime Birthday { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime DateAdded { get; set; }

        protected SystemUser(string firstName, string middleName, string lastName, string username, string email, string password, DateTime birthday, string employeeId, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Username = username;
            EmployeeId = employeeId;
            Email = email;
            Password = password;
            Birthday = birthday;
            IsActive = true;
            LastActive = DateTime.MinValue; // Default
            DateAdded = DateTime.Now;
            LastUpdated = DateTime.Now;
        }

        // Polymorphism: Abstract method implementation required
        public abstract string GetRoleDisplay();

        // Polymorphism: Virtual method can be overridden
        public virtual bool CanEditUsers()
        {
            return false;
        }
    }

    // Inheritance: ManagerUser inherits from SystemUser
    public class ManagerUser : SystemUser
    {
        public ManagerUser(string firstName, string middleName, string lastName, string username, string email, string password, DateTime birthday, string employeeId, Guid? id = null)
            : base(firstName, middleName, lastName, username, email, password, birthday, employeeId, id)
        {
        }

        public override string GetRoleDisplay()
        {
            return "Manager";
        }

        public override bool CanEditUsers()
        {
            return true;
        }
    }

    // Inheritance: FrontDeskUser inherits from SystemUser
    public class FrontDeskUser : SystemUser
    {
        public FrontDeskUser(string firstName, string middleName, string lastName, string username, string email, string password, DateTime birthday, string employeeId, Guid? id = null)
            : base(firstName, middleName, lastName, username, email, password, birthday, employeeId, id)
        {
        }

        public override string GetRoleDisplay()
        {
            return "FrontDesk";
        }
    }
}
