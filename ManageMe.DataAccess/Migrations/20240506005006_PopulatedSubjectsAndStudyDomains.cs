using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class PopulatedSubjectsAndStudyDomains : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                --proceduri stocate pentru popularea tabelelor de materii si domenii de studii
                GO
                CREATE OR ALTER PROCEDURE [PopulateSubject]
                AS
	                INSERT INTO [dbo].[Subject] VALUES ('Calculatoare numerice', 'CN', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Pregatirea proiectului de diploma', 'PPD', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Metode de dezvoltare software', 'MDS', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Informatica pentru aducere la nivel', 'IAN', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Educatie fizica si metodica III si IV', 'EFM', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Educatie fizica', 'EF', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Sisteme distribuite', 'SD', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Teoria probabilitatilor si statistica matematica', 'TPSM', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Programare orientata pe obiecte', 'POO', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Printare si modelare 3D', 'PM3D', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Programare procedurala', 'PP', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Limbaje formale si automate', 'LFA', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Elemente de calcul stiintific', 'ECS', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Realizarea lucrarilor de licenta', 'RLL', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Comert electronic', 'CE', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Pregatire pentru concursuri de matematica', 'PCM', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Inginerie software', 'IS', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Geometrie si algebra liniara', 'GAL', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Teoria sistemelor', 'TS', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Analiza matematica I', 'AMI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Matematici speciale', 'MS', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Competente specifice intr-o limba straina', 'CSILS', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Calculabilitate si complexitate', 'CC', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Teoria numerelor', 'TN', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Analiza complexa', 'AC', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Logica matematica si computatioanala', 'LMC', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Geometrie diferentiala I', 'GDI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Programare orientata pe obiecte I', 'POOI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Practica', 'P', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Bazele electrotehnicii', 'BE', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Gandire critica si etica academica', 'GCEA', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Structuri de date si algoritmi', 'SDA', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Algoritmi fundamentali', 'AF', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Compilatoare si translatoare', 'CST', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Logica matematica', 'LM', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Dezvoltarea aplicatiilor web', 'DAW', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Analiza numerica si metode numerice II', 'ANMII', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Inteligenta artificiala', 'IA', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Practica industriala', 'PI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Geometrie II', 'GII', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Sisteme de gestiune a bazelor de date', 'SGBD', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Analiza numerica si metode numerice I', 'ANMNI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Elemente de electronica analogica', 'EEA', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Analiza functionala', 'AF', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Educatie fizica si metoda', 'EFM', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Sisteme cu microprocesoare', 'SCM', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Structuri de date', 'SD', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Algebra I', 'AI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Geometrie I', 'GI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Grafica pe calculator', 'GPC', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Fizica', 'F', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Protocoale de comunicatie', 'PC', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Fundamente ale retelelor de calculatoare', 'FRC', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Programarea calculatoarelor I', 'PCI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Modelarea matematica a sistemelor materiale I', 'MMSMI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Programare competitiva', 'PC', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Competente avansate intr-o limba straina I', 'CAILS', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Electronica digitala', 'ED', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Elemente de teoria numerelor', 'ETN', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Calcul numeric', 'CN', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Geometrie diferentiala II', 'GDII', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Structuri algebrice in informatica', 'SAI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Algebra III', 'AIII', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Cercetari operationale', 'CO', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Analiza numerica si metode numerice', 'ANM', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Teoria masurii si a integrarii', 'TMI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Testarea sistemelor software', 'TSS', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Competente de baza intr-o limba straina', 'CBILS', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Grafica asistata de calculator', 'GAC', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Metode dezvoltare software', 'MDS', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Didactica predarii informaticii', 'DPI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Programare avansata pe obiecte', 'PAPO', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Tehnici Web', 'TW', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Algebra liniara', 'AL', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Analiza matematica', 'AM', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Algebra II', 'AII', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Istoria matematicii', 'IM', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Securitatea sistemelor informatice', 'SSI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Calcul diferential si integral', 'CDI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Utilizarea sistemelor de operare', 'USO', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Gandire critica si etica academica in informatica', 'GCEAI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Retele de calculatoare', 'RC', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Algebra IV', 'AIV', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Baze de date', 'BD', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Arhitectura sistemelor de', 'AS', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Fundamentele limbajelor de programare', 'FLP', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Analiza matematica II', 'AMI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Educatie fizica si metoda I si II*', 'EFM', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Statistica', 'S', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Probabilitati si statistica', 'PS', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Ingineria programarii', 'IP', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Sisteme de operare', 'SO', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Algoritmi avansati', 'AA', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Competente avansate intr-o limba straina II', 'CAILS', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Educatie fizica*', 'EF', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Pregatire pentru concursuri de informatica', 'PCI', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Modelarea matematica a sistemelor materiale II', 'MMSMII', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Programarea algoritmilor', 'PA', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Proiectarea bazelor de date', 'PBD', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Programarea calculatoarelor II', 'PCII', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Matematica pentru aducere la nivel', 'MAN', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Pedagogie II', 'P', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Arhitectura sistemelor paralele', 'ASP', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Probabilitati', 'P', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Programare functionala', 'PF', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Arhitectura sistemelor de calcul', 'ASC', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Proiectare logica', 'PL', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Algebra si geometrie', 'AG', 'insert description');
                    INSERT INTO [dbo].[Subject] VALUES ('Geometrie diferentiala III', 'GDIII', 'insert description');
                GO

                EXECUTE [PopulateSubject]

                GO
                CREATE OR ALTER PROCEDURE [PopulateStudyDomain]
                AS
	                INSERT INTO [StudyDomain] VALUES ('Informatica', 'insert description', 3);
	                INSERT INTO [StudyDomain] VALUES ('Matematica', 'insert description', 3);
	                INSERT INTO [StudyDomain] VALUES ('Calculatoare si Tehnologia Informatiei', 'insert description', 4);
	                INSERT INTO [StudyDomain] VALUES ('Matematica Pura', 'insert description', 3);
	                INSERT INTO [StudyDomain] VALUES ('Matematica Aplicata', 'insert description', 3);
	                INSERT INTO [StudyDomain] VALUES ('Matematica Informatica', 'insert description', 3);
                GO

                EXECUTE [PopulateStudyDomain]");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE [PopulateSubject]
                DROP PROCEDURE [PopulateStudyDomain]
                DELETE FROM [dbo].[Subject]
                DELETE FROM [dbo].[StudyDomain]");
        }
    }
}
