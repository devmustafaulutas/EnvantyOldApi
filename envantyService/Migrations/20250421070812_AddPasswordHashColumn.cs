using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace envantyService.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YapılmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AksiyonYapanKişi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AksiyonAdı = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AksiyonAçıklaması = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TakipFormId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Activites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnketYonetimi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurveyMetaData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    formTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EtkinlikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnketYonetimi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AyınProjeKonus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Konu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AyınProjeKonus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AyınProjes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Konu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TakipNumarisi = table.Column<int>(type: "int", nullable: false),
                    Icerik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AyınProjes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BirthdayManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmanId = table.Column<int>(type: "int", nullable: false),
                    SirketlerId = table.Column<int>(type: "int", nullable: false),
                    ÜnvanlarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirthdayManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CelebrationManagements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SirketlerId = table.Column<int>(type: "int", nullable: false),
                    KutlamaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KutlamaSaati = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MustJoinEvent = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelebrationManagements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CelebrationParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserMail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CelebrationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelebrationParticipants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ceokösesis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShareDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ceokösesis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CIPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eposta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmanId = table.Column<int>(type: "int", nullable: true),
                    DepartmanCategoryId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    TakipNumarısı = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CIPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionAdministrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SirketlerId = table.Column<int>(type: "int", nullable: false),
                    YarışmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    YarışmaSaati = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MustJoinEvent = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionAdministrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserMail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    yarışmaYönetimiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionParticipants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyNews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShareDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyNews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departmans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departmans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorShift = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NurseShift = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Duyuruyönetimis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShareDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duyuruyönetimis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Etkinliks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SirketlerId = table.Column<int>(type: "int", nullable: false),
                    EtkinlikTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EtkinlikSaati = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MustJoinEvent = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etkinliks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserMail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    etkinlikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodMenuList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfMeal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Food1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Food2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Food3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Food4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Food5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Food6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Food7 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Food8 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodMenuList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlusturulmaZamanı = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlanAdı = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AksiyonYapanKisi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AksiyonYapanPozisyon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EskiDeger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YeniDeger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TakipFormId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Iyileştirmetipis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iyileştirmetipis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KampanyaYönetimis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndirimOrani = table.Column<int>(type: "int", nullable: false),
                    SirketlerId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KampanyaYönetimis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    TakipFormId = table.Column<int>(type: "int", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationManagement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Omu_Departman",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Omu_Departman", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OmuAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OmuId = table.Column<int>(type: "int", nullable: false),
                    Planla_Acıklaması = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uygula_Acıklaması = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kontrol_Et_Acıklaması = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Onem_Al_Acıklaması = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OmuAnswer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Omus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TakipNumarisi = table.Column<int>(type: "int", nullable: false),
                    omu_DepartmanId = table.Column<int>(type: "int", nullable: false),
                    IyileştirmetipiId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmanId = table.Column<int>(type: "int", nullable: true),
                    Iyilestirme_Degerlendirmesi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SorumluKisi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Omus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PasswordManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kod = table.Column<int>(type: "int", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordManagement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlusturulmaZamanı = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AksiyonAdı = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AksiyonYapanKisi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AksiyonYapanPozisyon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AksiyonYapanGrup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SistemAciklaması = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TakipFormId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sirketlers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SGKSicilNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TehlikeSınıfı = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sirketlers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SirkettenHaberlerYönetimis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShareDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SirkettenHaberlerYönetimis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyAnswerMetaData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    SurveyNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TakipFormPool",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TakipNumarısı = table.Column<int>(type: "int", nullable: false),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icerik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SorumluMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmanCategoryId = table.Column<int>(type: "int", nullable: false),
                    CevapIcerigi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CevapTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AyınProjeKonu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowMail = table.Column<bool>(type: "bit", nullable: false),
                    DepartmanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakipFormPool", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UlasimYönetimis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServisSoforu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guzergahi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plaka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    TelefonNumarisi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlasimYönetimis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmanCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmanCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmanCategories_Departmans_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "Departmans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EmploymentStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmploymentEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmanId = table.Column<int>(type: "int", nullable: false),
                    DepartmanCategoryId = table.Column<int>(type: "int", nullable: false),
                    ÜnvanlarId = table.Column<int>(type: "int", nullable: false),
                    SirketlerId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    FirstLogin = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logins_Sirketlers_SirketlerId",
                        column: x => x.SirketlerId,
                        principalTable: "Sirketlers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TakipForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TakipNumarısı = table.Column<int>(type: "int", nullable: true),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    İçerik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SorumluMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmanCategoryId = table.Column<int>(type: "int", nullable: true),
                    KayitTipi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Onem_Derecesi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AyınProjeKonu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmanId = table.Column<int>(type: "int", nullable: true),
                    ShowMail = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakipForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TakipForms_DepartmanCategories_DepartmanCategoryId",
                        column: x => x.DepartmanCategoryId,
                        principalTable: "DepartmanCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "adminCevaplamas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cevapİçeriği = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CevapTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TakipFormId = table.Column<int>(type: "int", nullable: false),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adminCevaplamas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_adminCevaplamas_TakipForms_TakipFormId",
                        column: x => x.TakipFormId,
                        principalTable: "TakipForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_adminCevaplamas_TakipFormId",
                table: "adminCevaplamas",
                column: "TakipFormId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmanCategories_DepartmanId",
                table: "DepartmanCategories",
                column: "DepartmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_SirketlerId",
                table: "Logins",
                column: "SirketlerId");

            migrationBuilder.CreateIndex(
                name: "IX_TakipForms_DepartmanCategoryId",
                table: "TakipForms",
                column: "DepartmanCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionHistory");

            migrationBuilder.DropTable(
                name: "Activites");

            migrationBuilder.DropTable(
                name: "adminCevaplamas");

            migrationBuilder.DropTable(
                name: "AnketYonetimi");

            migrationBuilder.DropTable(
                name: "AyınProjeKonus");

            migrationBuilder.DropTable(
                name: "AyınProjes");

            migrationBuilder.DropTable(
                name: "BirthdayManagers");

            migrationBuilder.DropTable(
                name: "CelebrationManagements");

            migrationBuilder.DropTable(
                name: "CelebrationParticipants");

            migrationBuilder.DropTable(
                name: "Ceokösesis");

            migrationBuilder.DropTable(
                name: "CIPs");

            migrationBuilder.DropTable(
                name: "CompetitionAdministrations");

            migrationBuilder.DropTable(
                name: "CompetitionParticipants");

            migrationBuilder.DropTable(
                name: "DailyNews");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Duyuruyönetimis");

            migrationBuilder.DropTable(
                name: "Etkinliks");

            migrationBuilder.DropTable(
                name: "EventParticipants");

            migrationBuilder.DropTable(
                name: "FoodMenuList");

            migrationBuilder.DropTable(
                name: "FormHistory");

            migrationBuilder.DropTable(
                name: "Iyileştirmetipis");

            migrationBuilder.DropTable(
                name: "KampanyaYönetimis");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "NotificationManagement");

            migrationBuilder.DropTable(
                name: "Omu_Departman");

            migrationBuilder.DropTable(
                name: "OmuAnswer");

            migrationBuilder.DropTable(
                name: "Omus");

            migrationBuilder.DropTable(
                name: "PasswordManagement");

            migrationBuilder.DropTable(
                name: "RegistrationHistory");

            migrationBuilder.DropTable(
                name: "SirkettenHaberlerYönetimis");

            migrationBuilder.DropTable(
                name: "SurveyAnswers");

            migrationBuilder.DropTable(
                name: "TakipFormPool");

            migrationBuilder.DropTable(
                name: "UlasimYönetimis");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "TakipForms");

            migrationBuilder.DropTable(
                name: "Sirketlers");

            migrationBuilder.DropTable(
                name: "DepartmanCategories");

            migrationBuilder.DropTable(
                name: "Departmans");
        }
    }
}
