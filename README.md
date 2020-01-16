# Loading Related Data

See: [Loading Related Data](https://docs.microsoft.com/en-us/ef/core/querying/related-data)

Example using Include to load directly:
```cs
using (var context = new BloggingContext())
{
    var blogs = context.Blogs
        .Include(blog => blog.Posts)
        .Include(blog => blog.Owner)
        .ToList();
}
```

# Raw SQL

Use **.FromSql**, see: [Raw SQL Queries](https://docs.microsoft.com/en-us/ef/core/querying/raw-sql)

**ExecuteSqlCommand** is also available for submitting Insert, Update and Delete statements.


# Specific SQL Column data types

In your entity model you can use Data Annotations to specify an exact data type for a column, like:

```cs
[Column(TypeName = "varchar(200)")]
public string Url { get; set; }

[Column(TypeName = "date")]
public DateTime DateOfBirth { get; set; }

[Column(TypeName = "decimal(5, 2)")]
public decimal Rating { get; set; }
```
Fluent API:
```
modelBuilder.Entity<Blog>(eb =>
        {
            eb.Property(b => b.Url).HasColumnType("varchar(200)");
            eb.Property(b => b.Rating).HasColumnType("decimal(5, 2)");
        });
```
