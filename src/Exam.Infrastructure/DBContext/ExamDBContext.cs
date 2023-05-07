using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exam.Infrastructure.DBContext
{
    public class ExamDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ExamDBContext(DbContextOptions<ExamDBContext> options) : base(options)
        {
        }

    }


}
