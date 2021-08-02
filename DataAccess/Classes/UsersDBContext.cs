using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Classes
{
    public class UsersDBContext : DbContext
    {
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<RoleDTO> Roles { get; set; }
        public DbSet<UserRolesDTO> UsersRoles { get; set; }
        public DbSet<PostDTO> Posts { get; set; }
        public UsersDBContext(DbContextOptions<UsersDBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
