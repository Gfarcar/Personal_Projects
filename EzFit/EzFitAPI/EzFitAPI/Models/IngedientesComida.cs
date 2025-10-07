using System;
using System.Collections.Generic;

namespace EzFitAPI.Models;

public partial class IngedientesComida
{
    public short Id { get; set; }

    public string Food { get; set; } = null!;

    public short CaloricValue { get; set; }

    public double Fat { get; set; }

    public double Carbohydrates { get; set; }

    public double Sugars { get; set; }

    public double Protein { get; set; }

   
}
