//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AbBus.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class booking
    {
        public int ID { get; set; }
        public int TripID { get; set; }
        public int SeatID { get; set; }
        public int UserID { get; set; }
        public int BoardingPoint { get; set; }
        public int ArrivalPoint { get; set; }
        public int ChargeApplied { get; set; }
    
        public virtual seat seat { get; set; }
        public virtual master_station master_station { get; set; }
        public virtual seat seat1 { get; set; }
        public virtual trip trip { get; set; }
        public virtual user user { get; set; }
    }
}
