using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Models
{
    public class Meeting
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public string Description { get; set; }

        // Navigation property for many-to-many relationship
        public ICollection<UserMeeting> UserMeetings { get; set; }
    }
    

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        // Foreign key to reference the owner User

        [Required]
        public int OwnerId { get; set; }
        public User Owner { get; set; }

        // Navigation property for many-to-many relationship
        public ICollection<UserMeeting> UserMeetings { get; set; }
    }

    // Join entity for many-to-many relationship
    public class UserMeeting
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}
