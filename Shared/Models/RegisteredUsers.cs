using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.Models
{
    public class RegisteredUsers
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public DateTime DateRegistered { get; set; } = DateTime.Now;

    }
}
