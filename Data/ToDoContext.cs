﻿using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Data;
public class ToDoContext : DbContext
{
    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
    {

    }
    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
}
