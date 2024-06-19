using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Repositories
{
    public class SnackRepository : ISnackRepository
    {
        private readonly AppDbContext _context;

        public SnackRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Snack> Snacks => _context.Snacks.Include(c => c.Category);

        public IEnumerable<Snack> FavoriteSnacks => _context.Snacks
                                                    .Where(x => x.IsSnackFavorite)
                                                    .Include(c => c.Category);

        public Snack GetSnackById(int id)
        {
            return _context.Snacks.FirstOrDefault(x => x.Id == id) 
                ?? throw new InvalidOperationException("Snack not found");
        }
    }
}
