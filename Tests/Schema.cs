using NPoco;
using NUnit.Framework;

namespace Tests
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    public class Schema
    {
        #region ShcemaScript
        private string sql = @"IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_BlogPost_PostDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BlogPost] DROP CONSTRAINT [DF_BlogPost_PostDate]
END

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlogPost]') AND type in (N'U'))
DROP TABLE [dbo].[BlogPost]

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlogPost]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BlogPost](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Body] [ntext] NOT NULL,
	[PostDate] [datetime] NOT NULL,
	[Category] [int] NOT NULL,
 CONSTRAINT [PK_BlogPost] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_BlogPost_PostDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BlogPost] ADD  CONSTRAINT [DF_BlogPost_PostDate]  DEFAULT (getdate()) FOR [PostDate]
END
";
        #endregion

        [Test, Explicit]
        public void SetupSchema()
        {
            using (var db = new Database("database"))
            {
                db.Execute(sql);
            }
        }
    }

    // ReSharper restore InconsistentNaming 
}