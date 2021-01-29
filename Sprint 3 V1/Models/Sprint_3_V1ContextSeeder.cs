using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.Models
{
    public class Sprint_3_V1ContextSeeder: CreateDatabaseIfNotExists<Sprint_3_V1Context>
    {
            protected override void Seed(Sprint_3_V1Context context)
            {
                /// account admin data
                Account admin = new Account
                {
                    UserName = "Farmerbrown1",
                    Password = "Farmerbrown1",
                    ConfirmPassword = "Farmerbrown1",
                    Role = "Admin",
                    Disabled = false,
                    Type = "Admin",
                    AccountID = 1,
                    Customer = null,
                    Employee = null
                };
                context.Accounts.Add(admin);
                context.SaveChanges();

                /// position entity data
                IList<Position> positions = new List<Position>();

                positions.Add(new Position()
                {
                    PositionID = 1,
                    Name = "Manager",
                    Description = "Manage Farm",
                    BaseSalary = 1234.0,
                    Disabled = false,
                    EmpPos = null
                });
                positions.Add(new Position()
                {
                    PositionID = 2,
                    Name = "Human resources",
                    Description = "Manage Farmers",
                    BaseSalary = 1234.0,
                    Disabled = false,
                    EmpPos = null
                });
                positions.Add(new Position()
                {
                    PositionID = 3,
                    Name = "Worker",
                    Description = "Field work",
                    BaseSalary = 1230.0,
                    Disabled = false,
                    EmpPos = null
                });
                positions.Add(new Position()
                {
                    PositionID = 4,
                    Name = "Foreman",
                    Description = "Manage Farmers",
                    BaseSalary = 123.0,
                    Disabled = false,
                    EmpPos = null
                });
                positions.Add(new Position()
                {
                    PositionID = 5,
                    Name = "Clerk",
                    Description = "Manage Admin",
                    BaseSalary = 120.0,
                    Disabled = false,
                    EmpPos = null
                });
                positions.Add(new Position()
                {
                    PositionID = 6,
                    Name = "Delivery",
                    Description = "Deliver",
                    BaseSalary = 120.0,
                    Disabled = false,
                    EmpPos = null
                });

                context.Positions.AddRange(positions);
                context.SaveChanges();

                base.Seed(context);
            }
        }
    }