﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace api.Entities;

public partial class Assistant
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public string Company { get; set; }

    public bool PayCash { get; set; }

    public bool PayQr { get; set; }

    public bool PrintedLabel { get; set; }

    public int Calification { get; set; }

    public int PrintedSuccessful { get; set; }
}