using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibrarySystem_Labajo.Models;

namespace LibrarySystem_Labajo.Data
{
    public class LibrarySystem_LabajoContext : DbContext
    {
        public LibrarySystem_LabajoContext (DbContextOptions<LibrarySystem_LabajoContext> options)
            : base(options)
        {
        }

        public DbSet<LibrarySystem_Labajo.Models.User> User { get; set; } = default!;

        public DbSet<LibrarySystem_Labajo.Models.Books>? Books { get; set; }

        public DbSet<LibrarySystem_Labajo.Models.BookCategory>? BookCategory { get; set; }
    }
}
