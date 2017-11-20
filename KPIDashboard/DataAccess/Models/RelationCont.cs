namespace DataAccess.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class RelationCont : DbContext
    {
        // Your context has been configured to use a 'RelationCont' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataAccess.Models.RelationCont' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'RelationCont' 
        // connection string in the application configuration file.
        public RelationCont()
            : base("name=RelationCont")
        {
        }


        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Release> Release { get; set; }
        public virtual DbSet<KPI> kpi { get; set; }
        public virtual DbSet<JoinReleaseKpi> Releasekpi { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}