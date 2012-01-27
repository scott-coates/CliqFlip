using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
    [Migration(634632043311428388)]
    public class __634632043311428388_AddInterests : ConditionalMigration
    {
        protected override void ConditionalUp()
        {
            #region InsertInterests

            const string createInterests = @"INSERT INTO [Interests] ([Name],[Description],[Slug],[ParentInterestId])VALUES('Communications','Multum venit. quad nomen estis nomen et et Sed Id estum. quo Id Quad et regit, quantare quad quartu Et','Communications',NULL)
INSERT INTO [Interests] ([Name],[Description],[Slug],[ParentInterestId])VALUES('Automotive','Longam, eggredior. quartu volcans plurissimum in Pro quad travissimantor Pro nomen quad linguens Longam, travissimantor cognitio,','Automotive',NULL)
INSERT INTO [Interests] ([Name],[Description],[Slug],[ParentInterestId])VALUES('Fire, Police & Security','linguens vobis novum funem. trepicandor plorum eudis e Longam, e esset delerium. fecit. volcans quis','Fire-Police-Security',NULL)
INSERT INTO [Interests] ([Name],[Description],[Slug],[ParentInterestId])VALUES('Oil & Gas','fecit. et Et et in plorum Longam, habitatio Et gravum Pro si fecit, habitatio quis trepicandor quoque imaginator fecit. estum.','Oil-Gas',NULL)
INSERT INTO [Interests] ([Name],[Description],[Slug],[ParentInterestId])VALUES('Mechanical Electrical & Process Engineering','gravum quad et gravis imaginator nomen essit. vantis. et essit. Multum ut in pladior et quad Pro quartu','Mechanical-Electrical-Process-Engineering',NULL)
INSERT INTO [Interests] ([Name],[Description],[Slug],[ParentInterestId])VALUES('Financial Services','fecit, Versus imaginator novum plurissimum volcans cognitio, pars et non gravum e ut dolorum egreddior e','Financial-Services',NULL)
INSERT INTO [Interests] ([Name],[Description],[Slug],[ParentInterestId])VALUES('Electronics & IT Hardware','quartu e Multum plorum sed rarendum novum egreddior Longam, nomen quad apparens in in Longam, Tam quis manifestum Multum gravis','Electronics-IT-Hardware',NULL)
INSERT INTO [Interests] ([Name],[Description],[Slug],[ParentInterestId])VALUES('Textiles, Interior Textiles & Carpets','glavans e in quoque habitatio et essit. in non plorum non ut Versus et essit. rarendum imaginator imaginator et quad volcans','Textiles-Interior-Textiles-Carpets',NULL)
INSERT INTO [Interests] ([Name],[Description],[Slug],[ParentInterestId])VALUES('Marine','vobis ut Id travissimantor quad Multum et et Sed gravum sed quad nomen novum quad pladior estum. habitatio Quad homo,','Marine',NULL)
INSERT INTO [Interests] ([Name],[Description],[Slug],[ParentInterestId])VALUES('Aerospace (Civil)','e fecit, vobis Et in quoque in e parte et nomen Versus transit. trepicandor vantis. essit. linguens Multum','Aerospace-Civil',NULL)
INSERT INTO [Interests] ([Name],[Description],[Slug],[ParentInterestId])VALUES('Creative & Media','novum fecundio, et volcans gravis Id travissimantor ut et plorum Versus in manifestum Versus quis bono eggredior. plorum fecundio, non','creative-media',NULL)
INSERT INTO [Interests] ([Name],[Description],[Slug],[ParentInterestId])VALUES('Water','plorum bono funem. imaginator nomen travissimantor non e gravis si si Tam volcans glavans vobis habitatio rarendum habitatio funem. habitatio','water',NULL)
INSERT INTO [Interests] ([Name],[Description],[Slug],[ParentInterestId])VALUES('Metallurgical Process Plant','Et fecit, si parte cognitio, cognitio, eudis ut quis et plorum non non homo, quo cognitio, nomen gravis Versus','metallurgical-process-plant',NULL)
";
            #endregion

            Database.ExecuteNonQuery(createInterests);
        }

        protected override void ConditionalDown()
        {
            Database.ExecuteNonQuery("DELETE FROM [Interest]");
        }
    }
}
