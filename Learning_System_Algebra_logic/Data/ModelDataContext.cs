using System.Data.Entity;
using System.Linq;
using SQLite.CodeFirst;

namespace Learning_System_Algebra_logic.Data
{
	internal class ModelDataContext : DbContext
	{
		public ModelDataContext() : base("LMSdb")
		{
			Configuration.ProxyCreationEnabled = true;
			Configuration.LazyLoadingEnabled = true;
		}
		public DbSet<User> Users { get; set; }
		public DbSet<CodeWork> CodeWorks { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<VariantWork> VariantWorks { get; set; }
		public DbSet<Work> Works { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<ModelDataContext>(modelBuilder);
			Database.SetInitializer(sqliteConnectionInitializer);
			modelBuilder.Entity<Group>().HasMany(st => st.Students).WithOptional(st => st.Group)
				.WillCascadeOnDelete(true);
			modelBuilder.Entity<Work>().HasMany(w => w.VariantWorks).WithOptional(v => v.Work)
				.WillCascadeOnDelete(true);
			modelBuilder.Entity<VariantWork>().HasMany(v => v.CodeWorks).WithOptional(c => c.VariantWork)
				.WillCascadeOnDelete(true);
			modelBuilder.Entity<Student>().HasMany(s => s.CodeWorks).WithOptional(c => c.Student)
				.WillCascadeOnDelete(true);
		}
		public User Authorization(string login, string password)
		{
			using (var db = new ModelDataContext())
			{
				if (!db.Users.Any())
				{
					db.Users.Add(new User {EMail = "email@mail.ru", Login = "admin", Password = "qwerty"});
					db.SaveChanges();
				}
			}
			var user = from us in Users
				where us.Login == login && us.Password == password
				select us;
			if (user.Any()) return user.ToArray()[0];
			return null;
		}
	}
}