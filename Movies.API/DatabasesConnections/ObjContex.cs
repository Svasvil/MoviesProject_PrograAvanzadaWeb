using Microsoft.EntityFrameworkCore;
using Movies.API.Models;

namespace Movies.API.DatabasesConnections
{
    public class ObjContex : DbContext
    {
        public ObjContex(DbContextOptions<ObjContex> opts) : base(opts) { }

        public DbSet<MovieModel> Movie { get; set; } = null!;
        public DbSet<UserModel> User { get; set; } = null!;
    }

    
    }
