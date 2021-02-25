using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Library.Domain.Interfaces.IData;

namespace Library.Domain.Core
{
    //public class Book:IBook
    //{
    //    public int Id { get; set; }

    //    public string Title { get; set; }

    //    [MaybeNull]
    //    public string Description { get; set; }

    //    [NotMapped]
    //    public string ShortDescription 
    //    { 
    //        get 
    //        {
    //            if (Description != null & Description.Length > 100)
    //            {
    //                return Description.Substring(0, 100)+"...";
    //            }
    //            return "Краткое описание";
    //        } 
    //    }

    //    public string ImagePath { get; set; }

    //    public bool IsBusy { get; set; }

    //    public int IdAccount { get; set; }

    //    public bool IsBorrow { get; set; }

    //    public int BookingTime { get; set; }

    //    public int BookRequests { get; set; }
    
    //    public string State { get; set; }

    //    public string Author { get; set; }

    //    public string Genre { get; set; }

    //    public string Publisher { get; set; }

    //    public ushort PublisherYear { get; set; }

    //    public override string ToString()
    //    {
    //        return Title+" "+ShortDescription;
    //    }
    //}
}
