﻿#region

using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634739801458549631)]
    public class __634739801458549631_TrimInterestNames : Migration
    {
        public override void Up()
        {
            Database.ExecuteNonQuery("UPDATE Interests SET Name = LTRIM(RTRIM(Name))");
        }

        public override void Down()
        {
        }
    }
}