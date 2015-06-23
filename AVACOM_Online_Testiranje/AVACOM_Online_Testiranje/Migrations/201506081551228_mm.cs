namespace AVACOM_Online_Testiranje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Korisnik",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Ime = c.String(),
                        Prezime = c.String(),
                        Email = c.String(),
                        Admin = c.Boolean(nullable: false),
                        KorisnickoIme = c.String(),
                        Lozinka = c.String(),
                        Aktivan = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.KorisnikOdgovor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OdgovorId = c.Int(nullable: false),
                        TestOdgovorId = c.Int(nullable: false),
                        TestOdgovor_TestId = c.Int(),
                        TestOdgovor_PitanjeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TestOdgovor", t => new { t.TestOdgovor_TestId, t.TestOdgovor_PitanjeId })
                .Index(t => new { t.TestOdgovor_TestId, t.TestOdgovor_PitanjeId });

            CreateTable(
                "dbo.TestOdgovor",
                c => new
                    {
                        TestId = c.Int(nullable: false),
                        PitanjeId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        OdgovorTacan = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.TestId, t.PitanjeId })
                .ForeignKey("dbo.Pitanje", t => t.PitanjeId)
                .ForeignKey("dbo.Test", t => t.TestId)
                .Index(t => t.TestId)
                .Index(t => t.PitanjeId);

            CreateTable(
                "dbo.Pitanje",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Tekst = c.String(),
                        Bod = c.Int(nullable: false),
                        VrstaPitanjaId = c.Int(nullable: false),
                        OblastId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Oblast", t => t.OblastId)
                .ForeignKey("dbo.VrstaPitanja", t => t.VrstaPitanjaId)
                .Index(t => t.VrstaPitanjaId)
                .Index(t => t.OblastId);

            CreateTable(
                "dbo.Oblast",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.VrstaPitanja",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Test",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        KorisnikId = c.Int(nullable: false),
                        VrijemePocetka = c.DateTime(nullable: false),
                        VrijemeZavrsetka = c.DateTime(nullable: false),
                        Rezultat = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Korisnik", t => t.KorisnikId)
                .Index(t => t.KorisnikId);

            CreateTable(
                "dbo.Odgovor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Tekst = c.String(),
                        Tacan = c.Boolean(nullable: false),
                        PitanjeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pitanje", t => t.PitanjeId)
                .Index(t => t.PitanjeId);

            CreateTable(
                "dbo.TestOblast",
                c => new
                    {
                        TestId = c.Int(nullable: false),
                        OblastId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TestId, t.OblastId })
                .ForeignKey("dbo.Oblast", t => t.OblastId)
                .ForeignKey("dbo.Test", t => t.TestId)
                .Index(t => t.TestId)
                .Index(t => t.OblastId);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestOblast", "TestId", "dbo.Test");
            DropForeignKey("dbo.TestOblast", "OblastId", "dbo.Oblast");
            DropForeignKey("dbo.Odgovor", "PitanjeId", "dbo.Pitanje");
            DropForeignKey("dbo.KorisnikOdgovor", new[] { "TestOdgovor_TestId", "TestOdgovor_PitanjeId" }, "dbo.TestOdgovor");
            DropForeignKey("dbo.TestOdgovor", "TestId", "dbo.Test");
            DropForeignKey("dbo.Test", "KorisnikId", "dbo.Korisnik");
            DropForeignKey("dbo.TestOdgovor", "PitanjeId", "dbo.Pitanje");
            DropForeignKey("dbo.Pitanje", "VrstaPitanjaId", "dbo.VrstaPitanja");
            DropForeignKey("dbo.Pitanje", "OblastId", "dbo.Oblast");
            DropIndex("dbo.TestOblast", new[] { "OblastId" });
            DropIndex("dbo.TestOblast", new[] { "TestId" });
            DropIndex("dbo.Odgovor", new[] { "PitanjeId" });
            DropIndex("dbo.Test", new[] { "KorisnikId" });
            DropIndex("dbo.Pitanje", new[] { "OblastId" });
            DropIndex("dbo.Pitanje", new[] { "VrstaPitanjaId" });
            DropIndex("dbo.TestOdgovor", new[] { "PitanjeId" });
            DropIndex("dbo.TestOdgovor", new[] { "TestId" });
            DropIndex("dbo.KorisnikOdgovor", new[] { "TestOdgovor_TestId", "TestOdgovor_PitanjeId" });
            DropTable("dbo.TestOblast");
            DropTable("dbo.Odgovor");
            DropTable("dbo.Test");
            DropTable("dbo.VrstaPitanja");
            DropTable("dbo.Oblast");
            DropTable("dbo.Pitanje");
            DropTable("dbo.TestOdgovor");
            DropTable("dbo.KorisnikOdgovor");
            DropTable("dbo.Korisnik");
        }
    }
}
