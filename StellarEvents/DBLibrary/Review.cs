//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string Comments { get; set; }
        public System.DateTime Date { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}
