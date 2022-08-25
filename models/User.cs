using System;
using System.Collections.Generic;

namespace TodoApi.Models
{
  public partial class User
  {
    public long Id { get; set; }
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
  }
}