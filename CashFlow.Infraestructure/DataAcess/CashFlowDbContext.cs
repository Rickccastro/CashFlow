using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infraestructure.DataAcess;

internal class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public CashFlowDbContext(DbContextOptions options) : base(options) {}
    public DbSet<User> Users { get; set; }

}
