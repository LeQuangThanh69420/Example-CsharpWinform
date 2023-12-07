# csharp-winform-example
### mssql table
```
CREATE TABLE [dbo].[SV] (
    [MSSV]     NVARCHAR (50) NOT NULL,
    [TenSV]    NVARCHAR (50) NULL,
    [GioiTinh] BIT           NULL,
    CONSTRAINT [PK_SV] PRIMARY KEY CLUSTERED ([MSSV] ASC)
);
CREATE TABLE [dbo].[User] (
    [Username] NVARCHAR (50) NOT NULL,
    [Password] NVARCHAR (50) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Username] ASC)
);
```
