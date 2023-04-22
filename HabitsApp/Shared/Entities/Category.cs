using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitsApp.Shared.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required, NotNull]
        public string? Name { get; set; }
    }
}
